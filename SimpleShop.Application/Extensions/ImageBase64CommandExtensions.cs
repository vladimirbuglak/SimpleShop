using SimpleShop.Application.Modify.Commands.Products.Create;
using SimpleShop.Application.Modify.Interfaces;

namespace SimpleShop.Application.Modify.Extensions;

public static class ImageBase64CommandExtensions
{
    public static UploadFile ToUploadFile(this ImageBase64Command file, string productName)
    {
        var fileName = file.Name.Split(".").First();

        var extension = file.Name.Split(".").Last();

        return new UploadFile
        {
            Content = Convert.FromBase64String(file.Content),
            Name = $"{productName}-{fileName}.{extension}"
        };
    }
}