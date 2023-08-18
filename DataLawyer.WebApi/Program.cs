using DataLawyer.WebApi;

var builder = Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(web =>
{
    var config = Configuracao.Carregue();
    web.UseStartup<Startup>();
    web.UseUrls(config.UseUrls.ToArray());

    var caminho = Directory.GetCurrentDirectory();
#if DEBUG
    // Em modo DEBUG, obtemos a configuração da pasta de Debug, isto para não precisar alterar o arquivo do projeto.
    var caminhoDebug = Path.Combine("bin", "Debug", $"net{Environment.Version.Major}.{Environment.Version.Minor}");
    if (!caminho.Contains(caminhoDebug)) caminho = Path.Combine(caminho, caminhoDebug);
    web.UseContentRoot(caminho);
#endif
});

var app = builder.Build();
app.Run();