using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace StaffManagers.Models
{
    public class FuncionarioModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; }

        public string Foto { get; set; }
        [Required(ErrorMessage = "O campo RG é obrigatório.")]
        public string RG { get; set; }
        [Required(ErrorMessage = "O campo Cargo é obrigatório.")]
        public string Cargo { get; set; }
        public DateTime DataDeNascimento { get; set; }

        [ForeignKey("Departamento")]
        public int DepartamentoId { get; set; }



    }
}
