using WasmAI.ConditionChecker.Base;

namespace WasmAI.ConditionChecker.Checker
{
    /// <summary>
    /// Interface for managing conditions of a specified enum type.
    /// </summary>
    public interface IConditionProvider<TEnum> where TEnum : Enum
    {
        /// <summary>
        /// Registers a condition for a specific type.
        /// </summary>
        void Register(TEnum type, ICondition condition);

        /// <summary>
        /// Retrieves the first condition for a specific type.
        /// </summary>
        ICondition? Get(TEnum type);

        /// <summary>
        /// Retrieves all conditions regardless of type, filtering by the provided context.
        /// </summary>
        IEnumerable<ICondition> GetConditions(object context);

        /// <summary>
        /// Retrieves all condition types.
        /// </summary>
        IEnumerable<TEnum> GetConditionTypes();

        /// <summary>
        /// Retrieves all conditions, regardless of type.
        /// </summary>
        IEnumerable<ICondition> GetAllConditions();

        /// <summary>
        /// Retrieves the types of conditions that match a specific condition.
        /// </summary>
        IEnumerable<TEnum> GetConditionTypes(ICondition condition);

        /// <summary>
        /// Retrieves conditions for a specific type.
        /// </summary>
        IEnumerable<ICondition> GetConditions(TEnum type);

        /// <summary>
        /// Retrieves conditions for a specific type and context.
        /// </summary>
        IEnumerable<ICondition> GetConditions(TEnum type, object context);

        /// <summary>
        /// Retrieves conditions for a specific type, context, and optional filtering predicate.
        /// </summary>
        IEnumerable<ICondition> GetConditions(TEnum type, object context, Func<ICondition, bool> predicate);

        /// <summary>
        /// Retrieves conditions for a specific type, context, predicate, and an option to include inactive conditions.
        /// </summary>
        IEnumerable<ICondition> GetConditions(TEnum type, object context, Func<ICondition, bool> predicate, bool includeInactive);

        /// <summary>
        /// Retrieves conditions that match the provided predicate.
        /// </summary>
        IEnumerable<ICondition> Where(Func<ICondition, bool> predicate);

        /// <summary>
        /// Checks if any conditions pass for the provided context.
        /// </summary>
        IEnumerable<ConditionResult> AnyPass(object context);

        /// <summary>
        /// Checks if any conditions pass for an array of contexts.
        /// </summary>
        IEnumerable<ConditionResult> AnyPass(object[] contexts);

        /// <summary>
        /// Checks a specific condition type against a context asynchronously and returns the result.
        /// </summary>
        Task<ConditionResult> Check(TEnum type, object context);
    }

    /// <summary>
    /// Concrete implementation of IConditionProvider that manages conditions by enum types.
    /// </summary>
    internal class ConditionProvider<TEnum> : IConditionProvider<TEnum> where TEnum : Enum
    {
        // Dictionary to store conditions by type
        private readonly Dictionary<TEnum, List<ICondition>> _conditions = new();

        /// <summary>
        /// Registers a condition for a specific type.
        /// </summary>
        public void Register(TEnum type, ICondition condition)
        {
            if (!_conditions.ContainsKey(type))
                _conditions[type] = new List<ICondition>();

            _conditions[type].Add(condition);
        }

        /// <summary>
        /// Checks a specific condition type against the provided context asynchronously.
        /// </summary>
        public async Task<ConditionResult> Check(TEnum type, object context)
        {
            // Look for conditions for the specified type
            if (_conditions.TryGetValue(type, out var list) && list.Count > 0)
            {
                foreach (var condition in list)
                {
                    var result = await condition.Evaluate(context);
                    if (result.Success == true)
                    {
                        return result; // Return the first successful condition result
                    }
                }
            }
            return new ConditionResult(false, null, $"No {type} conditions passed");
        }

        /// <summary>
        /// Checks all conditions against the provided context, returns only the ones that pass.
        /// </summary>
        public IEnumerable<ConditionResult> AnyPass(object context)
        {
            foreach (var kvp in _conditions)
            {
                foreach (var condition in kvp.Value)
                {
                    var result = condition.Evaluate(context).Result;
                    if (result.Success == true)
                    {
                        yield return result;
                    }
                }
            }
        }

        /// <summary>
        /// Checks all conditions against an array of contexts, returns only those that pass.
        /// </summary>
        public IEnumerable<ConditionResult> AnyPass(object[] contexts)
        {
            foreach (var context in contexts)
            {
                foreach (var kvp in _conditions)
                {
                    foreach (var condition in kvp.Value)
                    {
                        var result = condition.Evaluate(context).Result;
                        if (result.Success == true)
                        {
                            yield return result;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves the first condition for a specific type.
        /// </summary>
        public ICondition? Get(TEnum type)
        {
            if (_conditions.TryGetValue(type, out var list) && list.Count > 0)
                return list[0];

            return null;
        }

        /// <summary>
        /// Retrieves all conditions, regardless of type, filtering by the provided context.
        /// </summary>
        public IEnumerable<ICondition> GetConditions(object context)
        {
            foreach (var kvp in _conditions)
            {
                foreach (var condition in kvp.Value)
                {
                    yield return condition;
                }
            }
        }

        /// <summary>
        /// Filters conditions based on a provided predicate.
        /// </summary>
        public IEnumerable<ICondition> Where(Func<ICondition, bool> predicate)
        {
            return _conditions.Values.SelectMany(x => x).Where(predicate);
        }

        /// <summary>
        /// Retrieves all condition types.
        /// </summary>
        public IEnumerable<TEnum> GetConditionTypes()
        {
            return _conditions.Keys;
        }

        /// <summary>
        /// Retrieves all conditions regardless of type.
        /// </summary>
        public IEnumerable<ICondition> GetAllConditions()
        {
            return _conditions.Values.SelectMany(x => x);
        }

        /// <summary>
        /// Retrieves the types of conditions that match a specific condition.
        /// </summary>
        public IEnumerable<TEnum> GetConditionTypes(ICondition condition)
        {
            return _conditions
                .Where(kvp => kvp.Value.Contains(condition))
                .Select(kvp => kvp.Key);
        }

        /// <summary>
        /// Retrieves conditions for a specific type.
        /// </summary>
        public IEnumerable<ICondition> GetConditions(TEnum type)
        {
            return _conditions.TryGetValue(type, out var list)
                ? list
                : Enumerable.Empty<ICondition>();
        }

        /// <summary>
        /// Retrieves conditions for a specific type and context.
        /// </summary>
        public IEnumerable<ICondition> GetConditions(TEnum type, object context)
        {
            // Can be modified later to filter by context if needed
            return GetConditions(type);
        }

        /// <summary>
        /// Retrieves conditions for a specific type, context, and predicate.
        /// </summary>
        public IEnumerable<ICondition> GetConditions(TEnum type, object context, Func<ICondition, bool> predicate)
        {
            return GetConditions(type, context).Where(predicate);
        }

        /// <summary>
        /// Retrieves conditions for a specific type, context, predicate, and optional filter to include inactive conditions.
        /// </summary>
        public IEnumerable<ICondition> GetConditions(TEnum type, object context, Func<ICondition, bool> predicate, bool includeInactive)
        {
            var filtered = GetConditions(type, context).Where(predicate);

            if (!includeInactive)
            {
                // Temporary filtering for inactive conditions (could use IsActive flag if available)
                filtered = filtered.Where(c => true); // Placeholder for future filtering logic
            }

            return filtered;
        }
    }

    /// <summary>
    /// 


  
}
