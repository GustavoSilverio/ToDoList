using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("/api/tarefas")]

    public class ToDoLIstController : ControllerBase
    {

        private ITarefaService _tarefaService;

        public ToDoLIstController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }


        [HttpGet]
        [Route("obter-todas")]
        public async Task<ActionResult<IAsyncEnumerable<Tarefa>>> ObterTodas()
        {
            try
            {
                var tarefas = await _tarefaService.ObterTodas();
                return Ok(tarefas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("obter-por-id/{id}")]

        public async Task<ActionResult<Tarefa>> ObterPorId(int id)
        {
            try
            {
                if (id > 0)
                {
                    var tarefaPorId = await _tarefaService.ObterPorId(id);
                    return Ok(tarefaPorId);
                }else
                {
                    throw new ArgumentException("O Id não pode ser nulo", nameof(id));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("criar-task")]
        public async Task<ActionResult<Tarefa>> CriarTarefa(Tarefa tarefa)
        {
            try
            {
                if(tarefa == null)
                {
                    throw new ArgumentException("É necessário enviar algo na requisição", nameof(tarefa));
                }

                await _tarefaService.CriarTarefa(tarefa);
                return Ok(tarefa);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("atualizar-task/{id}")]
        public async Task<ActionResult<Tarefa>> AtualizarTarefa(int id, Tarefa tarefa)
        {
            try
            {
                if (id > 0 && tarefa != null)
                {
                    await _tarefaService.AtualizarTask(id, tarefa);
                    return Ok(tarefa);
                }else
                { 
                    throw new ArgumentException("É necessário enviar o Id e o corpo da requisição com todos os dados da tarefa:", nameof(id));
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpDelete]
        [Route("excluir-task/{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                if (id > 0)
                {
                    _tarefaService.ExcluirTarefa(id);
                    return Ok(id);
                }
                else
                    throw new ArgumentException("O Id não pode ser nulo:", nameof(id));


            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
