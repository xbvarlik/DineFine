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



// TODO: Redis Cache will be implemented. - Done
// TODO: Test Redis Cache.
// TODO: BLOB storage will be implemented. - Done
// TODO: BLOB storage will be made up and running. - Done.
// TODO: Test BLOB storage.

// TODO: Notification Hub will be implemented.
// TODO: Notification Hub will be made up and running.

