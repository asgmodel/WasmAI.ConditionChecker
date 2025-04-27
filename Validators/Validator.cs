using System;
using WasmAI.ConditionChecker.Checker;

namespace WasmAI.ConditionChecker.Validators
{
    /// <summary>
    /// The IValidator interface defines the basic structure for validators.
    /// It is implemented by classes responsible for validating different conditions.
    /// </summary>
    public interface IValidator
    {
        // Define methods for validation
    }

    /// <summary>
    /// ITValidator interface defines the method for registering a condition checker with the validator.
    /// </summary>
    public interface ITValidator
    {
        /// <summary>
        /// Registers a condition checker with the validator.
        /// </summary>
        /// <param name="checker">The condition checker to be registered.</param>
        void Register(IBaseConditionChecker checker);
    }

    /// <summary>
    /// Abstract class BaseValidator provides a base implementation for a validator.
    /// It is used to validate conditions by registering a checker and a provider.
    /// The class works with an enumeration type <typeparamref name="EValidator"/>
    /// that defines the set of conditions to be validated.
    /// </summary>
    /// <typeparam name="EValidator">The enum type representing different conditions.</typeparam>
    public abstract class BaseValidator<EValidator> : ITValidator
        where EValidator : Enum
    {
        /// <summary>
        /// The condition provider for the specific validator type.
        /// </summary>
        protected readonly IConditionProvider<EValidator> _provider;

        /// <summary>
        /// The condition checker used to validate conditions.
        /// </summary>
        protected readonly IBaseConditionChecker _checker;

        /// <summary>
        /// Constructor for BaseValidator.
        /// Initializes the provider and checker, and registers the provider with the checker.
        /// </summary>
        /// <param name="checker">The condition checker instance to be used for validation.</param>
        public BaseValidator(IBaseConditionChecker checker)
        {
            // Initialize the condition provider and checker
            _provider = new ConditionProvider<EValidator>();
            _checker = checker;

            // Initialize conditions (defined in derived classes)
            Initializer();

            // Register the provider with the checker
            _checker.RegisterProvider(_provider);
        }

        /// <summary>
        /// Registers the provided condition checker.
        /// </summary>
        /// <param name="checker">The checker to register.</param>
        public virtual void Register(IBaseConditionChecker checker)
        {
            checker.RegisterProvider(_provider);
        }

        /// <summary>
        /// Abstract method to initialize conditions, which should be implemented in derived classes.
        /// </summary>
        protected abstract void InitializeConditions();

        /// <summary>
        /// Initializes conditions, called in the constructor.
        /// This method can be overridden by derived classes.
        /// </summary>
        protected virtual void Initializer()
        {
            InitializeConditions();
        }
    }
}
