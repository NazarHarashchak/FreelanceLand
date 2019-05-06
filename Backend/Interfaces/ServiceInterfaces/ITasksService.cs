using Backend.DTOs;
using System.Collections.Generic;
using FreelanceLand.Models;
using System.Threading.Tasks;
using Backend.Pagination;
using Backend.Models;

namespace Backend.Interfaces.ServiceInterfaces
{
    public interface ITasksService
    {
        Task<PagedList<TaskDTO>> GetHistoryTaskByUser(int id, int page, string search, int priceTo, int priceFrom, string[] categ);
        Task<int?> GetCustomerAsync(int id);
        Task<int?> GetExecutorAsync(int id);
        System.Threading.Tasks.Task DeleteTask(int id);
        Task<PagedList<TaskDTO>> GetActiveTaskByUser(int id, int page, string search, int priceTo, int priceFrom, string[] categ);
        Task<PagedList<TaskDTO>> GetTasks(int page, string searchText, int priceTo, int priceFrom, string[] categ);
        Task<PagedList<TaskDTO>> GetCreatedTaskByUser(int id, int page, string search, int priceTo, int priceFrom, string[] categ);
        Task<IEnumerable<TaskDTO>> GetCreatedTaskByUser(int id);
        Task<IEnumerable<TaskDTO>> GetActiveTaskByUserAsync(int id);
        Task<IEnumerable<TaskDTO>> DragAndDropTaskByCustomer(int taskId, int customerId,
                              string secondStatus);
        Task<IEnumerable<TaskDTO>> DragAndDropTaskByExecutorAsync(int taskId, int executorId,
                              string secondStatus);
        Task<IEnumerable<TaskDTO>> GetTopHistoryTaskForUser(int id);
        Task<IEnumerable<TaskDTO>> GetTopActiveTaskForUser(int id);
        Task<Ratings> RateUser(int UserId, int RateByUser, int Mark, int UserStatusId);
    }
}
