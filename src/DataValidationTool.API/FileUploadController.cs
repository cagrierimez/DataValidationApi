using DataValidationTool.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataValidationTool.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly ExcelService _excelService;

        public FileUploadController()
        {
            _excelService = new ExcelService(); // normalde DI ile eklenir
        }
        [HttpPost("upload-excel")]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var filePath = Path.Combine(Path.GetTempPath(), file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var (valid, invalid) = _excelService.ProcessExcel(filePath);
            var errorFile = _excelService.ExportInvalidRecords(invalid);

            return Ok(new
            {
                totalRecords = valid.Count + invalid.Count,
                validRecords = valid.Count,
                invalidRecords = invalid.Count,
                errorFile
            });
        }
    
}
}
