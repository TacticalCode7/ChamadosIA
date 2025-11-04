using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChamadosIA.Services
{
    public class IAssistenteService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "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"; // 
        private readonly string _endpoint = "https://api.openai.com/v1/chat/completions";

        public IAssistenteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // IA sugere solução com base em texto livre (descrição do problema)
        public async Task<string> SugerirSolucaoPorTextoAsync(string prompt)
        {
            var requestBody = new
            {
                model = "gpt-4",
                messages = new[]
                {
                    new { role = "system", content = "Você é um assistente técnico que sugere soluções simples para problemas descritos por usuários." },
                    new { role = "user", content = prompt }
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            var response = await _httpClient.PostAsync(_endpoint, content);
            var result = await response.Content.ReadAsStringAsync();

            dynamic json = JsonConvert.DeserializeObject(result);
            return json?.choices?[0]?.message?.content ?? "Não foi possível gerar uma sugestão.";
        }

        // IA sugere solução com base em seleção de serviço, categoria e subcategoria
        public async Task<string> SugerirSolucaoAsync(string servico, string categoria, string subcategoria)
        {
            var prompt = $"Usuário selecionou: Serviço = {servico}, Categoria = {categoria}, Subcategoria = {subcategoria}. Sugira uma solução simples que ele possa tentar.";
            return await SugerirSolucaoPorTextoAsync(prompt);
        }
    }
}
