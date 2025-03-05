using System;
using System.Collections.Generic;

namespace ConferencesOnInformationSecurity.Models;

public partial class Meropriatieandaction
{
    public int Meropriatandactionid { get; set; }

    public int? Idmeropriatie { get; set; }

    public DateTime Startdate { get; set; }

    public int? Countdays { get; set; }

    public int? Idaction { get; set; }

    public int? Numberday { get; set; }

    public TimeOnly Timestart { get; set; }

    public int? Idmoderator { get; set; }

    public int? Firstjury { get; set; }

    public int? Secondjury { get; set; }

    public int? Thirdjury { get; set; }

    public int? Fourthjury { get; set; }

    public int? Fifthjury { get; set; }

    public int? Winnerid { get; set; }

    public virtual Jury? FifthjuryNavigation { get; set; }

    public virtual Jury? FirstjuryNavigation { get; set; }

    public virtual Jury? FourthjuryNavigation { get; set; }

    public virtual Action? IdactionNavigation { get; set; }

    public virtual Meropriatie? IdmeropriatieNavigation { get; set; }

    public virtual Moder? IdmoderatorNavigation { get; set; }

    public virtual Jury? SecondjuryNavigation { get; set; }

    public virtual Jury? ThirdjuryNavigation { get; set; }

    public virtual Client? Winner { get; set; }
}
