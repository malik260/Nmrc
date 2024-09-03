using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class AddDocumentProcedureService : IAddDocumentProcedureService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public AddDocumentProcedureService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;

        }


        #region Submit data
        public async Task<TData<AddDocumentProcedureEntity>> SaveForm(IFormFile imageFile, string DocumentTitle, string TextEditor, string Comment, string NhfNumber, string ProductCode, string LoanId)
        {
            var obj = new TData<AddDocumentProcedureEntity>();
            if (imageFile != null)
            {
                var memoryStream = new MemoryStream();
                await imageFile.CopyToAsync(memoryStream);
                var entity = new AddDocumentProcedureEntity();
                entity.DocumentTitle = DocumentTitle;
                entity.TextEditor = TextEditor;
                entity.Comment = Comment;
                entity.NHFNo = NhfNumber;
                entity.ProductCode = ProductCode;
                entity.LoanId = LoanId;
                //byte[] filesData = memoryStream.ToArray();
                //entity.Files = filesData;
                entity.Files = memoryStream.ToArray();

                await _iUnitOfWork.AddDocumentsProcedure.SaveForm(entity);
                //obj.Data = entity.Id.ParseToString();
                obj.Tag = 1;
                obj.Message = "Document added successfully";
                return obj;
            }
            else
            {
                obj.Tag = -1;
                obj.Message = "Document Upload Failed";
                return obj;
            }


        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.AddDocumentsProcedure.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }




        #endregion
    }
}
