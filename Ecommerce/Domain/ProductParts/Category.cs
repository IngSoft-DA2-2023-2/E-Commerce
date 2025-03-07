﻿using System.Diagnostics.CodeAnalysis;

namespace Domain.ProductParts
{
    [ExcludeFromCodeCoverage]
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public override bool Equals(object? other)
        {
            return Name == ((Category)other).Name;
        }
    }
}