using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffManagers.Models
{
    public class DepartamentoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartamentoId { get; set; }
        [Required(ErrorMessage = "O campo Nome do Departamento é obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo Sigla do Departamento é obrigatório.")]
        public string Sigla { get; set; }


        public List<FuncionarioModel> Funcionarios { get; set; } = new List<FuncionarioModel>();
    }
}
