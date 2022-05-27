using LabAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string txt = "";

// Add services to the container.

//Circular Loop Exption Sol. (insted of [JsonIgnore])
builder.Services.AddControllers().AddNewtonsoftJson(n => n.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
//builder.Services.AddControllers();

//Lazy Loading
//builder.Services.AddDbContext<LabAPIContext>(n => n.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("LabAPIConnection")));
builder.Services.AddDbContext<LabAPIContext>(n => n.UseSqlServer(builder.Configuration.GetConnectionString("LabAPIConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Web(JS) Side
builder.Services.AddCors(options =>
{
    options.AddPolicy(txt,
    builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(txt);
app.MapControllers();

app.Run();
