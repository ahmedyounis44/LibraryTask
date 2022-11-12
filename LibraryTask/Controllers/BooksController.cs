using LibraryTask.BAL.Interface;
using LibraryTask.DAL;
using LibraryTask.DAL.Entities;
using LibraryTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace LibraryTask.Controllers
{
    public class BooksController : Controller
    {
        private IBookRepository _IBookRepository;
        private IAuthorRepository _IAuthorRepository;
        private ICategoryRepository _ICategoryRepository;
        private IWebHostEnvironment _webHostEnvironment;
        public BooksController(IBookRepository BookRepository, IWebHostEnvironment webHostEnvironment, IAuthorRepository iAuthorRepository, ICategoryRepository iCategoryRepository)
        {
            _IBookRepository = BookRepository;
            _webHostEnvironment = webHostEnvironment;
            _IAuthorRepository = iAuthorRepository;
            _ICategoryRepository = iCategoryRepository;
        }
        public async Task<IActionResult> Index()
        {

            return View(await _IBookRepository.GetBookList());
        }

        public async Task< IActionResult> AddNew()
        {
            var list = await _ICategoryRepository.GetCategoryList();
            list.Insert(0, new Category { Name = "Select", Id = 0 });
            ViewBag.ListofCategory = list;
            ViewBag.ListofAuthor = await _IAuthorRepository.GetAuthorList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(Book obj)
        {
            var list = await _ICategoryRepository.GetCategoryList();
            list.Insert(0, new Category { Name = "Select", Id = 0 });
            ViewBag.ListofCategory = list;
            ViewBag.ListofAuthor = await _IAuthorRepository.GetAuthorList();
            ModelState.Remove("BookCover");
            ModelState.Remove("CategoryData");
            ModelState.Remove("SubCategoryData");
            ModelState.Remove("AuthorData");
            
            if (ModelState.IsValid)
            {
                if (obj.Category_Id == 0)
                {
                    ModelState.AddModelError("", "Select Category");
                }

                if (obj.Bookimage != null && obj.Bookimage.Length > 0)
                {

                    if (obj.Bookimage.FileName.ToLower().EndsWith(".jpg") || obj.Bookimage.FileName.ToLower().EndsWith(".png") || obj.Bookimage.FileName.ToLower().EndsWith(".jpeg"))
                    {

                        var filename = obj.Bookimage.FileName.Replace("\"", string.Empty);
                        var NewfileName = "";
                        if (filename.Contains('.'))
                        {
                            var arrExtentions = filename.Split('.');
                            var lenExtention = arrExtentions.Length;
                            var extention = arrExtentions[lenExtention - 1];
                            NewfileName = "UploadedImages/" + DateTime.Now.Ticks.ToString() + "." + extention;
                        }
                        using (var stream = new FileStream(_webHostEnvironment.WebRootPath + "/" + NewfileName, FileMode.Create))
                        {
                            await obj.Bookimage.CopyToAsync(stream);
                            obj.BookCover = NewfileName;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Please Upload ONly Images");
                        return View(obj);

                    }
                }


                else
                {
                    ModelState.AddModelError(string.Empty, "Please Upload  Image");
                    return View(obj);
                }

                obj.CreatedDate =obj.ModifiedDate= DateTime.Now;

                _IBookRepository.AddBook(obj);


                if (_IBookRepository.Save())
                {
                    return RedirectToAction("Index", "Books");
                }
                else
                {
                    return View(obj);
                }
            }

            return View(obj);
        }

        public async Task<JsonResult> GetSubCategory(int CategoryID)
        {
           


            return Json(new SelectList(await _ICategoryRepository.GetSubCategoryList(CategoryID), "Id", "Name"));
        }


        public async Task<IActionResult> stackexchange()
        {

            Root obj = new Root();
            HttpClientHandler handler = new HttpClientHandler();
            handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            using (var httpClient = new HttpClient(handler))
            {
                var apiUrl = ("https://api.stackexchange.com/2.3/questions?page=1&pagesize=50&order=desc&sort=activity&site=stackoverflow");

                //setup HttpClient
                httpClient.BaseAddress = new Uri(apiUrl);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //make request
                var response = await httpClient.GetStringAsync(apiUrl);
                obj = JsonConvert.DeserializeObject<Root>(response);
            }

           

            return View(obj);
        }

        public async Task<IActionResult> QuestionDetails(long Id)
        {

            Root obj = new Root();
            HttpClientHandler handler = new HttpClientHandler();
            handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            using (var httpClient = new HttpClient(handler))
            {
                var apiUrl = ("https://api.stackexchange.com/2.3/questions/74412303?order=desc&sort=activity&site=stackoverflow");

                //setup HttpClient
                httpClient.BaseAddress = new Uri(apiUrl);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //make request
                var response = await httpClient.GetStringAsync(apiUrl);
                obj = JsonConvert.DeserializeObject<Root>(response);
            }



            return View(obj);
        }

        

    }
}
