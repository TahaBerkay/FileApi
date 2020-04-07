using System.IO;
using FileApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FileApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[Action]")]
    public class StreamFile2FsController : ControllerBase
    {
        private readonly StreamFile2FsService _fileService;
        private readonly ILogger<StreamFile2FsController> _logger;

        public StreamFile2FsController(ILogger<StreamFile2FsController> logger, StreamFile2FsService fileService)
        {
            _logger = logger;
            _fileService = fileService;
        }

        [HttpGet]
        [DisableRequestSizeLimit]
        public ActionResult GetFile(string fileId)
        {
            var filePath = "C:\\Users\\TahaBerkay\\Downloads\\Downloads.zip";
            var stream2 = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return File(stream2, "application/octet-stream", "Audio_Realtek_6.0.1.7027_W81x64_A.zip", true);
        }
    }
}