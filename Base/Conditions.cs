using System;
using System.Reflection;
using System.Threading.Tasks;

namespace WasmAI.ConditionChecker.Base
{
    /// <summary>
    /// Represents the result of a condition evaluation, containing success status, result, and error message.
    /// </summary>
    public class ConditionResult
    {
        /// <summary>
        /// Gets or sets the success status of the condition evaluation.
        /// </summary>
        public bool? Success { get; set; }

        /// <summary>
        /// Gets or sets the result of the condition evaluation.
        /// </summary>
        public object? Result { get; set; }

        /// <summary>
        /// Gets or sets the error message to be displayed if the evaluation fails.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionResult"/> class.
        /// </summary>
        /// <param name="success">Indicates whether the condition evaluation was successful.</param>
        /// <param name="result">The result of the condition evaluation.</param>
        /// <param name="message">The error message if the evaluation fails (optional).</param>
        public ConditionResult(bool success, object? result, string? message = "")
        {
            Success = success;
            Result = result;
            Message = message;
        }

        /// <summary>
        /// Creates a success result (non-async) with the provided result and message.
        /// </summary>
        /// <param name="result">The result of the evaluation.</param>
        /// <param name="message">The message to return with the result (optional).</param>
        /// <returns>A <see cref="ConditionResult"/> representing a successful evaluation.</returns>
        public static ConditionResult ToSuccess(object? result, string? message = "")
        {
            return new ConditionResult(true, result, message);
        }

        /// <summary>
        /// Creates a failure result (non-async) with the provided result and message.
        /// </summary>
        /// <param name="result">The result of the evaluation.</param>
        /// <param name="message">The message to return with the result (optional).</param>
        /// <returns>A <see cref="ConditionResult"/> representing a failed evaluation.</returns>
        public static ConditionResult ToFailure(object? result, string? message = "")
        {
            return new ConditionResult(false, result, message);
        }

        /// <summary>
        /// Creates an error result (non-async) with the provided message.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <returns>A <see cref="ConditionResult"/> representing an error evaluation.</returns>
        public static ConditionResult ToError(string message)
        {
            return new ConditionResult(false, null, message);
        }

        /// <summary>
        /// Creates a success result asynchronously (Task-based) with the provided result and message.
        /// </summary>
        /// <param name="result">The result of the evaluation.</param>
        /// <param name="message">The message to return with the result (optional).</param>
        /// <returns>A task that represents the asynchronous operation, containing a <see cref="ConditionResult"/>.</returns>
        public static Task<ConditionResult> ToSuccessAsync(object? result, string? message = "")
        {
            return Task.FromResult(ToSuccess(result, message));
        }

        /// <summary>
        /// Creates a failure result asynchronously (Task-based) with the provided result and message.
        /// </summary>
        /// <param name="result">The result of the evaluation.</param>
        /// <param name="message">The message to return with the result (optional).</param>
        /// <returns>A task that represents the asynchronous operation, containing a <see cref="ConditionResult"/>.</returns>
        public static Task<ConditionResult> ToFailureAsync(object? result, string? message = "")
        {
            return Task.FromResult(ToFailure(result, message));
        }

        /// <summary>
        /// Creates an error result asynchronously (Task-based) with the provided message.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <returns>A task that represents the asynchronous operation, containing a <see cref="ConditionResult"/>.</returns>
        public static Task<ConditionResult> ToErrorAsync(string message)
        {
            return Task.FromResult(ToError(message));
        }

        /// <summary>
        /// Overrides the ToString method for better debug output.
        /// </summary>
        /// <returns>A string representation of the <see cref="ConditionResult"/>.</returns>
        public override string ToString()
        {
            return $"Success: {Success}, Message: {Message}, Result: {Result}";
        }
    }

    /// <summary>
    /// Defines the interface for a condition that can be evaluated.
    /// </summary>
    public interface ICondition
    {
        /// <summary>
        /// Gets the name of the condition.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the error message associated with the condition.
        /// </summary>
        string? ErrorMessage { get; }

        /// <summary>
        /// Evaluates the condition asynchronously.
        /// </summary>
        /// <param name="context">The context object that is used for evaluation.</param>
        /// <returns>A task representing the asynchronous operation, containing the evaluation result.</returns>
        Task<ConditionResult> Evaluate(object? context);
    }

    /// <summary>
    /// Abstract base class for a condition that implements the ICondition interface.
    /// </summary>
    public abstract class BaseCondition : ICondition
    {
        /// <summary>
        /// Gets the name of the condition.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the error message associated with the condition.
        /// </summary>
        public string? ErrorMessage { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCondition"/> class.
        /// </summary>
        /// <param name="name">The name of the condition.</param>
        /// <param name="errorMessage">The error message associated with the condition (optional).</param>
        public BaseCondition(string name, string? errorMessage = null)
        {
            Name = name;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Evaluates the condition asynchronously.
        /// </summary>
        /// <param name="context">The context object to evaluate.</param>
        /// <returns>A task representing the asynchronous operation, containing the evaluation result.</returns>
        public abstract Task<ConditionResult> Evaluate(object? context);
    }

    /// <summary>
    /// Represents a condition that is evaluated using a lambda function.
    /// </summary>
    public class LambdaCondition<T> : BaseCondition
    {
        private readonly Func<T, Task<ConditionResult>> _predicate;

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaCondition{T}"/> class with a predicate.
        /// </summary>
        /// <param name="name">The name of the condition.</param>
        /// <param name="predicate">The predicate function used to evaluate the condition.</param>
        /// <param name="errorMessage">The error message associated with the condition (optional).</param>
        public LambdaCondition(string name, Func<T, object> predicate, string? errorMessage = null)
            : base(name, errorMessage)
        {
            _predicate = ConvertToConditionResult(predicate, errorMessage);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaCondition{T}"/> class with a predicate.
        /// </summary>
        /// <param name="name">The name of the condition.</param>
        /// <param name="predicate">The predicate function used to evaluate the condition.</param>
        /// <param name="errorMessage">The error message associated with the condition (optional).</param>
        public LambdaCondition(string name, Func<T, ConditionResult> predicate, string? errorMessage = null)
            : base(name, errorMessage)
        {
            _predicate = ConvertToConditionResult(predicate);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaCondition{T}"/> class with an async predicate.
        /// </summary>
        /// <param name="name">The name of the condition.</param>
        /// <param name="predicate">The async predicate function used to evaluate the condition.</param>
        /// <param name="errorMessage">The error message associated with the condition (optional).</param>
        public LambdaCondition(string name, Func<T, Task<ConditionResult>> predicate, string? errorMessage = null)
            : base(name, errorMessage)
        {
            _predicate = predicate;
        }

        /// <summary>
        /// Converts a predicate function to a Task-based predicate function that returns a <see cref="ConditionResult"/>.
        /// </summary>
        private static Func<T, Task<ConditionResult>> ConvertToConditionResult(Func<T, object> predicate, string? errorMessage)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return (T context) =>
            {
                var result = predicate(context);
                if (result is bool flag)
                    return Task.FromResult(new ConditionResult(flag, result, errorMessage));

                return Task.FromResult(new ConditionResult(false, result, errorMessage));
            };
        }

        /// <summary>
        /// Converts a predicate function that returns a <see cref="ConditionResult"/> to a Task-based predicate.
        /// </summary>
        private static Func<T, Task<ConditionResult>> ConvertToConditionResult(Func<T, ConditionResult> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return (T context) =>
            {
                return Task.FromResult(predicate(context));
            };
        }

        /// <summary>
        /// Evaluates the condition asynchronously using the context.
        /// </summary>
        /// <param name="context">The context object to evaluate.</param>
        /// <returns>A task representing the asynchronous operation, containing the evaluation result.</returns>
        public override async Task<ConditionResult> Evaluate(object? context)
        {
            try
            {
                if (context is T typedContext)
                {
                    ConditionResult result = await _predicate(typedContext);
                    return result;
                }
                else if (context is string str)
                {
                    var df = new DataFilter(str);
                    var result = await _predicate((T)(object)df);
                    return result;
                }
                else if (context == null && typeof(T) == typeof(DataFilter))
                {
                    var dfn = new DataFilter();
                    var result = await _predicate((T)(object)dfn);
                    return result;
                }

                return new ConditionResult(false, null, $"Invalid context type: {context?.GetType().Name}, expected {typeof(T).Name}");
            }
            catch (Exception ex)
            {
                return new ConditionResult(false, null, $"An error occurred: {ex.Message}");
            }
        }
    }
}
