using DataLawyer.WebApi;

var builder = Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(web =>
{
    var config = Configuracao.Carregue();
    web.UseStartup<Startup>();
    web.UseUrls(config.UseUrls.ToArray());
});

var app = builder.Build();
app.Run();