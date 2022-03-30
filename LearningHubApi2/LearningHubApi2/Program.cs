using LearningHubApi2.Data_Layer;
using Microsoft.EntityFrameworkCore;
using LearningHubApi2.Extensions;
using LearningHubApi2;
using System.Web.Http.Filters;
using Microsoft.IdentityModel.Logging;

//var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);//.WebApplication.CreateBuilder(args);
// Add services to the container.

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddJWTTokenServices(builder.Configuration);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddDbContext<LearningHubApiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LearningHub")));
builder.Services.AddAuthentication().AddJwtBearer(//options =>
        //{
        //    options.Audience = "https://localhost:7178/";
        //    options.Authority = "https://localhost:7203/";
        //}
);
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("https://localhost:7178").AllowAnyHeader()
                          .AllowAnyMethod().AllowAnyOrigin();
                      });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    IdentityModelEventSource.ShowPII = true;
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.MapControllers();

app.Run();
