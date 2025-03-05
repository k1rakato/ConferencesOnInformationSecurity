using System;
using System.Collections.Generic;

namespace ConferencesOnInformationSecurity.Models;

public partial class Action
{
    public int Actionid { get; set; }

    public string? Actionname { get; set; }

    public virtual ICollection<Meropriatieandaction> Meropriatieandactions { get; set; } = new List<Meropriatieandaction>();
}
