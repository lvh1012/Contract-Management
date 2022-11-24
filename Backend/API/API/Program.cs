using API.DataContext;
using API.Repository;
using API.Repository.Interface;
using API.Services;
using API.Services.Interface;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader(); ;
        });
});
builder.Services.AddControllers(options =>
{
    //options.Filters.Add<HttpResponseExceptionFilter>();
}).AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<ApplicationDataContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI Repository
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IContractProductRepository, ContractProductRepository>();

// DI Service
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IContractProductService, ContractProductService>();

builder.Services.AddScoped<ITemplateService, TemplateService>();

var app = builder.Build();
//app.UseExceptionHandler(a => a.Run(async context =>
//{
//    // 500
//    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
//    var exception = exceptionHandlerPathFeature.Error;

//    await context.Response.WriteAsJsonAsync(new { error = "hello" });
//}));
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();