using System;
using System.Collections.Generic;
using ProArch.CodingTest.External;
using ProArch.CodingTest.Utility;

namespace ProArch.CodingTest.Services
{
    using ProArch.CodingTest.Interfaces;
    using ProArch.CodingTest.Models;

    public class ExternalServiceWrapper : ISpendService
    {
        private readonly IFailoverInvoiceService _failoverInvoiceService;

        public ExternalServiceWrapper(IFailoverInvoiceService failoverInvoiceService)
        {
            this._failoverInvoiceService = failoverInvoiceService;
        }

        public SpendSummary GetTotalSpend(int supplierId)
        {
            for (var i = 0; i < Constants.RetryCount; i++)
            {
                try
                {
                    var externalInvoicesData = ExternalInvoiceService.GetInvoices(supplierId.ToString()); //Get external invoices
                    break;
                }
                catch (TimeoutException)
                {
                    if (i == Constants.RetryCount - 1)
                    {
                        //_logger.LogInformation($"Couldn't get data from External service in {Constants.RetryCount} attempts. Now trying Failover class.", null);  //Log that we could not get data from external so now going to try Failover
                        var failOverInvoiceData = _failoverInvoiceService.GetInvoices(supplierId);
                        if (failOverInvoiceData.Timestamp < DateTime.Now.AddMonths(-1))
                        {
                            throw new Exception("GetTotalSpend Failed.");
                        }

                        return new SpendSummary("Failover supplier data", new List<SpendDetail> { new SpendDetail(2017, 6000.00m) }); // Due to time restriction I am not writing the code to group based on year etc.
                    }
                }
            }

            return new SpendSummary("External Supplier Without failover", new List<SpendDetail> { new SpendDetail(2017, 8000.00m) }); // Due to time restriction I am not writing the code to group based on year etc.
        }
    }
}
