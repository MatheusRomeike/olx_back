using Domain.Dtos.Mensagem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMensagemService
    {
        List<MensagemDto> List(int anuncioId, int usuarioInteressadoId, int usuarioId);
    }
}
