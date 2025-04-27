namespace WasmAI.ConditionChecker.Base
{
    /// <summary>
    /// Base class representing a data filter with various properties.
    /// </summary>
    public class DataFilter
    {
        /// <summary>
        /// Gets or sets the unique identifier for the data filter.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the name associated with the data filter.
        /// </summary>
        public string? Name { get; set; } = null;

        /// <summary>
        /// Gets or sets the share of data associated with the filter.
        /// </summary>
        public object? Share { get; set; }

        /// <summary>
        /// Gets or sets the value of the data filter.
        /// </summary>
        public object? Value { get; set; }

        /// <summary>
        /// Gets or sets a collection of items associated with the filter.
        /// </summary>
        public IDictionary<string, object>? Items { get; set; }

        /// <summary>
        /// Default constructor for initializing a new instance of the DataFilter class.
        /// </summary>
        public DataFilter() { }

        /// <summary>
        /// Constructor for initializing a new instance of the DataFilter class with an ID.
        /// </summary>
        /// <param name="id">The ID of the data filter.</param>
        public DataFilter(string? id)
        {
            Id = id;
        }

        /// <summary>
        /// Copy constructor for creating a new instance of DataFilter from another DataFilter instance.
        /// </summary>
        /// <param name="other">The other DataFilter to copy from.</param>
        public DataFilter(DataFilter other)
        {
            Id = other.Id;
            Name = other.Name;
            Share = other.Share;
            Value = other.Value;
            Items = other.Items;
        }

        /// <summary>
        /// Implicit operator to convert a string ID to a DataFilter instance.
        /// </summary>
        /// <param name="id">The ID of the data filter.</param>
        public static implicit operator DataFilter(string? id)
        {
            return new DataFilter(id);
        }
    }

    /// <summary>
    /// Generic class extending DataFilter to support a typed value.
    /// </summary>
    /// <typeparam name="T">The type of the Value property.</typeparam>
    public class DataFilter<T> : DataFilter
    {
        /// <summary>
        /// Gets or sets the value of the data filter, typed to T.
        /// </summary>
        public new T? Value { get; set; }

        /// <summary>
        /// Default constructor for initializing a new instance of the DataFilter<T> class.
        /// </summary>
        public DataFilter() { }

        /// <summary>
        /// Copy constructor for creating a new instance of DataFilter<T> from another DataFilter instance.
        /// </summary>
        /// <param name="other">The other DataFilter to copy from.</param>
        public DataFilter(DataFilter other) : base(other)
        {
            if (other.Value is T typedValue)
            {
                Value = typedValue;
            }
        }
    }

    /// <summary>
    /// Generic class extending DataFilter<T> to support an additional typed Share property.
    /// </summary>
    /// <typeparam name="T">The type of the Value property.</typeparam>
    /// <typeparam name="E">The type of the Share property.</typeparam>
    public class DataFilter<T, E> : DataFilter<T>
    {
        /// <summary>
        /// Gets or sets the share of data associated with the filter, typed to E.
        /// </summary>
        public new E? Share { get; set; }

        /// <summary>
        /// Default constructor for initializing a new instance of the DataFilter<T, E> class.
        /// </summary>
        public DataFilter() { }

        /// <summary>
        /// Copy constructor for creating a new instance of DataFilter<T, E> from another DataFilter instance.
        /// </summary>
        /// <param name="other">The other DataFilter to copy from.</param>
        public DataFilter(DataFilter other) : base(other)
        {
            if (other.Share is E typedShare)
            {
                Share = typedShare;
            }
        }

        /// <summary>
        /// Copy constructor for creating a new instance of DataFilter<T, E> from another DataFilter<T> instance.
        /// </summary>
        /// <param name="other">The other DataFilter<T> to copy from.</param>
        public DataFilter(DataFilter<T> other) : base(other)
        {
            if (other.Share is E typedShare)
            {
                Share = typedShare;
            }
        }
    }
}
