using Backend.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Interfaces.ServiceInterfaces
{
    public interface ICommentsService
    {
        Task<IEnumerable<CommentDTO>> GetComments(int taskId);
        Task<IEnumerable<CommentDTO>> AddComment(CommentDTO comment);
        Task DeleteComment(int id);
    }
}
