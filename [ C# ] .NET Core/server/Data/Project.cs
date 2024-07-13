using System;
using System.Collections.Generic;

namespace server.Data;

public partial class Project
{
    public Guid IDProject { get; set; }

    public string? ProjectName { get; set; }

    public Guid? IDLeader { get; set; }

    public DateTime? DayCreate { get; set; }

    public string? Image { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();

    public virtual ICollection<Status> Statuses { get; set; } = new List<Status>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
