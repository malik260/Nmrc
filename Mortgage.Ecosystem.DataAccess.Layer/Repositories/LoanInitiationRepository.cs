using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System.Linq.Expressions;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class LoanInitiationRepository : DataRepository, ILoanInitiationRepository
    {
        #region Retrieve data
        public async Task<List<LoanInitiationEntity>> GetList(LoanInitiationListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<LoanInitiationEntity>> GetPageList(LoanInitiationListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<LoanInitiationEntity> GetEntity(string code)
        {
            return await BaseRepository().FindEntity<LoanInitiationEntity>(x => x.NHFNumber == code);
        }

       
        public async Task<LoanInitiationEntity> GetEntityById(long id)
        {
            return await BaseRepository().FindEntity<LoanInitiationEntity>(x => x.Id == id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await BaseRepository().FindObject("SELECT MAX(Sort) FROM tbl_Conpany");
            int sort = result.ToInt();
            sort++;
            return sort;
        }

        // Whether the company name exists
        // <param name="entity"></param>
        // <returns></returns>
        public bool ExistCompany(LoanInitiationEntity entity)
        {
            var expression = ExtensionLinq.True<LoanInitiationEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.LoanProduct == entity.LoanProduct);
            }
            else
            {
                expression = expression.And(t => t.LoanProduct == entity.LoanProduct && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(LoanInitiationEntity entity)
        {
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Id.IsNullOrZero())
                {

                    await entity.Create();
                    await db.Insert(entity);
                }
                else
                {
                    await entity.Modify();
                    await db.Update(entity);
                }

                if (entity != null && entity.file != null)
                {
                    for (int i = 0; i < entity.file.Count(); i++)
                    {
                        using (var stream = new MemoryStream())
                        {


                            entity.file[i].CopyTo(stream);

                            LoanInitiationUploadEntity loanInitiationUpload = new()
                            {
                                // FileId = entity.Id,
                                LoanId = entity.Id,
                                NHFNo = entity.NHFNumber,
                                Type = "",
                                Label = entity.file[i].FileName,
                                Images = entity.file[i].ContentType,
                                Size = entity.file[i].Length,
                                filedata = stream.ToArray(),
                            };
                            await loanInitiationUpload.Create();
                            await db.Insert(loanInitiationUpload);

                        }
                    }
                }
                await db.CommitTrans();
            }
            catch
            {
                await db.RollbackTrans();
                throw;
            }

            //try
            //{
            //    var underwriting = new UnderwritingEntity();

            //    underwriting.LoanAmount = entity.Principal;
            //    underwriting.InterestRate = entity.Rate;
            //    underwriting.Tenor = entity.Tenor.ToStr();
            //    //Name = entity.FirstName + " " + employeedetails.LastName,
            //    underwriting.ProductName = entity.LoanProduct;
            //    underwriting.NHFNumber = entity.NHFNumber.ToStr();
            //    underwriting.NextStafffLevel = entity.PMB;
            //    underwriting.LoanId = Convert.ToString(entity.Id);
            //    await underwriting.Create();
            //    await db.Insert(underwriting);


            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<LoanInitiationEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<LoanInitiationEntity, bool>> ListFilter(LoanInitiationListParam param)
        {
            var expression = ExtensionLinq.True<LoanInitiationEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.NHFNumber))
                {
                    expression = expression.And(t => t.NHFNumber.Contains(param.NHFNumber));
                }
            }
            return expression;
        }
        #endregion
    }
}