using Microsoft.AspNetCore.Http;

namespace DentalClinic.app.AppServices.FileUploadService
{
    //Service To Upload Files
    public interface IImageService
    {
        public string uploadImage(string FolderName, IFormFile Image);
        
        public  void deleteImage(string FolderName, string OldFileName);

        public string updateImage(string FolderName, IFormFile Image, string OldFileName);
    }
}
