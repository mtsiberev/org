using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using NLog;
using WebMatrix.WebData;

namespace OrganizationsWebApplication.Helpers
{
    public class ImageFile
    {
        public string FileName { get; private set; }
        public string FilePath { get; private set; }
        public string ContentType { get; private set; }

        public ImageFile(string fileName, string filePath, string contentType)
        {
            FileName = fileName;
            FilePath = filePath;
            ContentType = contentType;
        }
    }

    public class ImageHelper
    {
        private const string c_folderName = "~/App_Data/Upload/";
        private const string c_defaultImgName = "default.png";
        private const string c_defaultContentType = "application/.png";

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static ImageFile GetImageFileObject(int id)
        {
            try
            {
                var dir = new DirectoryInfo(HostingEnvironment.MapPath(c_folderName));
                var file = dir.GetFiles(String.Format("*{0}*", id)).First();
                var fileName = file.Name;
                var filePath = string.Concat(c_folderName, fileName);
                string contentType = string.Concat("application/", file.Extension);
                logger.Info("Getting file: '{0}' by user '{1}'", file, WebSecurity.CurrentUserName);

                return new ImageFile(fileName, filePath, contentType);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return new ImageFile(c_defaultImgName, string.Concat(c_folderName, c_defaultImgName), c_defaultContentType);
        }
        
        private static string GetFilePathForSaving(HttpPostedFileBase file, int id)
        {
            try
            {
                string extension = Path.GetExtension(file.FileName);
                string fileName = string.Concat(id.ToString(), extension);
                logger.Info("Saving file: '{0}' by user '{1}'", fileName, WebSecurity.CurrentUserName);

                return Path.Combine(HostingEnvironment.MapPath(c_folderName), fileName);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
        }
        
        public static void SaveUserImageById(HttpPostedFileBase file, int id)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    var fileName = GetFilePathForSaving(file, id);
                    file.SaveAs(fileName);
                    logger.Info("Saving file: '{0}' by user '{1}'", fileName, WebSecurity.CurrentUserName);

                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            else
            {
                logger.Info("You have not specified a file");
            }
        }
        
        public static void DeleteUserImageById(int id)
        {
            try
            {
                var dir = new DirectoryInfo(HostingEnvironment.MapPath(c_folderName));
                var file = dir.GetFiles(String.Format("*{0}*", id)).First();
                var fileName = file.Name;
                file.Delete();
                logger.Info("Image file : '{0}' was removed by user '{1}'", fileName, WebSecurity.CurrentUserName);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}