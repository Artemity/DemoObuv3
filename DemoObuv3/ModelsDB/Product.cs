using System;
using System.Collections.Generic;

namespace DemoObuv3.ModelsDB;

public partial class Product
{
    public string Article { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string Unit { get; set; } = null!;

    public int Cost { get; set; }

    public string Supplier { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    public string Category { get; set; } = null!;

    public int SaleNow { get; set; }

    public int Quantity { get; set; }

    public string Description { get; set; } = null!;

    public string? Photo { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public string ImagePath => string.IsNullOrEmpty(Photo) ? "/Images/picture.png" : "/Images/" + Photo;
}
