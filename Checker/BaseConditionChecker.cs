using WasmAI.ConditionChecker.Base;

namespace WasmAI.ConditionChecker.Checker
{



    /// <summary>
    /// Interface for performing various condition checks with multiple methods for both synchronous and asynchronous execution.
    /// </summary>
    public interface IBaseConditionChecker
    {
        /// <summary>
        /// Checks if a specific condition is met.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that represents the condition type.</typeparam>
        /// <param name="type">The condition to check.</param>
        /// <param name="context">Optional context to provide additional data for the condition check.</param>
        /// <returns>A boolean indicating whether the condition is met.</returns>
        bool Check<TEnum>(TEnum type, object? context = null) where TEnum : Enum;

        /// <summary>
        /// Asynchronously checks if a specific condition is met.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that represents the condition type.</typeparam>
        /// <param name="type">The condition to check.</param>
        /// <param name="context">Optional context to provide additional data for the condition check.</param>
        /// <returns>A task representing the asynchronous operation, with a boolean indicating whether the condition is met.</returns>
        Task<bool> CheckAsync<TEnum>(TEnum type, object? context = null) where TEnum : Enum;

        /// <summary>
        /// Checks if all conditions are met.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that represents the condition type.</typeparam>
        /// <param name="context">Optional context to provide additional data for the condition check.</param>
        /// <returns>A boolean indicating whether all conditions are met.</returns>
        bool CheckAll<TEnum>(object? context = null) where TEnum : Enum;

        /// <summary>
        /// Asynchronously checks if all conditions are met.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that represents the condition type.</typeparam>
        /// <param name="context">Optional context to provide additional data for the condition check.</param>
        /// <returns>A task representing the asynchronous operation, with a boolean indicating whether all conditions are met.</returns>
        Task<bool> CheckAllAsync<TEnum>(object? context = null) where TEnum : Enum;

        /// <summary>
        /// Checks if all conditions are met and returns a dictionary of failed conditions.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that represents the condition type.</typeparam>
        /// <param name="context">The context to provide additional data for the condition check.</param>
        /// <param name="failedConditions">Out parameter that returns the failed conditions as a dictionary.</param>
        /// <returns>A boolean indicating whether all conditions are met.</returns>
        bool AreAllConditionsMet<TEnum>(object context, out Dictionary<TEnum, string> failedConditions) where TEnum : Enum;

        /// <summary>
        /// Registers a condition provider for a specific condition type.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that represents the condition type.</typeparam>
        /// <param name="provider">The provider to register.</param>
        void RegisterProvider<TEnum>(IConditionProvider<TEnum> provider) where TEnum : Enum;

        /// <summary>
        /// Checks a condition and returns the result along with the status.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that represents the condition type.</typeparam>
        /// <param name="type">The condition to check.</param>
        /// <param name="context">Optional context to provide additional data for the condition check.</param>
        /// <returns>The result of the condition check.</returns>
        ConditionResult CheckAndResult<TEnum>(TEnum type, object? context = null) where TEnum : Enum;

        /// <summary>
        /// Asynchronously checks a condition and returns the result along with the status.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that represents the condition type.</typeparam>
        /// <param name="type">The condition to check.</param>
        /// <param name="context">Optional context to provide additional data for the condition check.</param>
        /// <returns>A task representing the asynchronous operation, with the result of the condition check.</returns>
        Task<ConditionResult> CheckAndResultAsync<TEnum>(TEnum type, object? context = null) where TEnum : Enum;

        /// <summary>
        /// Checks a condition and returns a boolean value along with an error message if the condition fails.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that represents the condition type.</typeparam>
        /// <param name="type">The condition to check.</param>
        /// <param name="context">The context to provide additional data for the condition check.</param>
        /// <param name="errorMessage">Out parameter that returns the error message if the condition fails.</param>
        /// <returns>A boolean indicating whether the condition is met.</returns>
        bool CheckWithError<TEnum>(TEnum type, object context, out string errorMessage) where TEnum : Enum;

        /// <summary>
        /// Asynchronously checks a condition and returns a boolean value along with an error message if the condition fails.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that represents the condition type.</typeparam>
        /// <param name="type">The condition to check.</param>
        /// <param name="context">The context to provide additional data for the condition check.</param>
        /// <param name="errorMessage">Out parameter that returns the error message if the condition fails.</param>
        /// <returns>A task representing the asynchronous operation, with a boolean indicating whether the condition is met.</returns>
        Task<bool> CheckWithErrorASync<TEnum>(TEnum type, object context, out string errorMessage) where TEnum : Enum;

        /// <summary>
        /// Checks a condition with multiple contexts.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that represents the condition type.</typeparam>
        /// <param name="type">The condition to check.</param>
        /// <param name="contexts">An array of contexts to provide additional data for the condition check.</param>
        /// <returns>A boolean indicating whether the condition is met.</returns>
        bool CheckWithMultipleContexts<TEnum>(TEnum type, object[] contexts) where TEnum : Enum;

        /// <summary>
        /// Asynchronously checks a condition with multiple contexts.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that represents the condition type.</typeparam>
        /// <param name="type">The condition to check.</param>
        /// <param name="contexts">An array of contexts to provide additional data for the condition check.</param>
        /// <returns>A task representing the asynchronous operation, with a boolean indicating whether the condition is met.</returns>
        Task<bool> CheckWithMultipleContextsAsync<TEnum>(TEnum type, object[] contexts) where TEnum : Enum;

        /// <summary>
        /// Resets the state of a condition checker for a specific context.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that represents the condition type.</typeparam>
        /// <param name="context">Optional context to reset the condition state for.</param>
        void ResetConditionState<TEnum>(object? context = null) where TEnum : Enum;

        /// <summary>
        /// Asynchronously checks a condition with a timeout.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that represents the condition type.</typeparam>
        /// <param name="type">The condition to check.</param>
        /// <param name="context">The context to provide additional data for the condition check.</param>
        /// <param name="timeout">The timeout duration for the condition check.</param>
        /// <returns>A task representing the asynchronous operation, with a boolean indicating whether the condition is met within the timeout.</returns>
        Task<bool> CheckConditionWithTimeoutAsync<TEnum>(TEnum type, object context, TimeSpan timeout) where TEnum : Enum;

        /// <summary>
        /// Asynchronously evaluates a condition with retry logic.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that represents the condition type.</typeparam>
        /// <param name="type">The condition to check.</param>
        /// <param name="context">The context to provide additional data for the condition check.</param>
        /// <param name="maxRetries">The maximum number of retries if the condition fails.</param>
        /// <param name="delay">The delay between each retry attempt.</param>
        /// <returns>A task representing the asynchronous operation, with a boolean indicating whether the condition is met after retries.</returns>
        Task<bool> EvaluateConditionWithRetryAsync<TEnum>(TEnum type, object context, int maxRetries, TimeSpan delay) where TEnum : Enum;

        /// <summary>
        /// Retrieves the details of failed conditions asynchronously.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that represents the condition type.</typeparam>
        /// <param name="context">Optional context to provide additional data for retrieving failed condition details.</param>
        /// <returns>A task representing the asynchronous operation, with a dictionary of failed conditions and their details.</returns>
        Task<Dictionary<TEnum, string>> GetFailedConditionDetailsAsync<TEnum>(object? context = null) where TEnum : Enum;

        /// <summary>
        /// Retrieves the details of failed conditions synchronously.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that represents the condition type.</typeparam>
        /// <param name="context">Optional context to provide additional data for retrieving failed condition details.</param>
        /// <returns>A dictionary of failed conditions and their details.</returns>
        Dictionary<TEnum, string> GetFailedConditionDetails<TEnum>(object? context = null) where TEnum : Enum;

        /// <summary>
        /// Asynchronously checks a condition considering contextual dependencies.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that represents the condition type.</typeparam>
        /// <param name="type">The condition to check.</param>
        /// <param name="contexts">An array of contexts to provide additional data for the condition check.</param>
        /// <returns>A task representing the asynchronous operation, with a boolean indicating whether the condition is met.</returns>
        Task<bool> CheckWithContextualDependenciesAsync<TEnum>(TEnum type, object[] contexts) where TEnum : Enum;

        /// <summary>
        /// Asynchronously checks a condition with a custom evaluator.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that represents the condition type.</typeparam>
        /// <param name="type">The condition to check.</param>
        /// <param name="context">The context to provide additional data for the condition check.</param>
        /// <param name="customEvaluator">A custom evaluator function to check the condition.</param>
        /// <returns>A task representing the asynchronous operation, with a boolean indicating whether the condition is met using the custom evaluator.</returns>
        Task<bool> CheckConditionByCustomEvaluatorAsync<TEnum>(TEnum type, object context, Func<object, Task<bool>> customEvaluator) where TEnum : Enum;

        /// <summary>
        /// Asynchronously checks if all conditions are met with retry logic.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that represents the condition type.</typeparam>
        /// <param name="context">The context to provide additional data for the condition check.</param>
        /// <param name="maxRetries">The maximum number of retries if conditions fail.</param>
        /// <param name="delay">The delay between each retry attempt.</param>
        /// <returns>A task representing the asynchronous operation, with a boolean indicating whether all conditions are met after retries.</returns>
        Task<bool> AreAllConditionsMetWithRetryAsync<TEnum>(object context, int maxRetries, TimeSpan delay) where TEnum : Enum;

        /// <summary>
        /// Asynchronously checks a condition with additional contextual data.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that represents the condition type.</typeparam>
        /// <param name="type">The condition to check.</param>
        /// <param name="context">The context to provide additional data for the condition check.</param>
        /// <param name="additionalData">Additional data to be used for the condition check.</param>
        /// <returns>A task representing the asynchronous operation, with a boolean indicating whether the condition is met.</returns>
        Task<bool> CheckWithContextDataAsync<TEnum>(TEnum type, object context, object additionalData) where TEnum : Enum;

        /// <summary>
        /// Retrieves the history of condition checks asynchronously.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that represents the condition type.</typeparam>
        /// <param name="context">Optional context to provide additional data for retrieving condition history.</param>
        /// <returns>A task representing the asynchronous operation, with a dictionary of condition history results.</returns>
        Task<Dictionary<TEnum, List<ConditionResult>>> GetConditionHistoryAsync<TEnum>(object? context = null) where TEnum : Enum;

        /// <summary>
        /// Retrieves the history of condition checks synchronously.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that represents the condition type.</typeparam>
        /// <param name="context">Optional context to provide additional data for retrieving condition history.</param>
        /// <returns>A dictionary of condition history results.</returns>
        Dictionary<TEnum, List<ConditionResult>> GetConditionHistory<TEnum>(object? context = null) where TEnum : Enum;

        /// <summary>
        /// Event triggered when a condition is met.
        /// </summary>
        public event EventHandler<ConditionResult> ConditionMet;

        /// <summary>
        /// Event triggered when a condition fails.
        /// </summary>
        public event EventHandler<ConditionResult> ConditionFailed;

        /// <summary>
        /// Executes a condition check with success and failure callbacks.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that represents the condition type.</typeparam>
        /// <param name="conditionType">The condition to check.</param>
        /// <param name="context">The context to provide additional data for the condition check.</param>
        /// <param name="onSuccess">Optional callback for when the condition is met.</param>
        /// <param name="onFailure">Optional callback for when the condition fails.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task ExecuteConditionWithCallbacksAsync<TEnum>(
            TEnum conditionType,
            object context,
            Func<ConditionResult, Task>? onSuccess = null,
            Func<ConditionResult, Task>? onFailure = null)
            where TEnum : Enum;

        /// <summary>
        /// Retrieves the provider for a specific condition type.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that represents the condition type.</typeparam>
        /// <returns>The provider for the specified condition type.</returns>
        public IConditionProvider<TEnum>? GetProvider<TEnum>() where TEnum : Enum;
    }


    /// <summary>
    /// This class represents a basic condition checker. 
    /// It implements the IBaseConditionChecker interface and includes a set of methods for checking conditions using a provided condition provider. 
    /// The conditions can be checked synchronously or asynchronously, and it supports checking a single condition, multiple conditions, or conditions with different contexts. 
    /// The class also supports checking conditions with a timeout, retry mechanisms, and retrieving details of failed conditions.
    /// </summary>
    public abstract class BaseConditionChecker :  IBaseConditionChecker
    {
        /// <summary>
        /// A dictionary that holds the registered condition providers, mapped by their type.
        /// This allows the dynamic registration and retrieval of condition providers based on their type.
        /// </summary>
        private readonly Dictionary<Type, object> _providers = new();

        /// <summary>
        /// Event triggered when the condition is met successfully.
        /// </summary>
        public event EventHandler<ConditionResult>? ConditionMet;

        /// <summary>
        /// Event triggered when the condition fails.
        /// </summary>
        public event EventHandler<ConditionResult>? ConditionFailed;

        /// <summary>
        /// Constructor for the BaseConditionChecker class.
        /// </summary>
        public BaseConditionChecker()
        {
            // Any future logic can be added here if necessary
        }

        /// <summary>
        /// Method to trigger the event when the condition is met successfully.
        /// </summary>
        /// <param name="result">The result of the condition check.</param>
        protected virtual void OnConditionMet(ConditionResult result)
        {
            ConditionMet?.Invoke(this, result); // Check for listeners and trigger the event
        }

        /// <summary>
        /// Method to trigger the event when the condition fails.
        /// </summary>
        /// <param name="result">The result of the failed condition check.</param>
        protected virtual void OnConditionFailed(ConditionResult result)
        {
            ConditionFailed?.Invoke(this, result); // Check for listeners and trigger the event
        }

        /// <summary>
        /// Method to register a condition provider based on the specified type.
        /// </summary>
        /// <typeparam name="TEnum">The type of conditions provided by the condition provider.</typeparam>
        /// <param name="provider">The condition provider to be registered.</param>
        public void RegisterProvider<TEnum>(IConditionProvider<TEnum> provider) where TEnum : Enum
        {
            _providers[typeof(TEnum)] = provider; // Register the provider in the dictionary based on the type of conditions
        }




        /// <summary>
        /// Synchronously checks the condition for a specific type.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to check, must be an enum.</typeparam>
        /// <param name="type">The specific condition type to evaluate.</param>
        /// <param name="context">Optional context for the condition evaluation.</param>
        /// <returns>True if the condition is met, otherwise false.</returns>
        public bool Check<TEnum>(TEnum type, object? context = null) where TEnum : Enum
        {
            var res = CheckAsync<TEnum>(type, context).GetAwaiter().GetResult();
            return res;
        }

        /// <summary>
        /// Retrieves the condition provider for a specific type.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition provider, must be an enum.</typeparam>
        /// <returns>The condition provider if found, otherwise null.</returns>
        public IConditionProvider<TEnum>? GetProvider<TEnum>() where TEnum : Enum
        {
            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                return rawProvider as IConditionProvider<TEnum>;
            }

            return null;
        }

        /// <summary>
        /// Asynchronously checks the condition for a specific type.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to check, must be an enum.</typeparam>
        /// <param name="type">The specific condition type to evaluate.</param>
        /// <param name="context">Optional context for the condition evaluation.</param>
        /// <returns>A task representing the result, with true if the condition is met, otherwise false.</returns>
        public async Task<bool> CheckAsync<TEnum>(TEnum type, object? context = null) where TEnum : Enum
        {
            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                var provider = rawProvider as IConditionProvider<TEnum>;
                var condition = provider?.Get(type);
                if (condition != null)
                {
                    var result = await condition.Evaluate(context);
                    return result?.Success ?? false;
                }
            }
            return false;
        }

        /// <summary>
        /// Synchronously checks all conditions for a specific type.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to check, must be an enum.</typeparam>
        /// <param name="context">Optional context for the condition evaluation.</param>
        /// <returns>True if all conditions are met, otherwise false.</returns>
        public bool CheckAll<TEnum>(object? context = null) where TEnum : Enum
        {
            var res = CheckAllAsync<TEnum>(context).GetAwaiter().GetResult();
            return res;
        }

        /// <summary>
        /// Asynchronously checks all conditions for a specific type and returns whether all conditions are met.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to check, must be an enum.</typeparam>
        /// <param name="context">Optional context for the condition evaluation.</param>
        /// <returns>A task representing the result, with true if all conditions are met, otherwise false.</returns>
        public async Task<bool> CheckAllAsync<TEnum>(object? context = null) where TEnum : Enum
        {
            var results = new Dictionary<TEnum, bool>();

            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                var provider = rawProvider as IConditionProvider<TEnum>;
                if (provider != null)
                {
                    foreach (TEnum type in Enum.GetValues(typeof(TEnum)))
                    {
                        var condition = provider.Get(type);
                        if (condition != null)
                        {
                            var result = await condition.Evaluate(context);
                            results[type] = result?.Success ?? false;
                        }
                    }
                }
            }

            return results.All(r => r.Value);
        }

        /// <summary>
        /// Checks a specific condition and returns the result along with an error message if the condition fails.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to check, must be an enum.</typeparam>
        /// <param name="type">The specific condition type to evaluate.</param>
        /// <param name="context">The context for the condition evaluation.</param>
        /// <param name="errorMessage">Output parameter that contains the error message in case of failure.</param>
        /// <returns>True if the condition is met, otherwise false. The error message provides further details in case of failure.</returns>
        public bool CheckWithError<TEnum>(TEnum type, object context, out string errorMessage) where TEnum : Enum
        {
            var result = CheckAndResultAsync(type, context).GetAwaiter().GetResult();
            if (result == null)
            {
                errorMessage = "Condition not found or provider unavailable";
                return false;
            }
            errorMessage = result?.Message ?? "Unknown error";

            return true;
        }

        /// <summary>
        /// Checks all conditions for a specific type and returns the results with detailed messages for each condition.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to check, must be an enum.</typeparam>
        /// <param name="context">The context for the condition evaluation.</param>
        /// <param name="results">Output parameter that contains a dictionary of condition results and messages for each condition.</param>
        /// <returns>True if all conditions are met, otherwise false. The results dictionary contains detailed messages for each condition.</returns>
        public bool CheckAllWithDetails<TEnum>(object context, out Dictionary<TEnum, string> results) where TEnum : Enum
        {
            results = new Dictionary<TEnum, string>();

            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                var provider = rawProvider as IConditionProvider<TEnum>;
                if (provider != null)
                {
                    foreach (TEnum type in Enum.GetValues(typeof(TEnum)))
                    {
                        var condition = provider.Get(type);
                        var result = condition?.Evaluate(context).GetAwaiter().GetResult();
                        results[type] = result?.Success == true ? "Success" : result?.Message ?? "Unknown error";
                    }
                }
            }

            return results.All(r => r.Value == "Success");
        }

        /// <summary>
        /// Synchronously checks a specific condition for multiple contexts and returns whether the condition is met across all contexts.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to check, must be an enum.</typeparam>
        /// <param name="type">The specific condition type to evaluate.</param>
        /// <param name="contexts">An array of contexts for the condition evaluation.</param>
        /// <returns>True if the condition is met across all contexts, otherwise false.</returns>
        public bool CheckWithMultipleContexts<TEnum>(TEnum type, object[] contexts) where TEnum : Enum
        {
            var res = CheckWithMultipleContextsAsync(type, contexts).GetAwaiter().GetResult();
            return res;
        }

        /// <summary>
        /// Asynchronously checks a specific condition for multiple contexts and returns whether the condition is met across all contexts.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to check, must be an enum.</typeparam>
        /// <param name="type">The specific condition type to evaluate.</param>
        /// <param name="contexts">An array of contexts for the condition evaluation.</param>
        /// <returns>A task representing the result, with true if the condition is met across all contexts, otherwise false.</returns>
        public async Task<bool> CheckWithMultipleContextsAsync<TEnum>(TEnum type, object[] contexts) where TEnum : Enum
        {
            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                var provider = rawProvider as IConditionProvider<TEnum>;
                var condition = provider?.Get(type);
                if (condition != null)
                {
                    foreach (var context in contexts)
                    {
                        var result = await condition.Evaluate(context);
                        if (result?.Success == false)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }


        /// <summary>
        /// Synchronously checks the condition for a specific type and returns the result.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to check, must be an enum.</typeparam>
        /// <param name="type">The specific condition type to evaluate.</param>
        /// <param name="context">The context for the condition evaluation (optional).</param>
        /// <returns>The result of the condition evaluation.</returns>
        public ConditionResult CheckAndResult<TEnum>(TEnum type, object? context = null) where TEnum : Enum
        {
            var result = CheckAndResultAsync(type, context).GetAwaiter().GetResult();
            return result;
        }

        /// <summary>
        /// Asynchronously checks the condition for a specific type and returns the result.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to check, must be an enum.</typeparam>
        /// <param name="type">The specific condition type to evaluate.</param>
        /// <param name="context">The context for the condition evaluation (optional).</param>
        /// <returns>A task representing the result of the condition evaluation.</returns>
        public async Task<ConditionResult> CheckAndResultAsync<TEnum>(TEnum type, object? context = null) where TEnum : Enum
        {
            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                var provider = rawProvider as IConditionProvider<TEnum>;
                var condition = provider?.Get(type);
                if (condition != null)
                {
                    var result = await condition.Evaluate(context);
                    return result ?? new ConditionResult(false, null, "Unknown error");
                }
            }
            return new ConditionResult(false, null, "Condition not found or provider unavailable");
        }

        /// <summary>
        /// Asynchronously checks the condition and returns a result along with an error message if the condition fails.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to check, must be an enum.</typeparam>
        /// <param name="type">The specific condition type to evaluate.</param>
        /// <param name="context">The context for the condition evaluation.</param>
        /// <param name="errorMessage">The output parameter that contains the error message if the condition fails.</param>
        /// <returns>A task representing the success or failure of the check.</returns>
        public Task<bool> CheckWithErrorASync<TEnum>(TEnum type, object context, out string errorMessage) where TEnum : Enum
        {
            var result = CheckAndResultAsync(type, context).GetAwaiter().GetResult();
            if (result == null)
            {
                errorMessage = "Condition not found or provider unavailable";
                return Task.FromResult(false);
            }
            errorMessage = result?.Message ?? "Unknown error";
            return Task.FromResult(true);
        }

        /// <summary>
        /// Checks if all conditions are met for a specific type and provides a dictionary of failed conditions with their messages.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to check, must be an enum.</typeparam>
        /// <param name="context">The context for the condition evaluation.</param>
        /// <param name="failedConditions">The output parameter containing a dictionary of failed conditions with their respective error messages.</param>
        /// <returns>True if all conditions are met, otherwise false. The dictionary contains failed conditions.</returns>
        public bool AreAllConditionsMet<TEnum>(object context, out Dictionary<TEnum, string> failedConditions) where TEnum : Enum
        {
            failedConditions = new Dictionary<TEnum, string>();

            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                var provider = rawProvider as IConditionProvider<TEnum>;
                if (provider != null)
                {
                    foreach (TEnum type in Enum.GetValues(typeof(TEnum)))
                    {
                        var condition = provider.Get(type);
                        var result = condition?.Evaluate(context).GetAwaiter().GetResult();
                        if (result?.Success == false)
                        {
                            failedConditions[type] = result?.Message ?? "Unknown error";
                        }
                    }
                }
            }

            return failedConditions.Count == 0;
        }

        /// <summary>
        /// Asynchronously checks if any condition is met for a specific type and returns whether the condition is successful.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to check, must be an enum.</typeparam>
        /// <param name="context">The context for the condition evaluation (optional).</param>
        /// <returns>A task representing whether any condition is met (true) or not (false).</returns>
        public async Task<bool> CheckAnyAsync<TEnum>(object? context = null) where TEnum : Enum
        {
            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                var provider = rawProvider as IConditionProvider<TEnum>;
                if (provider != null)
                {
                    foreach (TEnum type in Enum.GetValues(typeof(TEnum)))
                    {
                        var condition = provider.Get(type);
                        if (condition == null)
                        {
                            return false;
                        }
                        var result = await condition.Evaluate(context);
                        if (result?.Success == true)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }


        /// <summary>
        /// Synchronously checks if any condition is met for a specific type and returns the result.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to check, must be an enum.</typeparam>
        /// <param name="context">The context for the condition evaluation (optional).</param>
        /// <returns>True if any condition is met, otherwise false.</returns>
        public bool CheckAny<TEnum>(object? context = null) where TEnum : Enum
        {
            var res = CheckAnyAsync<TEnum>(context).GetAwaiter().GetResult();
            return res;
        }

        /// <summary>
        /// Asynchronously checks the condition with a timeout. If the condition is not evaluated within the specified time, it returns false.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to check, must be an enum.</typeparam>
        /// <param name="type">The specific condition type to evaluate.</param>
        /// <param name="context">The context for the condition evaluation.</param>
        /// <param name="timeout">The time span within which the condition should be evaluated.</param>
        /// <returns>A task representing whether the condition was successfully evaluated within the given timeout.</returns>
        public async Task<bool> CheckConditionWithTimeoutAsync<TEnum>(TEnum type, object context, TimeSpan timeout) where TEnum : Enum
        {
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(timeout);

            try
            {
                return await CheckAsync<TEnum>(type, context);
            }
            catch (TaskCanceledException)
            {
                return false;
            }
        }

        /// <summary>
        /// Asynchronously evaluates a condition with retries. If the condition fails, it will be retried up to the specified maximum number of retries, with a delay between each retry.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to check, must be an enum.</typeparam>
        /// <param name="type">The specific condition type to evaluate.</param>
        /// <param name="context">The context for the condition evaluation.</param>
        /// <param name="maxRetries">The maximum number of retry attempts.</param>
        /// <param name="delay">The delay between retries.</param>
        /// <returns>A task representing the result of the condition evaluation, true if the condition succeeds within the retry attempts, otherwise false.</returns>
        public async Task<bool> EvaluateConditionWithRetryAsync<TEnum>(TEnum type, object context, int maxRetries, TimeSpan delay) where TEnum : Enum
        {
            var retries = 0;
            while (retries < maxRetries)
            {
                var result = await CheckAsync<TEnum>(type, context);
                if (result)
                {
                    return true;
                }
                retries++;
                await Task.Delay(delay);
            }
            return false;
        }

        /// <summary>
        /// Asynchronously retrieves the details of failed conditions for a specific type. It returns a dictionary with the conditions and their respective error messages.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to check, must be an enum.</typeparam>
        /// <param name="context">The context for the condition evaluation (optional).</param>
        /// <returns>A task representing the dictionary of failed conditions with their respective error messages.</returns>
        public async Task<Dictionary<TEnum, string>> GetFailedConditionDetailsAsync<TEnum>(object? context = null) where TEnum : Enum
        {
            var failedConditions = new Dictionary<TEnum, string>();

            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                var provider = rawProvider as IConditionProvider<TEnum>;
                if (provider != null)
                {
                    foreach (TEnum type in Enum.GetValues(typeof(TEnum)))
                    {
                        var condition = provider.Get(type);
                        if (condition == null)
                        {
                            failedConditions[type] = "Condition not found";
                            continue;
                        }

                        var result = await condition.Evaluate(context);
                        if (result?.Success == false)
                        {
                            failedConditions[type] = result?.Message ?? "Unknown error";
                        }
                    }
                }
            }

            return failedConditions;
        }

        /// <summary>
        /// Synchronously retrieves the details of failed conditions for a specific type.
        /// It returns a dictionary with the conditions and their respective error messages.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to check, must be an enum.</typeparam>
        /// <param name="context">The context for the condition evaluation (optional).</param>
        /// <returns>A dictionary with the failed conditions and their error messages.</returns>
        public Dictionary<TEnum, string> GetFailedConditionDetails<TEnum>(object? context = null) where TEnum : Enum
        {
            var res = GetFailedConditionDetailsAsync<TEnum>(context).GetAwaiter().GetResult();
            return res;
        }

        /// <summary>
        /// Asynchronously checks the condition with multiple contexts. If any context fails the condition, it returns false.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to check, must be an enum.</typeparam>
        /// <param name="type">The specific condition type to evaluate.</param>
        /// <param name="contexts">An array of contexts to evaluate the condition against.</param>
        /// <returns>A task representing whether the condition was successfully evaluated across all contexts.</returns>
        public async Task<bool> CheckWithContextualDependenciesAsync<TEnum>(TEnum type, object[] contexts) where TEnum : Enum
        {
            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                var provider = rawProvider as IConditionProvider<TEnum>;
                var condition = provider?.Get(type);
                if (condition != null)
                {
                    foreach (var context in contexts)
                    {
                        var result = await condition.Evaluate(context);
                        if (result?.Success == false)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Asynchronously checks the condition using a custom evaluator.
        /// It allows the user to define their own evaluation logic for the condition.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to check, must be an enum.</typeparam>
        /// <param name="type">The specific condition type to evaluate.</param>
        /// <param name="context">The context for the condition evaluation.</param>
        /// <param name="customEvaluator">A custom evaluation function that takes the context as input and returns a boolean task indicating the result.</param>
        /// <returns>A task representing whether the custom evaluation succeeds for the condition.</returns>
        public async Task<bool> CheckConditionByCustomEvaluatorAsync<TEnum>(TEnum type, object context, Func<object, Task<bool>> customEvaluator) where TEnum : Enum
        {
            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                var provider = rawProvider as IConditionProvider<TEnum>;
                var condition = provider?.Get(type);
                if (condition != null)
                {
                    var result = await customEvaluator(context);
                    return result;
                }
            }
            return false;
        }

        /// <summary>
        /// Asynchronously checks the condition with additional data. This method allows evaluating a condition with both the context and additional data.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to check, must be an enum.</typeparam>
        /// <param name="type">The specific condition type to evaluate.</param>
        /// <param name="context">The context for the condition evaluation.</param>
        /// <param name="additionalData">Additional data that can be used for condition evaluation (optional).</param>
        /// <returns>A task representing whether the condition was successfully evaluated using the context and additional data.</returns>
        public async Task<bool> CheckWithContextDataAsync<TEnum>(TEnum type, object context, object additionalData) where TEnum : Enum
        {
            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                var provider = rawProvider as IConditionProvider<TEnum>;
                var condition = provider?.Get(type);
                if (condition != null)
                {
                    var result = await condition.Evaluate(context);
                    // Additional data can be used here if needed
                    return result?.Success ?? false;
                }
            }
            return false;
        }

        /// <summary>
        /// Asynchronously retrieves the history of conditions, returning a dictionary where each condition type
        /// maps to a list of results for that condition over time.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to check, must be an enum.</typeparam>
        /// <param name="context">The context for the condition evaluation (optional).</param>
        /// <returns>A task representing the condition history for each condition type.</returns>
        public async Task<Dictionary<TEnum, List<ConditionResult>>> GetConditionHistoryAsync<TEnum>(object? context = null) where TEnum : Enum
        {
            var history = new Dictionary<TEnum, List<ConditionResult>>();

            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                var provider = rawProvider as IConditionProvider<TEnum>;
                if (provider != null)
                {
                    foreach (TEnum type in Enum.GetValues(typeof(TEnum)))
                    {
                        var condition = provider.Get(type);
                        if (condition == null)
                        {
                            continue;
                        }
                        var result = await condition.Evaluate(context);
                        if (result != null)
                        {
                            if (!history.ContainsKey(type))
                            {
                                history[type] = new List<ConditionResult>();
                            }
                            history[type].Add(result);
                        }
                    }
                }
            }

            return history;
        }

        /// <summary>
        /// Synchronously retrieves the history of conditions, returning a dictionary where each condition type
        /// maps to a list of results for that condition over time.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to check, must be an enum.</typeparam>
        /// <param name="context">The context for the condition evaluation (optional).</param>
        /// <returns>The condition history for each condition type.</returns>
        public Dictionary<TEnum, List<ConditionResult>> GetConditionHistory<TEnum>(object? context = null) where TEnum : Enum
        {
            var res = GetConditionHistoryAsync<TEnum>(context).GetAwaiter().GetResult();
            return res;
        }

        /// <summary>
        /// Resets the state of conditions for a specific type, allowing for re-evaluation or state reset.
        /// This method is not yet implemented.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to reset, must be an enum.</typeparam>
        /// <param name="context">The context for resetting the condition state (optional).</param>
        public void ResetConditionState<TEnum>(object? context = null) where TEnum : Enum
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously checks if all conditions are met with retry logic. It will retry up to a maximum number of retries,
        /// waiting for a specified delay between each retry attempt.
        /// This method is not yet implemented.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to check, must be an enum.</typeparam>
        /// <param name="context">The context for the condition evaluation.</param>
        /// <param name="maxRetries">The maximum number of retries to attempt.</param>
        /// <param name="delay">The delay between each retry attempt.</param>
        /// <returns>A task representing whether all conditions were met after retries.</returns>
        public Task<bool> AreAllConditionsMetWithRetryAsync<TEnum>(object context, int maxRetries, TimeSpan delay) where TEnum : Enum
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously executes a condition and invokes success or failure callbacks based on the result.
        /// The appropriate callback (onSuccess or onFailure) is triggered depending on the condition result.
        /// </summary>
        /// <typeparam name="TEnum">The type of condition to evaluate, must be an enum.</typeparam>
        /// <param name="conditionType">The specific condition type to evaluate.</param>
        /// <param name="context">The context for evaluating the condition.</param>
        /// <param name="onSuccess">An optional callback to invoke if the condition is met (success).</param>
        /// <param name="onFailure">An optional callback to invoke if the condition fails (failure).</param>
        /// <returns>A task representing the asynchronous execution of the condition and callbacks.</returns>
        public async Task ExecuteConditionWithCallbacksAsync<TEnum>(TEnum conditionType, object context, Func<ConditionResult, Task>? onSuccess = null, Func<ConditionResult, Task>? onFailure = null) where TEnum : Enum
        {
            var result = await CheckAndResultAsync(conditionType, context);
            if (result.Success == true)
            {
                OnConditionMet(result);
                if (onSuccess != null)
                {
                    await onSuccess(result);
                }
            }
            else
            {
                OnConditionFailed(result);
                if (onFailure != null)
                {
                    await onFailure(result);
                }
            }
        }

    }


}





