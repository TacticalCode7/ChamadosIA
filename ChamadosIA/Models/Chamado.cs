using System;

namespace ChamadosIA.Models
{
    public enum StatusChamado
    {
        EmAberto,
        EmAtendimento,
        Resolvido,
        Fechado
    }

    public class Chamado
    {
        public int ID_Chamado { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string ClienteEmail { get; set; } = string.Empty;
        public string? TelefoneContato { get; set; }
        public string? EnderecoAtendimento { get; set; }
        public string? Setor { get; set; }
        public string? Observacoes { get; set; }

        public StatusChamado Status { get; set; } = StatusChamado.EmAberto;
        public DateTime DataAbertura { get; set; } = DateTime.Now;
        public DateTime? DataAtendimento { get; set; }
        public DateTime? DataResolucao { get; set; }

        public string? TecnicoResponsavel { get; set; }
        public string? Solucao { get; set; }
    }
}