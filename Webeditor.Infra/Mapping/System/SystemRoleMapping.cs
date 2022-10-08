using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webeditor.Domain.Entities.System;

namespace Webeditor.Infra.Mapping.System;

public class SystemRoleMapping : IEntityTypeConfiguration<SystemRole>
{
  public void Configure(EntityTypeBuilder<SystemRole> builder)
  {
    builder.ToTable("SystemRoles");
    builder.HasKey(t => t.Id);

    builder.Property(p => p.Guid).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()").IsRequired();
    builder.Property(p => p.CreatedAt).HasColumnType("timestamp").HasDefaultValueSql("NOW()").IsRequired();
    builder.Property(p => p.UpdatedAt).HasColumnType("timestamp").HasDefaultValueSql("NOW()").IsRequired();
    builder.Property(p => p.RemovedAt).HasColumnType("timestamp");

    builder.Property(p => p.Name).HasColumnType("varchar(30)").IsRequired();
    builder.Property(p => p.Label).HasColumnType("varchar(30)").IsRequired();
    builder.Property(p => p.Active).HasColumnType("smallint").IsRequired();

    builder.HasIndex(e => e.Guid).IsUnique();
    builder.HasIndex(e => e.SystemModuleId);

    builder.HasData(
      new SystemRole(1, new Guid("d68b4e25-a9c4-483b-becf-b4d6430c7714"), "ROLE_GET_SYSTEMUSER", "Pesquisar Usuários", 1),
      new SystemRole(2, new Guid("9da5e1b2-8b11-47de-8145-25b6f6bda019"), "ROLE_PUT_SYSTEMUSER", "Cadastrar Usuários", 1),
      new SystemRole(3, new Guid("838d3a8e-c762-4635-8a32-62338a87a821"), "ROLE_DEL_SYSTEMUSER", "Remover Usuários", 1)
    );
  }
}

