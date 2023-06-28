using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using System.Text;

namespace Mortgage.Ecosystem.DataAccess.Layer.Helpers
{
    // File Help
    public static class FileHelper
    {
        // Create a file
        // <param name="path"></param>
        // <param name="content"></param>
        public static void CreateFile(string path, string content)
        {
            var filePath = Path.GetDirectoryName(path);
            if (filePath == null) throw new Exception("Error path is null");
            if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);

            using var sw = new StreamWriter(path, false, Encoding.UTF8);
            sw.Write(content);
        }

        // Upload a single file
        // <param name="fileModule"></param>
        // <param name="fileCollection"></param>
        // <returns></returns>
        public async static Task<TData<string>> UploadFile(int fileModule, IFormFileCollection files)
        {
            var dirModule = string.Empty;
            var obj = new TData<string>();
            if (files == null || files.Count == 0)
            {
                obj.Message = "Please select a file first!";
                return obj;
            }
            if (files.Count > 1)
            {
                obj.Message = "Only one file can be uploaded at a time!";
                return obj;
            }
            var objCheck = new TData();
            var file = files[0];
            switch (fileModule)
            {
                case (int)UploadFileType.Portrait:
                    objCheck = CheckFileExtension(Path.GetExtension(file.FileName), ".jpg|.jpeg|.gif|.png");
                    if (objCheck.Tag != 1)
                    {
                        obj.Message = objCheck.Message;
                        return obj;
                    }
                    dirModule = UploadFileType.Portrait.ToString();
                    break;

                case (int)UploadFileType.Import:
                    objCheck = CheckFileExtension(Path.GetExtension(file.FileName), ".xls|.xlsx");
                    if (objCheck.Tag != 1)
                    {
                        obj.Message = objCheck.Message;
                        return obj;
                    }
                    dirModule = UploadFileType.Import.ToString();
                    break;

                default:
                    obj.Message = "Please specify the module to upload to";
                    return obj;
            }

            var fileExtension = TextHelper.GetCustomValue(Path.GetExtension(file.FileName), ".png");
            var newFileName = SecurityHelper.GetGuid(true) + fileExtension;
            var dir = "Resource" + Path.DirectorySeparatorChar + dirModule + Path.DirectorySeparatorChar + DateTime.Now.ToString("yyyy-MM-dd").Replace('-', Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;

            var absoluteDir = Path.Combine(GlobalContext.HostingEnvironment.ContentRootPath, dir);
            var absoluteFileName = string.Empty;
            if (!Directory.Exists(absoluteDir))
            {
                Directory.CreateDirectory(absoluteDir);
            }
            absoluteFileName = absoluteDir + newFileName;
            try
            {
                using (var fs = File.Create(absoluteFileName))
                {
                    await file.CopyToAsync(fs);
                    fs.Flush();
                }
                obj.Data = Path.AltDirectorySeparatorChar + ConvertDirectoryToHttp(dir) + newFileName;
                obj.Message = Path.GetFileNameWithoutExtension(TextHelper.GetCustomValue(file.FileName, newFileName));
                obj.Description = (file.Length / 1024).ToString(); // KB
                obj.Tag = 1;
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
            }
            return obj;
        }

        // Delete a single file
        // <param name="fileModule"></param>
        // <param name="filePath"></param>
        // <returns></returns>
        public static TData<string> DeleteFile(int fileModule, string filePath)
        {
            var obj = new TData<string>();
            var dirModule = fileModule.GetDescriptionByEnum<UploadFileType>();
            if (string.IsNullOrEmpty(filePath))
            {
                obj.Message = "Please select a file first!";
                return obj;
            }

            filePath = FilterFilePath(filePath);
            filePath = "Resource" + Path.DirectorySeparatorChar + dirModule + Path.DirectorySeparatorChar + filePath;
            var absoluteDir = Path.Combine(GlobalContext.HostingEnvironment.ContentRootPath, filePath);
            try
            {
                if (File.Exists(absoluteDir))
                {
                    File.Delete(absoluteDir);
                }
                else
                {
                    obj.Message = "File does not exist";
                }
                obj.Tag = 1;
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
            }
            return obj;
        }

        // Download File
        // <param name="filePath"></param>
        // <param name="delete"></param>
        // <returns></returns>
        public static TData<FileContentResult> DownloadFile(string filePath, int delete)
        {
            filePath = FilterFilePath(filePath);
            if (!filePath.StartsWith("wwwroot") && !filePath.StartsWith("Resource"))
            {
                throw new Exception("Illegal access");
            }
            var obj = new TData<FileContentResult>();
            var absoluteFilePath = GlobalContext.HostingEnvironment.ContentRootPath + Path.DirectorySeparatorChar + filePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            var fileBytes = File.ReadAllBytes(absoluteFilePath);
            if (delete == 1)
            {
                File.Delete(absoluteFilePath);
            }
            // md5 value
            var fileNamePrefix = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            var title = fileNameWithoutExtension;
            if (fileNameWithoutExtension.Contains("_"))
            {
                title = fileNameWithoutExtension.Split('_')[1].Trim();
            }
            var fileExtensionName = Path.GetExtension(filePath);
            obj.Data = new FileContentResult(fileBytes, "application/octet-stream")
            {
                FileDownloadName = string.Format("{0}_{1}{2}", fileNamePrefix, title, fileExtensionName)
            };
            obj.Tag = 1;
            return obj;
        }

        // GetContentType
        // <param name="path"></param>
        // <returns></returns>
        public static string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            var contentType = types[ext];
            if (string.IsNullOrEmpty(contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

        // GetMimeTypes
        // <returns></returns>
        public static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        // CreateDirectory
        // <param name="directory"></param>
        public static void CreateDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        // DeleteDirectory
        // <param name="filePath"></param>
        public static void DeleteDirectory(string filePath)
        {
            try
            {
                if (Directory.Exists(filePath)) //If this folder exists, delete it
                {
                    foreach (string d in Directory.GetFileSystemEntries(filePath))
                    {
                        if (File.Exists(d))
                            File.Delete(d); //Delete the file directly
                        else
                            DeleteDirectory(d); //Recursively delete subfolders
                    }
                    Directory.Delete(filePath, true); //Delete empty folder
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        // ConvertDirectoryToHttp
        // <param name="directory"></param>
        // <returns></returns>
        public static string ConvertDirectoryToHttp(string directory)
        {
            directory = directory.ToStr();
            directory = directory.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            return directory;
        }

        public static string ConvertHttpToDirectory(string http)
        {
            http = http.ToStr();
            http = http.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            return http;
        }

        // CheckFileExtension
        // <param name="fileExtension"></param>
        // <param name="allowExtension"></param>
        // <returns></returns>
        public static TData CheckFileExtension(string fileExtension, string allowExtension)
        {
            var obj = new TData();
            var allowArr = TextHelper.SplitToArray<string>(allowExtension.ToLower(), '|');
            if (allowArr.Where(p => p.Trim() == fileExtension.ToStr().ToLower()).Any())
            {
                obj.Tag = 1;
            }
            else
            {
                obj.Message = "Only files with the file extension " + allowExtension + " can be uploaded";
            }
            return obj;
        }

        public static string FilterFilePath(string filePath)
        {
            filePath = filePath.Replace("../", string.Empty);
            filePath = filePath.Replace("..", string.Empty);
            filePath = filePath.TrimStart('/');
            return filePath;
        }
    }
}