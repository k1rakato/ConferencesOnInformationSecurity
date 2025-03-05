using System;
using System.Collections.Generic;

namespace ConferencesOnInformationSecurity.Models;

public partial class Jury
{
    public int Juryid { get; set; }

    public string Lastname { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public int Genderid { get; set; }

    public string Email { get; set; } = null!;

    public DateTime Dateofbirth { get; set; }

    public int Countryid { get; set; }

    public string Phone { get; set; } = null!;

    public int Directionsid { get; set; }

    public string Password { get; set; } = null!;

    public string Foto { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual Direction Directions { get; set; } = null!;

    public virtual Gender Gender { get; set; } = null!;

    public virtual ICollection<Meropriatieandaction> MeropriatieandactionFifthjuryNavigations { get; set; } = new List<Meropriatieandaction>();

    public virtual ICollection<Meropriatieandaction> MeropriatieandactionFirstjuryNavigations { get; set; } = new List<Meropriatieandaction>();

    public virtual ICollection<Meropriatieandaction> MeropriatieandactionFourthjuryNavigations { get; set; } = new List<Meropriatieandaction>();

    public virtual ICollection<Meropriatieandaction> MeropriatieandactionSecondjuryNavigations { get; set; } = new List<Meropriatieandaction>();

    public virtual ICollection<Meropriatieandaction> MeropriatieandactionThirdjuryNavigations { get; set; } = new List<Meropriatieandaction>();
}
