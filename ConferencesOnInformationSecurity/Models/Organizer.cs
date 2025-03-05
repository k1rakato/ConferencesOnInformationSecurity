using System;
using System.Collections.Generic;

namespace ConferencesOnInformationSecurity.Models;

public partial class Organizer
{
    public int Idorganizer { get; set; }

    public string Lastname { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime Dateofbirth { get; set; }

    public int Countryid { get; set; }

    public string Phone { get; set; } = null!;

    public string? Password { get; set; }

    public string Foto { get; set; } = null!;

    public int? Genderid { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual Gender? Gender { get; set; }
}
