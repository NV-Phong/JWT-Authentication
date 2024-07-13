using System;
using System.Collections.Generic;

namespace server.Data;

public partial class User
{
    public Guid IDUser { get; set; }

    public string? UserName { get; set; }

    public string? Role { get; set; }

    public string? Email { get; set; }

    public string? DisplayName { get; set; }

    public string? Password { get; set; }

    public string? Avatar { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
