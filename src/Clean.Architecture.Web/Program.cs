using Clean.Architecture.Web;

var builder = WebApplication.CreateBuilder(args);

var app = builder.ConfigureServices()
                 .ConfigurePipeline();

StartupExtensions.CreateDatabase(app);

app.Run();

public partial class Program
{
}
