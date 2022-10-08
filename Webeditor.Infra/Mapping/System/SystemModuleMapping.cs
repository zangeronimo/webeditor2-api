using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webeditor.Domain.Entities.System;

namespace Webeditor.Infra.Mapping.System;

public class SystemModuleMapping : IEntityTypeConfiguration<SystemModule>
{
  public void Configure(EntityTypeBuilder<SystemModule> builder)
  {
    builder.ToTable("SystemModules");
    builder.HasKey(t => t.Id);

    builder.Property(p => p.Guid).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()").IsRequired();
    builder.Property(p => p.CreatedAt).HasColumnType("timestamp").HasDefaultValueSql("NOW()").IsRequired();
    builder.Property(p => p.UpdatedAt).HasColumnType("timestamp").HasDefaultValueSql("NOW()").IsRequired();
    builder.Property(p => p.RemovedAt).HasColumnType("timestamp");

    builder.Property(p => p.Name).HasColumnType("varchar(20)").IsRequired();
    builder.Property(p => p.Active).HasColumnType("smallint").IsRequired();

    builder.HasIndex(e => e.Guid).IsUnique();
    builder.HasMany(e => e.SystemCompanies).WithMany(e => e.SystemModules);

    builder.HasData(
      new SystemModule(1, new Guid("98dbdbcc-a818-4262-a558-9f69b24b1589"), "Sistema")
    );
  }
}
