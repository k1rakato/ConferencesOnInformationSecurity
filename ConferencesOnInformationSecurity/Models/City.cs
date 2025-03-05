using System;
using System.Collections.Generic;

namespace ConferencesOnInformationSecurity.Models;

public partial class City
{
    public int Idcity { get; set; }

    public string Cityname { get; set; } = null!;

    public string Cityimage { get; set; } = null!;

    public virtual ICollection<Meropriatie> Meropriaties { get; set; } = new List<Meropriatie>();
}
