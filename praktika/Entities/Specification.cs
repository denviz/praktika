using System;
using System.Collections.Generic;

namespace praktika.Entities;

public partial class Specification
{
    public int SpecId { get; set; }

    public int? ProjectInSpecification { get; set; }

    public string SpecName { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public virtual Employee? CreatedByNavigation { get; set; }

    public virtual Project? ProjectInSpecificationNavigation { get; set; }
}
