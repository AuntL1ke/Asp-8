namespace DataAccess.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public short Year { get; set; }
        public string? ImagePath { get; set; }
     

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
