using System;
using System.Collections.Generic;

namespace UserManagement.Entities;

public partial class SubCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CategoryType { get; set; }

    public virtual Category CategoryTypeNavigation { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
