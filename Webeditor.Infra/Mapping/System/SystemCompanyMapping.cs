using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webeditor.Domain.Entities.System;

namespace Webeditor.Infra.Mapping.System;

public class SystemCompanyMapping : IEntityTypeConfiguration<SystemCompany>
{
  public void Configure(EntityTypeBuilder<SystemCompany> builder)
  {
    builder.ToTable("SystemCompanies");
    builder.HasKey(t => t.Id);

    builder.Property(p => p.Guid).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()").IsRequired();
    builder.Property(p => p.CreatedAt).HasColumnType("timestamp").HasDefaultValueSql("NOW()").IsRequired();
    builder.Property(p => p.UpdatedAt).HasColumnType("timestamp").HasDefaultValueSql("NOW()").IsRequired();
    builder.Property(p => p.RemovedAt).HasColumnType("timestamp");

    builder.Property(p => p.Name).HasColumnType("varchar(30)").IsRequired();
    builder.Property(p => p.Active).HasColumnType("smallint").IsRequired();

    builder.HasIndex(e => e.Guid).IsUnique();
    builder.HasMany(e => e.SystemModules).WithMany(e => e.SystemCompanies);
    builder.HasMany(e => e.SystemUsers).WithOne(e => e.SystemCompany);

    builder.HasData(
      new SystemCompany(1, new Guid("ba5911fc-39b6-4ca2-bcdb-d480b753f078"), "Tudo Linux")
    );
  }
}
