using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webeditor.Domain.Entities.Recipes;

namespace Webeditor.Infra.Mapping.Recipes;

public class RecipeMapping : IEntityTypeConfiguration<Recipe>
{
  public void Configure(EntityTypeBuilder<Recipe> builder)
  {
    builder.ToTable("Recipes");
    builder.HasKey(t => t.Id);

    builder.Property(p => p.Guid).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()").IsRequired();
    builder.Property(p => p.CreatedAt).HasColumnType("timestamp").HasDefaultValueSql("NOW()").IsRequired();
    builder.Property(p => p.UpdatedAt).HasColumnType("timestamp").HasDefaultValueSql("NOW()").IsRequired();
    builder.Property(p => p.RemovedAt).HasColumnType("timestamp");

    builder.Property(p => p.Name).HasColumnType("varchar(80)").IsRequired();
    builder.Property(p => p.Slug).HasColumnType("varchar(80)").IsRequired();
    builder.Property(p => p.Active).HasColumnType("smallint").IsRequired();
    builder.Property(p => p.Ingredients).HasColumnType("varchar").IsRequired();
    builder.Property(p => p.Preparation).HasColumnType("varchar").IsRequired();

    builder.HasIndex(e => e.Guid).IsUnique();
    builder.HasIndex(e => e.SystemCompanyId);
    builder.HasIndex(e => e.RecipeCategoryId);

    builder.HasOne(e => e.RecipeCategory).WithMany().HasForeignKey(e => e.RecipeCategoryId);
    builder.HasMany(e => e.RecipeImages).WithOne();
    builder.HasMany(e => e.RecipeRates).WithOne();
  }
}
