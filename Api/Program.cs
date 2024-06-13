using Data.Context;
using Application.Interfaces;
using Application.Services;
using Application.Token;
using Data.Repository;
using Domain.Anuncio.Contracts;
using Domain.AnuncioCategoria.Contracts;
using Domain.AutoComplete.Contracts;
using Domain.Categoria.Contracts;
using Domain.FotoAnuncio.Contracts;
using Domain.Interesse.Contracts;
using Domain.Mensagem.Contracts;
using Domain.Usuario.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Data;
using Data.Contracts;
using System.Diagnostics;
using Microsoft.OpenApi.Models;
using System.Reflection;

#region Npgsql
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
#endregion

#region Environment
#if DEBUG
DotNetEnv.Env.Load(Path.Combine(Directory.GetCurrentDirectory(), ".env.local"));
#else
    DotNetEnv.Env.Load(Path.Combine(Directory.GetCurrentDirectory(), ".env"));
#endif

#endregion

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Olx", Version = "v1" });

    // Configure o caminho e o nome do arquivo XML de documentação
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    // Adicione suporte para autenticação Bearer no Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Adicione o esquema de segurança Bearer ao requisitar endpoints no Swagger UI
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
});

#region Token JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(option =>
      {
          option.TokenValidationParameters = new TokenValidationParameters
          {
              ValidateIssuer = false,
              ValidateAudience = false,
              ValidateLifetime = true,
              ValidateIssuerSigningKey = true,

              ValidIssuer = "Teste.Securiry.Bearer",
              ValidAudience = "Teste.Securiry.Bearer",
              IssuerSigningKey = JwtSecurityKey.Create("b7e94be513e96e8c45cd23d162275e5a12ebde9100a425c4ebcdd7fa4dcd897c")
          };

          option.Events = new JwtBearerEvents
          {
              OnAuthenticationFailed = context =>
              {
                  Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                  return Task.CompletedTask;
              },
              OnTokenValidated = context =>
              {
                  Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                  return Task.CompletedTask;
              }
          };
      });
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Access
//var urlDev = "https://dominiodocliente.com.br";
//var urlHML = "https://dominiodocliente2.com.br";
//var urlPROD = "https://dominiodocliente3.com.br";
//app.UseCors(b => b.WithOrigins(urlDev, urlHML, urlPROD));

var devCliente = "http://localhost:4200";
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader().WithOrigins(devCliente));
#endregion

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseSwaggerUI();

app.Run();

void ConfigureServices(IServiceCollection services)
{
    #region DataContext
    string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

    services.AddDbContext<DataContext>(options =>
                    options.UseNpgsql(connectionString),
    ServiceLifetime.Scoped);
    #endregion

    services.AddScoped<IUnitOfWork, UnitOfWork>();

    #region Repository
    services.AddTransient<IAutoCompleteRepository, AutoCompleteRepository>();
    services.AddTransient<IUsuarioRepository, UsuarioRepository>();
    services.AddTransient<IAnuncioCategoriaRepository, AnuncioCategoriaRepository>();
    services.AddTransient<ICategoriaRepository, CategoriaRepository>();
    services.AddTransient<IFotoAnuncioRepository, FotoAnuncioRepository>();
    services.AddTransient<IInteresseRepository, InteresseRepository>();
    services.AddTransient<IMensagemRepository, MensagemRepository>();
    services.AddTransient<IAnuncioRepository, AnuncioRepository>();
    #endregion

    #region Service
    services.AddScoped<IAutoCompleteService, AutoCompleteService>();
    services.AddScoped<IAmazonS3Service, AmazonS3Service>();
    services.AddScoped<IFotoAnuncioService, FotoAnuncioService>();
    builder.Services.AddScoped<IAnuncioService, AnuncioService>();
    builder.Services.AddScoped<IUsuarioService, UsuarioService>();
    #endregion
}
