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
using Domain.UsuarioRelatorio.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Domain.Usuario;
using Microsoft.AspNetCore.Identity;

#region Npgsql
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
#endregion

#region Envinroment
DotNetEnv.Env.Load(Path.Combine(Directory.GetCurrentDirectory(), ".env"));

#endregion

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region DbContext
string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

builder.Services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(connectionString));
#endregion

#region DI

#region Repository
builder.Services.AddTransient<IAutoCompleteRepository, AutoCompleteRepository>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<IAnuncioCategoriaRepository, AnuncioCategoriaRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<IFotoAnuncioRepository, FotoAnuncioRepository>();
builder.Services.AddTransient<IInteresseRepository, InteresseRepository>();
builder.Services.AddTransient<IMensagemRepository, MensagemRepository>();
builder.Services.AddTransient<IAnuncioRepository, AnuncioRepository>();
builder.Services.AddTransient<IUsuarioRelatorioRepository, UsuarioRelatorioRepository>();
#endregion

#region Service

builder.Services.AddScoped<IAutoCompleteService, AutoCompleteService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
#endregion

#endregion

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
