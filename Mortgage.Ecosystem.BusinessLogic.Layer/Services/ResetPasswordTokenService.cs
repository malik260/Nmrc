using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.DataAccess.Layer.Request;
using MySqlX.XDevAPI.Common;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class ResetPasswordTokenService : IResetPasswordTokenService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public ResetPasswordTokenService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<ResetPasswordTokenEntity>>> GetList(ResetPasswordTokenListParam param)
        {
            TData<List<ResetPasswordTokenEntity>> obj = new TData<List<ResetPasswordTokenEntity>>();
            obj.Data = await _iUnitOfWork.ResetPasswordTokens.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ResetPasswordTokenEntity>>> GetPageList(ResetPasswordTokenListParam param, Pagination pagination)
        {
            TData<List<ResetPasswordTokenEntity>> obj = new TData<List<ResetPasswordTokenEntity>>();
            obj.Data = await _iUnitOfWork.ResetPasswordTokens.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeResetPasswordTokenList(ResetPasswordTokenListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ResetPasswordTokenEntity> ResetPasswordTokenList = await _iUnitOfWork.ResetPasswordTokens.GetList(param);
            foreach (ResetPasswordTokenEntity ResetPasswordToken in ResetPasswordTokenList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = ResetPasswordToken.Id,
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(ResetPasswordTokenListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ResetPasswordTokenEntity> ResetPasswordTokenList = await _iUnitOfWork.ResetPasswordTokens.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (ResetPasswordTokenEntity ResetPasswordToken in ResetPasswordTokenList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = ResetPasswordToken.Id,
                });
                List<long> userIdList = userList.Where(t => t.Company == ResetPasswordToken.Id).Select(t => t.Employee).ToList();
                foreach (UserEntity user in userList.Where(t => userIdList.Contains(t.Employee)))
                {
                    obj.Data.Add(new ZtreeInfo
                    {
                        id = user.Id,
                        name = user.RealName
                    });
                }
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<ResetPasswordTokenEntity>> GetEntity(long id)
        {
            TData<ResetPasswordTokenEntity> obj = new TData<ResetPasswordTokenEntity>();
            obj.Data = await _iUnitOfWork.ResetPasswordTokens.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.ResetPasswordTokens.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> GenerateToken(ResetPasswordTokenEntity entity)
        {
            TData<string> obj = new TData<string>();
            if (string.IsNullOrEmpty(entity.EmailAddress))
            {
                obj.Message = "Email Address must be provided!";
                return obj;
            }
            var userExist = await _iUnitOfWork.Users.GetEntityByUsername(entity.EmailAddress);
            if (userExist == null)
            {
                obj.Message = "User does not exist";
                return obj;
            }
            string token = GenerateDefaultToken();

            entity.PasswordToken = token;
            entity.BaseCreateTime = DateTime.Now.AddSeconds(360);

            await _iUnitOfWork.ResetPasswordTokens.SaveForm(entity);

            var message = string.Empty;
            MailParameter mailParameter = new()
            {
                UserEmail = entity.EmailAddress,
                UserToken = entity.PasswordToken,
            };
            var sendEmail = EmailHelper.IsUserTokenSent(mailParameter, out message);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            obj.Message = "<span style='color: black;'>Token successfully generated. Please check your email for the verification code.</span>";
            return obj;
        }

        public async Task<TData<ResetPasswordTokenEntity>> GetTokenList(ResetPasswordTokenListParam param)
        {
            var context = new ApplicationDbContext();
            TData<ResetPasswordTokenEntity> obj = new TData<ResetPasswordTokenEntity>();

            // Check if the provided token exists in the database

            var getToken = context.ResetPasswordTokenEntity.Where(i => i.EmailAddress == param.EmailAddress && i.PasswordToken == param.PasswordToken).FirstOrDefault();
            if (getToken == null)
            {
                // If the token does not exist, set the error message and return the obj
                obj.Message = "<span style='color: black;'>Incorrect token</span>";
                obj.Tag = 0;
                return obj;
            }

            if (getToken.BaseCreateTime < DateTime.Now)
            {
                obj.Message = "<span style='color: black;'>Expired token</span>";
                obj.Tag = 0;

                return obj;

            }

            // Proceed with retrieving the list if the token exists
            obj.Data = getToken;
            obj.Tag = 1;
            obj.Message = "<span style='color: black;'>Proceed to Set New Password</span>";
            return obj;
        }


        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.ResetPasswordTokens.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region public method
        // Generate Default Token
        // <returns></returns>
        public string GenerateDefaultToken()
        {
            return new Random().Next(1, 10000).ToStr();
        }
        #endregion

    }
}
