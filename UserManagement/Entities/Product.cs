using System;
using System.Collections.Generic;

namespace UserManagement.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int SubCategory { get; set; }

    public virtual SubCategory SubCategoryNavigation { get; set; } = null!;
}
