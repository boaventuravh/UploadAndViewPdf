using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadAndViewPdf.Models
{
    public class FilesModel
    {
        public int FileId { get; set; } = 0;

        public string Name { get; set; } = "";

        public string Path { get; set; } = "";

        public List<FilesModel> Files { get; set; } = new List<FilesModel>();

    }
}
