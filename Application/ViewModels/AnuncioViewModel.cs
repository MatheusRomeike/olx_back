using Domain.Anuncio.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class AnuncioViewModel
    {
        [DefaultValueAttribute("1")]
        public int? AnuncioId { get; set; }

        [DefaultValueAttribute("1")]
        public int UsuarioId { get; set; }

        [DefaultValueAttribute("Macbook Air 5")]
        public string Titulo { get; set; }

        [DefaultValueAttribute("Macbook em bom estado de conservação. Possui 128gb de armazenamento.")]
        public string Descricao { get; set; }

        [DefaultValueAttribute("5000")]
        public decimal Preco { get; set; }

        [DefaultValueAttribute("10")]
        public int CategoriaId { get; set; }

        [DefaultValueAttribute("EstadoAnuncio.Ativo")]
        public EstadoAnuncio EstadoAnuncio { get; set; }

        [DefaultValueAttribute("01/01/2000")]
        public DateTime DataCriacao { get; set; }

        [DefaultValueAttribute("/9j/4AAQSkZJRgABAQAAAQABAAD/7QCEUGhvdG9zaG9wIDMuMAA4QklNBAQAAAAAAGgcAigAYkZCTUQwYTAwMGE2ZTAxMDAwMGRlMDEwMDAwNTkwMjAwMDA5MDAyMDAwMGNiMDIwMDAwMzEwMzAwMDA5ZjAzMDAwMGQ1MDMwMDAwMTEwNDAwMDA1MTA0MDAwMDFjMDUwMDAwAP/bAEMABgQFBgUEBgYFBgcHBggKEAoKCQkKFA4PDBAXFBgYFxQWFhodJR8aGyMcFhYgLCAjJicpKikZHy0wLSgwJSgpKP/bAEMBBwcHCggKEwoKEygaFhooKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKP/CABEIADMAMgMBIgACEQEDEQH/xAAbAAACAwEBAQAAAAAAAAAAAAAABAIDBQYBB//EABgBAAMBAQAAAAAAAAAAAAAAAAECAwAE/9oADAMBAAIQAxAAAAHatXmzZ6jS8+nqYovUgHoUx85qpKZ7aMJ9ejv5ldOfYKRpZU17AOa57qeaO+nq3sBpkx589YEirgg27G0BpgUH/8QAIBAAAgICAwEAAwAAAAAAAAAAAQIAAwQRBRASIRMjMf/aAAgBAQABBQIQTkq/y0can7+xC2o9xcU+qjjW++twGZdpEYFC7bUE47hvQ3AZcgsFhKqmRT7YBhTuloD1y5Kij7kzJQmD+DrlU9Y+EN5YMHQ6yPteCii4QTZn/8QAHREAAQQCAwAAAAAAAAAAAAAAAQACESEQEgMgIv/aAAgBAwEBPwFN4zqnNjDDaBgWj6yStotT1//EABoRAAMAAwEAAAAAAAAAAAAAAAABEBESMSH/2gAIAQIBAT8BG/Tsc5UYyaxCn//EACUQAAEDAwMDBQAAAAAAAAAAAAEAAhEQICEDEjFBUXIiQlJhcf/aAAgBAQAGPwKkdiCpG3A6C4gQvSUQbAwdUCvtNPtUjiuVkzCZDwZKg8LY4y08GxkFafkKNI/Fms/EytHyudPZMIGbP//EAB8QAQACAgICAwAAAAAAAAAAAAEAESExEEFhsVGR8f/aAAgBAQABPyHif86zBMDji3wyo4BtlXK/uZCtStRbm07l8xzc9ZeDN0swJtCbasMMXtd8j5aNJ1KhUWYzAyVFR2tx++HS+C5bUCo1Lk7fZAmTrWh5mjt3FBlwNhFC8OJyjiQ0MhU2lG7iiZ5p/9oADAMBAAIAAwAAABDiEoO60AwD0o+F58H/xAAaEQEBAQEBAQEAAAAAAAAAAAABABEhEDFh/9oACAEDAQE/EA2chOyPGyIC3fUwfvg5KvYTxaifp7//xAAaEQEBAQEBAQEAAAAAAAAAAAABEQAQITFh/9oACAECAQE/EMMuEFNDFSGlfDHr815GgR6+Hr//xAAhEAEAAgICAQUBAAAAAAAAAAABABEhMUFhURCBkaGx8P/aAAgBAQABPxB6izKRqkR0iyPssM3nqqtvVkSoJfxMJHLUBa+ICVBKcgeYVAsF2wQcvPKVZp+GJ9OMlyd+XW1Vfb9RzpQG0jXHUyeFBY5zEHwX3ye+zsh2y2DmWmuHwpm2v54gOsl0zR4Of2XPQRW7XGN7qViDQmQx2mzRE+PTVUz5jzqeS0CZl5Kr2O2sNb7lQ1TyFRT9bl9JqYeeZjPRr1fsDT+xSi2/4bfyabZamYUETRmJvcI9Qk84ZT5JqxqxiAZgtLO7P//Z")]
        public IFormFile? Foto { get; set; }

        [DefaultValueAttribute("5")]
        public int? SequenciaFoto { get; set; }

    }
}
