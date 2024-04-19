using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Components
{
    public class BookTypesViewComponent : ViewComponent
    {
        private IBookstoreRepository _repo;
        public BookTypesViewComponent(IBookstoreRepository temp) { 
            _repo = temp;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedBookType = RouteData?.Values["bookType"];

            var bookTypes = _repo.Books
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);
           
            return View(bookTypes);
        }
    }
}
