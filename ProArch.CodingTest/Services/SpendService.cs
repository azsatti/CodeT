using System;
using ProArch.CodingTest.Utility;

namespace ProArch.CodingTest.Services
{
    using Interfaces;
    using Models;
    using Microsoft.Extensions.Logging;

    public class SpendService : ISpendService
    {
        private readonly ISupplierService _supplierService;
        private readonly IFailoverInvoiceService _failoverInvoiceService;
        private ISpendService _spendService;
        private readonly ILogger<SpendService> _logger;

        public SpendService(ISupplierService supplierService, ISpendService spendService, IFailoverInvoiceService failoverInvoiceService, ILoggerFactory loggerFactory)
        {
            this._supplierService = supplierService;
            this._failoverInvoiceService = failoverInvoiceService;
            this._spendService = spendService;
            this._logger = loggerFactory.CreateLogger<SpendService>();
        }

        public SpendSummary GetTotalSpend(int supplierId)
        {
            _logger.LogInformation($"Calling Total spend for {supplierId}");
            try
            {
                var supplier = _supplierService.GetById(supplierId);
                _spendService = supplier.IsExternal ? (ISpendService) new ExternalServiceWrapper(_failoverInvoiceService) : new InvoiceRepository(); 
                return _spendService.GetTotalSpend(supplierId);
            }
            catch (CustomException)
            {
                _logger.LogError($"GetTotalSpend is failed to acquire data for external supplier {supplierId} from both locations.");
                return null; // sending out null intentionally.
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured. Error message is {ex.Message}");
                throw;
            }     
        }               
       
    }
}
