using Artec3DSample.Models.DTO.Enums;
using System;

namespace Artec3DSample.Models.DTO
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public TaskItemStatus Status { get; set; }
        public string Description { get; set; }
    }
}
