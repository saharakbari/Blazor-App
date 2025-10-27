using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TaskItem
    {
    
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public StatusTask Status { get; set; }
        // تاریخ سررسید
        public DateTime? DueDate { get; set; }
        // تاریخ ایجاد وظیفه
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        // تاریخ آخرین به‌روزرسانی
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

    public enum StatusTask
    {
        Pending = 1,
        InProgress = 2,
        Done = 3

    }
}
