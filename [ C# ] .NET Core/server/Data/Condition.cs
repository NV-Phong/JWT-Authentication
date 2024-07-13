using System;
using System.Collections.Generic;

namespace server.Data;

public partial class Condition
{
    public Guid IDCondition { get; set; }

    public Guid IDPermission { get; set; }

    public Guid IDWorkflow { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Permission IDPermissionNavigation { get; set; } = null!;

    public virtual Workflow IDWorkflowNavigation { get; set; } = null!;
}
