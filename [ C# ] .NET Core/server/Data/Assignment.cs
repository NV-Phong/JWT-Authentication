using System;
using System.Collections.Generic;

namespace server.Data;

public partial class Assignment
{
    public Guid? IDAssignment { get; set; }

    public Guid IDUser { get; set; }

    public Guid IDTask { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Task IDTaskNavigation { get; set; } = null!;

    public virtual User IDUserNavigation { get; set; } = null!;
}
