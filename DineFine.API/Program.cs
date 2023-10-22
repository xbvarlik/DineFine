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


// TODO: seed data don't see passwords
// TODO: change sql to local db