using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Data;
using MyPortfolio.Models;
using MyPortfolio.Repository.IRepository;
using PagedList;
using Stripe.Checkout;
using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;
using System.Diagnostics;

namespace MyPortfolio.Controllers
{
    [Route("Brand")]
    public class BrandController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        private IUnitOfWork _UnitOfWork;
        public BrandController(IUnitOfWork UnitOfWork, ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            _UnitOfWork = UnitOfWork;
        }

        public IActionResult Index(string searchQuery, int? page)
        {

            //var data = _context.Brands.AsQueryable();
            var data = _UnitOfWork.Brandy.GetAll();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                data = data.Where(p =>
                    p.brand.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                    p.MobileModel.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                    p.UsedYears.ToString().Contains(searchQuery, StringComparison.OrdinalIgnoreCase)
                );
            }

            int pageNumber = page ?? 1;
            int pageSize = 7;

            var pagedData = data.ToPagedList(pageNumber, pageSize);

            ViewBag.HasNextPage = pagedData.HasNextPage;
            ViewBag.HasPreviousPage = pagedData.HasPreviousPage;
            ViewBag.TotalPages = pagedData.PageCount;

            return View(pagedData);
        }



        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insert(Brand obj)
        {
            //_context.Brands.Add(obj);
            //_context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var branddata = _UnitOfWork.Brandy.GetBYId(u => u.Id == id);
            return View(branddata);
        }

        [HttpPost]
        public IActionResult Edit(Brand obj)
        {
            _UnitOfWork.Brandy.Update(obj);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var branddata = _UnitOfWork.Brandy.GetBYId(u => u.Id == id);
            return View(branddata);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            Brand deletedata = _UnitOfWork.Brandy.GetBYId(u => u.Id == id);
            _UnitOfWork.Brandy.Remove(deletedata);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
      

    }
}
