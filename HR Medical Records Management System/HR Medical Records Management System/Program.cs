using FluentValidation.AspNetCore;
using HR_Medical_Records_Management_System.Context;
using HR_Medical_Records_Management_System.Dtos.Request;
using HR_Medical_Records_Management_System.Models;
using HR_Medical_Records_Management_System.Repositories.Implementation;
using HR_Medical_Records_Management_System.Repositories.Interfaces;
using HR_Medical_Records_Management_System.Responses;
using HR_Medical_Records_Management_System.Services.Implementation;
using HR_Medical_Records_Management_System.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//scopes 
builder.Services.AddScoped<IBaseRepository<TMedicalRecord, int>, MedicalRecordRepositoryImpl>();
builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordRepositoryImpl>();
builder.Services.AddScoped<IBaseService<BaseResponse<TMedicalRecord>, int, PostMedicalRecordDto,UpdateMedicalRecordDto, DeleteMedicalRecordDto>, MedicalRecordServiceImpl>();
builder.Services.AddScoped<IMedicalRecordService, MedicalRecordServiceImpl>();


//Config of DbContext
builder.Services.AddDbContext<HRMedicalRecordsContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Config of AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//Config of FluentValidation
builder.Services.AddFluentValidation((opt) =>
{
    opt.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
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
