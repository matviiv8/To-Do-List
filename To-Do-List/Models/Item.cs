namespace To_Do_List.Models
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }      
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


    }
}