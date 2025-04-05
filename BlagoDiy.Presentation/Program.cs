using BlagoDiy.BusinessLogic.Services;
using BlagoDiy.DataAccessLayer;
using BlagoDiy.DataAccessLayer.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(CampaignService).Assembly);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<BlagoContext>();


builder.Services.AddScoped<CampaignService>();
builder.Services.AddScoped<DonationService>();
builder.Services.AddScoped<UserService>();



builder.Services.AddControllers();




var app = builder.Build();

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

