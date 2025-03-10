using System;
using System.Collections.Generic;

namespace praktika.Entities;

public partial class Report
{
    public int ReportId { get; set; }

    public int? ProjectInReport { get; set; }

    public string ReportName { get; set; } = null!;

    public int? GeneratedBy { get; set; }

    public DateOnly GenerationDate { get; set; }

    public virtual Employee? GeneratedByNavigation { get; set; }

    public virtual Project? ProjectInReportNavigation { get; set; }
}
