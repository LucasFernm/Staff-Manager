using Microsoft.EntityFrameworkCore;
using StaffManagers.Models;

namespace StaffManagers.App.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<DepartamentoModel> Departamentos { get; set; }
        public DbSet<FuncionarioModel> Funcionarios { get; set; }
        public DbSet<HistoricoDepartamentoModel> HistoricoDepartamentos { get; set; }
        public DbSet<HistoricoFuncionarioModel> HistoricoFuncionarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DepartamentoModel>()
                .HasMany(d => d.Funcionarios)
                .WithOne()
                .HasForeignKey(f => f.DepartamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }



        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Deleted)
                {
                    if (entry.Entity is DepartamentoModel)
                    {

                        var historicoDepartamento = new HistoricoDepartamentoModel
                        {
                            Nome = entry.OriginalValues["Nome"].ToString(),
                            Sigla = entry.OriginalValues["Sigla"].ToString(),
                            DataExclusao = DateTime.UtcNow
                        };

                        HistoricoDepartamentos.Add(historicoDepartamento);
                    }
                    else if (entry.Entity is FuncionarioModel)
                    {

                        var historicoFuncionario = new HistoricoFuncionarioModel
                        {
                            Nome = entry.OriginalValues["Nome"].ToString(),
                            RG = entry.OriginalValues["RG"].ToString(),
                            Cargo = entry.OriginalValues["Cargo"].ToString(),
                            DataNascimento = (DateTime)entry.OriginalValues["DataDeNascimento"],
                            DepartamentoId = (int)entry.OriginalValues["DepartamentoId"],
                            DataExclusao = DateTime.UtcNow
                        };

                        HistoricoFuncionarios.Add(historicoFuncionario);
                    }

                    entry.State = EntityState.Detached;
                }
            }

            return base.SaveChanges();
        }
    }
}
