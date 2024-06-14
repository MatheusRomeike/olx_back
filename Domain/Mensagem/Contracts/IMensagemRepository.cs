using Domain.Core.Contracts;
using Domain.Dtos.Mensagem;

namespace Domain.Mensagem.Contracts
{
    public interface IMensagemRepository : IBaseRepository<Mensagem>
    {
        List<ConversasDto> GetConversas(int usuarioId);
    }
}
