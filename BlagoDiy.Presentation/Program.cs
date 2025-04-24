using BlagoDiy.BusinessLogic.Services;
using BlagoDiy.DataAccessLayer;
using BlagoDiy.DataAccessLayer.UnitOfWork;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(CampaignService).Assembly);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<BlagoContext>();


builder.Services.AddScoped<CampaignService>();
builder.Services.AddScoped<DonationService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AchievementService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
});


var app = builder.Build();

app.UseCors("AllowAll");

app.UseRouting();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("default", "WTF is this");
});

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlagoDiy API V1");
    c.RoutePrefix = string.Empty;
});



app.Run();

