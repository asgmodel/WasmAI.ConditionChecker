namespace WasmAI.ConditionChecker.Base;



/// <summary>
/// This attribute is used to register a condition validator method.
/// It can be applied multiple times to a method.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class RegisterConditionValidatorAttribute : Attribute
{
    /// <summary>
    /// Gets the state associated with the validator.
    /// </summary>
    public object? State { get; }

    /// <summary>
    /// Gets the enum type associated with the validator.
    /// </summary>
    public Type? EnumType { get; }

    /// <summary>
    /// Gets the error message to be displayed when validation fails.
    /// </summary>
    public string? ErrorMessage { get; }

    /// <summary>
    /// Gets or sets the value that can be used in the validation (optional).
    /// </summary>
    public object? Value { get; set; } = null;

    /// <summary>
    /// Gets or sets a value indicating whether the validation result should be cached.
    /// </summary>
    public bool IsCachability { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterConditionValidatorAttribute"/> class.
    /// </summary>
    /// <param name="enumType">The enum type to associate with the validator.</param>
    /// <param name="state">The state associated with the validator.</param>
    /// <param name="errorMessage">The error message to display when validation fails (optional).</param>
    /// <param name="isCachability">Indicates whether the validation result should be cached (default is true).</param>
    /// <exception cref="ArgumentException">Thrown if the provided <paramref name="enumType"/> is not an enum type.</exception>
    public RegisterConditionValidatorAttribute(Type enumType, object state, string? errorMessage = "", bool isCachability = true)
    {
        if (!enumType.IsEnum)
            throw new ArgumentException("EnumType must be an enumeration type"); // Ensures the enumType is an enumeration

        EnumType = enumType;
        State = state;
        ErrorMessage = errorMessage;
        IsCachability = isCachability;
    }
}

/// <summary>
/// This attribute is used to build a condition validator method.
/// It can only be applied once to a method.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class BuildConditionValidatorAttribute : Attribute
{
    /// <summary>
    /// Gets the state associated with the validator.
    /// </summary>
    public object State { get; }

    /// <summary>
    /// Gets the enum type associated with the validator.
    /// </summary>
    public Type EnumType { get; }

    /// <summary>
    /// Gets the error message to be displayed when validation fails.
    /// </summary>
    public string ErrorMessage { get; }

    /// <summary>
    /// Gets or sets the value that can be used in the validation (optional).
    /// </summary>
    public object? Value { get; set; } = null;

    /// <summary>
    /// Gets or sets a value indicating whether the validation result should be cached.
    /// </summary>
    public bool IsCachability { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BuildConditionValidatorAttribute"/> class.
    /// </summary>
    /// <param name="enumType">The enum type to associate with the validator.</param>
    /// <param name="state">The state associated with the validator.</param>
    /// <param name="errorMessage">The error message to display when validation fails.</param>
    /// <param name="isCachability">Indicates whether the validation result should be cached (default is true).</param>
    /// <exception cref="ArgumentException">Thrown if the provided <paramref name="enumType"/> is not an enum type.</exception>
    public BuildConditionValidatorAttribute(Type enumType, object state, string errorMessage, bool isCachability = true)
    {
        if (!enumType.IsEnum)
            throw new ArgumentException("EnumType must be an enumeration type"); // Ensures the enumType is an enumeration

        EnumType = enumType;
        State = state;
        ErrorMessage = errorMessage;
        IsCachability = isCachability;
    }
}
