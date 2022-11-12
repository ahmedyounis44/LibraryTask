using LibraryTask.BAL.Interface;
using LibraryTask.BAL.Services;
using LibraryTask.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("AppDB");
builder.Services.AddDbContext<LibraryDbContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.SwaggerDoc(
        "Test",
        new Microsoft.OpenApi.Models.OpenApiInfo()
        {
            Title = "Test API",
            Version = "1",
            Description = "Through this API you can access MOD",
            Contact = new Microsoft.OpenApi.Models.OpenApiContact()
            {
                Email = "Ayounis482@gmail.com",
                Name = "younis",
                            //Url = new Uri("https://www.twitter.com/KevinDockx")
            },
            License = new Microsoft.OpenApi.Models.OpenApiLicense()
            {
                Name = "MIT License",
                            //Url = new Uri("https://opensource.org/licenses/MIT")
            }
        });

    //var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

    //setupAction.IncludeXmlComments(xmlCommentsFullPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    
}
app.UseStaticFiles();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(setupAction =>
{
    setupAction.SwaggerEndpoint(
        "/swagger/Test/swagger.json",
        "Test");

});
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Books}/{action=Index}/{id?}");

app.Run();
