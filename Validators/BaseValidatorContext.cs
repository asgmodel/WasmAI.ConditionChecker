using WasmAI.ConditionChecker.Base;
using WasmAI.ConditionChecker.Checker;

namespace WasmAI.ConditionChecker.Validators
{


    public enum GeneralValidatorStates
    {
        HasId,
        HasName,
        IsEnabled,
        IsDisabled,
        HasOwner,
        IsLinkedToEntity,
        IsActive,
        IsArchived,
        HasCreatedDate,
        HasUpdatedDate,
        HasValidUri,
        HasDescription,
        HasCategory,
        IsVerified,
        HasTags,
        HasPermissions,
        IsInUserClaims,
        HasParent,
        HasChildren,
        HasValidEmail,
        HasPhone,
        IsPublic,
        IsPrivate,
        IsDefault,
        IsRequired,
        IsEditable,
        IsDeletable,
        IsValidState,
        HasValidStatus,
        IsSystemDefined
    }

    /// <summary>
    /// Represents the base class for validator context, used for validating various conditions in the context of an object.
    /// This class extends the base validator context and provides validations for multiple properties.
    /// </summary>
    /// <typeparam name="TContext">The type of the context that will be validated.</typeparam>
    /// <typeparam name="EValidator">The enum representing the validation states.</typeparam>
    public abstract class BaseValidatorContext<TContext, EValidator> : TBaseValidatorContext<TContext, EValidator>
        where TContext : class
        where EValidator : Enum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseValidatorContext"/> class.
        /// </summary>
        /// <param name="checker">The checker used for condition validation.</param>
        public BaseValidatorContext(IBaseConditionChecker checker) : base(checker) { }

        /// <summary>
        /// Validates whether the object has a valid Id.
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasId)]
        private Task<ConditionResult> ValidateHasId(DataFilter<string, TContext> f) =>
            ValidatePropertyExists(f, "Id", "Id is missing");

        /// <summary>
        /// Validates whether the object has a valid Name.
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasName)]
        private Task<ConditionResult> ValidateHasName(DataFilter<string, TContext> f) =>
            ValidatePropertyExists(f, "Name", "Name is missing");

        /// <summary>
        /// Validates whether the object is enabled (IsEnabled property is true).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsEnabled)]
        private Task<ConditionResult> ValidateIsEnabled(DataFilter<string, TContext> f) =>
            ValidateBoolProperty(f, "IsEnabled", true, "Object is not enabled");

        /// <summary>
        /// Validates whether the object is disabled (IsEnabled property is false).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsDisabled)]
        private Task<ConditionResult> ValidateIsDisabled(DataFilter<string, TContext> f) =>
            ValidateBoolProperty(f, "IsEnabled", false, "Object is not disabled");

        /// <summary>
        /// Validates whether the object has an Owner (OwnerId property is present).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasOwner)]
        private Task<ConditionResult> ValidateHasOwner(DataFilter<string, TContext> f) =>
            ValidatePropertyExists(f, "OwnerId", "Owner is missing");

        /// <summary>
        /// Validates whether the object is linked to at least one entity (LinkedEntities property is not empty).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsLinkedToEntity)]
        private Task<ConditionResult> ValidateIsLinkedToEntity(DataFilter<string, TContext> f) =>
            ValidateCollectionNotEmpty(f, "LinkedEntities", "Not linked to any entity");

        /// <summary>
        /// Validates whether the object is active (IsActive property is true).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsActive)]
        private Task<ConditionResult> ValidateIsActive(DataFilter<string, TContext> f) =>
            ValidateBoolProperty(f, "IsActive", true, "Object is not active");

        /// <summary>
        /// Validates whether the object is archived (IsArchived property is true).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsArchived)]
        private Task<ConditionResult> ValidateIsArchived(DataFilter<string, TContext> f) =>
            ValidateBoolProperty(f, "IsArchived", true, "Object is not archived");

        /// <summary>
        /// Validates whether the object has a valid CreatedDate (CreatedAt property).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasCreatedDate)]
        private Task<ConditionResult> ValidateHasCreatedDate(DataFilter<string, TContext> f) =>
            ValidatePropertyExists(f, "CreatedAt", "Missing created date");

        /// <summary>
        /// Validates whether the object has a valid UpdatedDate (UpdatedAt property).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasUpdatedDate)]
        private Task<ConditionResult> ValidateHasUpdatedDate(DataFilter<string, TContext> f) =>
            ValidatePropertyExists(f, "UpdatedAt", "Missing updated date");

        /// <summary>
        /// Validates whether the object has a valid URI (Uri property).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasValidUri)]
        private Task<ConditionResult> ValidateHasValidUri(DataFilter<string, TContext> f) =>
            ValidateUri(f, "Uri", "Invalid URI");

        /// <summary>
        /// Validates whether the object has a valid Description (Description property).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasDescription)]
        private Task<ConditionResult> ValidateHasDescription(DataFilter<string, TContext> f) =>
            ValidatePropertyExists(f, "Description", "Description is missing");

        /// <summary>
        /// Validates whether the object has a valid Category (Category property).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasCategory)]
        private Task<ConditionResult> ValidateHasCategory(DataFilter<string, TContext> f) =>
            ValidatePropertyExists(f, "Category", "Category is missing");

        /// <summary>
        /// Validates whether the object is verified (IsVerified property is true).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsVerified)]
        private Task<ConditionResult> ValidateIsVerified(DataFilter<string, TContext> f) =>
            ValidateBoolProperty(f, "IsVerified", true, "Object is not verified");

        /// <summary>
        /// Validates whether the object has tags (Tags collection is not empty).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasTags)]
        private Task<ConditionResult> ValidateHasTags(DataFilter<string, TContext> f) =>
            ValidateCollectionNotEmpty(f, "Tags", "Tags are missing");

        /// <summary>
        /// Validates whether the object has permissions (Permissions collection is not empty).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasPermissions)]
        private Task<ConditionResult> ValidateHasPermissions(DataFilter<string, TContext> f) =>
            ValidateCollectionNotEmpty(f, "Permissions", "Permissions are missing");

        /// <summary>
        /// Validates whether the object is present in the user's claims (UserClaims property).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsInUserClaims)]
        private Task<ConditionResult> ValidateIsInUserClaims(DataFilter<string, TContext> f) =>
            ValidatePropertyExists(f, "UserClaims", "Not in user claims");

        /// <summary>
        /// Validates whether the object has a valid ParentId (ParentId property).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasParent)]
        private Task<ConditionResult> ValidateHasParent(DataFilter<string, TContext> f) =>
            ValidatePropertyExists(f, "ParentId", "Missing parent");

        /// <summary>
        /// Validates whether the object has children (Children collection is not empty).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasChildren)]
        private Task<ConditionResult> ValidateHasChildren(DataFilter<string, TContext> f) =>
            ValidateCollectionNotEmpty(f, "Children", "No children found");

        /// <summary>
        /// Validates whether the object has a valid email (Email property).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasValidEmail)]
        private Task<ConditionResult> ValidateHasValidEmail(DataFilter<string, TContext> f) =>
            ValidateEmail(f, "Email", "Invalid email");

        /// <summary>
        /// Validates whether the object has a valid phone number (Phone property).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasPhone)]
        private Task<ConditionResult> ValidateHasPhone(DataFilter<string, TContext> f) =>
            ValidatePropertyExists(f, "Phone", "Phone is missing");

        /// <summary>
        /// Validates whether the object is marked as public (IsPublic property is true).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsPublic)]
        private Task<ConditionResult> ValidateIsPublic(DataFilter<string, TContext> f) =>
            ValidateBoolProperty(f, "IsPublic", true, "Not public");

        /// <summary>
        /// Validates whether the object is marked as private (IsPrivate property is true).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsPrivate)]
        private Task<ConditionResult> ValidateIsPrivate(DataFilter<string, TContext> f) =>
            ValidateBoolProperty(f, "IsPrivate", true, "Not private");

        /// <summary>
        /// Validates whether the object is marked as default (IsDefault property is true).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsDefault)]
        private Task<ConditionResult> ValidateIsDefault(DataFilter<string, TContext> f) =>
            ValidateBoolProperty(f, "IsDefault", true, "Not default");

        /// <summary>
        /// Validates whether the object is required (IsRequired property is true).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsRequired)]
        private Task<ConditionResult> ValidateIsRequired(DataFilter<string, TContext> f) =>
            ValidateBoolProperty(f, "IsRequired", true, "Not required");

        /// <summary>
        /// Validates whether the object is editable (IsEditable property is true).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsEditable)]
        private Task<ConditionResult> ValidateIsEditable(DataFilter<string, TContext> f) =>
            ValidateBoolProperty(f, "IsEditable", true, "Not editable");

        /// <summary>
        /// Validates whether the object is deletable (IsDeletable property is true).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsDeletable)]
        private Task<ConditionResult> ValidateIsDeletable(DataFilter<string, TContext> f) =>
            ValidateBoolProperty(f, "IsDeletable", true, "Not deletable");

        /// <summary>
        /// Validates whether the object has a valid state (State property exists).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsValidState)]
        private Task<ConditionResult> ValidateIsValidState(DataFilter<string, TContext> f) =>
            ValidatePropertyExists(f, "State", "Invalid state");

        /// <summary>
        /// Validates whether the object has a valid status (Status property exists).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasValidStatus)]
        private Task<ConditionResult> ValidateHasValidStatus(DataFilter<string, TContext> f) =>
            ValidatePropertyExists(f, "Status", "Invalid status");

        /// <summary>
        /// Validates whether the object is system-defined (IsSystem property is true).
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <returns>A task representing the validation result.</returns>
        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsSystemDefined)]
        private Task<ConditionResult> ValidateIsSystemDefined(DataFilter<string, TContext> f) =>
            ValidateBoolProperty(f, "IsSystem", true, "Not system defined");
        /// <summary>
        /// Validates whether the specified property exists and has a non-null value.
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <param name="propertyName">The name of the property to validate.</param>
        /// <param name="errorMsg">The error message to return if validation fails.</param>
        /// <returns>A task representing the validation result.</returns>
        protected virtual Task<ConditionResult> ValidatePropertyExists(DataFilter<string, TContext> f, string propertyName, string errorMsg)
        {
            var value = f.Share?.GetType().GetProperty(propertyName)?.GetValue(f.Share);
            return value != null ? ConditionResult.ToSuccessAsync(value) : ConditionResult.ToFailureAsync(null, errorMsg);
        }

        /// <summary>
        /// Validates whether the specified property is a boolean and matches the expected value.
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <param name="propertyName">The name of the boolean property to validate.</param>
        /// <param name="expected">The expected boolean value of the property.</param>
        /// <param name="errorMsg">The error message to return if validation fails.</param>
        /// <returns>A task representing the validation result.</returns>
        protected virtual Task<ConditionResult> ValidateBoolProperty(DataFilter<string, TContext> f, string propertyName, bool expected, string errorMsg)
        {
            var value = f.Share?.GetType().GetProperty(propertyName)?.GetValue(f.Share) as bool?;
            return value == expected ? ConditionResult.ToSuccessAsync(value) : ConditionResult.ToFailureAsync(value, errorMsg);
        }

        /// <summary>
        /// Validates whether the specified property is a collection and contains at least one item.
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <param name="propertyName">The name of the collection property to validate.</param>
        /// <param name="errorMsg">The error message to return if validation fails.</param>
        /// <returns>A task representing the validation result.</returns>
        protected virtual Task<ConditionResult> ValidateCollectionNotEmpty(DataFilter<string, TContext> f, string propertyName, string errorMsg)
        {
            var value = f.Share?.GetType().GetProperty(propertyName)?.GetValue(f.Share) as System.Collections.IEnumerable;
            var hasItems = value?.Cast<object>().Any() ?? false;
            return hasItems ? ConditionResult.ToSuccessAsync(value) : ConditionResult.ToFailureAsync(null, errorMsg);
        }

        /// <summary>
        /// Validates whether the specified property contains a well-formed URI string.
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <param name="propertyName">The name of the URI property to validate.</param>
        /// <param name="errorMsg">The error message to return if validation fails.</param>
        /// <returns>A task representing the validation result.</returns>
        protected virtual Task<ConditionResult> ValidateUri(DataFilter<string, TContext> f, string propertyName, string errorMsg)
        {
            var value = f.Share?.GetType().GetProperty(propertyName)?.GetValue(f.Share)?.ToString();
            bool valid = Uri.IsWellFormedUriString(value, UriKind.Absolute);
            return valid ? ConditionResult.ToSuccessAsync(value) : ConditionResult.ToFailureAsync(value, errorMsg);
        }

        /// <summary>
        /// Validates whether the specified property contains a valid email address.
        /// </summary>
        /// <param name="f">The data filter to be validated.</param>
        /// <param name="propertyName">The name of the email property to validate.</param>
        /// <param name="errorMsg">The error message to return if validation fails.</param>
        /// <returns>A task representing the validation result.</returns>
        protected virtual Task<ConditionResult> ValidateEmail(DataFilter<string, TContext> f, string propertyName, string errorMsg)
        {
            var value = f.Share?.GetType().GetProperty(propertyName)?.GetValue(f.Share)?.ToString();
            bool valid = !string.IsNullOrEmpty(value) && System.Text.RegularExpressions.Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return valid ? ConditionResult.ToSuccessAsync(value) : ConditionResult.ToFailureAsync(value, errorMsg);
        }

        /// <summary>
        /// Initializes the conditions, currently not implemented.
        /// </summary>
        protected override void InitializeConditions()
        {
        }


    }



}