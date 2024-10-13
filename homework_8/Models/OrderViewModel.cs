using DataAccess.Models;

namespace homework_8.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice {  get; set; }
        public List<Car> Cars { get; set; }
    }
}
