using System;
using System.Collections.Generic;

namespace ChudoObuvApp.Models;

public partial class Size
{
    public int Id { get; set; }

    public decimal SizeValue { get; set; }

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
}
