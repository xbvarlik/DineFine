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

// TODO: Restrict read as tenant-based.
// TODO: Create notification for stock info. (Alert if stock is low)

// TODO: BLOB storage will be implemented.
// TODO: Notification Hub will be implemented.


