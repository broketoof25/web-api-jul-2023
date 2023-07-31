
namespace EmployeesHrApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //above this is configuration for teh "behind the scenes stuff"
            var app = builder.Build();
            //everything after this is setting up Middleware - that's the code that receives the HTTP request and makes the response

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(); //code that will let you get the documentation
                app.UseSwaggerUI(); //middleware that provides the UI for the documentation
            }

            app.UseAuthorization();


            app.MapControllers(); //when we are doing controller based apis this is where it creates the lookup table (route table)

            app.Run(); //this is when the api is up and running and it BLOCKS here
        }
    }
}