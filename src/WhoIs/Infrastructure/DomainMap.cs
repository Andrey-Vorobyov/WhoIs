using System;
using Microsoft.EntityFrameworkCore;
using WhoIs.Entities;

namespace WhoIs.Infrastructure
{
    public static class ModelBuilderExtension
    {
        public static ModelBuilder MapDomain(this ModelBuilder modelBuilder)
        {
            var entityBuilder = modelBuilder.Entity<Domain>();

            entityBuilder.HasKey(x => x.Id);
            entityBuilder.ToTable("domains");

            entityBuilder.Property(x => x.Id).HasColumnName("id");
            entityBuilder.Property(x => x.Name).HasColumnName("name");
            entityBuilder.Property(x => x.Status).HasColumnName("status");
            entityBuilder.Property(x => x.NameServer).HasColumnName("nameserver");
            entityBuilder.Property(x => x.DnsSec).HasColumnName("dnssec");
            entityBuilder.Property(x => x.CreationDate).HasColumnName("creationdate");
            entityBuilder.Property(x => x.UpdateDate).HasColumnName("updatedate");

            entityBuilder.Property<Guid>("RegistrantId").HasColumnName("registrant");
            entityBuilder.Property<Guid>("AdminId").HasColumnName("admin");
            entityBuilder.Property<Guid>("TechId").HasColumnName("tech");

            entityBuilder.HasOne(c => c.Registrant).WithOne();
            entityBuilder.HasOne(c=>c.Admin).WithOne();
            entityBuilder.HasOne(c=>c.Tech).WithOne();

            return modelBuilder;
        }

        // Decided to try data annotations style instead
        public static ModelBuilder MapContact(this ModelBuilder modelBuilder)
        {
            var entityBuilder = modelBuilder.Entity<Contact>();

            entityBuilder.HasKey(x => x.Id);
            entityBuilder.ToTable("contacts");

            entityBuilder.Property(x => x.Id).HasColumnName("id");
            entityBuilder.Property(x => x.Name).HasColumnName("name");
            entityBuilder.Property(x => x.Organization).HasColumnName("organization");
            entityBuilder.Property(x => x.Street).HasColumnName("street");
            entityBuilder.Property(x => x.City).HasColumnName("city");
            entityBuilder.Property(x => x.State).HasColumnName("state");
            entityBuilder.Property(x => x.PostalCode).HasColumnName("postalcode");
            entityBuilder.Property(x => x.Country).HasColumnName("country");
            entityBuilder.Property(x => x.Phone).HasColumnName("phone");
            entityBuilder.Property(x => x.PhoneExt).HasColumnName("phoneext");
            entityBuilder.Property(x => x.Fax).HasColumnName("fax");
            entityBuilder.Property(x => x.FaxExt).HasColumnName("faxext");
            entityBuilder.Property(x => x.Email).HasColumnName("email");

            return modelBuilder;
        }
    }
}
