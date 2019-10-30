using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Projeto.Data.Contracts;
using Projeto.Data.Entities;
using Projeto.Services.Models;
using Projeto.Services.Validations;
using AutoMapper;
using Microsoft.AspNetCore.Cors;

namespace Projeto.Services.Controllers
{
    [EnableCors("DefaultPolicy")]
    [Route("api/[controller]")]
    public class FuncionarioController : Controller
    {
        [HttpPost]
        public IActionResult Post(
            [FromServices] IFuncionarioRepository repository,
            [FromServices] IMapper mapper, 
            [FromBody] FuncionarioCadastroModel model
            )
        {
            if (!ModelState.IsValid) return StatusCode(400, ValidationUtil.GetErrors(ModelState));

            try
            {
                var funcionario = mapper.Map<Funcionario>(model);
                funcionario.DataAdmissao = model.DataAdmissao;

                repository.Inserir(funcionario);

                //retornar um status http 200 (ok)

                return Ok("Funcionário cadastrado com sucesso.");
            }
            catch (Exception e)
            {
                //retornar um status HTTP 500 
                return StatusCode(500, "Internal Server error");
            }
        }

        [HttpPut]
        public IActionResult Put([FromServices] IFuncionarioRepository repository,
            [FromServices] IMapper mapper,
            [FromBody] FuncionarioEdicaoModel model)
        {

            if (!ModelState.IsValid) return StatusCode(400, ValidationUtil.GetErrors(ModelState)); 

            try
            {
                //verificar se o funcionário existe na base de dados
                var funcionario = repository.ObterPorId(model.IdFuncionario);
                if (funcionario != null)
                {
                    funcionario = mapper.Map<Funcionario>(model);
                    repository.Alterar(funcionario);

                    return Ok("Funcionário atualizado com sucesso.");
                }
                else
                {
                    return StatusCode(422, "Funcionário não encontrado.");
                }
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{idFuncionario}")]
        public IActionResult Delete(
            [FromServices] IFuncionarioRepository repository, 
            int idFuncionario )
        {
            try
            {
                //verificar se o funcionário foi encontrado
                var funcionario = repository.ObterPorId(idFuncionario);
                if (funcionario != null)
                {
                    repository.Excluir(idFuncionario);
                    return StatusCode(204, "Funcionario excluído com sucesso");
                }
                else
                {
                    return StatusCode(422, "Funcionario não encontrado.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<FuncionarioConsultaModel>), 200)]
        public IActionResult GetAll(
            [FromServices] IFuncionarioRepository repository,
            [FromServices] IMapper mapper
            )
        {
            try
            {
                var model = mapper.Map<List<FuncionarioConsultaModel>>(repository.ObterTodos());

                if (model != null && model.Count > 0)
                {
                    return Ok(model);
                }
                else
                {
                    return StatusCode(204); //Nocontent
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{idFuncionario}")]
        //ProducesResponseType gera a model de entrada no swagger
        [ProducesResponseType(typeof(FuncionarioConsultaModel), 200)]
        public IActionResult Get(
            [FromServices] IFuncionarioRepository repository,
            [FromServices] IMapper mapper,
            int idFuncionario
            )
        {
            try
            {
                var model = mapper.Map<FuncionarioConsultaModel>(repository.ObterPorId(idFuncionario));

                if (model != null)
                {
                    return Ok(model);
                }
                else
                {
                    return StatusCode(204); //Nocontent
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}