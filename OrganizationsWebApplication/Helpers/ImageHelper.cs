using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using NLog;
using WebMatrix.WebData;

namespace OrganizationsWebApplication.Helpers
{
    public class ImageHelper
    {
        private const string c_folderName = "~/Content/Upload/";

        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        public static string GetImagePath(int id)
        {
            try
            {
                var dir = new DirectoryInfo(HostingEnvironment.MapPath(c_folderName));
                var fileName = dir.GetFiles(String.Format("*{0}*", id));
                logger.Info("Getting file: '{0}' by user '{1}'", fileName, WebSecurity.CurrentUserName);

                return string.Concat(c_folderName, fileName.First().Name);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
       
            return null;
        }

        public static string GetFilePathForSaving(HttpPostedFileBase file, int id)
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
            }
            return null;
        }
    }
}