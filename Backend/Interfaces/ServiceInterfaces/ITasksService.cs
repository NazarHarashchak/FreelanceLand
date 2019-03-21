using Backend.DTOs;
using System.Collections.Generic;
using FreelanceLand.Models;
using System.Threading.Tasks;

namespace Backend.Interfaces.ServiceInterfaces
{
    public interface ITasksService
    {
        Task<IEnumerable<TaskDTO>> GetToDoEntities();
        IEnumerable<TaskDTO> GetHistoryTaskByUser(int id);
        System.Threading.Tasks.Task DeleteTask(int id);
    }
}
