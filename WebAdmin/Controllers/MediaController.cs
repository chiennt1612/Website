using EntityFramework.Web.DBContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAdmin.Helpers;
using WebAdmin.Models;
using X.PagedList;

namespace WebAdmin.Controllers
{
    [SecurityHeaders]
    [Authorize(Roles = "Admin,Mod")]
    public class MediasController : Controller
    {
        private readonly ILogger<MediasController> _logger;
        private IConfiguration configuration;
        private readonly IStringLocalizer<MediasController> _localizer;
        private readonly string FileExtention;
        private readonly string FileListStatic;
        private readonly string SubFolder;
        private readonly string OriginDirectory;

        public MediasController(IConfiguration configuration, ILogger<MediasController> logger, IStringLocalizer<MediasController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
            this.configuration = configuration;
            SubFolder = this.configuration.GetSection(@"Documents:SubFolder").Value.ToString();
            OriginDirectory = this.configuration.GetSection(@"Documents:OriginDirectory").Value.ToString();
            FileExtention = this.configuration.GetSection(@"Documents:FileExtention").Value.ToString();
            FileListStatic = this.configuration.GetSection(@"Documents:FileListStatic").Value.ToString();
        }

        public async Task<IActionResult> Index(int? page, string Keyword, string dir)
        {
            ViewData["SubFolder"] = SubFolder;
            ViewData["OriginDirectory"] = OriginDirectory;
            _logger.LogInformation($"Param SubFolder:{SubFolder}, OriginDirectory:{OriginDirectory}");
            int pageSize = Constants.PageSize;
            int pageIndex = 1;
            pageIndex = page.HasValue ? page.Value : 1;

            var path = Directory.GetCurrentDirectory();
            DirectoryInfo di = new DirectoryInfo(path + @"\wwwroot\Upload\");
            IEnumerable<DirectoryInfo> directoryInfos = di.GetDirectories("*", SearchOption.AllDirectories);
            if (!String.IsNullOrEmpty(dir))
            {
                di = new DirectoryInfo(dir);
            }

            IEnumerable<FileInfo> fileInfos = di.GetFiles("*.*", SearchOption.AllDirectories);
            _logger.LogInformation($"fileInfos count:{fileInfos.Count()}");

            if (Directory.Exists(OriginDirectory))
            {
                di = new DirectoryInfo(OriginDirectory);
                IEnumerable<FileInfo> docsInfos = di.GetFiles("*.*", SearchOption.AllDirectories);
                _logger.LogInformation($"docsInfos count:{docsInfos.Count()}");
                var a = new List<FileInfo>();
                a.AddRange(fileInfos);
                a.AddRange(docsInfos);
                _logger.LogInformation($"a count:{a.Count()}");
                fileInfos = a;
            }

            directoryInfos = directoryInfos.OrderBy(a => a.FullName);

            if (!String.IsNullOrEmpty(Keyword))
            {
                ViewData["Keyword"] = Keyword;
                fileInfos = fileInfos.Where(a => a.Name.ToUpper().Contains(Keyword.ToUpper()));
            }
            else
            {
                ViewData["Keyword"] = "";
            }
            fileInfos = fileInfos
                .Where(u => (
                    !FileListStatic.Contains($",{u.Name},") &&
                    u.Name.Substring(0, 2) != "~$" &&
                    FileExtention.Contains($",{u.Extension},")
                    ))
                .OrderByDescending(a => a.CreationTime);

            var fileInfosByPage = await fileInfos.ToPagedListAsync(pageIndex, pageSize);

            FileInfoModel model = new FileInfoModel()
            {
                directoryInfos = directoryInfos,
                fileInfos = fileInfosByPage
            };
            return View(model);
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile pic)
        {
            string errorMsg = "State invalid!";
            if (ModelState.IsValid)
            {
                var _index = pic.FileName.LastIndexOf(".");
                string fileType = "";
                if (_index > -1)
                {
                    fileType = pic.FileName.Substring(_index + 1);
                }

                errorMsg = "File extention invalid!";
                if (FileExtention.IndexOf($",.{fileType},") > -1)
                {
                    //string guid = Guid.NewGuid().ToString();
                    string path = Directory.GetCurrentDirectory() + @"\wwwroot\Upload\" + fileType;// + @"\" + guid;
                    string fileName = path + @"\" + pic.FileName;
                    string filePath = Util.UrlHostUpload(Request) + fileType + @"/" + pic.FileName;// + @"/" + guid
                    try
                    {
                        errorMsg = "";
                        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                        using (var stream = new FileStream(fileName, FileMode.Create))
                        {
                            await pic.CopyToAsync(stream);
                            stream.Close();
                        }
                        _logger.LogInformation($"Upload to {fileName} success!");
                        return Content(filePath);
                    }
                    catch (Exception ex)
                    {
                        errorMsg = ex.Message;
                        _logger.LogInformation($"Upload to {fileName} on error {errorMsg} ");
                    }
                }
            }
            return Content($"Error: {errorMsg}");
        }

        [HttpGet]
        public async Task<IActionResult> ImageResize()
        {
            string url; int width; int height;
            try
            {
                url = HttpContext.Request.Query["url"];
                width = int.Parse(HttpContext.Request.Query["width"]);
                height = int.Parse(HttpContext.Request.Query["height"]);

                Image img = await ImageExtention.GetUrl(url);
                img = img.Resize(width, height);

                var memoryStream = img.ToStream(ImageFormat.Jpeg);
                byte[] myBinary = new byte[memoryStream.Length];
                memoryStream.Read(myBinary, 0, (int)memoryStream.Length);
                return File(myBinary, "image/jpg");
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
