using Microsoft.AspNetCore.Authentication.JwtBearer;

using PrimeiraAPI.Infraestrutura;
using PrimeiraAPI.Model;
using Microsoft.IdentityModel.Tokens;

using System.Text;
using Microsoft.OpenApi.Models;
using PrimeiraAPI.Application.Mapping;



var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços de controladores
builder.Services.AddControllers();


builder.Services.AddAutoMapper(typeof(DomainToDTOMapping));


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
    {
        new OpenApiSecurityScheme
        {
        Reference = new OpenApiReference
            {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header,

        },
        new List<string>()
        }
    });


});

builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();


var key = Encoding.ASCII.GetBytes(PrimeiraAPI.Key.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error-development");

    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error");

    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();




// Define rota para o controller de funcionários
app.MapControllers();

app.Run();

