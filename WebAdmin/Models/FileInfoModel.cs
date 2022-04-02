using System.Collections.Generic;
using System.IO;
using X.PagedList;

namespace WebAdmin.Models
{
    public class FileInfoModel
    {
        public IPagedList<FileInfo> fileInfos { get; set; }
        public IEnumerable<DirectoryInfo> directoryInfos { get; set; }
    }
}
