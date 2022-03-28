using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models
{
    [Table("Categorias")]//indica que esse modelo será persistido na tabela Categorias
    public class Categoria
    {
        public Categoria()
        {
            //Boa prática inicializar as Lists dos relacionamentos no ctor.
            Produtos = new Collection<Produto>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(80)]
        public string Nome { get; set; }
        [Required]
        [StringLength(300)]
        public string ImagemUrl { get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}
