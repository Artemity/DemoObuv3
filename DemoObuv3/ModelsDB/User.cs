using System;
using System.Collections.Generic;

namespace DemoObuv3.ModelsDB;

public partial class User
{
    public int UserId { get; set; }

    public string Role { get; set; } = null!;

    public int RoleId { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role RoleNavigation { get; set; } = null!;
}
