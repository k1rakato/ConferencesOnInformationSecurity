using System;
using System.Collections.Generic;

namespace ConferencesOnInformationSecurity.Models;

public partial class Meropriatie
{
    public int Meropriatieid { get; set; }

    public string? Meropriatiename { get; set; }

    public DateTime Datemeropriatie { get; set; }

    public int? Cityid { get; set; }

    public string? Image { get; set; }

    public int? Eventid { get; set; }

    public virtual City? City { get; set; }

    public virtual Event? Event { get; set; }

    public virtual ICollection<Meropriatieandaction> Meropriatieandactions { get; set; } = new List<Meropriatieandaction>();
}
