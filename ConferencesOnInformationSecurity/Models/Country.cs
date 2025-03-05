using System;
using System.Collections.Generic;

namespace ConferencesOnInformationSecurity.Models;

public partial class Country
{
    public int Idcountry { get; set; }

    public string Countryname { get; set; } = null!;

    public string Countrynameeng { get; set; } = null!;

    public string Firstcode { get; set; } = null!;

    public int Secondcode { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Jury> Juries { get; set; } = new List<Jury>();

    public virtual ICollection<Moder> Moders { get; set; } = new List<Moder>();

    public virtual ICollection<Organizer> Organizers { get; set; } = new List<Organizer>();
}
