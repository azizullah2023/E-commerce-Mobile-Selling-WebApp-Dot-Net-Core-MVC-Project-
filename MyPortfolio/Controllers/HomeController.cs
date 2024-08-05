
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Data;
using MyPortfolio.Models;
using PagedList.Mvc;
using PagedList;
using System.Diagnostics;
using System.Linq;
using MyPortfolio.Repository.IRepository;
using Stripe.Checkout;

namespace MyPortfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        private IUnitOfWork _UnitOfWork;
        public HomeController(IUnitOfWork UnitOfWork, ILogger<HomeController> logger, ApplicationDbContext context)
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
            var branddata = _UnitOfWork.Brandy.GetBYId(u=>u.Id==id);
            return View(branddata);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            Brand deletedata = _UnitOfWork.Brandy.GetBYId(u=>u.Id==id);
            _UnitOfWork.Brandy.Remove(deletedata);
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Payment(int Id)
        {
            var row = _UnitOfWork.mobileSelling.GetBYId(u => u.Id == Id);
            var ProductName = row.FullName;
            var Description = row.TAAccessories;
            var Price = row.Price * 100;
            var rowfrostripe = _context.Stripes.OrderByDescending(p => p.Id).FirstOrDefault();
            var options = new SessionCreateOptions
            {
                Mode = "payment",
                SuccessUrl = Url.Action("Success", "Home", new { id = rowfrostripe.Id }, Request.Scheme),
                CancelUrl = Url.Action("Cancel", "Home", null, Request.Scheme),

                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions> {
                new SessionLineItemOptions{
                PriceData=new SessionLineItemPriceDataOptions{
                Currency="usd",
                UnitAmount=(long)Price,
                ProductData =new SessionLineItemPriceDataProductDataOptions{

                Name=ProductName,
                Description=Description,
                Metadata=new Dictionary<string, string>{
                    { "product_id",Id.ToString()}
                },
                }
                },
               Quantity=1,


                },


                },
            };
            
            var service = new SessionService();
           
            Session session = service.Create(options);
            _UnitOfWork.striper.UpdateStripeRecord(Id, session.Id, session.PaymentIntentId);
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        public IActionResult Success(int id)
        {
            var service = new SessionService();
            var row = _context.Stripes.FirstOrDefault(u => u.Id == id);

            Session session = service.Get(row.SessionI);
            if (session.PaymentStatus.ToLower() == "paid")
            {
                var paymentIntentId = session.PaymentIntentId;
                _UnitOfWork.striper.UpdationStripeRecord(id, session.Id, paymentIntentId);
            }
            return View();
        }

        public IActionResult Cancel()
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
