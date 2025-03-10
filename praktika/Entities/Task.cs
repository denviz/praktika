using System;
using System.Collections.Generic;

namespace praktika.Entities;

public partial class Task
{
    public int TaskId { get; set; }

    public int? ProjectInTask { get; set; }

    public string TaskName { get; set; } = null!;

    public string? Description { get; set; }

    public int? AssignedTo { get; set; }

    public DateOnly Deadline { get; set; }

    public virtual Employee? AssignedToNavigation { get; set; }

    public virtual Project? ProjectInTaskNavigation { get; set; }
}
