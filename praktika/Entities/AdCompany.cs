using System;
using System.Collections.Generic;

namespace praktika.Entities;

public partial class AdCompany
{
    public int CompanyId { get; set; }

    public int? ProjectInCompany { get; set; }

    public string CompanyName { get; set; } = null!;

    public string Platform { get; set; } = null!;

    public decimal Budget { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual Project? ProjectInCompanyNavigation { get; set; }
}
