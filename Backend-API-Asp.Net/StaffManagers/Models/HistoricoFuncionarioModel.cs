using System.ComponentModel.DataAnnotations;

namespace StaffManagers.Models
{
    public class HistoricoFuncionarioModel
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }

        public string RG { get; set; }

        public string Cargo { get; set; }

        public DateTime DataNascimento { get; set; }

        public int DepartamentoId { get; set; }

        public DateTime DataExclusao { get; set; }
    }
}
