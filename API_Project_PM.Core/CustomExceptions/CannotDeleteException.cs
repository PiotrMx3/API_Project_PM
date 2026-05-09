namespace API_Project_PM.Core.CustomExceptions
{
    public class CannotDeleteException : Exception
    {
        public string ResourceName { get; }
        public IReadOnlyList<string> RelatedEntities { get; }

        public CannotDeleteException(string resourceName, IEnumerable<string> relatedEntities)
            : base($"Cannot delete '{resourceName}' because it is referenced by: "
                  + string.Join(", ", relatedEntities) + ".")
        {
            ResourceName = resourceName;
            RelatedEntities = relatedEntities.ToList().AsReadOnly();
        }

        public CannotDeleteException(string resourceName, params string[] relatedEntities)
            : this(resourceName, (IEnumerable<string>)relatedEntities) { }
    }
}
