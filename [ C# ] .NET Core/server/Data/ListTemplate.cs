using System;
using System.Collections.Generic;

namespace server.Data;

public partial class ListTemplate
{
    public Guid IDListTemplate { get; set; }

    public Guid IDTemplate { get; set; }

    public string? StatusName { get; set; }

    public int? StatusOrder { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Template IDTemplateNavigation { get; set; } = null!;
}
