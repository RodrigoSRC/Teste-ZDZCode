using System;
using System.ComponentModel.DataAnnotations;

namespace APITeste.Models
{
    public class Categoria
    {
        public Guid Id { get; init; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "O nome da categoria deve ter entre 5 e 15 caracteres.")]
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public Categoria(string nome, string descricao)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
        }
    }
}
