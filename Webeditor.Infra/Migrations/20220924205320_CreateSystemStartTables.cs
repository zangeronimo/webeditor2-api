using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Webeditor.Infra.Migrations
{
  public partial class CreateSystemStartTables : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "RecipeCategories",
          columns: table => new
          {
            Id = table.Column<long>(type: "bigint", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            Slug = table.Column<string>(type: "varchar(45)", nullable: false),
            Name = table.Column<string>(type: "varchar(45)", nullable: false),
            SystemCompanyId = table.Column<long>(type: "bigint", nullable: false),
            Guid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
            Active = table.Column<short>(type: "smallint", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "NOW()"),
            UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "NOW()"),
            RemovedAt = table.Column<DateTime>(type: "timestamp", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_RecipeCategories", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "SystemCompanies",
          columns: table => new
          {
            Id = table.Column<long>(type: "bigint", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            Name = table.Column<string>(type: "varchar(30)", nullable: false),
            Guid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
            Active = table.Column<short>(type: "smallint", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "NOW()"),
            UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "NOW()"),
            RemovedAt = table.Column<DateTime>(type: "timestamp", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_SystemCompanies", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "SystemModules",
          columns: table => new
          {
            Id = table.Column<long>(type: "bigint", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            Name = table.Column<string>(type: "varchar(20)", nullable: false),
            Guid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
            Active = table.Column<short>(type: "smallint", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "NOW()"),
            UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "NOW()"),
            RemovedAt = table.Column<DateTime>(type: "timestamp", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_SystemModules", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Recipes",
          columns: table => new
          {
            Id = table.Column<long>(type: "bigint", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            Slug = table.Column<string>(type: "varchar(80)", nullable: false),
            Name = table.Column<string>(type: "varchar(80)", nullable: false),
            Ingredients = table.Column<string>(type: "varchar", nullable: false),
            Preparation = table.Column<string>(type: "varchar", nullable: false),
            RecipeCategoryId = table.Column<long>(type: "bigint", nullable: false),
            SystemCompanyId = table.Column<long>(type: "bigint", nullable: false),
            Guid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
            Active = table.Column<short>(type: "smallint", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "NOW()"),
            UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "NOW()"),
            RemovedAt = table.Column<DateTime>(type: "timestamp", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Recipes", x => x.Id);
            table.ForeignKey(
                      name: "FK_Recipes_RecipeCategories_RecipeCategoryId",
                      column: x => x.RecipeCategoryId,
                      principalTable: "RecipeCategories",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "RecipeTags",
          columns: table => new
          {
            Id = table.Column<long>(type: "bigint", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            Name = table.Column<string>(type: "varchar(45)", nullable: false),
            SystemCompanyId = table.Column<long>(type: "bigint", nullable: false),
            RecipeCategoryId = table.Column<long>(type: "bigint", nullable: false),
            Guid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
            Active = table.Column<short>(type: "smallint", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "NOW()"),
            UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "NOW()"),
            RemovedAt = table.Column<DateTime>(type: "timestamp", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_RecipeTags", x => x.Id);
            table.ForeignKey(
                      name: "FK_RecipeTags_RecipeCategories_RecipeCategoryId",
                      column: x => x.RecipeCategoryId,
                      principalTable: "RecipeCategories",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "SystemUsers",
          columns: table => new
          {
            Id = table.Column<long>(type: "bigint", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            Name = table.Column<string>(type: "varchar(200)", nullable: false),
            Email = table.Column<string>(type: "varchar(200)", nullable: false),
            Avatar = table.Column<string>(type: "varchar(255)", nullable: true),
            Password = table.Column<string>(type: "varchar(100)", nullable: false),
            SystemCompanyId = table.Column<long>(type: "bigint", nullable: false),
            Guid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
            Active = table.Column<short>(type: "smallint", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "NOW()"),
            UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "NOW()"),
            RemovedAt = table.Column<DateTime>(type: "timestamp", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_SystemUsers", x => x.Id);
            table.ForeignKey(
                      name: "FK_SystemUsers_SystemCompanies_SystemCompanyId",
                      column: x => x.SystemCompanyId,
                      principalTable: "SystemCompanies",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "SystemCompanySystemModule",
          columns: table => new
          {
            SystemCompaniesId = table.Column<long>(type: "bigint", nullable: false),
            SystemModulesId = table.Column<long>(type: "bigint", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_SystemCompanySystemModule", x => new { x.SystemCompaniesId, x.SystemModulesId });
            table.ForeignKey(
                      name: "FK_SystemCompanySystemModule_SystemCompanies_SystemCompaniesId",
                      column: x => x.SystemCompaniesId,
                      principalTable: "SystemCompanies",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_SystemCompanySystemModule_SystemModules_SystemModulesId",
                      column: x => x.SystemModulesId,
                      principalTable: "SystemModules",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "SystemRoles",
          columns: table => new
          {
            Id = table.Column<long>(type: "bigint", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            Name = table.Column<string>(type: "varchar(30)", nullable: false),
            Label = table.Column<string>(type: "varchar(30)", nullable: false),
            SystemModuleId = table.Column<long>(type: "bigint", nullable: false),
            Guid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
            Active = table.Column<short>(type: "smallint", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "NOW()"),
            UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "NOW()"),
            RemovedAt = table.Column<DateTime>(type: "timestamp", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_SystemRoles", x => x.Id);
            table.ForeignKey(
                      name: "FK_SystemRoles_SystemModules_SystemModuleId",
                      column: x => x.SystemModuleId,
                      principalTable: "SystemModules",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "RecipeImages",
          columns: table => new
          {
            Id = table.Column<long>(type: "bigint", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            Path = table.Column<string>(type: "varchar(100)", nullable: true),
            RecipeId = table.Column<long>(type: "bigint", nullable: false),
            SystemCompanyId = table.Column<long>(type: "bigint", nullable: false),
            Guid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
            Active = table.Column<short>(type: "smallint", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "NOW()"),
            UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "NOW()"),
            RemovedAt = table.Column<DateTime>(type: "timestamp", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_RecipeImages", x => x.Id);
            table.ForeignKey(
                      name: "FK_RecipeImages_Recipes_RecipeId",
                      column: x => x.RecipeId,
                      principalTable: "Recipes",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "RecipeRates",
          columns: table => new
          {
            Id = table.Column<long>(type: "bigint", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            Rate = table.Column<short>(type: "smallint", nullable: false),
            Comment = table.Column<string>(type: "varchar(100)", nullable: true),
            RecipeId = table.Column<long>(type: "bigint", nullable: false),
            SystemCompanyId = table.Column<long>(type: "bigint", nullable: false),
            Guid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
            Active = table.Column<short>(type: "smallint", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "NOW()"),
            UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "NOW()"),
            RemovedAt = table.Column<DateTime>(type: "timestamp", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_RecipeRates", x => x.Id);
            table.ForeignKey(
                      name: "FK_RecipeRates_Recipes_RecipeId",
                      column: x => x.RecipeId,
                      principalTable: "Recipes",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "RecipeRecipeTag",
          columns: table => new
          {
            RecipeTagsId = table.Column<long>(type: "bigint", nullable: false),
            RecipesId = table.Column<long>(type: "bigint", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_RecipeRecipeTag", x => new { x.RecipeTagsId, x.RecipesId });
            table.ForeignKey(
                      name: "FK_RecipeRecipeTag_Recipes_RecipesId",
                      column: x => x.RecipesId,
                      principalTable: "Recipes",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_RecipeRecipeTag_RecipeTags_RecipeTagsId",
                      column: x => x.RecipeTagsId,
                      principalTable: "RecipeTags",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "SystemRoleSystemUser",
          columns: table => new
          {
            SystemRolesId = table.Column<long>(type: "bigint", nullable: false),
            SystemUsersId = table.Column<long>(type: "bigint", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_SystemRoleSystemUser", x => new { x.SystemRolesId, x.SystemUsersId });
            table.ForeignKey(
                      name: "FK_SystemRoleSystemUser_SystemRoles_SystemRolesId",
                      column: x => x.SystemRolesId,
                      principalTable: "SystemRoles",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_SystemRoleSystemUser_SystemUsers_SystemUsersId",
                      column: x => x.SystemUsersId,
                      principalTable: "SystemUsers",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.InsertData(
          table: "SystemCompanies",
          columns: new[] { "Id", "Active", "Guid", "Name", "RemovedAt" },
          values: new object[] { 1L, (short)0, new Guid("ba5911fc-39b6-4ca2-bcdb-d480b753f078"), "Tudo Linux", null });

      migrationBuilder.InsertData(
          table: "SystemModules",
          columns: new[] { "Id", "Active", "Guid", "Name", "RemovedAt" },
          values: new object[] { 1L, (short)0, new Guid("98dbdbcc-a818-4262-a558-9f69b24b1589"), "Sistema", null });

      migrationBuilder.InsertData(
          table: "SystemRoles",
          columns: new[] { "Id", "Active", "Guid", "Label", "Name", "RemovedAt", "SystemModuleId" },
          values: new object[,]
          {
                    { 1L, (short)0, new Guid("d68b4e25-a9c4-483b-becf-b4d6430c7714"), "Pesquisar Usuários", "ROLE_GET_SYSTEMUSER", null, 1L },
                    { 2L, (short)0, new Guid("9da5e1b2-8b11-47de-8145-25b6f6bda019"), "Cadastrar Usuários", "ROLE_PUT_SYSTEMUSER", null, 1L },
                    { 3L, (short)0, new Guid("838d3a8e-c762-4635-8a32-62338a87a821"), "Remover Usuários", "ROLE_DEL_SYSTEMUSER", null, 1L }
          });

      migrationBuilder.InsertData(
          table: "SystemUsers",
          columns: new[] { "Id", "Active", "Avatar", "Email", "Guid", "Name", "Password", "RemovedAt", "SystemCompanyId" },
          values: new object[] { 1L, (short)0, null, "zangeronimo@gmail.com", new Guid("41ddc041-fd45-4d62-96e7-4d7b9c0dc4a8"), "Luciano Zangeronimo", "$2a$11$LUraF.DU.IcRA3S1B980feDxdUTNK9NSVvct.jwpP67dlK2Ibk0dC", null, 1L });

      migrationBuilder.CreateIndex(
          name: "IX_RecipeCategories_Guid",
          table: "RecipeCategories",
          column: "Guid",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_RecipeCategories_SystemCompanyId",
          table: "RecipeCategories",
          column: "SystemCompanyId");

      migrationBuilder.CreateIndex(
          name: "IX_RecipeImages_Guid",
          table: "RecipeImages",
          column: "Guid",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_RecipeImages_RecipeId",
          table: "RecipeImages",
          column: "RecipeId");

      migrationBuilder.CreateIndex(
          name: "IX_RecipeImages_SystemCompanyId",
          table: "RecipeImages",
          column: "SystemCompanyId");

      migrationBuilder.CreateIndex(
          name: "IX_RecipeRates_Guid",
          table: "RecipeRates",
          column: "Guid",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_RecipeRates_RecipeId",
          table: "RecipeRates",
          column: "RecipeId");

      migrationBuilder.CreateIndex(
          name: "IX_RecipeRates_SystemCompanyId",
          table: "RecipeRates",
          column: "SystemCompanyId");

      migrationBuilder.CreateIndex(
          name: "IX_RecipeRecipeTag_RecipesId",
          table: "RecipeRecipeTag",
          column: "RecipesId");

      migrationBuilder.CreateIndex(
          name: "IX_Recipes_Guid",
          table: "Recipes",
          column: "Guid",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_Recipes_RecipeCategoryId",
          table: "Recipes",
          column: "RecipeCategoryId");

      migrationBuilder.CreateIndex(
          name: "IX_Recipes_SystemCompanyId",
          table: "Recipes",
          column: "SystemCompanyId");

      migrationBuilder.CreateIndex(
          name: "IX_RecipeTags_Guid",
          table: "RecipeTags",
          column: "Guid",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_RecipeTags_RecipeCategoryId",
          table: "RecipeTags",
          column: "RecipeCategoryId");

      migrationBuilder.CreateIndex(
          name: "IX_RecipeTags_SystemCompanyId",
          table: "RecipeTags",
          column: "SystemCompanyId");

      migrationBuilder.CreateIndex(
          name: "IX_SystemCompanies_Guid",
          table: "SystemCompanies",
          column: "Guid",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_SystemCompanySystemModule_SystemModulesId",
          table: "SystemCompanySystemModule",
          column: "SystemModulesId");

      migrationBuilder.CreateIndex(
          name: "IX_SystemModules_Guid",
          table: "SystemModules",
          column: "Guid",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_SystemRoles_Guid",
          table: "SystemRoles",
          column: "Guid",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_SystemRoles_SystemModuleId",
          table: "SystemRoles",
          column: "SystemModuleId");

      migrationBuilder.CreateIndex(
          name: "IX_SystemRoleSystemUser_SystemUsersId",
          table: "SystemRoleSystemUser",
          column: "SystemUsersId");

      migrationBuilder.CreateIndex(
          name: "IX_SystemUsers_Guid",
          table: "SystemUsers",
          column: "Guid",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_SystemUsers_SystemCompanyId",
          table: "SystemUsers",
          column: "SystemCompanyId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "RecipeImages");

      migrationBuilder.DropTable(
          name: "RecipeRates");

      migrationBuilder.DropTable(
          name: "RecipeRecipeTag");

      migrationBuilder.DropTable(
          name: "SystemCompanySystemModule");

      migrationBuilder.DropTable(
          name: "SystemRoleSystemUser");

      migrationBuilder.DropTable(
          name: "Recipes");

      migrationBuilder.DropTable(
          name: "RecipeTags");

      migrationBuilder.DropTable(
          name: "SystemRoles");

      migrationBuilder.DropTable(
          name: "SystemUsers");

      migrationBuilder.DropTable(
          name: "RecipeCategories");

      migrationBuilder.DropTable(
          name: "SystemModules");

      migrationBuilder.DropTable(
          name: "SystemCompanies");
    }
  }
}
