using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Projeto.Data.Contracts;
using Projeto.Data.Repositories;
using AutoMapper;

namespace Projeto.Services
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //configurando o uso do automapper
            services.AddAutoMapper(typeof(Startup));

            //mapeamento da injeção de dependêcia do projeto
            var connectionString = Configuration.GetConnectionString("Aula");

            services.AddTransient<IFuncionarioRepository, FuncionarioRepository>
                (map => new FuncionarioRepository(connectionString));

            //incluindo a configuração para o Swagger
            services.AddSwaggerGen(
                swagger =>
                {
                    swagger.SwaggerDoc("v1", new Info
                    {
                        Title = "Api de controle de funcionários",
                        Version = "v1",
                        Description = "Projeto demo para controle de funcionários"
                    });
                }
                );

            //incluindo a configuração do CORS
            services.AddCors(c => c.AddPolicy("DefaultPolicy",
                builder =>
                {
                    builder.AllowAnyOrigin(); //Hosts
                    builder.AllowAnyHeader(); // cabeçalho da requisição
                    builder.AllowAnyMethod(); // POST, PUT, DELETE, GET, etc...
                }
                ));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // configuração para o Swagger
            app.UseSwagger();
            app.UseSwaggerUI(
                swagger =>
                {
                    swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Projeto");
                }
                );

            app.UseMvc();

            app.UseCors("DefaultPolicy");
        }
    }
}
