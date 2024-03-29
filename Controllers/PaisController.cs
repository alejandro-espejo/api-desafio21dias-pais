using System;
using api_desafio21dias.Servicos;
using Microsoft.AspNetCore.Mvc;
using api_desafio21dias.Models;
using MongoDB.Bson;

namespace api_desafio21dias.Controllers
{
    [ApiController]
    public class PaisController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PaisController(IConfiguration configuration)
        {
            this._configuration = configuration;
            this.paiMongoRepo = new PaisMongodb(_configuration);
        }

        private PaisMongodb paiMongoRepo;
        
        // GET: Pais - Aula 14 - 1:24
        [HttpGet]
        [Route("/pais")]
        public async Task<IActionResult> Index()
        {
            return StatusCode(200, await paiMongoRepo.Todos());
        }

        // GET: pais/5
        [HttpGet]
        [Route("/pais/{id}")]
        public async Task<IActionResult> Details(ObjectId id)
        {
            var pai = await paiMongoRepo.BuscaPorId(id);
            if (pai == null)
            {
                return NotFound();
            }
            return StatusCode(200, pai);
        }

        // POST: /pais
        [HttpPost]
        [Route("/pais")]
        public async Task<IActionResult> Create(Pai pai)
        {
            if (ModelState.IsValid)
            {
                if (!await AlunoServico.ValidarAluno(pai.AlunoId))
                {
                    return StatusCode(400, new { Mensagem = "O aluno passado não é válido ou não existe."});
                }
                paiMongoRepo.Inserir(pai);
                return StatusCode(201, pai);
            }
            return StatusCode(400, new { Mensagem = "O pai passado é inválido." });
        }

        // PUT: pais/5
        [HttpPut]
        [Route("/pais/{id}")]
        public async Task<IActionResult> Edit(ObjectId id, Pai pai)
        {
            if (ModelState.IsValid)
            {
                if (! await AlunoServico.ValidarAluno(pai.AlunoId))
                {
                    return StatusCode(400, new { Mensagem = "O aluno passado não é válido ou não está cadastrado." });
                }
                try
                {
                    pai.Id = id;
                    paiMongoRepo.Atualizar(pai);
                }
                catch (Exception erro)
                {
                    if (!await PaiExists(pai.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return StatusCode(500, erro);
                    }
                }
                return StatusCode(200, pai);
            }
            return StatusCode(200, pai);
        }
        
        // DELETE: pai/5
        [HttpDelete]
        [Route("/pais/{id}")]
        public IActionResult DeleteConfirmed(ObjectId id)
        {
            paiMongoRepo.RemovePorId(id);
            return StatusCode(204);
        }

        private async Task<bool> PaiExists(ObjectId id)
        {
            return (await paiMongoRepo.BuscaPorId(id)) != null;
        }
    }
}
