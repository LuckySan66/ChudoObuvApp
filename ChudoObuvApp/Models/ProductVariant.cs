using System;
using System.Collections.Generic;

namespace ChudoObuvApp.Models;

public partial class ProductVariant
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public int? SizeId { get; set; }

    public int StockQuantity { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Product? Product { get; set; }

    public virtual Size? Size { get; set; }
}
