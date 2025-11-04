using Microsoft.AspNetCore.Mvc;
using ChamadosIA.Models;
using ChamadosIA.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChamadosIA.Controllers
{
    public class ChamadoController : Controller
    {
        // Simulação de banco de dados em memória
        private static List<Chamado> chamados = new();

        private readonly IAssistenteService _iaService;

        // Construtor com injeção do serviço de IA
        public ChamadoController(IAssistenteService iaService)
        {
            _iaService = iaService;
        }

        // Tela de abertura de chamado
        public IActionResult Abrir()
        {
            return View();
        }

        // Recebe e salva o chamado
        [HttpPost]
        public IActionResult Abrir(Chamado chamado)
        {
            chamado.ID_Chamado = chamados.Count + 1;
            chamados.Add(chamado);
            return RedirectToAction("MeusChamados");
        }

        // Lista de chamados do cliente
        public IActionResult MeusChamados()
        {
            var lista = chamados.Where(c => c.ClienteEmail == "cliente@email.com").ToList(); // Simulação
            return View(lista);
        }

        // Chamados em aberto (fila técnica)
        public IActionResult Fila()
        {
            var fila = chamados.Where(c => c.Status == StatusChamado.EmAberto).ToList();
            return View(fila);
        }

        // Chamados em atendimento
        public IActionResult Atendimento()
        {
            var emAtendimento = chamados.Where(c => c.Status == StatusChamado.EmAtendimento).ToList();
            return View(emAtendimento);
        }

        // Chamados resolvidos
        public IActionResult Resolvidos()
        {
            var resolvidos = chamados.Where(c => c.Status == StatusChamado.Resolvido).ToList();
            return View(resolvidos);
        }

        // Tela de edição de chamado
        public IActionResult Editar(int id)
        {
            var chamado = chamados.FirstOrDefault(c => c.ID_Chamado == id);
            return View(chamado);
        }

        // Atualiza dados do chamado
        [HttpPost]
        public IActionResult Editar(Chamado chamadoAtualizado)
        {
            var chamado = chamados.FirstOrDefault(c => c.ID_Chamado == chamadoAtualizado.ID_Chamado);
            if (chamado != null)
            {
                chamado.Observacoes = chamadoAtualizado.Observacoes;
                chamado.TelefoneContato = chamadoAtualizado.TelefoneContato;
                chamado.EnderecoAtendimento = chamadoAtualizado.EnderecoAtendimento;
            }
            return RedirectToAction("MeusChamados");
        }

        // Cancela o chamado
        public IActionResult Cancelar(int id)
        {
            var chamado = chamados.FirstOrDefault(c => c.ID_Chamado == id);
            if (chamado != null)
            {
                chamado.Status = StatusChamado.Fechado;
            }
            return RedirectToAction("MeusChamados");
        }

        // Sugestão via IA com base em classificação
        [HttpPost]
        public async Task<IActionResult> SugestaoIA([FromBody] ProblemaDTO problema)
        {
            var sugestao = await _iaService.SugerirSolucaoAsync(problema.Servico, problema.Categoria, problema.Subcategoria);
            return Json(new { sugestao });
        }

        // Sugestão via IA com base na descrição do chamado
        [HttpPost]
        public async Task<IActionResult> SugestaoPorDescricao([FromBody] DescricaoDTO input)
        {
            var prompt = $"Usuário descreveu o problema: \"{input.Descricao}\". Sugira uma solução simples que o próprio usuário possa tentar.";
            var sugestao = await _iaService.SugerirSolucaoPorTextoAsync(prompt);
            return Json(new { sugestao });
        }
    }
}
