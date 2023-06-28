using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers
{
    // File controller
    public class FileController : BaseController
    {
        public FileController(IUnitOfWork iUnitOfWork) : base(iUnitOfWork)
        {
        }

        // Upload a single file
        // <param name="fileModule"></param>
        // <param name="fileList"></param>
        // <returns></returns>
        [HttpPost]
        [AuthorizeFilter]
        public async Task<TData<string>> UploadFile(int fileModule, IFormCollection fileList)
        {
            TData<string> obj = await FileHelper.UploadFile(fileModule, fileList.Files);
            return obj;
        }

        // Delete a single file
        // <param name="fileModule"></param>
        // <param name="filePath">file path</param>
        // <returns></returns>
        [HttpPost]
        [AuthorizeFilter]
        public TData<string> DeleteFile(int fileModule, string filePath)
        {
            TData<string> obj = FileHelper.DeleteFile(fileModule, filePath);
            return obj;
        }

        // Download file
        // <param name="filePath">file path</param>
        // <param name="delete"></param>
        // <returns></returns>
        // <exception cref="Exception"></exception>
        [HttpGet]
        public FileContentResult DownloadFile(string filePath, int delete = 1)
        {
            var obj = FileHelper.DownloadFile(filePath, delete);
            if (obj.Tag == 1)
            {
                return obj.Data;
            }
            else
            {
                throw new Exception("Download failed: " + obj.Message);
            }
        }
    }
}