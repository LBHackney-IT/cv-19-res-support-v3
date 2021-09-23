using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.Infrastructure;
using cv19ResSupportV3.V3.UseCase;
using cv19ResSupportV3.V3.UseCase.Interfaces;
using cv19ResSupportV3.V4.UseCase;
using cv19ResSupportV3.V4.UseCase.Interface;
using cv19ResSupportV3.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace cv19ResSupportV3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private const string ApiName = "cv-19-resident-support";

        // This method gets called by the runtime. Use this method to add services to the container.
        public static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc(setupAction =>
                {
                    setupAction.EnableEndpointRouting = false;
                }).AddJsonOptions(jsonOptions =>
                {
                    jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            var versions = new List<ApiVersion>();
            versions.Add(new ApiVersion(3, 0));
            //            versions.Add(new ApiVersion(4, 0));

            foreach (var apiVersion in versions)
            {
                services.AddApiVersioning(o =>
                {
                    o.DefaultApiVersion = apiVersion;
                    o.AssumeDefaultVersionWhenUnspecified =
                        true; // assume that the caller wants the default version if they don't specify
                    o.ApiVersionReader =
                        new UrlSegmentApiVersionReader(); // read the version number from the url segment header)
                });
            }

            services.AddSingleton<IApiVersionDescriptionProvider, DefaultApiVersionDescriptionProvider>();

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Token",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Your Hackney API Key",
                        Name = "X-Api-Key",
                        Type = SecuritySchemeType.ApiKey
                    });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Token" }
                        },
                        new List<string>()
                    }
                });

                //Looks at the APIVersionAttribute [ApiVersion("x")] on controllers and decides whether or not
                //to include it in that version of the swagger document
                //Controllers must have this [ApiVersion("x")] to be included in swagger documentation!!
                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    apiDesc.TryGetMethodInfo(out var methodInfo);

                    var versions = methodInfo?
                        .DeclaringType?.GetCustomAttributes()
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions).ToList();

                    return versions?.Any(v => $"{v.GetFormattedApiVersion()}" == docName) ?? false;
                });

                //Get every ApiVersion attribute specified and create swagger docs for them
                foreach (var apiVersion in versions)
                {
                    var version = $"v{apiVersion.ToString()}";
                    c.SwaggerDoc(version,
                        new OpenApiInfo
                        {
                            Title = $"{ApiName}-api {version}",
                            Version = version,
                            Description =
                                $"{ApiName} version {version}. Please check older versions for deprecated endpoints."
                        });
                }

                c.CustomSchemaIds(x => x.FullName);
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                    c.IncludeXmlComments(xmlPath);
            });
            ConfigureDbContext(services);
            RegisterGateways(services);
            RegisterUseCases(services);
        }

        private static void ConfigureDbContext(IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

            if (connectionString != null && !connectionString.Contains("CommandTimeout")) { connectionString += $";CommandTimeout=900"; }

            services.AddDbContext<HelpRequestsContext>(
                opt => opt.UseNpgsql(connectionString, options => { options.CommandTimeout(900); }));
        }

        private static void RegisterGateways(IServiceCollection services)
        {
            services.AddScoped<IHelpRequestGateway, HelpRequestGateway>();
            services.AddScoped<IHelpRequestCallGateway, HelpRequestCallGateway>();
            services.AddScoped<IResidentGateway, ResidentGateway>();
            services.AddScoped<ICaseNotesGateway, CaseNotesGateway>();
        }

        private static void RegisterUseCases(IServiceCollection services)
        {
            services.AddScoped<ICreateResidentAndHelpRequestUseCase, CreateResidentAndHelpRequestUseCase>();
            services.AddScoped<ICreateHelpRequestUseCase, CreateHelpRequestUseCase>();
            services.AddScoped<IUpdateResidentAndHelpRequestUseCase, UpdateResidentAndHelpRequestUseCase>();
            services.AddScoped<IPatchResidentAndHelpRequestUseCase, PatchResidentAndHelpRequestUseCase>();
            services.AddScoped<IPatchResidentUseCase, V3.UseCase.PatchResidentUseCase>();
            services.AddScoped<IPatchHelpRequestUseCase, PatchHelpRequestUseCase>();
            services.AddScoped<IGetResidentsAndHelpRequestsUseCase, GetResidentsAndHelpRequestsUseCase>();
            services.AddScoped<IGetResidentAndHelpRequestUseCase, GetResidentAndHelpRequestUseCase>();
            services.AddScoped<IGetCallbacksUseCase, GetCallbacksUseCase>();
            services.AddScoped<IGetLookupsUseCase, GetLookupsUseCase>();
            services.AddScoped<ICreateHelpRequestUseCase, CreateHelpRequestUseCase>();
            services.AddScoped<ICreateHelpRequestCallUseCase, CreateHelpRequestCallUseCase>();
            services.AddScoped<ICreateResidentUseCase, CreateResidentUseCase>();
            services.AddScoped<IPatchCaseNoteUseCase, PatchCaseNoteUseCase>();
            services.AddScoped<IUpdateResidentUseCase, UpdateResidentUseCase>();
            services.AddScoped<IUpdateHelpRequestUseCase, UpdateHelpRequestUseCase>();
            services.AddScoped<ICreateCaseNoteUseCase, CreateCaseNoteUseCase>();
            services.AddScoped<IUpdateCaseNoteUseCase, UpdateCaseNoteUseCase>();
            services.AddScoped<IUpdateStaffAssignmentsUseCase, UpdateStaffAssignmentsUseCase>();
            services.AddScoped<IGetResidentsUseCase, GetResidentsUseCase>();
            services.AddScoped<ISearchResidentsUseCase, SearchResidentsUseCase>();
            services.AddScoped<V4.UseCase.Interfaces.IPatchResidentUseCase, V4.UseCase.PatchResidentUseCase>();
            services.AddScoped<IGetResidentHelpRequestsUseCase, GetResidentHelpRequestsUseCase>();
            services.AddScoped<IGetResidentHelpRequestUseCase, GetResidentHelpRequestUseCase>();
            services.AddScoped<ICreateResidentHelpRequestUseCase, CreateResidentHelpRequestUseCase>();
            services.AddScoped<IPatchResidentHelpRequestUseCase, PatchResidentHelpRequestUseCase>();
            services.AddScoped<IGetCaseNotesByResidentIdUseCase, GetCaseNotesByResidentIdUseCase>();
            services.AddScoped<IGetCaseNotesByHelpRequestIdUseCase, GetCaseNotesByHelpRequestIdUseCase>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            env.EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //Get All ApiVersions,
            var api = app.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
            var apiVersions = api.ApiVersionDescriptions.ToList();

            //Swagger ui to view the swagger.json file
            app.UseSwaggerUI(c =>
            {
                foreach (var apiVersionDescription in apiVersions)
                {
                    //Create a swagger endpoint for each swagger version
                    c.SwaggerEndpoint($"{apiVersionDescription.GetFormattedApiVersion()}/swagger.json",
                        $"{ApiName}-api {apiVersionDescription.GetFormattedApiVersion()}");
                }
            });
            app.UseSwagger();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                // SwaggerGen won't find controllers that are routed via this technique.
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
