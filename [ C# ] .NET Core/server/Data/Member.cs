using System;
using System.Collections.Generic;

namespace server.Data;

public partial class Member
{
    public Guid IDMember { get; set; }

    public Guid IDUser { get; set; }

    public Guid IDProject { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Project IDProjectNavigation { get; set; } = null!;

    public virtual User IDUserNavigation { get; set; } = null!;
}
