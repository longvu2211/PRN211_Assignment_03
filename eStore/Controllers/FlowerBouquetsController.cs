using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories;

namespace eStore.Controllers
{
    public class FlowerBouquetsController : Controller
    {
        //private readonly FUFlowerBouquetManagementV4Context _context;

        //public FlowerBouquetsController(FUFlowerBouquetManagementV4Context context)
        //{
        //    _context = context;
        //}

        IFlowerRepo _flowerRepo = new FlowerRepo();
        // GET: FlowerBouquets
        public IActionResult Index()
        {
            return View(_flowerRepo.GetAllFlowers());
        }

        // GET: FlowerBouquets/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flowerBouquet = _flowerRepo.GetFlowerById((int)id);
            if (flowerBouquet == null)
            {
                return NotFound();
            }

            return View(flowerBouquet);
        }

        // GET: FlowerBouquets/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_flowerRepo.GetCategories(), "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(_flowerRepo.GetSuppliers(), "SupplierId", "SupplierName");
            return View();
        }

        // POST: FlowerBouquets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlowerBouquetId,CategoryId,FlowerBouquetName,Description,UnitPrice,UnitsInStock,FlowerBouquetStatus,SupplierId,Morphology")] FlowerBouquet flowerBouquet)
        {
            if (ModelState.IsValid)
            {
                _flowerRepo.AddAFlower(flowerBouquet);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_flowerRepo.GetCategories(), "CategoryId", "CategoryName", flowerBouquet.CategoryId);
            ViewData["SupplierId"] = new SelectList(_flowerRepo.GetSuppliers(), "SupplierId", "SupplierName", flowerBouquet.SupplierId);
            return View(flowerBouquet);
        }

        // GET: FlowerBouquets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flowerBouquet = _flowerRepo.GetFlowerById((int)id);
            if (flowerBouquet == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_flowerRepo.GetCategories(), "CategoryId", "CategoryName", flowerBouquet.CategoryId);
            ViewData["SupplierId"] = new SelectList(_flowerRepo.GetSuppliers(), "SupplierId", "SupplierName", flowerBouquet.SupplierId);
            return View(flowerBouquet);
        }

        // POST: FlowerBouquets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FlowerBouquetId,CategoryId,FlowerBouquetName,Description,UnitPrice,UnitsInStock,FlowerBouquetStatus,SupplierId,Morphology")] FlowerBouquet flowerBouquet)
        {
            if (id != flowerBouquet.FlowerBouquetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(flowerBouquet);
                    //await _context.SaveChangesAsync();
                    _flowerRepo.UpdateAFlower(flowerBouquet);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlowerBouquetExists(flowerBouquet.FlowerBouquetId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_flowerRepo.GetCategories(), "CategoryId", "CategoryName", flowerBouquet.CategoryId);
            ViewData["SupplierId"] = new SelectList(_flowerRepo.GetSuppliers(), "SupplierId", "SupplierName", flowerBouquet.SupplierId);
            return View(flowerBouquet);
        }

        // GET: FlowerBouquets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flowerBouquet = _flowerRepo.GetFlowerById((int)id);
            if (flowerBouquet == null)
            {
                return NotFound();
            }

            return View(flowerBouquet);
        }

        // POST: FlowerBouquets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _flowerRepo.DeleteAFlower(id);
            return RedirectToAction(nameof(Index));
        }

        private bool FlowerBouquetExists(int id)
        {
            bool check = false;
            check = _flowerRepo.GetAllFlowers().Any(f => f.FlowerBouquetId == id);
            return check;
            //return _context.FlowerBouquets.Any(e => e.FlowerBouquetId == id);
        }
    }
}
