using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffManagers.App.Data;
using StaffManagers.Models;


namespace StaffManagers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepartamentoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartamentos()
        {
            try
            {
                var departamentos = await _context.Departamentos
                    .ToListAsync();

                return Ok(departamentos);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Ocorreu um erro ao recuperar os departamentos.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDepartamento(int id)
        {
            try
            {
                var departamento = await _context.Departamentos
                    .Include(d => d.Funcionarios)
                    .FirstOrDefaultAsync(d => d.DepartamentoId == id);

                if (departamento == null)
                {
                    return NotFound();
                }

                return Ok(departamento);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Ocorreu um erro ao recuperar o departamento.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CriarDepartamento([FromBody] DepartamentoModel departamento)
        {
            try
            {
                if (departamento == null || string.IsNullOrWhiteSpace(departamento.Nome) || string.IsNullOrWhiteSpace(departamento.Sigla))
                {
                    return BadRequest("Dados inválidos");
                }

                _context.Departamentos.Add(departamento);
                await _context.SaveChangesAsync();

                return Ok(new { status = "Departamento criado com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao criar o departamento.");
            }
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> AtualizarPropriedadesDoDepartamento(int id, [FromBody] DepartamentoModel departamentoAtualizado)
        {
            try
            {
                if (departamentoAtualizado == null || id != departamentoAtualizado.DepartamentoId)
                {
                    return BadRequest();
                }

                var departamentoExistente = await _context.Departamentos.FindAsync(id);

                if (departamentoExistente == null)
                {
                    return NotFound();
                }

                departamentoExistente.Nome = departamentoAtualizado.Nome;
                departamentoExistente.Sigla = departamentoAtualizado.Sigla;

                _context.Entry(departamentoExistente).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    status = "Departamento atualizado com sucesso."
                });
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Ocorreu um erro ao atualizar o departamento.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> ExcluirDepartamento(int id)
        {
            try
            {
                var departamentoExistente = await _context.Departamentos.FindAsync(id);

                if (departamentoExistente == null)
                {
                    return NotFound();
                }

                var historicoDepartamento = new HistoricoDepartamentoModel
                {
                    Nome = departamentoExistente.Nome,
                    Sigla = departamentoExistente.Sigla,
                    DataExclusao = DateTime.UtcNow
                };

                _context.HistoricoDepartamentos.Add(historicoDepartamento);

                _context.Departamentos.Remove(departamentoExistente);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    status = "Departamento excluído com sucesso."
                });
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Ocorreu um erro ao excluir o departamento.");
            }
        }


        private DepartamentoModel ValidarDepartamento(DepartamentoModel departamento)
        {
            if (string.IsNullOrWhiteSpace(departamento.Nome))
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(departamento.Sigla))
            {
                return null;
            }

            return departamento;
        }


    }
}
