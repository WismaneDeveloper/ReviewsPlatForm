using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class Review
{
    public int Id { get; set; }

    public int? IdBook { get; set; }

    public int? IdUser { get; set; }

    public string? Descriptions { get; set; }

    public virtual Book? IdBookNavigation { get; set; }

    public virtual Users? IdUserNavigation { get; set; }
}
