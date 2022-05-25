﻿using AlunosApi.Models;
using AlunosApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlunosApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase {

        private IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService) {
            _alunoService = alunoService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos() {

            try {

                var alunos = await _alunoService.GetAlunos();
                return Ok(alunos);
            }
            catch {
                // return BadRequest("Requisição inválida");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos");
            }
        }

        [HttpGet("AlunosPorNome")]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunosByName([FromQuery] string nome) {

            try {

                var alunos = await _alunoService.GetAlunosByNome(nome);

                if (alunos == null)
                    return NotFound($"Não existe aluno com o critério {nome}");
                
                return Ok(alunos);
            }
            catch {

                return BadRequest("Requisição inválida");          
            }

        }

        [HttpGet("{id:int}", Name= "GetAluno")]
        public async Task<ActionResult<Aluno>> GetAluno(int id) {

            try {
                var aluno = await _alunoService.GetAluno(id);

                if (aluno == null)
                    return NotFound($"Não existe aluno com o id = {id}");
                return Ok(aluno);
            }
            catch {

                return BadRequest("Requisição inválida");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Aluno aluno) {

            try {

                await _alunoService.CreateAluno(aluno);
                return CreatedAtRoute(nameof(GetAluno), new { id = aluno.Id }, aluno);
            }
            catch {

                return BadRequest("Requisição inválida");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Edit(int id, [FromBody] Aluno aluno) {

            try {

                if(aluno.Id == id) {

                    await _alunoService.UpdateAluno(aluno);
                    //return NoContent();
                    return Ok($"Aluno com id= {id} foi atualizado com sucesso");
                }
                else {
                    return BadRequest("Dados inconsistentes");
                }
            }
            catch {

                return BadRequest("Requisição inválida");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) {

            try {
                var aluno = await _alunoService.GetAluno(id);

                if (aluno != null) {
                    await _alunoService.DeleteAluno(aluno);
                    return Ok($"Aluno de id= {id} foi excluido com sucesso");
                }
                else {
                    return NotFound($"Aluno com o id={id} não encontrado");
                }
            }
            catch {

                return BadRequest("Requisição inválida");
            }
        }
    }

 }

