using System;
using System.Collections.Generic;

namespace server.Data;

public partial class Status
{
    public Guid IDStatus { get; set; }

    public Guid IDProject { get; set; }

    public string? StatusName { get; set; }

    public int? StatusOrder { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Project IDProjectNavigation { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<Workflow> Workflows { get; set; } = new List<Workflow>();
}
