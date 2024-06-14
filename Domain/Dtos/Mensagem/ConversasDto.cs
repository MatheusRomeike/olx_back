using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Mensagem;

public class ConversasDto
{
    public int AnuncioId { get; set; }
    public int UsuarioId { get; set; }
    public string FotoAnuncio { get; set; }
    public string TituloAnuncio { get; set; }
    public string AutorMensagem { get; set; }
    public string Texto { get; set; }
    public DateTime DataCriacao { get; set; }
}
