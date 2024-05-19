using Laba_3_MCV.Interfaces;
using Laba_3_MCV.Repositories;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Travel_Blog.Context;
using Travel_Blog.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddOData(options => options.AddRouteComponents("odata", GetEdmModel())
    .Count().Filter().OrderBy().Expand().Select().SetMaxTop(50)
);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddDbContext<DBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
IEdmModel GetEdmModel()
{
    var edmBuilder = new ODataConventionModelBuilder();
    edmBuilder.EntitySet<Blog>("Blogs");
    return edmBuilder.GetEdmModel();
}