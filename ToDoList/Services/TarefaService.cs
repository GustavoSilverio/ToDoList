using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Context;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class TarefaService : ITarefaService
    {

        private readonly AppDbContext _dbContext;

        public TarefaService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Tarefa>> ObterTodas() => await _dbContext.Tasks.ToListAsync();

        public async Task<Tarefa> ObterPorId(int Id)
        {
            try
            {
                return await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == Id);
            }
            catch (Exception ex)
            {
                throw new Exception("o Id passado não existe no banco de dados id:" + Id);
            }
        }

        public async Task<Tarefa> CriarTarefa(Tarefa tarefa)
        {
            tarefa.IsDone = false;
            await _dbContext.AddAsync(tarefa);
            _dbContext.SaveChanges();
            
            return tarefa;
        }
        public async Task<Tarefa> AtualizarTask(int id, Tarefa tarefa)
        {
            Tarefa tarefaSelecionada = await ObterPorId(id);

            if (tarefaSelecionada != null)
            {
                tarefaSelecionada.Title = tarefa.Title;
                tarefaSelecionada.Description = tarefa.Description;
                tarefaSelecionada.IsDone = tarefa.IsDone;

                _dbContext.Update(tarefaSelecionada);
                _dbContext.SaveChanges();

                return tarefaSelecionada;
            }
            else
                throw new ArgumentException("O id não existe no banco de dados:", nameof(id));

        }


        public async Task<Tarefa> ExcluirTarefa(int id)
        {
            Tarefa tarefaSelecionada = await ObterPorId(id);

            if (tarefaSelecionada != null)
            {
            _dbContext.Remove(tarefaSelecionada);
            await _dbContext.SaveChangesAsync();
            
            return tarefaSelecionada;
            }
            else
                throw new ArgumentException("O id não existe no banco de dados:", nameof(id));

        }

    }
}
