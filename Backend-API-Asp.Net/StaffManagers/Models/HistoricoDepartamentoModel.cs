using System.ComponentModel.DataAnnotations;

namespace StaffManagers.Models
{
    public class HistoricoDepartamentoModel
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Sigla { get; set; }

        public DateTime DataExclusao { get; set; }
    }
}
