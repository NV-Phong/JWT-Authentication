using System;
using System.Collections.Generic;

namespace server.Data;

public partial class Workflow
{
    public Guid IDWorkflow { get; set; }

    public Guid IDStatus { get; set; }

    public string? Transition { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Condition> Conditions { get; set; } = new List<Condition>();

    public virtual Status IDStatusNavigation { get; set; } = null!;
}
