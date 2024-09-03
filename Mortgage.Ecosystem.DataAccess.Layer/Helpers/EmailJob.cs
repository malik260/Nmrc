using Microsoft.Extensions.Logging;
using Mortgage.Ecosystem.DataAccess.Layer.Models;
using Mortgage.Ecosystem.DataAccess.Layer.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Helpers
{
    public class EmailJob
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private readonly ILogger<EmailJob> _logger;
        public EmailJob(ILogger<EmailJob> logger)
        {
            _logger = logger;
        }

        public void SendEmail()
        {
            string message = string.Empty;
            var EligibleContributions = db.FinanceCounterpartyTransactionEntity
                .Where(i => i.TransactionDate <= DateTime.Now.AddMonths(1) && i.TransactionDate >= DateTime.Now.AddMonths(-7) && i.Approved == 1 && i.IsCleared != 1)
                .GroupBy(i => i.Ref)
                .Where(e => e.Count() >= 6)
                .Select(e => e.Key)
                .ToList();

            foreach (var key in EligibleContributions)
            {
                var EmployeeInformation = db.EmployeeEntity.Where(i => i.NHFNumber == long.Parse(key)).DefaultIfEmpty().ToList();
                foreach (var item in EmployeeInformation)
                {
                    MailParameter mailParameter = new()
                    {
                        UserName = item.FirstName + " " + item.LastName,
                        UserEmail = item.EmailAddress,
                        NhfNumber = Convert.ToString(item.NHFNumber),
                        RealName = item.FirstName + " " + item.LastName,
                        UserCompany = "Federal Mortgage Bank of Nigeria"

                    };

                    var sendemail = EmailHelper.IsContributionMailSent(mailParameter, out message);




                }
                var ClearedContribution = db.FinanceCounterpartyTransactionEntity.Where(i => i.Ref == key).DefaultIfEmpty().ToList();
                foreach (var item in ClearedContribution)
                {
                    item.IsCleared = 1;
                    db.SaveChanges();

                }


            }



        }

    }
}
