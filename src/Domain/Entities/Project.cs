using Domain.Common;
using Domain.Enums;
using System;

namespace Domain.Entities
{
    public class Project : EntityBase<int>
    {
        public string ProjectNumber { get; set; }
        public decimal Price { get; set; }
        public Priority Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompletedOn { get; set; }
    }
}