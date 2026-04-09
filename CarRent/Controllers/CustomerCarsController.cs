using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRent.Data;
using CarRent.Models;

namespace CarRent.Controllers
{
    public class CustomerCarsController : Controller
    {
        private readonly DataContext _context;

        public CustomerCarsController(DataContext context)
        {
            _context = context;
        }

        // GET: CustomerCars
        public async Task<IActionResult> Index()
        {
            
            var favorites = _context.CustomerCars
                .Include(cc => cc.Customer)
                .Include(cc => cc.Car);

            return View(await favorites.ToListAsync());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFavorite(int customerId, int carId)
        {
            
            var exists = await _context.CustomerCars
                .AnyAsync(cc => cc.CustomerId == customerId && cc.CarId == carId);

            if (!exists)
            {
                var favorite = new CustomerCar
                {
                    CustomerId = customerId,
                    CarId = carId
                };

                _context.CustomerCars.Add(favorite);
                await _context.SaveChangesAsync();
            }


            return RedirectToAction("Index", "Rentals");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFavorite(int customerId, int carId)
        {
            var favorite = await _context.CustomerCars
                .FirstOrDefaultAsync(f => f.CustomerId == customerId && f.CarId == carId);

            if (favorite != null)
            {
                _context.CustomerCars.Remove(favorite);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


    }
}