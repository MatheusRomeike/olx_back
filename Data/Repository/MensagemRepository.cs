
using Data.Migrations;
using Domain.Dtos.Mensagem;
using Domain.Mensagem;
using Domain.Mensagem.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class MensagemRepository : BaseRepository<Mensagem>, IMensagemRepository
    {
        public List<ConversasDto> GetConversas(int usuarioId)
        {
            var ultimasMensagens = context.Database.SqlQueryRaw<ConversasDto>(@"
                       SELECT
                         m.""UsuarioId"", 
                         m.""AnuncioId"", 
                         m.""Texto"", 
                         m.""DataCriacao"",
                         CONCAT('https://olx-bucket-free.s3.amazonaws.com/adimages/', m.""AnuncioId"", '/1') as FotoAnuncio,                        
                         a.""Titulo"" as TituloAnuncio,
                         u.""Nome"" as AutorMensagem
                     FROM ""Mensagem"" m
                     JOIN (
                         SELECT ""UsuarioId"", ""AnuncioId"", MAX(""SequenciaMensagem"") AS MaxSequenciaMensagem
                         FROM ""Mensagem""
                         GROUP BY ""UsuarioId"", ""AnuncioId""
                     ) AS ult ON m.""UsuarioId"" = ult.""UsuarioId"" AND m.""AnuncioId"" = ult.""AnuncioId"" AND m.""SequenciaMensagem"" = ult.MaxSequenciaMensagem
                     JOIN ""Anuncio"" a ON m.""AnuncioId"" = a.""AnuncioId""
                     LEFT JOIN ""Usuario"" u ON u.""UsuarioId"" = m.""UsuarioAutorId""
                     WHERE m.""UsuarioId"" = {0} OR a.""UsuarioId"" = {0}
                ", usuarioId).ToList();

            return ultimasMensagens;
        }
    }
}
