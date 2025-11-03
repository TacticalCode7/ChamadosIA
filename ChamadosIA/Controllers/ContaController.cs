using Microsoft.AspNetCore.Mvc;
using ChamadosIA.Models;
using System.Security.Cryptography;
using System.Text;

namespace ChamadosIA.Controllers
{
    public class ContaController : Controller
    {
        // Simulação de usuários em memória
        private static List<Usuario> usuarios = new List<Usuario>
        {
            new Usuario
            {
                Id = 1,
                Email = "tecnico@cati.com",
                Telefone = "11999999999",
                Setor = "Suporte",
                Tipo = "Tecnico",
                SenhaHash = GerarHash("123456")
            },
            new Usuario
            {
                Id = 2,
                Email = "cliente@cati.com",
                Telefone = "11888888888",
                Setor = "Financeiro",
                Tipo = "Cliente",
                SenhaHash = GerarHash("123456")
            }
        };

        private static Usuario? usuarioLogado;

        // GET: Login
        public IActionResult Login() => View();

        // POST: Login
        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var senhaHash = GerarHash(senha);

            usuarioLogado = usuarios.FirstOrDefault(u => u.Email == email && u.SenhaHash == senhaHash);

            if (usuarioLogado != null)
            {
                HttpContext.Session.SetInt32("UsuarioId", usuarioLogado.Id);

                if (usuarioLogado.Tipo == "Tecnico")
                    return RedirectToAction("Dashboard", "Tecnico");
                else
                    return RedirectToAction("Dashboard", "Cliente");
            }

            ViewBag.Erro = "Credenciais inválidas";
            return View();
        }

        // GET: AtualizarConta
        [HttpGet]
        public IActionResult AtualizarConta()
        {
            var id = HttpContext.Session.GetInt32("UsuarioId");
            if (id == null || usuarioLogado == null || id != usuarioLogado.Id)
                return RedirectToAction("Login");

            var modelo = new Usuario
            {
                Email = usuarioLogado.Email,
                Telefone = usuarioLogado.Telefone,
                Setor = usuarioLogado.Setor
            };

            return View(modelo);
        }

        // POST: AtualizarConta
        [HttpPost]
        public IActionResult AtualizarConta(Usuario dados)
        {
            var id = HttpContext.Session.GetInt32("UsuarioId");
            if (id == null || usuarioLogado == null || id != usuarioLogado.Id)
                return RedirectToAction("Login");

            if (!string.IsNullOrEmpty(dados.NovaSenha))
            {
                if (dados.NovaSenha != dados.ConfirmarSenha)
                {
                    TempData["Erro"] = "? As senhas não coincidem.";
                    return View(dados);
                }

                usuarioLogado.SenhaHash = GerarHash(dados.NovaSenha);
            }

            usuarioLogado.Email = dados.Email;
            usuarioLogado.Telefone = dados.Telefone;
            usuarioLogado.Setor = dados.Setor;

            TempData["Sucesso"] = "? Dados atualizados com sucesso!";
            return RedirectToAction("Confirmacao");
        }

        public IActionResult Confirmacao()
        {
            ViewBag.Mensagem = TempData["Sucesso"] ?? TempData["Erro"];
            return View();
        }

        private static string GerarHash(string senha)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
            return Convert.ToBase64String(bytes);
        }
    }
}
