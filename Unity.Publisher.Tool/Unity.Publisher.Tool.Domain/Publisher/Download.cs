namespace Unity.Publisher.Tool.Domain.Publisher;

public sealed class Download
{
    public readonly string Product;

    public  int Downloads;

    public  int Downloaders;

    public Download(string product, int downloads, int downloaders)
    {
        Product = product;
        Downloads = downloads;
        Downloaders = downloaders;
    }

    public bool IsEmpty => Downloads == 0;

    public static Download Empty(string product)
    {
        return new Download(product: product, downloads: 0, downloaders: 0);
    }
}
