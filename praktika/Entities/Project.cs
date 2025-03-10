using System;
using System.Collections.Generic;

namespace praktika.Entities;

public partial class Project
{
    public int ProjectId { get; set; }

    public int? ClientInProject { get; set; }

    public string ProjectName { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<AdCompany> AdCompanies { get; set; } = new List<AdCompany>();

    public virtual Client? ClientInProjectNavigation { get; set; }

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Specification> Specifications { get; set; } = new List<Specification>();

    public virtual ProjectsStatus? StatusNavigation { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
