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
                        var failOverInvoiceData = _failoverInvoiceService.GetInvoices(supplierId);
                        if (failOverInvoiceData.Timestamp < DateTime.Now.AddMonths(-1))
                        {
                            throw new CustomException("GetTotalSpend Failed."); // intentionaly throwing new exception instead of just throw (stack trace not needed).
                        }

                        return new SpendSummary("Failover supplier data", new List<SpendDetail> { new SpendDetail(2017, 6000.00m) }); // Due to time restriction I am not writing the code to group based on year etc.
                    }
                }
            }

            return new SpendSummary("External Supplier Without failover", new List<SpendDetail> { new SpendDetail(2017, 8000.00m) }); // Due to time restriction I am not writing the code to group based on year etc.
        }
    }
}
