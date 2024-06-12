using System;
using System.ComponentModel.DataAnnotations;

namespace APITeste.Models
{
    public class Produto
    {
        public Guid Id { get; set; }
        public Guid CategoriaId { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "O nome do produto deve ter entre 5 e 20 caracteres.")]
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Estoque { get; set; }
        public DateTime DataCadastrada { get; set; }


        public Produto()
        {
            Id = Guid.NewGuid();
            DataCadastrada = DateTime.Now;
            Estoque = 0;
        }
    }
}
