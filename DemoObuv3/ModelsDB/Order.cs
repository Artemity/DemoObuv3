using System;
using System.Collections.Generic;

namespace DemoObuv3.ModelsDB;

public partial class Order
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string Article { get; set; } = null!;

    public int Quantity { get; set; }

    public DateOnly OrderDate { get; set; }

    public DateOnly DeliveryDate { get; set; }

    public int Pvzadress { get; set; }

    public int User { get; set; }

    public int DelieveryCode { get; set; }

    public string OrderStatus { get; set; } = null!;

    public virtual Product ArticleNavigation { get; set; } = null!;

    public virtual Pvz PvzadressNavigation { get; set; } = null!;

    public virtual User UserNavigation { get; set; } = null!;
}
