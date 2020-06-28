using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using ZPProductManagement.Api.ViewModels;
using ZPProductManagement.Application;

namespace ZPProductManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly CreateFileApplication _createFileApplication;
        private readonly IFileRepository _fileRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public FileController(CreateFileApplication createFileApplication, IFileRepository fileRepository, IUnitOfWork uow, IMapper mapper)
        {
            _createFileApplication = createFileApplication;
            _fileRepository = fileRepository;
            _uow = uow;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromForm] CreateFileViewModel viewModel)
        {
            var formFile = viewModel.Content;

            var id = viewModel.Id;

            var name = formFile.FileName;

            var ext = Path.GetExtension(formFile.FileName);

            var path = $"{id}{ext}";

            var content = viewModel.Content.OpenReadStream();

            var createFile = new CreateFile
            {
                Id = id,
                Name = name,
                Path = path,
                Ext = ext,
                Content = content
            };

            var result = await _createFileApplication.Execute(createFile);

            if (result.Failure)
            {
                return BadRequest(result.Message);
            }

            var createdFile = result.Value;

            var showFileViewModel = _mapper.Map<ShowFileViewModel>(createdFile);

            _uow.Commit();

            return CreatedAtAction(nameof(Show), new { id = showFileViewModel.Id }, showFileViewModel);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Show([FromRoute] Guid id)
        {
            var maybeFile = await _fileRepository.FindOne(id);

            if (maybeFile.HasNoValue)
            {
                return NotFound();
            }

            var storedFile = maybeFile.Value;

            var showFileViewModel = _mapper.Map<ShowFileViewModel>(storedFile);

            return Ok(showFileViewModel);
        }
    }
}
