using Bookstore.Models;
using Bookstore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bookstore.Controllers
{
    public class HomeController : Controller
    {
        private IBookstoreRepository _repo;

        public HomeController(IBookstoreRepository temp)
        {
            _repo = temp;
        }

        public IActionResult Index(int pageNum, string? bookType)
        {
            int pageSize = 10;

            var listView = new BooksListViewModel
            {
                Books = _repo.Books
                .Where(x => x.Category == bookType || bookType == null)
                .OrderBy(x => x.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = bookType == null ? _repo.Books.Count() : _repo.Books.Where(x => x.Category == bookType).Count()
                },

                CurrentBookType = bookType

            };
            
            return View(listView);
        }

    }
}
