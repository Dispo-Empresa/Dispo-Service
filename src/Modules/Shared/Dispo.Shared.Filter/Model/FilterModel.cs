namespace Dispo.Shared.Filter.Model
{
    public class FilterModel
    {
        public required string Entity { get; set; }
        public required List<PropertyModel> Properties { get; set; }
    }
}
