using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ZPProductManagement.Application.Files;
using ZPProductManagement.Common;
using ZPProductManagement.Web.Infrastructure;
using ZPProductManagement.Web.ViewModels;

namespace ZPProductManagement.Web.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class FileController : Controller
    {
        private readonly CreateFile _createFile;
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configuration;

        public FileController(CreateFile createFileApplication, IUnitOfWork uow, IConfiguration configuration)
        {
            _createFile = createFileApplication;
            _uow = uow;
            _configuration = configuration;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CreateFileViewModel viewModel)
        {
            var createFiles = Create(viewModel.Files);

            var results = await Task.WhenAll(createFiles);

            var result = Result.Combine(results);

            if (result.Failure)
            {
                TempData["Failure"] = result.Message;

                return RedirectToAction("Index", "Product");
            }

            TempData["Success"] = "File has been created";

            _uow.Commit();

            return RedirectToAction("Index", "Product");
        }

        private IEnumerable<Task<Result>> Create(IFormFileCollection files)
        {
            foreach (var file in files)
            {
                yield return Create(file);
            }
        }

        private async Task<Result> Create(IFormFile file)
        {
            var name = Path.GetFileNameWithoutExtension(file.FileName);

            var extension = Path.GetExtension(file.FileName);

            var path = $"{Path.GetRandomFileName()}{extension}";

            var fileAdapter = new InputFileAdapter(Guid.NewGuid(), name, path, extension);

            var result = await _createFile.Execute(fileAdapter);

            if (result.Failure)
            {
                return Result.Fail(result.Message);
            }

            using var stream = new FileStream(Path.Combine(_configuration["Upload:Directory"], path), FileMode.Create, FileAccess.Write);

            await file.CopyToAsync(stream);

            return Result.Ok();
        }
    }
}