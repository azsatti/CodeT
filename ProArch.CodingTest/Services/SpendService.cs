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
        private readonly ILogger _logger;

        public SpendService(ISupplierService supplierService, ISpendService spendService, IFailoverInvoiceService failoverInvoiceService, ILogger logger)
        {
            this._supplierService = supplierService;
            this._failoverInvoiceService = failoverInvoiceService;
            this._spendService = spendService;
            this._logger = logger;
        }

        public SpendSummary GetTotalSpend(int supplierId)
        {
            var supplier = _supplierService.GetById(supplierId);
            _spendService = supplier.IsExternal ? (ISpendService) new ExternalServiceWrapper(_failoverInvoiceService) : new InvoiceRepository();
            return _spendService.GetTotalSpend(supplierId);           
        }               
       
    }
}
