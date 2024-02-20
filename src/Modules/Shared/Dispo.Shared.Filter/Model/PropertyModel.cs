﻿namespace Dispo.Shared.Filter.Model
{
    public class PropertyModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public SearchType SearchType { get; set; }
    }

    public enum SearchType
    {
        Equals,
        Contains,
        StartsWith,
        EndsWith,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual
    }
}
