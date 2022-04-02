namespace WebNuoc.Helpers
{
    public static class ImageExtention
    {
        private static string _Host = "https://localhost:5002/Media/ImageResize?url={url}&width={width}&height={height}";
        public static string ImageResizeUrl(this string url, int width, int height)
        {
            return _Host.Replace("{url}", url)
                .Replace("{width}", width.ToString())
                .Replace("{height}", height.ToString());
        }
    }
}
