using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffManagers.App.Data;
using StaffManagers.Models;
using System.Text.RegularExpressions;

namespace StaffManagers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FuncionarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetFuncionarios()
        {
            try
            {
                var funcionarios = await _context.Funcionarios
                    .ToListAsync();

                return Ok(funcionarios);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Ocorreu um erro ao recuperar os funcionários.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFuncionario(int id)
        {
            try
            {
                var funcionario = await _context.Funcionarios
                    .FirstOrDefaultAsync(f => f.Id == id);

                if (funcionario == null)
                {
                    return NotFound();
                }

                return Ok(funcionario);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Ocorreu um erro ao recuperar o funcionário.");
            }
        }


        [HttpPost]
        public async Task<IActionResult> CriarFuncionario([FromBody] FuncionarioModel funcionario)
        {
            try
            {
                if (funcionario == null || !ValidarFuncionario(funcionario))
                {
                    return BadRequest(new { error = "Dados inválidos para criar o funcionário." });
                }

                _context.Funcionarios.Add(funcionario);
                await _context.SaveChangesAsync();

                return Ok(new { status = "Funcionário criado com sucesso." });
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Ocorreu um erro ao criar o funcionário.");
            }
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> AtualizarFuncionario(int id, [FromBody] FuncionarioModel funcionarioAtualizado)
        {
            try
            {
                if (funcionarioAtualizado == null || id != funcionarioAtualizado.Id)
                {
                    return BadRequest(new { error = "Dados inválidos para atualizar o funcionário." });
                }

                var funcionarioExistente = await _context.Funcionarios.FindAsync(id);

                if (funcionarioExistente == null)
                {
                    return NotFound();
                }

                funcionarioExistente.Nome = funcionarioAtualizado.Nome;
                funcionarioExistente.RG = funcionarioAtualizado.RG;
                funcionarioExistente.Cargo = funcionarioAtualizado.Cargo;
                funcionarioExistente.DataDeNascimento = funcionarioAtualizado.DataDeNascimento;
                funcionarioExistente.DepartamentoId = funcionarioAtualizado.DepartamentoId;

                _context.Entry(funcionarioExistente).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new { status = "Funcionário atualizado com sucesso." });
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Ocorreu um erro ao atualizar o funcionário.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> ExcluirFuncionario(int id)
        {
            try
            {
                var funcionarioExistente = await _context.Funcionarios.FindAsync(id);

                if (funcionarioExistente == null)
                {
                    return NotFound();
                }

                _context.Funcionarios.Remove(funcionarioExistente);

                var historicoFuncionario = new HistoricoFuncionarioModel
                {
                    Nome = funcionarioExistente.Nome,
                    RG = funcionarioExistente.RG,
                    Cargo = funcionarioExistente.Cargo,
                    DataNascimento = funcionarioExistente.DataDeNascimento,
                    DepartamentoId = funcionarioExistente.DepartamentoId,
                    DataExclusao = DateTime.UtcNow
                };

                _context.HistoricoFuncionarios.Add(historicoFuncionario);


                await _context.SaveChangesAsync();

                return Ok(new
                {
                    status = "Funcionário excluído com sucesso."
                });
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Ocorreu um erro ao excluir o funcionário.");
            }
        }

        private bool ValidarFuncionario(FuncionarioModel funcionario)
        {
            var regexNome = new Regex(@"^[a-zA-ZÀ-Úâêôõçíúãêôõçíú\s]+$");
            var regexCargo = new Regex(@"^[a-zA-ZÀ-Úâêôõçíúãêôõçíú\s\-]+$");
            var regexRG = new Regex(@"^[0-9]{9}$");

            if (
                string.IsNullOrWhiteSpace(funcionario.Nome) ||
                string.IsNullOrWhiteSpace(funcionario.Cargo) ||
                string.IsNullOrWhiteSpace(funcionario.RG) ||
                !regexNome.IsMatch(funcionario.Nome) ||
                !regexCargo.IsMatch(funcionario.Cargo) ||
                !regexRG.IsMatch(funcionario.RG)
            )
            {
                return false;
            }

            var departamentoExistente = _context.Departamentos.Find(funcionario.DepartamentoId);
            return departamentoExistente != null;
        }
    }

}

