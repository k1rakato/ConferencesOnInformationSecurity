using System;
using System.Collections.Generic;

namespace ConferencesOnInformationSecurity.Models;

public partial class Gender
{
    public int Idgender { get; set; }

    public string Gendername { get; set; } = null!;

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Jury> Juries { get; set; } = new List<Jury>();

    public virtual ICollection<Moder> Moders { get; set; } = new List<Moder>();

    public virtual ICollection<Organizer> Organizers { get; set; } = new List<Organizer>();
}
