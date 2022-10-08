using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webeditor.Domain.Entities.System;

namespace Webeditor.Infra.Mapping.System;

public class SystemUserMapping : IEntityTypeConfiguration<SystemUser>
{
  public void Configure(EntityTypeBuilder<SystemUser> builder)
  {
    builder.ToTable("SystemUsers");
    builder.HasKey(t => t.Id);

    builder.Property(p => p.Guid).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()").IsRequired();
    builder.Property(p => p.CreatedAt).HasColumnType("timestamp").HasDefaultValueSql("NOW()").IsRequired();
    builder.Property(p => p.UpdatedAt).HasColumnType("timestamp").HasDefaultValueSql("NOW()").IsRequired();
    builder.Property(p => p.RemovedAt).HasColumnType("timestamp");

    builder.Property(p => p.Name).HasColumnType("varchar(200)").IsRequired();
    builder.Property(p => p.Email).HasColumnType("varchar(200)").IsRequired();
    builder.Property(p => p.Password).HasColumnType("varchar(100)").IsRequired();
    builder.Property(p => p.Avatar).HasColumnType("varchar(255)");
    builder.Property(p => p.Active).HasColumnType("smallint").IsRequired();

    builder.HasIndex(e => e.Guid).IsUnique();
    builder.HasIndex(e => e.SystemCompanyId);
    builder.HasMany<SystemRole>(e => e.SystemRoles).WithMany(e => e.SystemUsers);
    builder.HasOne(e => e.SystemCompany).WithMany(e => e.SystemUsers).HasForeignKey(e => e.SystemCompanyId);

    builder.HasData(
      new SystemUser(1, new Guid("41ddc041-fd45-4d62-96e7-4d7b9c0dc4a8"), "Luciano Zangeronimo", "zangeronimo@gmail.com", "$2a$11$LUraF.DU.IcRA3S1B980feDxdUTNK9NSVvct.jwpP67dlK2Ibk0dC", 1)
    );
  }
}
