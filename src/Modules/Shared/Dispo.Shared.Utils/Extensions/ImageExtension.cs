using Microsoft.AspNetCore.Http;

namespace Dispo.Shared.Utils.Extensions
{
    public static class ImageExtension
    {
        public static byte[] ConvertToByteArray(this IFormFile? formFile)
        {
            if (formFile == null)
                return new byte[0];

            using (var ms = new MemoryStream())
            {
                formFile.CopyTo(ms);

                return ms.ToArray();
            }
        }
    }
}
