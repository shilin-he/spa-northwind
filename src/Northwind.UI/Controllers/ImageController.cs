using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Northwind.Repository;

namespace Northwind.UI.Controllers
{
    public class ImageController : Controller
    {
        private readonly ICategoryRepository _repo;

        public ImageController(ICategoryRepository categoryRepository)
        {
            _repo = categoryRepository;
        }

        public ImageController()
        {
            _repo = new CategoryRepository();
        }

        public ActionResult CategoryPicture(Guid? id)
        {
            Byte[] picture;

            if (id == null)
            {
                picture = GetPlaceHolderImage();
            }
            else
            {
                var category = _repo.GetCategories().FirstOrDefault(c => c.PictureId == id);

                if (category == null || category.Picture == null)
                {
                    picture = GetPlaceHolderImage();
                }
                else
                {
                    // Strip off the 78 bytes Ole header (a relic from old MS Access databases)
                    // var picture = category.Picture.Skip(78).ToArray();
                    picture = category.Picture;
                }
            }

            return File(picture, "image/jpeg");
        }

        private byte[] GetPlaceHolderImage()
        {
            return System.IO.File.ReadAllBytes(Server.MapPath("~/Content/placeholder.jpg"));
        }

        public ActionResult Upload(HttpPostedFileBase file)
        {
            var imageId = Guid.NewGuid().ToString();
            var savePath = Path.Combine(Server.MapPath("~/App_Data"), imageId);
            file.SaveAs(savePath);

            return Json(imageId, "text/plain");
        }

        public ActionResult Remove(string imageId, string fileName)
        {
            var folderPath = Server.MapPath("~/App_Data");

            if (!String.IsNullOrEmpty(imageId))
            {
                Directory.EnumerateFiles(folderPath, imageId + "*").ToList().ForEach(System.IO.File.Delete);
            }

            return Content(String.Empty);
        }
    }
}