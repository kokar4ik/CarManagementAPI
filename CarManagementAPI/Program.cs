using Microsoft.EntityFrameworkCore;
using CarManagementAPI.Data;
using CarManagementAPI.Interfaces;
using CarManagementAPI.Models;
var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы в контейнер
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Регистрируем DbContext
builder.Services.AddDbContext<CarContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICarRepository, CarRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var repository = scope.ServiceProvider.GetRequiredService<ICarRepository>();

    Console.WriteLine("=== ТЕСТИРУЕМ РЕПОЗИТОРИЙ ===");

//    // Тест 1: Добавляем автомобиль
//    var newCar = new Car
//    {
//        Brand = "Test Brand",
//        Model = "Test Model",
//        Year = 2023,
//        Price = 30000,
//        Mileage = 0
//    };
//    repository.Add(newCar);
//    Console.WriteLine("? Автомобиль добавлен");

//    // Тест 2: Получаем все автомобили
//    var allCars = repository.GetAll();
//    Console.WriteLine($"? Найдено автомобилей: {allCars.Count()}");

//    // Тест 3: Ищем по ID
//    var car = repository.GetById(1);
//    if (car != null)
//        Console.WriteLine($"? Найден автомобиль: {car.Brand} {car.Model}");
}
// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();