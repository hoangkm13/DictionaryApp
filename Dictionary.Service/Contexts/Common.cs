namespace Dictionary.Service.Contexts;

public static class Common
{
    public static string GetUrlImage(string host, string image)
    {
        if (!string.IsNullOrEmpty(image))
            return "https://" + host + "/Upload/" + image;
        return image;
    }

    public static string SaveImage(string host, string urlImage)
    {
        if (!string.IsNullOrEmpty(urlImage))
        {
            return urlImage.Split(host + "/Upload/")[1];
            ;
        }

        return null;
    }
}