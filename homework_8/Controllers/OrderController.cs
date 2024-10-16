using DataAccess.Data;
using DataAccess.Models;
using homework_8.Models;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using BusinessLogic.Interfaces;

namespace homework_8.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly CarDbContext _context;
        private readonly IMailService _mailService;

        private readonly SessionData _sessionData;
        public OrderController(CarDbContext context, SessionData sessionData, IMailService mailService)
        {
            _context = context;
            _sessionData = sessionData;
            _mailService = mailService;
        }
        public IActionResult Index()
        {
            List<OrderViewModel> ordersIncludeCars = GetOrdersIncludeCars();
            return View(ordersIncludeCars);
        }
        private List<OrderViewModel> GetOrdersIncludeCars()
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            List<Order> orders = _context.GetOrderListByUserId(userId);
            List<Car> cars = _context.GetCarList();
            return orders.Select(order => GetOrderIncludeCars(order)).ToList();
        }
        private OrderViewModel GetOrderIncludeCars(Order order)
        {
            string ids = order.IdsProduct;
            List<Car> selectedCars = _context.GetCarsFromIdsString(ids);
            return new OrderViewModel()
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Cars = selectedCars
            };
        }
        public async Task<IActionResult> Create()
        {
            string? userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Order order = CreateOrder();
     
       
            HttpContext.Session.Clear();
            _context.AddOrder(order);
            string? userName = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var orders = _context.Orders.Where(o => o.UserId == userId).ToList(); ;
            string text = "";
            foreach (var item in orders)
            {
                text += $"<p>{item.Id} {item.OrderDate} {item.TotalPrice}</p>";
            }
         
            await _mailService.SendMailAsync("Your Order", text, userName!);

            return RedirectToAction("Index");
        }
        private Order CreateOrder()
        {
            string? userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Car> cars = _context.GetCarList();
            Dictionary<int, int> idsAndQuantities = _sessionData.GetCartData();
            string carIdsString = IdsAndQuantitiesToString(idsAndQuantities);
            List<Car> selectedCars = _context.GetCarsFromIdsString(carIdsString);
            return new Order()
            {
                OrderDate = DateTime.Now,
                IdsProduct = carIdsString,
                UserId = userId,
            };
        }
        private string IdsAndQuantitiesToString(Dictionary<int, int> idsAndQuantities)
        {
            if (idsAndQuantities == null)
            {
                return "";
            }
            List<string> carIdStrings = idsAndQuantities.Select(idAndQuantity => IdAndQuantityToString(idAndQuantity)).ToList();
            string ids = String.Join(", ", carIdStrings);
            return ids;
        }
        private string IdAndQuantityToString(KeyValuePair<int, int> idAndQuantity)
        {
            string prompt = $"{idAndQuantity.Key}";
            string res = prompt;
            for (int i = 1; i < idAndQuantity.Value; i++)
            {
                res += ", ";
                res += idAndQuantity.Key;
            }
            return res;
        }
        private void RemoveCarsFromStorage()
        {
            List<Car> cars = _context.GetCarList();
            Dictionary<int, int> idsAndQuantities = _sessionData.GetCartData();
            string carIdsString = IdsAndQuantitiesToString(idsAndQuantities);
            List<Car> selectedCars = _context.GetCarsFromIdsString(carIdsString);
  
        }
    }
}
