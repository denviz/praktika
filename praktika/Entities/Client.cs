using System;
using System.Collections.Generic;

namespace praktika.Entities;

public partial class Client
{
    public int ClientId { get; set; }

    public string ClientName { get; set; } = null!;

    public string ClientEmail { get; set; } = null!;

    public string ClientPhone { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
