using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRent.Data;
using CarRent.Models;

namespace CarRent.Controllers
{
    public class RentalsController : Controller
    {
        private readonly DataContext _context;

        public RentalsController(DataContext context)
        {
            _context = context;
        }

        [Route("rentals/MyNiceCustomers/Index")]
        public async Task<IActionResult> Index()
        {
            var rentals = _context.tblRentals
                .Include(r => r.Car)
                .Include(r => r.Customer);
            return View(await rentals.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var rental = await _context.tblRentals
                .Include(r => r.Car)
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.RentalId == id);

            if (rental == null)
                return NotFound();

            return View(rental);
        }

        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.tblCustomers, "CustomerId", "FullName");
            ViewData["CarId"] = new SelectList(_context.tblCars, "CarId", "Brand");

            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rental rental)
        {
            if (ModelState.IsValid)
            {
                _context.tblRentals.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            
            ViewData["CustomerId"] = new SelectList(_context.tblCustomers, "CustomerId", "FullName", rental.CustomerId);
            ViewData["CarId"] = new SelectList(_context.tblCars, "CarId", "Brand", rental.CarId);

            return View(rental);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var rental = await _context.tblRentals.FindAsync(id);
            if (rental == null)
                return NotFound();

            ViewBag.Customers = new SelectList(
                _context.tblCustomers,
                "CustomerId",
                "FullName",
                rental.CustomerId
            );

            ViewBag.Cars = new SelectList(
                _context.tblCars,
                "CarId",
                "Brand",
                rental.CarId
            );

            return View(rental);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Rental rental)
        {
            if (id != rental.RentalId)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            
            ViewBag.Customers = new SelectList(
                _context.tblCustomers,
                "CustomerId",
                "FullName",
                rental.CustomerId
            );

            ViewBag.Cars = new SelectList(
                _context.tblCars,
                "CarId",
                "Brand",
                rental.CarId
            );

            return View(rental);
        }


        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var rental = await _context.tblRentals
                .Include(r => r.Car)
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.RentalId == id);

            if (rental == null)
                return NotFound();

            return View(rental);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rental = await _context.tblRentals.FindAsync(id);
            if (rental != null)
            {
                _context.tblRentals.Remove(rental);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Report()
        {
            var report = _context.tblRentals
                .GroupBy(r => r.Car.Brand)
                .Select(g => new
                {
                    Brand = g.Key,
                    TotalRevenue = g.Sum(r => r.TotalPrice)
                })
                .ToList();

            return View(report);
        }

    }
}
