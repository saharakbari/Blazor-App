using Shared.Enums;

namespace Shared.DTO
{
    public class TaskItemDTO
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public StatusTask Status { get; set; }
        public string DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
