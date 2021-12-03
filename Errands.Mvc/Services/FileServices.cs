using Errands.Application.Common;
using Errands.Application.Exceptions;
using Errands.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Errands.Mvc.Services
{
    public class FileServices : IFileServices
    {

        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IImageProfile _logoImageProfile;
        private readonly IFileProfile _fileProfile;

        public FileServices(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            _logoImageProfile = new LogoImageProfile();
            _fileProfile = new BoxFile();
        }
        public async Task<FileModel> SaveFile(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName);

            ValidateExtensionFile(fileExtension, _fileProfile.AllowedExtensions);
            ValidateSizeFile(file, _fileProfile.MaxSizeBytes);
            // 
            string filePath;
            string fileName;
            string folderPath = Path.Combine(_appEnvironment.WebRootPath, "repos", "usersFiles", _fileProfile.Folder);
            do
            {
                fileName = GenerateFileName(file);
                filePath = Path.Combine(folderPath, fileName);
            } while (System.IO.File.Exists(filePath));

            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                //Save file
                await file.CopyToAsync(fileStream);
            }
            return new FileModel
            {
                Path = Path.Combine("repos", "usersFiles", _fileProfile.Folder, fileName),
                Name = file.FileName,
                Type = IdentifyTypeFile(fileExtension),
            };

        }
        public async Task<Logo> SaveLogoAsync(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName);

            ValidateExtensionFile(fileExtension, _logoImageProfile.AllowedExtensions);
            ValidateSizeFile(file, _logoImageProfile.MaxSizeBytes);

            Image image;
            await using(var fileStream = file.OpenReadStream())
            {
                image = await Image.LoadAsync(fileStream);
            }
        
            ValidateSizeImageLogo(image);

            string filePath;
            string fileName;
            string folderPath = Path.Combine(_appEnvironment.WebRootPath, "repos", "usersFiles", _logoImageProfile.Folder);
            do
            {
                fileName = GenerateFileName(file);
                filePath = Path.Combine(folderPath, fileName);
            } while (System.IO.File.Exists(filePath));

            Resize(image, _logoImageProfile);
            Crop(image, _logoImageProfile);
            //
            await image.SaveAsync(filePath, new JpegEncoder { Quality = 75 });
            return new Logo
            {
                Path = Path.Combine("repos", "usersFiles", _logoImageProfile.Folder, fileName),
                Name = file.FileName
            };
        }
        public void DeleteFile(string filePath)
        {
            string fullPath = Path.Combine(_appEnvironment.WebRootPath, filePath);
            File.Delete(fullPath);
        }
        private void ValidateSizeImageLogo(Image image)
        {
            if (image.Width < _logoImageProfile.Width || image.Height < _logoImageProfile.Height)
            {
                throw new ImageProcessingException("Logo too small");
            }
        }
        private void ValidateExtensionFile(string fileExtension, IEnumerable<string> allowExtension)
        {
            if (allowExtension.All(e => e != fileExtension))
            {
                throw new WrongExtensionFileException(fileExtension);
            }
        }
        private void ValidateSizeFile(IFormFile file, long MaxlengthFileInBytes)
        {
            if (file.Length > MaxlengthFileInBytes)
            {
                throw new InvalidSizeFileException("File too large");
            }
        }
        private string GenerateFileName(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName);
            var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());

            return $"{fileName}{fileExtension}";
        }
        private void Resize(Image image, IImageProfile imageProfile)
        {
            var resizeOptions = new ResizeOptions
            {
                Mode = ResizeMode.Min,
                Size = new Size(imageProfile.Width)
            };

            image.Mutate(action => action.Resize(resizeOptions));
        }
        private TypeFile IdentifyTypeFile(string fileExtension)
        {

            if (_logoImageProfile.AllowedExtensions.Any(ext => ext == fileExtension.ToLower()))
            {
                return TypeFile.Image;
            }
            else
            {
                return TypeFile.File;
            }
        }
        private void Crop(Image image, IImageProfile imageProfile)
        {
            var rectangle = GetCropRectangle(image, imageProfile.Width, imageProfile.Height);
            image.Mutate(action => action.Crop(rectangle));
        }
        private Rectangle GetCropRectangle(IImageInfo image, int width, int height)
        {
            var widthDifference = image.Width - width;
            var heightDifference = image.Height - height;
            var x = widthDifference / 2;
            var y = heightDifference / 2;

            return new Rectangle(x, y, width, height);
        }
    }
}
