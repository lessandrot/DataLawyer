using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using DataLawyer.Dominio;

namespace DataLawyer.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected IActionResult Execute(Action acao)
    {
        try
        {
            PrepareExecucao(acao.Method);

            acao.Invoke();

            FinalizeExecucao();

            return Ok("OK");
        }
        catch (Exception ex)
        {
            return Erro(acao.Method, ex);
        }
    }

    protected IActionResult Execute<T>(Func<T> acao)
    {
        try
        {
            PrepareExecucao(acao.Method);

            var resultado = acao.Invoke();
            if (resultado is null)
            {
                var mensagem = "Não encontrado";
                Console.WriteLine(mensagem);
                return NotFound(mensagem);
            }

            FinalizeExecucao();

            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return Erro(acao.Method, ex);
        }
    }

    private void FinalizeExecucao() => Console.WriteLine($"[{DateTime.Now.ToShortDateTime()}] Executado com sucesso");

    private void PrepareExecucao(MethodInfo acao) => Console.WriteLine($"[{DateTime.Now.ToShortDateTime()}] {NomeDoMetodo(acao)}...");

    private string NomeDoMetodo(MethodInfo acao) => Regex.Match(acao.Name, @"<(\w+)>").Groups[1].Value;

    private IActionResult Erro(MethodInfo acao, Exception ex)
    {
        if (ex is ApplicationException)
        {
            Console.WriteLine(ex.Message);
            return Ok(ex.Message); // exceções da aplicação
        }

        var mensagem = $"Falha em {NomeDoMetodo(acao)}: {ex.FullMessageWithStack()}";
        Console.WriteLine(mensagem);

        return Problem(mensagem);
    }
}