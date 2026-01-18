using Contacts.Application.Interfaces;
using Contacts.Infrastructure.Persistence;
using Contacts.Infrastructure.Repositories;
using Contacts.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы в контейнер зависимостей
builder.Services.AddControllersWithViews();

// Подключаем DbContext
builder.Services.AddDbContext<ContactsDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Настраиваем DI (внедрение зависимостей)
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactService>();

var app = builder.Build();

// Настройка обработки ошибок
if (!app.Environment.IsDevelopment())
{
    // Перенаправляем на кастомный ErrorController
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Создаем БД, если нет
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ContactsDbContext>();
    db.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Маршрутизация: открываем сразу список контактов
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Contacts}/{action=Index}/{id?}");

app.Run();