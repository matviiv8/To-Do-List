using System.ComponentModel.DataAnnotations;

namespace To_Do_List.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; } 
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}