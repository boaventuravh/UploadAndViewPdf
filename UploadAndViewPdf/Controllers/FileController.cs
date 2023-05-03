using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UploadAndViewPdf.Models;

namespace UploadAndViewPdf.Controllers
{
    public class FileController : Controller
    {
        IWebHostEnvironment _hostEnvironment = null;

        public FileController(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Index(string fileName="")
        {
            FilesModel fileObj = new FilesModel();
            fileObj.Name = fileName;

            string path = $"{_hostEnvironment.WebRootPath}\\files\\";
            int nId = 1;

            foreach(string pdfPath in Directory.EnumerateFiles(path, "*.pdf"))
            {
                fileObj.Files.Add(new FilesModel()
                { 
                    FileId = nId++,
                    Name = Path.GetFileName(pdfPath),
                    Path = pdfPath
                });
            }

            return View(fileObj);
        }

        [HttpPost]
        public IActionResult Index(IFormFile file, [FromServices] IWebHostEnvironment hostEnvironment)
        {
            string fileName = $"{hostEnvironment.WebRootPath}\\files\\{file.FileName}";
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            return Index();
        }

        public IActionResult PdfViewerNewTab(string fileName) 
        {
            string path = _hostEnvironment.WebRootPath+"\\files\\"+fileName;
            return File(System.IO.File.ReadAllBytes(path), "application/pdf");
        }
    }
}
