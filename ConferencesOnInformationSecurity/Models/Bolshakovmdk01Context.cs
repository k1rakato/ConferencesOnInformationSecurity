using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ConferencesOnInformationSecurity.Models;

public partial class Bolshakovmdk01Context : DbContext
{
    public Bolshakovmdk01Context()
    {
    }

    public Bolshakovmdk01Context(DbContextOptions<Bolshakovmdk01Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Action> Actions { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Direction> Directions { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Jury> Juries { get; set; }

    public virtual DbSet<Meropriatie> Meropriaties { get; set; }

    public virtual DbSet<Meropriatieandaction> Meropriatieandactions { get; set; }

    public virtual DbSet<Moder> Moders { get; set; }

    public virtual DbSet<Organizer> Organizers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=bolshakovmdk01;Username=postgres;Password=12345");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Action>(entity =>
        {
            entity.HasKey(e => e.Actionid).HasName("action_pkey");

            entity.ToTable("action");

            entity.Property(e => e.Actionid).HasColumnName("actionid");
            entity.Property(e => e.Actionname).HasColumnName("actionname");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Idcity).HasName("cities_pkey");

            entity.ToTable("cities");

            entity.Property(e => e.Idcity).HasColumnName("idcity");
            entity.Property(e => e.Cityimage).HasColumnName("cityimage");
            entity.Property(e => e.Cityname).HasColumnName("cityname");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Idclient).HasName("client_pkey");

            entity.ToTable("client");

            entity.Property(e => e.Idclient).HasColumnName("idclient");
            entity.Property(e => e.Countryid).HasColumnName("countryid");
            entity.Property(e => e.Dateofbirth)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateofbirth");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.Genderid).HasColumnName("genderid");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .HasColumnName("patronymic");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");

            entity.HasOne(d => d.Country).WithMany(p => p.Clients)
                .HasForeignKey(d => d.Countryid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("client_countryid_fkey");

            entity.HasOne(d => d.Gender).WithMany(p => p.Clients)
                .HasForeignKey(d => d.Genderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("client_genderid_fkey");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Idcountry).HasName("countries_pkey");

            entity.ToTable("countries");

            entity.Property(e => e.Idcountry).HasColumnName("idcountry");
            entity.Property(e => e.Countryname).HasColumnName("countryname");
            entity.Property(e => e.Countrynameeng).HasColumnName("countrynameeng");
            entity.Property(e => e.Firstcode)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("firstcode");
            entity.Property(e => e.Secondcode).HasColumnName("secondcode");
        });

        modelBuilder.Entity<Direction>(entity =>
        {
            entity.HasKey(e => e.Iddirections).HasName("directions_pkey");

            entity.ToTable("directions");

            entity.Property(e => e.Iddirections).HasColumnName("iddirections");
            entity.Property(e => e.Directionsname).HasColumnName("directionsname");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Idevent).HasName("events_pkey");

            entity.ToTable("events");

            entity.Property(e => e.Idevent).HasColumnName("idevent");
            entity.Property(e => e.Eventname).HasColumnName("eventname");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.Idgender).HasName("genders_pkey");

            entity.ToTable("genders");

            entity.Property(e => e.Idgender).HasColumnName("idgender");
            entity.Property(e => e.Gendername).HasColumnName("gendername");
        });

        modelBuilder.Entity<Jury>(entity =>
        {
            entity.HasKey(e => e.Juryid).HasName("jury_pkey");

            entity.ToTable("jury");

            entity.Property(e => e.Juryid).HasColumnName("juryid");
            entity.Property(e => e.Countryid).HasColumnName("countryid");
            entity.Property(e => e.Dateofbirth)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateofbirth");
            entity.Property(e => e.Directionsid).HasColumnName("directionsid");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.Foto).HasColumnName("foto");
            entity.Property(e => e.Genderid).HasColumnName("genderid");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .HasColumnName("patronymic");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");

            entity.HasOne(d => d.Country).WithMany(p => p.Juries)
                .HasForeignKey(d => d.Countryid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("jury_countryid_fkey");

            entity.HasOne(d => d.Directions).WithMany(p => p.Juries)
                .HasForeignKey(d => d.Directionsid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("jury_directionsid_fkey");

            entity.HasOne(d => d.Gender).WithMany(p => p.Juries)
                .HasForeignKey(d => d.Genderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("jury_genderid_fkey");
        });

        modelBuilder.Entity<Meropriatie>(entity =>
        {
            entity.HasKey(e => e.Meropriatieid).HasName("meropriatie_pkey");

            entity.ToTable("meropriatie");

            entity.Property(e => e.Meropriatieid).HasColumnName("meropriatieid");
            entity.Property(e => e.Cityid).HasColumnName("cityid");
            entity.Property(e => e.Datemeropriatie)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datemeropriatie");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Meropriatiename).HasColumnName("meropriatiename");

            entity.HasOne(d => d.City).WithMany(p => p.Meropriaties)
                .HasForeignKey(d => d.Cityid)
                .HasConstraintName("meropriatie_cityid_fkey");

            entity.HasOne(d => d.Event).WithMany(p => p.Meropriaties)
                .HasForeignKey(d => d.Eventid)
                .HasConstraintName("meropriatie_eventid_fkey");
        });

        modelBuilder.Entity<Meropriatieandaction>(entity =>
        {
            entity.HasKey(e => e.Meropriatandactionid).HasName("meropriatieandaction_pkey");

            entity.ToTable("meropriatieandaction");

            entity.Property(e => e.Meropriatandactionid).HasColumnName("meropriatandactionid");
            entity.Property(e => e.Countdays).HasColumnName("countdays");
            entity.Property(e => e.Fifthjury).HasColumnName("fifthjury");
            entity.Property(e => e.Firstjury).HasColumnName("firstjury");
            entity.Property(e => e.Fourthjury).HasColumnName("fourthjury");
            entity.Property(e => e.Idaction).HasColumnName("idaction");
            entity.Property(e => e.Idmeropriatie).HasColumnName("idmeropriatie");
            entity.Property(e => e.Idmoderator).HasColumnName("idmoderator");
            entity.Property(e => e.Numberday).HasColumnName("numberday");
            entity.Property(e => e.Secondjury).HasColumnName("secondjury");
            entity.Property(e => e.Startdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("startdate");
            entity.Property(e => e.Thirdjury).HasColumnName("thirdjury");
            entity.Property(e => e.Timestart).HasColumnName("timestart");
            entity.Property(e => e.Winnerid).HasColumnName("winnerid");

            entity.HasOne(d => d.FifthjuryNavigation).WithMany(p => p.MeropriatieandactionFifthjuryNavigations)
                .HasForeignKey(d => d.Fifthjury)
                .HasConstraintName("meropriatieandaction_fifthjury_fkey");

            entity.HasOne(d => d.FirstjuryNavigation).WithMany(p => p.MeropriatieandactionFirstjuryNavigations)
                .HasForeignKey(d => d.Firstjury)
                .HasConstraintName("meropriatieandaction_firstjury_fkey");

            entity.HasOne(d => d.FourthjuryNavigation).WithMany(p => p.MeropriatieandactionFourthjuryNavigations)
                .HasForeignKey(d => d.Fourthjury)
                .HasConstraintName("meropriatieandaction_fourthjury_fkey");

            entity.HasOne(d => d.IdactionNavigation).WithMany(p => p.Meropriatieandactions)
                .HasForeignKey(d => d.Idaction)
                .HasConstraintName("meropriatieandaction_idaction_fkey");

            entity.HasOne(d => d.IdmeropriatieNavigation).WithMany(p => p.Meropriatieandactions)
                .HasForeignKey(d => d.Idmeropriatie)
                .HasConstraintName("meropriatieandaction_idmeropriatie_fkey");

            entity.HasOne(d => d.IdmoderatorNavigation).WithMany(p => p.Meropriatieandactions)
                .HasForeignKey(d => d.Idmoderator)
                .HasConstraintName("meropriatieandaction_idmoderator_fkey");

            entity.HasOne(d => d.SecondjuryNavigation).WithMany(p => p.MeropriatieandactionSecondjuryNavigations)
                .HasForeignKey(d => d.Secondjury)
                .HasConstraintName("meropriatieandaction_secondjury_fkey");

            entity.HasOne(d => d.ThirdjuryNavigation).WithMany(p => p.MeropriatieandactionThirdjuryNavigations)
                .HasForeignKey(d => d.Thirdjury)
                .HasConstraintName("meropriatieandaction_thirdjury_fkey");

            entity.HasOne(d => d.Winner).WithMany(p => p.Meropriatieandactions)
                .HasForeignKey(d => d.Winnerid)
                .HasConstraintName("meropriatieandaction_winnerid_fkey");
        });

        modelBuilder.Entity<Moder>(entity =>
        {
            entity.HasKey(e => e.Moderid).HasName("moders_pkey");

            entity.ToTable("moders");

            entity.Property(e => e.Moderid).HasColumnName("moderid");
            entity.Property(e => e.Countryid).HasColumnName("countryid");
            entity.Property(e => e.Dateofbirth)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateofbirth");
            entity.Property(e => e.Directionsid).HasColumnName("directionsid");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.Foto).HasColumnName("foto");
            entity.Property(e => e.Genderid).HasColumnName("genderid");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .HasColumnName("patronymic");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");

            entity.HasOne(d => d.Country).WithMany(p => p.Moders)
                .HasForeignKey(d => d.Countryid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("moders_countryid_fkey");

            entity.HasOne(d => d.Directions).WithMany(p => p.Moders)
                .HasForeignKey(d => d.Directionsid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("moders_directionsid_fkey");

            entity.HasOne(d => d.Event).WithMany(p => p.Moders)
                .HasForeignKey(d => d.Eventid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("moders_eventid_fkey");

            entity.HasOne(d => d.Gender).WithMany(p => p.Moders)
                .HasForeignKey(d => d.Genderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("moders_genderid_fkey");
        });

        modelBuilder.Entity<Organizer>(entity =>
        {
            entity.HasKey(e => e.Idorganizer).HasName("organizers_pkey");

            entity.ToTable("organizers");

            entity.Property(e => e.Idorganizer).HasColumnName("idorganizer");
            entity.Property(e => e.Countryid).HasColumnName("countryid");
            entity.Property(e => e.Dateofbirth)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateofbirth");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.Foto).HasColumnName("foto");
            entity.Property(e => e.Genderid).HasColumnName("genderid");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .HasColumnName("patronymic");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");

            entity.HasOne(d => d.Country).WithMany(p => p.Organizers)
                .HasForeignKey(d => d.Countryid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("organizers_countryid_fkey");

            entity.HasOne(d => d.Gender).WithMany(p => p.Organizers)
                .HasForeignKey(d => d.Genderid)
                .HasConstraintName("organizers_genderid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
