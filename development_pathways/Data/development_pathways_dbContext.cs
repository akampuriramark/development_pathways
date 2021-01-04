using System;
using development_pathways.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace development_pathways.Data
{
    public partial class development_pathways_dbContext : DbContext
    {
        public development_pathways_dbContext()
        {
        }

        public development_pathways_dbContext(DbContextOptions<development_pathways_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<County> Counties { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<SubCounty> SubCounties { get; set; }
        public virtual DbSet<SubLocation> SubLocations { get; set; }
        public virtual DbSet<TelephoneContact> TelephoneContacts { get; set; }
        public virtual DbSet<Village> Villages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasIndex(e => e.County, "Applications_index_0");

                entity.HasIndex(e => e.SubCounty, "Applications_index_1");

                entity.HasIndex(e => e.Location, "Applications_index_2");

                entity.HasIndex(e => e.FullName, "Applications_index_3");

                entity.HasIndex(e => e.SubLocation, "Applications_index_4");

                entity.HasIndex(e => new { e.County, e.SubCounty, e.Location, e.FullName, e.SubLocation }, "Applications_index_5");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.AnyOther).HasMaxLength(255);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.DateOfCollection).HasColumnType("date");

                entity.Property(e => e.DesignationOfCollector).HasMaxLength(255);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.IdNumber)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.InfoCollectedBy)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.MaritalStatus).HasMaxLength(255);

                entity.Property(e => e.OrphanAndVulnerableChildren).HasMaxLength(255);

                entity.Property(e => e.PersonsInExtremePoverty).HasMaxLength(255);

                entity.Property(e => e.PersonsWithDisability).HasMaxLength(255);

                entity.Property(e => e.PhysicalAddress).HasMaxLength(255);

                entity.Property(e => e.PoorElderlyPersons).HasMaxLength(255);

                entity.Property(e => e.PostalAddress).HasMaxLength(255);

                entity.HasOne(d => d.CountyNavigation)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.County)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Applicati__Count__09A971A2");

                entity.HasOne(d => d.LocationNavigation)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.Location)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Applicati__Locat__0B91BA14");

                entity.HasOne(d => d.SubCountyNavigation)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.SubCounty)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Applicati__SubCo__0A9D95DB");

                entity.HasOne(d => d.SubLocationNavigation)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.SubLocation)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Applicati__SubLo__0C85DE4D");

                entity.HasOne(d => d.VillageNavigation)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.Village)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Applicati__Villa__0D7A0286");
            });

            modelBuilder.Entity<County>(entity =>
            {
                entity.HasIndex(e => e.CountyCode, "Counties_index_6");

                entity.HasIndex(e => e.CountyName, "Counties_index_7");

                entity.HasIndex(e => e.CountyCode, "UQ__Counties__34DC46ADAD734D1B")
                    .IsUnique();

                entity.Property(e => e.CountyId).HasColumnName("CountyID");

                entity.Property(e => e.CountyCode)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CountyName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasIndex(e => e.LocationCode, "Locations_index_10");

                entity.HasIndex(e => e.LocationName, "Locations_index_11");

                entity.HasIndex(e => e.LocationCode, "UQ__Location__DDB144D58FF55A64")
                    .IsUnique();

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LocationCode)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.SubCountyId).HasColumnName("SubCountyID");

                entity.HasOne(d => d.SubCounty)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.SubCountyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Locations__SubCo__0F624AF8");
            });

            modelBuilder.Entity<SubCounty>(entity =>
            {
                entity.HasIndex(e => e.SubCountyCode, "SubCounties_index_8");

                entity.HasIndex(e => e.SubCountyName, "SubCounties_index_9");

                entity.HasIndex(e => e.SubCountyCode, "UQ__SubCount__DDDA1049691C1271")
                    .IsUnique();

                entity.Property(e => e.SubCountyId).HasColumnName("SubCountyID");

                entity.Property(e => e.CountyId).HasColumnName("CountyID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SubCountyCode)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.SubCountyName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.County)
                    .WithMany(p => p.SubCounties)
                    .HasForeignKey(d => d.CountyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__SubCounti__Count__0E6E26BF");
            });

            modelBuilder.Entity<SubLocation>(entity =>
            {
                entity.HasIndex(e => e.SubLocationCode, "SubLocations_index_12");

                entity.HasIndex(e => e.SubLocationName, "SubLocations_index_13");

                entity.HasIndex(e => e.SubLocationCode, "UQ__SubLocat__9AF90E7899268B20")
                    .IsUnique();

                entity.Property(e => e.SubLocationId).HasColumnName("SubLocationID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.SubLocationCode)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.SubLocationName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.SubLocations)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__SubLocati__Locat__10566F31");
            });

            modelBuilder.Entity<TelephoneContact>(entity =>
            {
                entity.HasIndex(e => e.TelephoneContact1, "TelephoneContacts_index_16");

                entity.HasIndex(e => e.ApplicationId, "TelephoneContacts_index_17");

                entity.HasIndex(e => e.TelephoneContact1, "UQ__Telephon__C9D3EEEDD13E708C")
                    .IsUnique();

                entity.Property(e => e.TelephoneContactId).HasColumnName("TelephoneContactID");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TelephoneContact1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("TelephoneContact");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.TelephoneContacts)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Telephone__Appli__123EB7A3");
            });

            modelBuilder.Entity<Village>(entity =>
            {
                entity.HasIndex(e => e.VillageCode, "UQ__Villages__66342B719B1EA90D")
                    .IsUnique();

                entity.HasIndex(e => e.VillageCode, "Villages_index_14");

                entity.HasIndex(e => e.VillageName, "Villages_index_15");

                entity.Property(e => e.VillageId).HasColumnName("VillageID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SubLocationId).HasColumnName("SubLocationID");

                entity.Property(e => e.VillageCode)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.VillageName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.SubLocation)
                    .WithMany(p => p.Villages)
                    .HasForeignKey(d => d.SubLocationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Villages__SubLoc__114A936A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
