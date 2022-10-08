﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Webeditor.Infra.Context;

#nullable disable

namespace Webeditor.Infra.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RecipeRecipeTag", b =>
                {
                    b.Property<long>("RecipeTagsId")
                        .HasColumnType("bigint");

                    b.Property<long>("RecipesId")
                        .HasColumnType("bigint");

                    b.HasKey("RecipeTagsId", "RecipesId");

                    b.HasIndex("RecipesId");

                    b.ToTable("RecipeRecipeTag");
                });

            modelBuilder.Entity("SystemCompanySystemModule", b =>
                {
                    b.Property<long>("SystemCompaniesId")
                        .HasColumnType("bigint");

                    b.Property<long>("SystemModulesId")
                        .HasColumnType("bigint");

                    b.HasKey("SystemCompaniesId", "SystemModulesId");

                    b.HasIndex("SystemModulesId");

                    b.ToTable("SystemCompanySystemModule");
                });

            modelBuilder.Entity("SystemRoleSystemUser", b =>
                {
                    b.Property<long>("SystemRolesId")
                        .HasColumnType("bigint");

                    b.Property<long>("SystemUsersId")
                        .HasColumnType("bigint");

                    b.HasKey("SystemRolesId", "SystemUsersId");

                    b.HasIndex("SystemUsersId");

                    b.ToTable("SystemRoleSystemUser");
                });

            modelBuilder.Entity("SystemUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<short>("Active")
                        .HasColumnType("smallint");

                    b.Property<string>("Avatar")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("RemovedAt")
                        .HasColumnType("timestamp");

                    b.Property<long>("SystemCompanyId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NOW()");

                    b.HasKey("Id");

                    b.HasIndex("Guid")
                        .IsUnique();

                    b.HasIndex("SystemCompanyId");

                    b.ToTable("SystemUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Active = (short)0,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "zangeronimo@gmail.com",
                            Guid = new Guid("41ddc041-fd45-4d62-96e7-4d7b9c0dc4a8"),
                            Name = "Luciano Zangeronimo",
                            Password = "$2a$11$LUraF.DU.IcRA3S1B980feDxdUTNK9NSVvct.jwpP67dlK2Ibk0dC",
                            SystemCompanyId = 1L,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Webeditor.Domain.Entities.Recipes.Recipe", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<short>("Active")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NOW()");

                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("Ingredients")
                        .IsRequired()
                        .HasColumnType("varchar");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Preparation")
                        .IsRequired()
                        .HasColumnType("varchar");

                    b.Property<long>("RecipeCategoryId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("RemovedAt")
                        .HasColumnType("timestamp");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<long>("SystemCompanyId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NOW()");

                    b.HasKey("Id");

                    b.HasIndex("Guid")
                        .IsUnique();

                    b.HasIndex("RecipeCategoryId");

                    b.HasIndex("SystemCompanyId");

                    b.ToTable("Recipes", (string)null);
                });

            modelBuilder.Entity("Webeditor.Domain.Entities.Recipes.RecipeCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<short>("Active")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NOW()");

                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(45)");

                    b.Property<DateTime?>("RemovedAt")
                        .HasColumnType("timestamp");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("varchar(45)");

                    b.Property<long>("SystemCompanyId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NOW()");

                    b.HasKey("Id");

                    b.HasIndex("Guid")
                        .IsUnique();

                    b.HasIndex("SystemCompanyId");

                    b.ToTable("RecipeCategories", (string)null);
                });

            modelBuilder.Entity("Webeditor.Domain.Entities.Recipes.RecipeImage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<short>("Active")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NOW()");

                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("Path")
                        .HasColumnType("varchar(100)");

                    b.Property<long>("RecipeId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("RemovedAt")
                        .HasColumnType("timestamp");

                    b.Property<long>("SystemCompanyId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NOW()");

                    b.HasKey("Id");

                    b.HasIndex("Guid")
                        .IsUnique();

                    b.HasIndex("RecipeId");

                    b.HasIndex("SystemCompanyId");

                    b.ToTable("RecipeImages", (string)null);
                });

            modelBuilder.Entity("Webeditor.Domain.Entities.Recipes.RecipeRate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<short>("Active")
                        .HasColumnType("smallint");

                    b.Property<string>("Comment")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NOW()");

                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<short>("Rate")
                        .HasColumnType("smallint");

                    b.Property<long>("RecipeId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("RemovedAt")
                        .HasColumnType("timestamp");

                    b.Property<long>("SystemCompanyId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NOW()");

                    b.HasKey("Id");

                    b.HasIndex("Guid")
                        .IsUnique();

                    b.HasIndex("RecipeId");

                    b.HasIndex("SystemCompanyId");

                    b.ToTable("RecipeRates", (string)null);
                });

            modelBuilder.Entity("Webeditor.Domain.Entities.Recipes.RecipeTag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<short>("Active")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NOW()");

                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(45)");

                    b.Property<long>("RecipeCategoryId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("RemovedAt")
                        .HasColumnType("timestamp");

                    b.Property<long>("SystemCompanyId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NOW()");

                    b.HasKey("Id");

                    b.HasIndex("Guid")
                        .IsUnique();

                    b.HasIndex("RecipeCategoryId");

                    b.HasIndex("SystemCompanyId");

                    b.ToTable("RecipeTags", (string)null);
                });

            modelBuilder.Entity("Webeditor.Domain.Entities.System.SystemCompany", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<short>("Active")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NOW()");

                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("RemovedAt")
                        .HasColumnType("timestamp");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NOW()");

                    b.HasKey("Id");

                    b.HasIndex("Guid")
                        .IsUnique();

                    b.ToTable("SystemCompanies", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Active = (short)0,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Guid = new Guid("ba5911fc-39b6-4ca2-bcdb-d480b753f078"),
                            Name = "Tudo Linux",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Webeditor.Domain.Entities.System.SystemModule", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<short>("Active")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NOW()");

                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime?>("RemovedAt")
                        .HasColumnType("timestamp");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NOW()");

                    b.HasKey("Id");

                    b.HasIndex("Guid")
                        .IsUnique();

                    b.ToTable("SystemModules", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Active = (short)0,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Guid = new Guid("98dbdbcc-a818-4262-a558-9f69b24b1589"),
                            Name = "Sistema",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Webeditor.Domain.Entities.System.SystemRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<short>("Active")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NOW()");

                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("RemovedAt")
                        .HasColumnType("timestamp");

                    b.Property<long>("SystemModuleId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("NOW()");

                    b.HasKey("Id");

                    b.HasIndex("Guid")
                        .IsUnique();

                    b.HasIndex("SystemModuleId");

                    b.ToTable("SystemRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Active = (short)0,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Guid = new Guid("d68b4e25-a9c4-483b-becf-b4d6430c7714"),
                            Label = "Pesquisar Usuários",
                            Name = "ROLE_GET_SYSTEMUSER",
                            SystemModuleId = 1L,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2L,
                            Active = (short)0,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Guid = new Guid("9da5e1b2-8b11-47de-8145-25b6f6bda019"),
                            Label = "Cadastrar Usuários",
                            Name = "ROLE_PUT_SYSTEMUSER",
                            SystemModuleId = 1L,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3L,
                            Active = (short)0,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Guid = new Guid("838d3a8e-c762-4635-8a32-62338a87a821"),
                            Label = "Remover Usuários",
                            Name = "ROLE_DEL_SYSTEMUSER",
                            SystemModuleId = 1L,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("RecipeRecipeTag", b =>
                {
                    b.HasOne("Webeditor.Domain.Entities.Recipes.RecipeTag", null)
                        .WithMany()
                        .HasForeignKey("RecipeTagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Webeditor.Domain.Entities.Recipes.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SystemCompanySystemModule", b =>
                {
                    b.HasOne("Webeditor.Domain.Entities.System.SystemCompany", null)
                        .WithMany()
                        .HasForeignKey("SystemCompaniesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Webeditor.Domain.Entities.System.SystemModule", null)
                        .WithMany()
                        .HasForeignKey("SystemModulesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SystemRoleSystemUser", b =>
                {
                    b.HasOne("Webeditor.Domain.Entities.System.SystemRole", null)
                        .WithMany()
                        .HasForeignKey("SystemRolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SystemUser", null)
                        .WithMany()
                        .HasForeignKey("SystemUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SystemUser", b =>
                {
                    b.HasOne("Webeditor.Domain.Entities.System.SystemCompany", "SystemCompany")
                        .WithMany("SystemUsers")
                        .HasForeignKey("SystemCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SystemCompany");
                });

            modelBuilder.Entity("Webeditor.Domain.Entities.Recipes.Recipe", b =>
                {
                    b.HasOne("Webeditor.Domain.Entities.Recipes.RecipeCategory", "RecipeCategory")
                        .WithMany()
                        .HasForeignKey("RecipeCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RecipeCategory");
                });

            modelBuilder.Entity("Webeditor.Domain.Entities.Recipes.RecipeImage", b =>
                {
                    b.HasOne("Webeditor.Domain.Entities.Recipes.Recipe", null)
                        .WithMany("RecipeImages")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Webeditor.Domain.Entities.Recipes.RecipeRate", b =>
                {
                    b.HasOne("Webeditor.Domain.Entities.Recipes.Recipe", null)
                        .WithMany("RecipeRates")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Webeditor.Domain.Entities.Recipes.RecipeTag", b =>
                {
                    b.HasOne("Webeditor.Domain.Entities.Recipes.RecipeCategory", "RecipeCategory")
                        .WithMany("RecipeTags")
                        .HasForeignKey("RecipeCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RecipeCategory");
                });

            modelBuilder.Entity("Webeditor.Domain.Entities.System.SystemRole", b =>
                {
                    b.HasOne("Webeditor.Domain.Entities.System.SystemModule", "SystemModule")
                        .WithMany()
                        .HasForeignKey("SystemModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SystemModule");
                });

            modelBuilder.Entity("Webeditor.Domain.Entities.Recipes.Recipe", b =>
                {
                    b.Navigation("RecipeImages");

                    b.Navigation("RecipeRates");
                });

            modelBuilder.Entity("Webeditor.Domain.Entities.Recipes.RecipeCategory", b =>
                {
                    b.Navigation("RecipeTags");
                });

            modelBuilder.Entity("Webeditor.Domain.Entities.System.SystemCompany", b =>
                {
                    b.Navigation("SystemUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
