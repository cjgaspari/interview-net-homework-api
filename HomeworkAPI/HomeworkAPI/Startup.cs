using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using HomeworkAPI.Authorization.Services;
using HomeworkAPI.Data.EFCore;
using HomeworkAPI.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace HomeworkAPI
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
      services.AddControllers();

      //Add Swagger components for API UI
      //This allows for easy debugging, without the use of PostMan
      //SwaggerUI also allows you to self document API
      //https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-5.0&tabs=visual-studio-mac
      services.AddSwaggerGen(doc =>
      {
        doc.SwaggerDoc("v1", new OpenApiInfo
        {
          Version = "v1",
          Title = "Homework API",
          Description = "This Homework API endpoint encompasses several endpoints for students and teachers." +
          "\n\n\nTo get started, use the Authorize button with the lock icon." +
          "\n\n\nTo test ***student endpoints***, type any username and password." +
          "\n\n\nTo test ***teacher endpoints***, use the following username: \"admin\" or \"teacher\" with any password." +
          "\n\n\nThis authentication code is stubbed to generalize securing specific endpoints.",
          Contact = new OpenApiContact
          {
            Name = "CJ Gaspari",
            Email = string.Empty,
            Url = new Uri("https://www.linkedin.com/in/cjgaspari"),
          }
        }); ;
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        doc.IncludeXmlComments(xmlPath);

        // add Basic Authentication
        var basicSecurityScheme = new OpenApiSecurityScheme
        {
          Type = SecuritySchemeType.Http,
          Scheme = "basic",
          Reference = new OpenApiReference { Id = "BasicAuth", Type = ReferenceType.SecurityScheme }
        };
        doc.AddSecurityDefinition(basicSecurityScheme.Reference.Id, basicSecurityScheme);
        doc.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {basicSecurityScheme, new string[] { }}
                });
      });

      services.AddScoped<HomeworkContext>();
      services.AddScoped<AssignmentRepository>();
      services.AddScoped<AttachmentRepository>();
      services.AddScoped<NoteRepository>();
      services.AddScoped<IUserService, UserService>();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseStaticFiles();

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

      app.UseSwagger();
      app.UseSwaggerUI(ui =>
      {
        ui.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        ui.RoutePrefix = string.Empty;
        ui.DefaultModelExpandDepth(0);
        //Inject stylesheet to remove Swagger logo using CSS
        ui.InjectStylesheet("/customizeSwagger.css");
      });

      //If the SQLite db does not exist, this piece of code will use the migrations to create it 
      using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
      {
        var context = serviceScope.ServiceProvider.GetRequiredService<HomeworkContext>();
        context.Database.Migrate();
      }
    }
  }
}
