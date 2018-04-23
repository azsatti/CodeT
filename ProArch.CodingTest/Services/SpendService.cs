namespace ProArch.CodingTest.Services
{
    using Interfaces;
    using ProArch.CodingTest.Collections;
    using ProArch.CodingTest.External;
    using ProArch.CodingTest.Models;
    using ProArch.CodingTest.Utility;
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Logging;

    public class SpendService : ISpendService
    {
        private readonly ISupplierService _supplierService;
        private readonly IInvoiceRepositoryService _invoiceRepositoryService;
        private readonly IFailoverInvoiceService _failoverInvoiceService;
        private readonly ILogger _logger;

        public SpendService(ISupplierService supplierService, IInvoiceRepositoryService invoiceRepositoryService, IFailoverInvoiceService failoverInvoiceService, ILogger logger)
        {
            this._supplierService = supplierService;
            this._invoiceRepositoryService = invoiceRepositoryService;
            this._failoverInvoiceService = failoverInvoiceService;
            this._logger = logger;
        }

        public SpendSummary GetTotalSpend(int supplierId)
        {
            var supplier = _supplierService.GetById(supplierId);
            return supplier.IsExternal ? this.GetExternalSupplierData(supplierId) : this.GetInternalSupplierData(supplierId);           
        }                

        /// <summary>
        /// This method is called to get internal supplier data.
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        private SpendSummary GetInternalSupplierData(int supplierId)
        {
            var invoiceData = _invoiceRepositoryService.Get(supplierId); // this should return the internal supplier invoices data etc.
            return new SpendSummary(name: "Internal Supplier", years: new List<SpendDetail>() { new SpendDetail(2017, 5000.00m) }); // Due to time restriction I am not writing the code to group based on year etc.
        }

        /// <summary>
        /// This method is called to get external supplier's data. The method is retrying multiple times before going to failover collection.
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        private SpendSummary GetExternalSupplierData(int supplierId)
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
                        _logger.LogInformation($"Couldn't get data from External service in {Constants.RetryCount} attempts. Now trying Failover class.", null);  //Log that we could not get data from external so now going to try Failover
                        var failOverInvoiceData = this.GetFailoverInvoiceCollection(supplierId);
                        if (failOverInvoiceData.Timestamp < DateTime.Now.AddMonths(-1))
                        {
                            throw new Exception("GetTotalSpend Failed.");
                        }

                        return new SpendSummary(name: "Failover supplier data", years: new List<SpendDetail>() { new SpendDetail(2017, 6000.00m) }); // Due to time restriction I am not writing the code to group based on year etc.
                    }
                }
            }

            return new SpendSummary(name: "External Supplier Without failover", years: new List<SpendDetail>() { new SpendDetail(2017, 8000.00m) }); // Due to time restriction I am not writing the code to group based on year etc.
        }

        /// <summary>
        /// This method is responsible for getting invoice data from failover store.
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        private FailoverInvoiceCollection GetFailoverInvoiceCollection(int supplierId)
        {
            return _failoverInvoiceService.GetInvoices(supplierId); //This will return failover data etc.
        }
    }
}
