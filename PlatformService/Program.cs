using Microsoft.EntityFrameworkCore;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.SyncDataService.Http;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


builder.Services.AddScoped<IPlatformRepository , PlatformRepository>();

builder.Services.AddHttpClient<ICommandDataClient , HttpCommandDataClient>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

var test = builder.Configuration.GetConnectionString("PlatformConn");
var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
Console.WriteLine(environmentName);

//مشخص کردن دیتابیس تو هر محیط
if (environmentName == "Development")
{
    Console.WriteLine("--> Using InMem Db");
    builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("InMem"));
}
else
{
    Console.WriteLine("--> Using SQLServer Db");
    builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformConn")));
}

/// <summary>
/// یه راه دیگه
/// </summary>
//public override void OnActionExecuting(ActionExecutingContext context)  
//{
//    base.OnActionExecuting(context);

//    if (env.IsDevelopment())
//    {
//        conStr = Configuration.GetConnectionString("DevConnection");
//    }
//    else
//    {
//        conStr = Configuration.GetConnectionString("LiveConnection");
//    }

//    myDbContext = DbContextFactory.Create(conStr, Configuration);
//}



builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());   
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
PrepDb.PrepPopulation(app, app.Environment.IsProduction());
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{

}




//app.UseHttpsRedirection(); 

app.UseAuthorization();

app.MapControllers();

app.Run();


