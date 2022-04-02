using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebAdmin.Helpers
{
    public static class ImageExtention
    {
        //public async static Task<Image> ResizeImage(this IFormFile file, int width, int height)
        //{
        //    using (var memoryStream = new MemoryStream())
        //    {
        //        await file.CopyToAsync(memoryStream);
        //        using (var img = Image.FromStream(memoryStream))
        //        {
        //            return img.Resize(width, height);
        //        }
        //    }
        //}

        public static Image Resize(this Image image, int width, int height)
        {
            var widthOri = image.Width;
            var heightOri = image.Height;
            if (width < 1 || width > widthOri) width = widthOri;
            if (height < 1 || height > heightOri) height = heightOri;
            var res = new Bitmap(width, height);
            using (var graphic = Graphics.FromImage(res))
            {
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphic.CompositingQuality = CompositingQuality.HighQuality;
                graphic.DrawImage(image, 0, 0, width, height);
            }
            return res;
        }

        public static Stream ToStream(this Image image, ImageFormat format)
        {
            var stream = new MemoryStream();
            image.Save(stream, format);
            stream.Position = 0;
            return stream;
        }

        public static async Task<Image> GetUrl(string url)
        {
            try
            {
                using (HttpClient c = new HttpClient())
                {
                    using (Stream s = await c.GetStreamAsync(url))
                    {
                        return Image.FromStream(s);
                    }
                }
            }
            catch { }
            return default;
        }
    }
}
