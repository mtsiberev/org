﻿using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using NLog;
using WebMatrix.WebData;

namespace OrganizationsWebApplication.Helpers
{
    public class ImageFileDescription
    {
        public string FileName { get; private set; }
        public string FilePath { get; private set; }
        public string ContentType { get; private set; }

        public ImageFileDescription(string fileName, string filePath, string contentType)
        {
            FileName = fileName;
            FilePath = filePath;
            ContentType = contentType;
        }
    }

    public class ImageObject
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        private const string c_folderName = "~/App_Data/Upload/";
        private const string c_defaultImgName = "default.png";
        private const string c_defaultContentType = "application/.png";
        public int Id { get; private set; }

        public ImageObject(int id)
        {
            Id = id;
        }
        
        public void SaveImage(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    string extension = Path.GetExtension(file.FileName);
                    string fileName = string.Concat(Id.ToString(), extension);
                    var filePathName = Path.Combine(HostingEnvironment.MapPath(c_folderName), fileName);

                    file.SaveAs(filePathName);
                    logger.Info("Saving file: '{0}' by user '{1}'", filePathName, WebSecurity.CurrentUserName);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    throw;
                }
            else
            {
                logger.Info("You have not specified a file");
            }
        }
        
        public void DeleteImage()
        {
            try
            {
                var file = GetImageFileById();
                var fileName = file.Name;
                file.Delete();
                logger.Info("Image file : '{0}' was removed by user '{1}'", fileName, WebSecurity.CurrentUserName);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
        }
        
        public FilePathResult GetImage()
        {
            try
            {
                var file = GetImageFileById();
                var fileName = file.Name;
                var filePath = string.Concat(c_folderName, fileName);
                string contentType = string.Concat("application/", file.Extension);
                var description = new ImageFileDescription(fileName, filePath, contentType);

                return new FilePathResult(description.FilePath, description.ContentType);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            var defaultDescription = new ImageFileDescription(c_defaultImgName, string.Concat(c_folderName, c_defaultImgName), c_defaultContentType);
            return new FilePathResult(defaultDescription.FilePath, defaultDescription.ContentType);
        }
        
        private FileInfo GetImageFileById()
        {
            try
            {
                var dir = new DirectoryInfo(HostingEnvironment.MapPath(c_folderName));
                var files = dir.GetFiles(String.Format("*{0}*", Id));
                var file = files.First(x => Path.GetFileNameWithoutExtension(x.Name) == Id.ToString());

                return file;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
        }
    }
}