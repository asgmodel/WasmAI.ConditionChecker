using System.Reflection;
using WasmAI.ConditionChecker.Base;
using WasmAI.ConditionChecker.Checker;

namespace WasmAI.ConditionChecker.Validators
{
    /// <summary>
    /// Interface for validating the context object of type TContext.
    /// </summary>
    public interface IValidator<TContext>
    {
        // Define methods for validation
    }

    /// <summary>
    /// Base abstract class that provides common functionality for validating conditions in a given context (TContext).
    /// </summary>
    public abstract class TBaseValidatorContext<TContext, EValidator> : BaseValidator<EValidator>, IValidator<TContext>, ITValidator
        where TContext : class
        where EValidator : Enum
    {
        /// <summary>
        /// Constructor for initializing the base validator context with a given condition checker.
        /// </summary>
        protected TBaseValidatorContext(IBaseConditionChecker checker) : base(checker)
        {
        }

        /// <summary>
        /// Abstract method to get a model of type TContext by its ID.
        /// </summary>
        protected abstract Task<TContext?> GetModel(string? id);

        /// <summary>
        /// Maps a given data filter to a context model if the 'Id' is provided and 'Share' is null.
        /// </summary>
        protected virtual async Task<DataFilter<TValue, TContext>> MapTo<TValue>(DataFilter<TValue, TContext> filter)
        {
            if (filter.Id != null && filter.Share == null)
                filter.Share = await GetModel(filter.Id);

            return filter;
        }

        /// <summary>
        /// Initializes the validator context by registering the conditions using reflection.
        /// </summary>
        protected override void Initializer()
        {
            ReflectionRegisterConditionValidator();
            base.Initializer();
        }

        /// <summary>
        /// Registers a condition validator method with the appropriate attributes.
        /// </summary>
        private void RegisterConditionValidator(MethodInfo method, RegisterConditionValidatorAttribute attr)
        {
            var valueType = method.GetParameters().FirstOrDefault()?.ParameterType.GenericTypeArguments.FirstOrDefault();
            if (valueType == null)
                return;

            var state = attr.State;
            var message = attr.ErrorMessage;
            var value = attr.Value;
            var isCachable = attr.IsCachability;

            var genericRegisterMethod = typeof(TBaseValidatorContext<,>)
                .MakeGenericType(typeof(TContext), typeof(EValidator))
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .FirstOrDefault(m => m.Name == "RegisterCondition" && m.IsGenericMethodDefinition && m.GetParameters().Length > 3);

            if (genericRegisterMethod == null)
                return;

            var finalMethod = genericRegisterMethod.MakeGenericMethod(valueType);

            var delegateType = typeof(Func<,>).MakeGenericType(
                typeof(DataFilter<,>).MakeGenericType(valueType, typeof(TContext)),
                typeof(Task<ConditionResult>)
            );

            try
            {
                var funcDelegate = Delegate.CreateDelegate(delegateType, this, method);
                finalMethod.Invoke(this, new object[] { (EValidator)state, funcDelegate, message, value, isCachable });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error registering condition: {method.Name} must be Share Type TContext", ex);
            }
        }

        /// <summary>
        /// Uses reflection to register condition validators for methods that have the RegisterConditionValidator attribute.
        /// </summary>
        private void ReflectionRegisterConditionValidator()
        {
            var methods = GetType()
                         .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly)
                         .Where(m => m.GetCustomAttributes<RegisterConditionValidatorAttribute>().Any());

            foreach (var method in methods)
            {
                var attrs = method.GetCustomAttributes<RegisterConditionValidatorAttribute>().ToArray();

                if (attrs.Length == 1)
                {
                    RegisterConditionValidator(method, attrs[0]);
                }
                else
                {
                    foreach (var attr in attrs)
                    {
                        RegisterConditionValidator(method, attr);
                    }
                }
            }
        }

        /// <summary>
        /// Registers a condition with the specified state, check function, and error message.
        /// </summary>
        protected virtual void RegisterCondition<TValue>(
            EValidator state,
            Func<DataFilter<TValue, TContext>, Task<ConditionResult>> checkFunc,
            string errorMessage
        )
        {
            RegisterCondition(state, checkFunc, errorMessage, null);
        }

        /// <summary>
        /// Registers a condition with the specified state, check function, error message, value, and cacheability.
        /// </summary>
        protected virtual void RegisterCondition<TValue>(
            EValidator state,
            Func<DataFilter<TValue, TContext>, Task<ConditionResult>> checkFunc,
            string errorMessage,
            object? value,
            bool iscachable = false
        )
        {
            _provider.Register(
                state,
                new LambdaCondition<DataFilter>(state.ToString(), async context =>
                {
                    var filter = new DataFilter<TValue, TContext>(context);
                    if (value != null && value is TValue v)
                        filter.Value = v;

                    if (iscachable)
                        filter = await MapTo(filter);

                    return await checkFunc(filter);
                },
                errorMessage
                )
            );
        }
    }
}
