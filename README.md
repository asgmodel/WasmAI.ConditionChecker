

# **WasmAI.ConditionChecker Library**

## **Overview**

The **WasmAI.ConditionChecker** library is a robust framework designed to validate models and conditions in your application. It allows you to define, check, and manage validation rules using a modular and extensible approach. This library makes it easy to integrate condition-based checks and validations into your models, ensuring that data integrity is maintained across your system.

### **Key Features:**

- **Condition Checking**: Perform conditional checks against your models using custom logic.
- **Validation Framework**: Build custom validators for different conditions, including states such as "Active", "Valid", etc.
- **Asynchronous Validation**: Supports asynchronous processing for non-blocking checks, ideal for web and microservices architectures.
- **Extensibility**: Easily extend the library to fit your specific needs by adding custom conditions and validators.

---

## **Installation**

To integrate the **WasmAI.ConditionChecker** library into your project, use the following steps:

### **Using NuGet Package:**

To install via NuGet, run the following command in the **Package Manager Console**:

```bash
Install-Package WasmAI.ConditionChecker
```
or
```bash
dotnet add package WasmAI.ConditionChecker --version 1.0.0
```

Alternatively, you can clone or download the source code and add the project to your solution.

---

## **Getting Started**

### **1. BaseConditionChecker Implementation**

The core of the condition checking is the `BaseConditionChecker` class. This class is used to check conditions on your models. You can extend it to create your custom condition checkers.

#### **Example:**

```csharp
public class ConditionChecker : BaseConditionChecker, IConditionChecker
{
    public ConditionChecker() : base()
    {
    }
}
```

The `ConditionChecker` inherits from `BaseConditionChecker` and implements the `IConditionChecker` interface, allowing it to perform condition checks on models.

---

### **2. Creating a Custom Validator Context**

To apply validation to your models, extend `ValidatorContext<TContext, EValidator>`, and implement custom logic to fetch the model and perform validation.

#### **Example:**

```csharp
public class ApplicationUserValidatorContext : ValidatorContext<ApplicationUser, ApplicationUserValidatorStates>
{
    public ApplicationUserValidatorContext(IConditionChecker checker) : base(checker)
    {
    }

    protected override async Task<ApplicationUser?> FinModel(string? id)
    {
        var user = await _injector.Context.Set<ApplicationUser>().FindAsync(id);
        return user;
    }
}
```

Here, `ApplicationUserValidatorContext` inherits from `ValidatorContext` and implements the logic for retrieving the `ApplicationUser` model.

---

### **3. Registering Validators and Conditions**

You can define custom validators and register conditions using attributes. This allows you to check various properties of a model and ensure they match the expected values.

#### **Example:**

```csharp
[RegisterConditionValidator(typeof(ModelValidatorStates), ModelValidatorStates.HasCategory, "Model category does not match the required value.", Value = ModelFeatureValidatorKeys.Category)]
private Task<ConditionResult> CheckHasCategory(DataFilter<string, ModelAi> f)
{
    return f.Share?.Category == f.Value
        ? ConditionResult.ToSuccessAsync(f.Share)
        : ConditionResult.ToFailureAsync("Category mismatch.");
}
```

This example checks if the `Category` of the model matches a required value. The `RegisterConditionValidator` attribute helps in associating the condition with the model's state.

---

### **4. Running a Validation**

Once your validators and conditions are set up, you can perform the validation. The `IConditionChecker` interface checks all the conditions and returns the result of the validation.

#### **Example:**

```csharp
public async Task ValidateUser(string userId)
{
    var checker = new ConditionChecker(new ValidatorProvider());
    var validatorContext = new ApplicationUserValidatorContext(checker);

    var user = await validatorContext.GetModel(userId);
    if (user != null)
    {
        var validationResult = await validatorContext.ValidateAsync(user);
        if (validationResult.IsSuccess)
        {
            Console.WriteLine("Validation passed!");
        }
        else
        {
            Console.WriteLine("Validation failed: " + validationResult.Message);
        }
    }
}
```

This example validates an `ApplicationUser` by calling the `ValidateAsync` method and outputs the result.

---

### **5. Validation States Enum**

Each validation is associated with a specific state, typically represented by an `Enum`. These states are used to track the different conditions that need to be validated.

#### **Example:**

```csharp
public enum ApplicationUserValidatorStates
{
    IsActive,
    IsFull,
    IsValid
}
```

Each state corresponds to a particular condition, such as whether the user is active, whether the model is full, or whether the model is valid.

---

## **Condition Results**

Validation results are wrapped in a `ConditionResult` object, which provides both success and failure states. You can use these results to handle conditions and make decisions based on the validation.

#### **ConditionResult Class:**

```csharp
public class ConditionResult
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }

    public static ConditionResult ToSuccess(object? result)
    {
        return new ConditionResult { IsSuccess = true, Message = "Success!" };
    }

    public static ConditionResult ToFailure(string message)
    {
        return new ConditionResult { IsSuccess = false, Message = message };
    }
}
```

---

## **Customizing and Extending the Library**

This library is designed to be flexible, allowing for extensive customization and extension.

### **1. Custom Condition Checkers**

If you need to add custom validation logic, simply implement the `IConditionChecker` interface and register your custom conditions.

#### **Example:**

```csharp
public class CustomConditionChecker : BaseConditionChecker, IConditionChecker
{
    public CustomConditionChecker() : base() { }

    public override Task<ConditionResult> CheckConditionAsync(string model)
    {
        if (model.Contains("valid"))
        {
            return Task.FromResult(ConditionResult.ToSuccess(null));
        }
        return Task.FromResult(ConditionResult.ToFailure("Model is invalid"));
    }
}
```

### **2. Custom Validators**

You can create new validators by inheriting from `ValidatorContext` and overriding methods to add additional validation logic.

---

## **Supported Features**

- **Asynchronous Validation**: Perform validation checks asynchronously, making it ideal for applications that require non-blocking operations.
- **Custom Conditions**: Define and check custom conditions to match your application’s needs.
- **Flexible State Handling**: Manage and track validation states using Enums.
- **Modular and Extensible**: Add your own condition checkers, validators, and states easily.

---

## **Example Use Case**

Here’s a complete example where we validate a `ModelAi` object to ensure it meets specific conditions (category, language, dialect, etc.):

```csharp
public class ModelValidator : ValidatorContext<ModelAi, ModelValidatorStates>
{
    public ModelValidator(IConditionChecker checker) : base(checker)
    {
    }

    protected override void InitializeConditions()
    {
        _provider.Register(ModelValidatorStates.HasCategory, new LambdaCondition<ModelAi>(nameof(ModelValidatorStates.HasCategory), context => context.Category != null, "Category is missing"));
    }
}
```

---

---

## Related Packages

For additional functionality, check out our other library, [WasmAI.ConditionChecker](https://www.nuget.org/packages/WasmAI.ConditionChecker/#readme-body-tab).

This package provides tools for condition checking across different application domains and can be used in conjunction with ` WasmAI.ConditionChecker` for more robust applications.

---
## **License**

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

---

## **Conclusion**

The **WasmAI.ConditionChecker** library offers a powerful and flexible framework for model validation based on custom conditions. It is designed to be easily extensible and integrated into any application requiring complex validation logic.

For more information or to contribute to the project, please visit the repository’s GitHub page.
