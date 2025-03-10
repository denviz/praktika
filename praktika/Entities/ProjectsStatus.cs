using System;
using System.Collections.Generic;

namespace praktika.Entities;

public partial class ProjectsStatus
{
    public int ProjectStatusId { get; set; }

    public string? StatusName { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
