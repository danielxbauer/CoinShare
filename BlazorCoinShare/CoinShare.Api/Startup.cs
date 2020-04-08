using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoinShare.Api.Dtos;
using CoinShare.Api.Shared.Dtos;
using CoinShare.Core.Logic;
using CoinShare.Core.Logic.Impl;
using CoinShare.Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoinShare.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin();
            }));

            services.AddControllers();

            // Auto Mapper configuration
            services.AddSingleton(ConfigureMapper());

            // Logic registration
            var mockDataLogic = new MockDataLogic();

            services
                .AddSingleton<IGroupLogic>(mockDataLogic)
                .AddSingleton<ITransactionLogic>(mockDataLogic)
                .AddSingleton<IPersonLogic>(mockDataLogic);
        }

        public static IMapper ConfigureMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Group, GroupDto>();
                cfg.CreateMap<GroupDto, Group>()
                    .ForMember(dest => dest.Id, c => c.MapFrom(src => src.Id == null ? Guid.Empty : (Guid)src.Id));

                cfg.CreateMap<Person, PersonDto>();
                cfg.CreateMap<PersonDto, Person>()
                    .ForMember(dest => dest.GroupId, c => c.MapFrom(src => src.GroupId == null ? Guid.Empty : (Guid)src.GroupId));

                cfg.CreateMap<Transaction, TransactionDto>();
                cfg.CreateMap<TransactionDto, Transaction>()
                    .ForMember(dest => dest.GroupId, c => c.MapFrom(src => src.GroupId == null ? Guid.Empty : (Guid)src.GroupId));

                cfg.CreateMap<PersonOverview, PersonOverviewDto>();
            })
            .CreateMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
