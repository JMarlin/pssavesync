using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PSaveSync.Data;

//Provide shift-jis encoding
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services => services
        .AddDbContext<PSaveSyncDbContext>(options => options
            .UseMySQL(PSaveSyncDbContext.CONNECTION_STRING)) )
    .Build();

host.Run();
