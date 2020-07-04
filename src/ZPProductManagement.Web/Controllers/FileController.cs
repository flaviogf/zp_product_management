using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using ZPProductManagement.Application.Files;
using ZPProductManagement.Web.Infrastructure;
using ZPProductManagement.Web.ViewModels;

namespace ZPProductManagement.Web.Controllers
{
    [Route("[controller]")]
    public class FileController : Controller
    {
        private readonly CreateFileApplication _createFileApplication;
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configuration;

        public FileController(CreateFileApplication createFileApplication, IUnitOfWork uow, IConfiguration configuration)
        {
            _createFileApplication = createFileApplication;
            _uow = uow;
            _configuration = configuration;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CreateFileViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Failure"] = "Choose at least a file";

                return RedirectToAction("Index", "Product");
            }

            var file = viewModel.File;

            var name = Path.GetFileNameWithoutExtension(file.FileName);

            var extension = Path.GetExtension(file.FileName);

            var path = $"{Path.GetRandomFileName()}{extension}";

            var createFile = new CreateFile(viewModel.Id, name, path, extension);

            var result = await _createFileApplication.Execute(createFile);

            if (result.Failure)
            {
                TempData["Failure"] = result.Message;

                return RedirectToAction("Index", "Product");
            }

            using var stream = new FileStream(Path.Combine(_configuration["Upload:Directory"], path), FileMode.Create, FileAccess.Write);

            await file.CopyToAsync(stream);

            TempData["Success"] = "File has been created";

            _uow.Commit();

            return RedirectToAction("Index", "Product");
        }
    }
}