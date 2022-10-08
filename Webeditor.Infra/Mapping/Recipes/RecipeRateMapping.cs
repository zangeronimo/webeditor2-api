using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webeditor.Domain.Entities.Recipes;

namespace Webeditor.Infra.Mapping.Recipes;

public class RecipeRateMapping : IEntityTypeConfiguration<RecipeRate>
{
  public void Configure(EntityTypeBuilder<RecipeRate> builder)
  {
    builder.ToTable("RecipeRates");
    builder.HasKey(t => t.Id);

    builder.Property(p => p.Guid).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()").IsRequired();
    builder.Property(p => p.CreatedAt).HasColumnType("timestamp").HasDefaultValueSql("NOW()").IsRequired();
    builder.Property(p => p.UpdatedAt).HasColumnType("timestamp").HasDefaultValueSql("NOW()").IsRequired();
    builder.Property(p => p.RemovedAt).HasColumnType("timestamp");

    builder.Property(p => p.Rate).HasColumnType("smallint").IsRequired();
    builder.Property(p => p.Comment).HasColumnType("varchar(100)");
    builder.Property(p => p.Active).HasColumnType("smallint").IsRequired();

    builder.HasIndex(e => e.Guid).IsUnique();
    builder.HasIndex(e => e.SystemCompanyId);
    builder.HasIndex(e => e.RecipeId);
  }
}
