using System;
using System.Collections.Generic;

namespace server.Data;

public partial class Template
{
    public Guid IDTemplate { get; set; }

    public string? TemplateName { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<ListTemplate> ListTemplates { get; set; } = new List<ListTemplate>();
}
