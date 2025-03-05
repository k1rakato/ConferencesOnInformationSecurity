using System;
using System.Collections.Generic;

namespace ConferencesOnInformationSecurity.Models;

public partial class Event
{
    public int Idevent { get; set; }

    public string Eventname { get; set; } = null!;

    public virtual ICollection<Meropriatie> Meropriaties { get; set; } = new List<Meropriatie>();

    public virtual ICollection<Moder> Moders { get; set; } = new List<Moder>();
}
