using System;
using System.Collections.Generic;

namespace ConferencesOnInformationSecurity.Models;

public partial class Direction
{
    public int Iddirections { get; set; }

    public string Directionsname { get; set; } = null!;

    public virtual ICollection<Jury> Juries { get; set; } = new List<Jury>();

    public virtual ICollection<Moder> Moders { get; set; } = new List<Moder>();
}
