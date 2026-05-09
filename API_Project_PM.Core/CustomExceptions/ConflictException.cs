namespace API_Project_PM.Core.CustomException
{
    public class ConflictException : Exception
    {
        public string Field { get; }
        public object Value { get; }

        public ConflictException(string message)
            : base(message)
        {
            Field = string.Empty;
            Value = string.Empty;
        }

        public ConflictException(string field, object value)
            : base($"A record with {field} '{value}' already exists.")
        {
            Field = field;
            Value = value;
        }
    }
}
