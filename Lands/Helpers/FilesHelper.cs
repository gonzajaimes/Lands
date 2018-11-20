namespace Lands.Helpers
{
    using System.IO;

   
    //Class used to manipulate the photos in Stream Base64
    public class FilesHelper
    {
        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}