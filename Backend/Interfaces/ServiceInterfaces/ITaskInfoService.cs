using Backend.DTOs;
using System.Collections.Generic;

namespace Backend.Interfaces.ServiceInterfaces
{
    public interface ITaskInfoService
    {
        TaskPageDTO GetTaskDescription(int id);
        TaskPageDTO AddTask(TaskPageDTO task);
        ExcecutorDTO AddExcecutor(ExcecutorDTO user);
    }
}
