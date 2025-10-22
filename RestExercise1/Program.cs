using FootballTeamLib;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
                              policy =>
                              {
                                  policy.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader();
                              });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers();
builder.Services.AddOpenApi();

bool useSql = false;
if (useSql)
{
    builder.Services.AddSingleton<IFootballRepo>(new FootBallRepoDB());
}
else
{
    builder.Services.AddSingleton<IFootballRepo>(new FootballRepoList());
}

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapOpenApi();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
