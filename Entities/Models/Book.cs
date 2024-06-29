using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class Book
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Author { get; set; }

    public string? Category { get; set; }

    public string? Descriptions { get; set; }

    public string? Rute { get; set; } // ruta imagen

    public string? Img { get; set; } // nombre imagen

    public virtual ICollection<Review> Review { get; set; } = new List<Review>();
}
