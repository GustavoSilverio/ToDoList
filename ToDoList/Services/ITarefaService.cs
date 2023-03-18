using ToDoList.Models;

namespace ToDoList.Services
{
    public interface ITarefaService
    {
        Task<IEnumerable<Tarefa>> ObterTodas();
        Task<Tarefa> ObterPorId(int Id);
        Task<Tarefa> CriarTarefa(Tarefa tarefa);
        Task<Tarefa> AtualizarTask(int id, Tarefa tarefa);
        Task<Tarefa> ExcluirTarefa(int id);
    }
}
