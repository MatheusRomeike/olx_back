using Domain.Anuncio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Mensagem
{
    public class MensagemDto
    {
        //public int AnuncioId { get; set; }
        //public int UsuarioId { get; set; }
        public string Texto { get; set; }
        public string Tipo { get; set; }
        public string Autor { get; set; }
        public DateTime DataCriacao { get; set; }

    }
}
