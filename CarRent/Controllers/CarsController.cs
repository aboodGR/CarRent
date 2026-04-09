using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRent.Data;
using CarRent.Models;

namespace CarRent.Controllers
{
    public class CarsController : Controller
    {
        private readonly DataContext _context;

        public CarsController(DataContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index(string search, string sort)
        {
            ViewBag.CurrentSearch = search;
            ViewBag.CurrentSort = sort;

            var cars = _context.tblCars.AsQueryable();


            if (!string.IsNullOrEmpty(search))
            {
                cars = cars.Where(c => c.Model.Contains(search));
            }


            switch (sort)
            {
                case "price_asc":
                    cars = cars.OrderBy(c => c.Year);          
                    break;

                case "price_desc":
                    cars = cars.OrderByDescending(c => c.Year); 
                    break;

                default:
                    cars = cars.OrderBy(c => c.Brand);                
                    break;
            }

            return View(await cars.ToListAsync());
        }





        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var car = await _context.tblCars
                .FirstOrDefaultAsync(c => c.CarId == id);

            if (car == null)
                return NotFound();

            return View(car);
        }


        public IActionResult Create()
        {
            return View();
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarId,Brand,Model,Year,PricePerDay")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var car = await _context.tblCars.FindAsync(id);
            if (car == null)
                return NotFound();

            return View(car);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarId,Brand,Model,Year,PricePerDay")] Car car)
        {
            if (id != car.CarId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.tblCars.Any(c => c.CarId == car.CarId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var car = await _context.tblCars
                .FirstOrDefaultAsync(c => c.CarId == id);

            if (car == null)
                return NotFound();

            return View(car);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.tblCars.FindAsync(id);
            if (car != null)
            {
                _context.tblCars.Remove(car);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
