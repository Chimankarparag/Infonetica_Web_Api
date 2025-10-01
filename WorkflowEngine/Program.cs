using WorkflowEngine.Services;
using WorkflowEngine.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register dependencies
builder.Services.AddSingleton<IWorkflowStorage, InMemoryWorkflowStorage>();
builder.Services.AddScoped<IWorkflowService, WorkflowService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // Using Swagger for API testing in development
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();