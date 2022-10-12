using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Webeditor.Application.DTOs.Recipes;
using Webeditor.Application.DTOs.Recipes.Categories;
using Webeditor.Application.DTOs.Recipes.Images;
using Webeditor.Application.DTOs.Recipes.Rates;
using Webeditor.Application.DTOs.Recipes.Tags;
using Webeditor.Application.DTOs.System;
using Webeditor.Application.Interfaces.Application.Authorize;
using Webeditor.Application.Interfaces.Application.Recipes;
using Webeditor.Application.Interfaces.Application.System;
using Webeditor.Application.Services.Authorizes;
using Webeditor.Application.Services.Recipes;
using Webeditor.Application.Services.System;
using Webeditor.Domain.Entities.Recipes;
using Webeditor.Domain.Entities.System;
using Webeditor.Domain.Interfaces.Infra;
using Webeditor.Domain.Interfaces.Recipes;
using Webeditor.Domain.Interfaces.System;
using Webeditor.Infra.Context;
using Webeditor.Infra.Providers.FileUploadProvider;
using Webeditor.Infra.Providers.HashProvider;
using Webeditor.Infra.Providers.TokenProvider;
using Webeditor.Infra.Repositories.Recipes;
using Webeditor.Infra.Repositories.System;

namespace Webeditor.Infra.Extensions;

public static class DependencyInjection
{
  public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
  {
    var connectionString = builder.Configuration.GetConnectionString("PgSQL");
    builder.Services.AddDbContext<AppDbContext>(o => o.UseNpgsql(connectionString));

    // System
    builder.Services.AddTransient<ISystemUserRepository, SystemUserRepository>();
    builder.Services.AddTransient<ISystemRoleRepository, SystemRoleRepository>();

    // Recipes
    builder.Services.AddTransient<IRecipeTagRepository, RecipeTagRepository>();
    builder.Services.AddTransient<IRecipeCategoryRepository, RecipeCategoryRepository>();
    builder.Services.AddTransient<IRecipeImageRepository, RecipeImageRepository>();
    builder.Services.AddTransient<IRecipeRateRepository, RecipeRateRepository>();
    builder.Services.AddTransient<IRecipeRepository, RecipeRepository>();

    // System
    builder.Services.AddTransient<IAuthorizeService, AuthorizeService>();
    builder.Services.AddTransient<IProfileService, ProfileService>();
    builder.Services.AddTransient<ISystemUserService, SystemUserService>();

    // Recipes
    builder.Services.AddTransient<IRecipeTagService, RecipeTagService>();
    builder.Services.AddTransient<IRecipeCategoryService, RecipeCategoryService>();
    builder.Services.AddTransient<IRecipeImageService, RecipeImageService>();
    builder.Services.AddTransient<IRecipeRateService, RecipeRateService>();
    builder.Services.AddTransient<IRecipeService, RecipeService>();

    // Providers
    builder.Services.AddTransient<IHashProvider, HashProvider>();
    builder.Services.AddTransient<ITokenProvider, TokenProvider>();
    builder.Services.AddTransient<IFileUploadProvider, BufferedFileUploadLocalProvider>();

    builder.Services.AddAutoMapper(config =>
    {
      // System
      config.CreateMap<SystemUser, ProfileDto>().ReverseMap();
      config.CreateMap<SystemUser, SystemUserDto>().ReverseMap();
      config.CreateMap<SystemRole, SystemRoleDto>().ReverseMap();

      // Recipes
      config.CreateMap<RecipeTag, RecipeTagDto>().ReverseMap();
      config.CreateMap<RecipeCategory, RecipeCategoryDto>().ReverseMap();
      config.CreateMap<RecipeRate, RecipeRateDto>().ReverseMap();
      config.CreateMap<RecipeImage, RecipeImageDto>().ReverseMap();
      config.CreateMap<Recipe, RecipeDto>().ReverseMap();
    });

    return builder;
  }
}
