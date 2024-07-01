

using BL.Services;
using BL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using reservation_app_server.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IActivityDayListService, ActivityDayListService>();
builder.Services.AddScoped<IActivityDayListRepository, ActivityDayListRepository>();
builder.Services.AddScoped<IHelperService<ActivityDayListItem>, HelperService<ActivityDayListItem>>();
builder.Services.AddScoped<IHelperService<ReservationListItem>, HelperService<ReservationListItem>>();
builder.Services.AddScoped<IHelperService<Invoice>, HelperService<Invoice>>();
builder.Services.AddScoped<IReservationsListService, ReservationsListService>();
builder.Services.AddScoped<IReservationListRepository, ReservationListRepository>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200",
                                "development web site")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                ;
        });
});
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("CorsPolicy");

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
