using System;
using System.Collections.Generic;

namespace ChudoObuvApp.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    public string? Description { get; set; }

    public string? Composition { get; set; }

    public string? ImageFile { get; set; }

    public decimal BasePrice { get; set; }

    public int? CategoryId { get; set; }

    public int? SubcategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();

    public virtual Subcategory? Subcategory { get; set; }
}
