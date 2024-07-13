using System;
using System.Collections.Generic;

namespace server.Data;

public partial class Permission
{
    public Guid IDPermission { get; set; }

    public Guid IDUser { get; set; }

    public Guid IDProject { get; set; }

    public string? Role { get; set; }

    public string? Object { get; set; }

    public string? Privilege { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Condition> Conditions { get; set; } = new List<Condition>();

    public virtual Project IDProjectNavigation { get; set; } = null!;

    public virtual User IDUserNavigation { get; set; } = null!;
}
