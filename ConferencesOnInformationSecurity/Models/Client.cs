using System;
using System.Collections.Generic;

namespace ConferencesOnInformationSecurity.Models;

public partial class Client
{
    public int Idclient { get; set; }

    public string Lastname { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime Dateofbirth { get; set; }

    public int Countryid { get; set; }

    public string Phone { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Image { get; set; } = null!;

    public int Genderid { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual Gender Gender { get; set; } = null!;

    public virtual ICollection<Meropriatieandaction> Meropriatieandactions { get; set; } = new List<Meropriatieandaction>();
}
