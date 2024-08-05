using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPortfolio.Data;
using MyPortfolio.Models;
using MyPortfolio.Repository.IRepository;
using MyPortfolio.Models.ViewModels;
using System.Collections.Generic;
using Stripe.BillingPortal;
using Stripe.Checkout;
using System.Net.Http;
using System.Text;
using Stripe;
using Stripe.FinancialConnections;
using SessionCreateOptions = Stripe.BillingPortal.SessionCreateOptions;
using Microsoft.AspNetCore.Identity;
namespace MyPortfolio.Controllers
{
    public class MobileSellingController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;
        public ViewModel1? ViewModel;
        private IUnitOfWork _UnitOfWork;
        public FormMobileSellIt formMobileSellIt { get; set; }
        public MobileSellingController(UserManager<IdentityUser> userManager ,IWebHostEnvironment webHostEnvironment, IUnitOfWork UnitOfWork, ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            _UnitOfWork = UnitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _userManager= userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {

            IEnumerable<SelectListItem> Brandfromdb = _UnitOfWork.Brandy.GetAll().Select(
               u => new SelectListItem
               {
                   Text = u.brand,
                   Value = u.Id.ToString(),
               }
                );
            ViewBag.Brandfromdb = Brandfromdb;
            return View();
        }




        [HttpPost]
        public IActionResult Index(FormMobileSellIt formMobileSellIt, IFormFile? file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string filepath = Path.Combine(wwwRootPath, "Images", "Mobiles");
                using (FileStream fileStream = new FileStream(Path.Combine(filepath, filename), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                    formMobileSellIt.ImageURL = "/Images/Mobiles/" + filename;
                }

                _UnitOfWork.mobileSelling.Add(formMobileSellIt);

            }
            return RedirectToAction(nameof(DisplayCustomer));
        }
        public async Task<IActionResult> DisplayCustomer(int itemid,int Id)
        {
           
            var CurrentUser = await _userManager.GetUserAsync(User);
            
            if (CurrentUser == null)
            {
                return Redirect("/Identity/Account/Login");
            }
            var rowFromLikes = _context.Likess.ToList();
            var viewModel = new ViewModel1
            {
                MobileSellIt = _UnitOfWork.mobileSelling.GetAll(IncludeProperties: "Brand"),
                Comment = _context.Comments.ToList(),
                rowsnumbr = rowFromLikes
            };
            
           

            
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult UpdateMobile(int id) {
            var row = _UnitOfWork.mobileSelling.GetBYId(u=>u.Id==id);

            IEnumerable<SelectListItem> Brandfromdb = _UnitOfWork.Brandy.GetAll().Select(
               u => new SelectListItem
               {
                   Text = u.brand,
                   Value = u.Id.ToString(),
               }
                );
            ViewBag.Brandfromdb = Brandfromdb;
            return View(row);
        }
        [HttpPost]
        public IActionResult UpdateMobile(FormMobileSellIt mobilesell,IFormFile? file)
        {
            /*if (!string.IsNullOrEmpty(formMobileSellIt.ImageURL))*/
            if (file!=null) {
            if(mobilesell.ImageURL!=null){
            var oldimagepath = Path.Combine(_webHostEnvironment.WebRootPath, mobilesell.ImageURL.TrimStart('/', '\\'));
                if (System.IO.File.Exists(oldimagepath)) {
                    System.IO.File.Delete(oldimagepath);
                }

            }
            var filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filelocation = Path.Combine(_webHostEnvironment.WebRootPath,"Images","Mobiles");
                using (FileStream filestream = new FileStream(Path.Combine(filelocation, filename), FileMode.Create))
                {
                    file.CopyTo(filestream);
                    mobilesell.ImageURL = "/Images/Mobiles/" + filename;
                }
            }
            _UnitOfWork.mobileSelling.Update(mobilesell);
                return RedirectToAction(nameof(DisplayCustomer));
        }
        [HttpGet]
        public IActionResult DeleteMobile(int id)
        {
            var row = _UnitOfWork.mobileSelling.GetBYId(u => u.Id == id);

            IEnumerable<SelectListItem> Brandfromdb = _UnitOfWork.Brandy.GetAll().Select(
               u => new SelectListItem
               {
                   Text = u.brand,
                   Value = u.Id.ToString(),
               }
                );
            ViewBag.Brandfromdb = Brandfromdb;
            return View(row);
        }

        [HttpPost,ActionName("DeleteMobile")]
        public IActionResult DeleteMobilePost(int id)
        {
            var row = _UnitOfWork.mobileSelling.GetBYId(u=>u.Id==id);
            if (row.ImageURL!=null) {
                var oldimagepath = Path.Combine(_webHostEnvironment.WebRootPath,row.ImageURL.TrimStart('/','\\'));
                if (System.IO.File.Exists(oldimagepath)) {
                    System.IO.File.Delete(oldimagepath);
                }
                _UnitOfWork.mobileSelling.Remove(row);

            }
            return RedirectToAction(nameof(DisplayCustomer));
        }
        [HttpPost]
        public async Task<IActionResult> TogglesLike(int itemID) {
            var CurrentUser= await _userManager.GetUserAsync(User);
            if (CurrentUser==null) {
                return Redirect("/Identity/Account/Login");
            }
            
            var role = await _userManager.GetRolesAsync(CurrentUser);
            var UserRole = role.FirstOrDefault();

            bool alreayLiked = _context.Likess.Any(u=>u.UserId==CurrentUser.Id  && u.ItemId==itemID);
            if (!alreayLiked)
            {
                var like = new Likes
                {
                    Like = 1,
                    UserId = CurrentUser.Id,
                    ItemId = itemID
                };
                _context.Likess.Add(like);
                _context.SaveChanges();
            }
            else {
                var rowfromlikes = _context.Likess.FirstOrDefault(u=>u.UserId==CurrentUser.Id && u.ItemId== itemID);
                if (rowfromlikes!=null) {
                    _context.Likess.Remove(rowfromlikes);
                    _context.SaveChanges();
                }
                
            }
            var multiplelikerow = _context.Likess.ToList();
            var itemids = multiplelikerow.Select(u=>u.ItemId);

            return RedirectToAction(nameof(DisplayCustomer), new {itemid= itemids });
        }

        [HttpPost]
        public IActionResult Comment(string Cmnt, int Id)
        {

            var comment = new Comment {
                ComntConetent = Cmnt,
                CraetedAt = DateTime.Now,
                MobileSellingID=Id
            };
            _context.Comments.Add(comment);
            _context.SaveChanges();
            var viewmodel = new ViewModel1 {
          
                MobileSellIt = _UnitOfWork.mobileSelling.GetAll(IncludeProperties: "Brand"),
                Comment = _context.Comments.ToList()
                

            };

            return RedirectToAction(nameof(DisplayCustomer), viewmodel);
        
        }

      



    }



}


