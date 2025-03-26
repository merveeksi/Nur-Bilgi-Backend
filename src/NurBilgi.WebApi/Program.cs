using Microsoft.AspNetCore.Mvc;
using NurBilgi.Application;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Infrastructure;
using NurBilgi.WebApi;
using NurBilgi.WebApi.Extensions;
using NurBilgi.WebApi.Extensions.Swagger;
using NurBilgi.WebApi.Filters;
using NurBilgi.WebApi.Services;
using Google.Cloud.AIPlatform.V1;
using NurBilgi.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => {
    options.Filters.Add<GlobalExceptionFilter>();
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddLogging(config => config.AddConsole());

builder.Services.AddSwaggerWithVersion();

builder.Services.AddSingleton(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    string? locationId = configuration["GoogleCloud:LocationId"];
    if (string.IsNullOrWhiteSpace(locationId))
    {
         throw new InvalidOperationException("Google Cloud LocationId yapılandırması (appsettings.json -> GoogleCloud:LocationId) eksik veya hatalı.");
    }
    try
    {
        return new PredictionServiceClientBuilder
        {
            Endpoint = $"{locationId}-aiplatform.googleapis.com"
        }.Build();
    }
    catch (Exception ex)
    {
        var logger = provider.GetRequiredService<ILogger<Program>>();
        logger.LogCritical(ex, "PredictionServiceClient oluşturulamadı. Endpoint: {Endpoint}", $"{locationId}-aiplatform.googleapis.com");
        throw;
    }
});

builder.Services.AddScoped<IChatbotService, GeminiChatbotService>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddWebApi(builder.Configuration);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddScoped<ICurrentUserService, CurrentUserManager>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var frontendUrl = builder.Configuration["FrontendUrl"];
if (!string.IsNullOrWhiteSpace(frontendUrl))
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin",
            policy =>
            {
                policy.WithOrigins(frontendUrl)
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
    });
}
else
{
    Console.WriteLine("Uyarı: FrontendUrl yapılandırması eksik veya boş. CORS politikası uygulanmayacak veya kısıtlı olabilir.");
     // İsteğe bağlı: Geliştirme ortamı için daha esnek bir politika eklenebilir
     // builder.Services.AddCors(...);
}


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithVersion();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

if (!string.IsNullOrWhiteSpace(frontendUrl))
{
    app.UseCors("AllowSpecificOrigin");
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.ApplyMigrations();

app.Run();