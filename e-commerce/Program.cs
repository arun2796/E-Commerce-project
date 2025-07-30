using Application.RegisterAppServices;
using CInfrastructure.Dbconnection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ApplicationService();
#region   Database connection 
builder.Services.AddDbContext<ApplicationDbcontext>( option =>

    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
#endregion

#region frontend connection

builder.Services.AddCors(opt=>opt.AddPolicy("Policy",p=>p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

#endregion


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors("Policy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
