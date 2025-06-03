using Microsoft.AspNetCore.Http;
using pizzashop.services.Interfaces;

namespace pizzashop.services.Implementations;

public class UploadService : IUploadService
{
     private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;

    public UploadService(Microsoft.AspNetCore.Hosting.IHostingEnvironment env){
        hostingEnvironment = env;
    }

    // helper
    public string GetUniqueFileName(string fileName)
    {
        fileName = Path.GetFileName(fileName);
        return Path.GetFileNameWithoutExtension(fileName)
                  + "_"
                  + Guid.NewGuid().ToString().Substring(0, 4)
                  + Path.GetExtension(fileName);
    }
    // retuen path to store in db
    // file object and folder name

    public string? Upload(IFormFile? Image, string folder_name)
    {
        if (Image != null)
        {
            //var fileName = Path.GetFileName(Image.FileName);
            var FileName = GetUniqueFileName(Image.FileName);
            
            var uploads = Path.Combine("uploads", $"{folder_name}");
            // for database
            var profilePath = Path.Combine(uploads, FileName);
            // for storing
            uploads = Path.Combine(hostingEnvironment.WebRootPath, uploads);

            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }
            var uploadPath = Path.Combine(uploads, FileName);
            Image.CopyTo(new FileStream(uploadPath, FileMode.Create));
            
            
            // Construct the file path
            return profilePath;

            //to do : Save uniqueFileName  to your db table   
        }
        return null;
    }


}
