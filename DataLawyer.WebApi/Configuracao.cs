namespace DataLawyer.WebApi;
internal sealed class Configuracao
{
    public IEnumerable<string> UseUrls { get; private set; } = new HashSet<string>();

    private Configuracao() { }

    public static Configuracao Carregue()
    {
        var caminho = Directory.GetCurrentDirectory();
#if DEBUG
        // Em modo DEBUG, obtemos a configuração da pasta de Debug, isto para não precisar alterar o arquivo do projeto.
        var caminhoDebug = Path.Combine("bin", "Debug", $"net{Environment.Version.Major}.{Environment.Version.Minor}");
        if (!caminho.Contains(caminhoDebug)) caminho = Path.Combine(caminho, caminhoDebug);
#endif

        var config = new ConfigurationBuilder().SetBasePath(caminho).AddJsonFile("appsettings.json").Build();
        var sessao = config.GetSection("WebApi");

        var configuracao = new Configuracao
        {
            UseUrls = sessao.GetSection("UseUrls").Value.Split(";")
        };

        return configuracao;
    }
}