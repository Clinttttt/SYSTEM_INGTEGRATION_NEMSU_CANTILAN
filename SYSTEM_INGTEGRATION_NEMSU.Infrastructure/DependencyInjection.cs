using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.CommandHandlers;
using SYSTEM_INGTEGRATION_NEMSU.Application.External;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Command;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Data;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Respositories;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Services;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = configuration["AppSettings:Issuer"],
                ValidateAudience = true,
                ValidAudience = configuration["AppSettings:Audience"],
                ValidateLifetime = true,

                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["AppSettings:Token"]!)),
                ValidateIssuerSigningKey = true,
                RoleClaimType = ClaimTypes.Role,
                NameClaimType = ClaimTypes.Name
            });

            TypeAdapterConfig<EnrollmentCourse, EnrollCourseDto>
            .NewConfig()
            .Map(dest => dest.CourseCode, src => src.Course.CourseCode)
            .Map(dest => dest.Title, src => src.Course.Title)
            .Map(dest => dest.Unit, src => src.Course.Unit);
           
            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IHandlingCourse, HandlingCourse>();
            services.AddScoped<IEnrollmentServices, EnrollmentServices>();
            services.AddScoped<IPaymentServices, PaymentServices>();
            services.AddScoped<IUserRespository, UserRespository>();
            services.AddScoped<IHandlingMessage, HandlingMessage>();
            services.AddScoped<IRespondCommand, RespondCommand>();
            services.AddScoped<IStudentRecordCommand, StudentRecordCommand>();
            services.AddScoped<IFacultyRecordCommand, FacultyRecordCommand>();
            services.AddScoped<IHandlingStudents, HandlingStudents>();
            services.AddScoped<IHandlingDepartment, HandlingDepartment>();
         
            return services;
        }
    }
}
