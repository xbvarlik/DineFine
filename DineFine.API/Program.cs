using DineFine.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBootstrapper(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

// TODO: Restrict read as tenant-based. - Done
// TODO: Create notification for stock info. (Alert if stock is low) - Done
// TODO: Test restricted CRUD operations for tenant-based entities.
// TODO: Migrate the database for RestaurantStockInfo entity is changed.
// TODO: Redis Cache will be implemented. - Done
// TODO: Test Redis Cache.
// TODO: BLOB storage will be implemented. - Done

// TODO: BLOB storage will be made up and running.
// TODO: Test BLOB storage.

// TODO: Notification Hub will be implemented.
// TODO: Notification Hub will be made up and running.

