using System;
using System.Collections.Generic;

namespace server.Data;

public partial class TaskDetail
{
    public Guid IDTask { get; set; }

    public string? Description { get; set; }

    public string? Attachments { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Task IDTaskNavigation { get; set; } = null!;
}
