using DataAccess.Data;
using DataAccess.Models;
using BusinessLogic.Extensions;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using System;

namespace homework_8.Controllers
{ 
    public class CartController : Controller
    {
        public readonly CarDbContext _context;
        public readonly SessionData _sessionData;

        public CartController(CarDbContext context, SessionData sessionData)
        {
            _context = context;
            _sessionData = sessionData;
        }

        public IActionResult Index()
        {
            Dictionary<int, int> idsAndQuantities = _sessionData.GetCartData();
            List<int> ids = idsAndQuantities.Keys.ToList();
            DbSet<Car> cars = _context.Cars;
            IIncludableQueryable<Car, Category> carsWithCat = cars.Include(car => car.Category)!;
            List<Car> carsInCart = null!;

            if (carsWithCat != null)
            {
                carsInCart = carsWithCat.Where(car => ids.Contains(car.Id)).ToList();
            }


            return View(carsInCart);
        }

        public IActionResult Add(int id)
        {
            //    Dictionary<int, int> idsAndQuantities = _sessionData.GetCartData();

            //    Invoke(null!, id, controllerName: "Cars");

            //    return RedirectToAction("Index", "Home");
            Dictionary<int, int> idsAndQuantities = _sessionData.GetCartData();

            if (!idsAndQuantities.ContainsKey(id))
            {
                idsAndQuantities.Add(id, 1);
            }

  


            _sessionData.SetCartData(idsAndQuantities);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Remove(int id)
        {
            //Invoke(RemoveCarIdFromCart, id);

            //return RedirectToAction("Index");
            Dictionary<int, int> idsAndQuantities = _sessionData.GetCartData();

            if (!idsAndQuantities.ContainsKey(id))
            {
                idsAndQuantities.Add(id, 1);
            }

            idsAndQuantities = RemoveCarIdFromCart(id, idsAndQuantities);


            _sessionData.SetCartData(idsAndQuantities);

            return RedirectToAction("Index");
        }

        public IActionResult PlusProductQuantity(int id)
        {
            //Invoke(PlusQuantity, id);

            //return RedirectToAction("Index");
            Dictionary<int, int> idsAndQuantities = _sessionData.GetCartData();

            if (!idsAndQuantities.ContainsKey(id))
            {
                idsAndQuantities.Add(id, 1);
            }

            idsAndQuantities = PlusQuantity(id, idsAndQuantities);


            _sessionData.SetCartData(idsAndQuantities);

            return RedirectToAction("Index");
        }

        public IActionResult MinusProductQuantity(int id)
        {

            //return Invoke(MinusQuantity, id);

            //return RedirectToAction("Index");
            Dictionary<int, int> idsAndQuantities = _sessionData.GetCartData();

            if (!idsAndQuantities.ContainsKey(id))
            {
                idsAndQuantities.Add(id, 1);
            }

            idsAndQuantities = MinusQuantity(id, idsAndQuantities);


            _sessionData.SetCartData(idsAndQuantities);

            return RedirectToAction("Index");
        }

        //private IActionResult Invoke(Func<int, Dictionary<int, int>, Dictionary<int, int>> func, int id, string controllerName = "Cart")
        //{
        //    Dictionary<int, int> idsAndQuantities = _sessionData.GetCartData();

        //    if (!idsAndQuantities.ContainsKey(id))
        //    {
        //        idsAndQuantities.Add(id, 1);
        //    }

        //    if (func != null)
        //    {
        //        idsAndQuantities = func.Invoke(id, idsAndQuantities);
        //    }

        //    _sessionData.SetCartData(idsAndQuantities);

        //    return RedirectToAction("Index", controllerName);
        //}

        private Dictionary<int, int> RemoveCarIdFromCart(int id, Dictionary<int, int> idsAndQuantities)
        {
            idsAndQuantities.Remove(id);

            return idsAndQuantities;
        }

        private Dictionary<int, int> PlusQuantity(int id, Dictionary<int, int> idsAndQuantities)
        {
            idsAndQuantities[id] = ++idsAndQuantities[id];

            return idsAndQuantities;
        }

        private Dictionary<int, int> MinusQuantity(int id, Dictionary<int, int> idsAndQuantities)
        {
            if (idsAndQuantities[id] != 1)
            {
                idsAndQuantities[id] = --idsAndQuantities[id];
            }

            return idsAndQuantities;
        }
    }
}
