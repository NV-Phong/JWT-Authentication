using System;
using System.Collections.Generic;

namespace server.Data;

public partial class Task
{
    public Guid IDTask { get; set; }

    public Guid IDProject { get; set; }

    public Guid IDStatus { get; set; }

    public string? TaskName { get; set; }

    public DateTime? DayCreate { get; set; }

    public DateTime? DayStart { get; set; }

    public DateTime? Deadline { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual Project IDProjectNavigation { get; set; } = null!;

    public virtual Status IDStatusNavigation { get; set; } = null!;

    public virtual TaskDetail? TaskDetail { get; set; }
}
