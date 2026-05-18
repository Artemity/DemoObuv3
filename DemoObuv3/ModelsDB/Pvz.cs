using System;
using System.Collections.Generic;

namespace DemoObuv3.ModelsDB;

public partial class Pvz
{
    public int IdPvz { get; set; }

    public int Pvznumber { get; set; }

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public int? House { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
