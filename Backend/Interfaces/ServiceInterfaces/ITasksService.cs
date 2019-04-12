using Backend.DTOs;
using System.Collections.Generic;
using FreelanceLand.Models;
using System.Threading.Tasks;
using Backend.Pagination;

namespace Backend.Interfaces.ServiceInterfaces
{
    public interface ITasksService
    {
       // Task<IEnumerable<TaskDTO>> GetToDoEntities();
        Task<IEnumerable<TaskDTO>> GetHistoryTaskByUser(int id);
        System.Threading.Tasks.Task DeleteTask(int id);
        Task<IEnumerable<TaskDTO>> GetActiveTaskByUser(int id);
        Task<PagedList<TaskDTO>> GetTasks(int page, string searchText, int priceTo, int priceFrom, string[] categ);
    }
}
