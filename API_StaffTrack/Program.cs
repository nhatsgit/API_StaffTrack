using API_StaffTrack.Application.Mapper;
using API_StaffTrack.Application.Services;
using API_StaffTrack.Application.Ultilities;
using API_StaffTrack.Data.EF;
using API_StaffTrack.Data.Entities;
using API_StaffTrack.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


static void InitUtilitiesService(IServiceCollection services)
{
    services.AddScoped<ISendMailSMTP, SendMailSMTP>();
}

static void InitDaoService(IServiceCollection services)
{
    services.AddScoped<IS_Account, S_Account>();
    services.AddScoped<IS_Employee, S_Employee>();
    services.AddScoped<IS_WorkPlan, S_WorkPlan>();
    services.AddScoped<IS_Notification, S_Notification>();
}
// Add services to the container.
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<MainDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddDbContext<MainDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DB")));
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressInferBindingSourcesForParameters = true;
    options.SuppressModelStateInvalidFilter = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

InitUtilitiesService(builder.Services);
InitDaoService(builder.Services);

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressInferBindingSourcesForParameters = true;
    options.SuppressModelStateInvalidFilter = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Add JWT Authentication
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});
builder.Services.AddSwaggerGen(c =>
{
    //c.OperationFilter<API.H2ADBSite.Portal.Variables.AddAuthorizationHeaderOperationHeader>();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API_Sample.WebApi", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[]{}
        }
    });
});

//Config IpRateLimit https://github.com/stefanprodan/AspNetCoreRateLimit/wiki/IpRateLimitMiddleware
builder.Services.AddMemoryCache();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var path = Directory.GetCurrentDirectory();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(c =>
    {
        c.DefaultModelExpandDepth(2);
        c.DefaultModelRendering(ModelRendering.Model);
        c.DefaultModelsExpandDepth(-1);
        c.DisplayOperationId();
        c.DisplayRequestDuration();
        c.DocExpansion(DocExpansion.None);
        c.EnableDeepLinking();
        c.EnableFilter();
        //c.MaxDisplayedTags(5);
        c.ShowExtensions();
        c.ShowCommonExtensions();
        c.EnableValidator();
        //c.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Head);
        c.UseRequestInterceptor("(request) => { return request; }");
    });
}
else
{
    app.UseHsts();
    if (builder.Configuration.GetValue<bool>("Swagger:Active"))
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.DefaultModelExpandDepth(2);
            c.DefaultModelRendering(ModelRendering.Model);
            c.DefaultModelsExpandDepth(-1);
            c.DisplayOperationId();
            c.DisplayRequestDuration();
            c.DocExpansion(DocExpansion.None);
            c.EnableDeepLinking();
            c.EnableFilter();
            //c.MaxDisplayedTags(5);
            c.ShowExtensions();
            c.ShowCommonExtensions();
            c.EnableValidator();
            //c.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Head);
            c.UseRequestInterceptor("(request) => { return request; }");
        });
    }
}







app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
