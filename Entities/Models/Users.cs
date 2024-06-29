using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class Users
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? Pass { get; set; }

    public string? Rute { get; set; }

    public string? Img { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Review> Review { get; set; } = new List<Review>();
}
