namespace Backend.DTOs
{
    public class TaskDTO
    {
        public int Id { get; set; }

        public int Price { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string DateAdded { get; set; }

        public string TaskCategoryName { get; set; }

        public int CommentsCount  { get; set; }

        public string TaskStatus { get; set; }

        public int ExcecutorId { get; set; }
    }
}
