using System;
using System.Linq;
using System.Reflection;
using WasmAI.ConditionChecker.Checker;

namespace WasmAI.ConditionChecker.Validators
{
    public static class BaseConfigValidator
    {
        /// <summary>
        /// Registers all classes that implement ITValidator from the given assembly.
        /// </summary>
        /// <param name="checker">The condition checker that will be passed to each validator.</param>
        /// <param name="assembly">The assembly containing the validator classes.</param>
        public static void Register(IBaseConditionChecker checker, Assembly assembly)
        {
            // Get all classes that implement ITValidator and are not abstract
            var validators = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && typeof(ITValidator).IsAssignableFrom(t))
                .ToList();

            foreach (var validator in validators)
            {
                try
                {
                    // Create an instance of the validator class, passing the checker to the constructor
                    var instance = Activator.CreateInstance(validator, checker) as ITValidator;

                    if (instance != null)
                    {
                        // Call the Register method on each validator to perform its registration
                        instance.Register(checker);
                    }
                    else
                    {
                        // Handle the case where the instance could not be created
                        Console.WriteLine($"Failed to create an instance of {validator.Name}");
                    }
                }
                catch (Exception ex)
                {
                    // Log the error if instantiation fails for any reason
                    Console.WriteLine($"Error creating instance of {validator.Name}: {ex.Message}");
                }
            }
        }
    }
}
