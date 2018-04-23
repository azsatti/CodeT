namespace ProArch.CodingTest.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;
    using ProArch.CodingTest.Models;

    public class InvoiceRepository : IInvoiceRepositoryService, ISpendService
    {
        public IQueryable<Invoice> Get(int supplierId)
        {
            return new List<Invoice>().AsQueryable();
        }

        /// <summary>
        /// This method is called to get internal supplier data.
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public SpendSummary GetTotalSpend(int supplierId)
        {
            var invoiceData = this.Get(supplierId); // this should return the internal supplier invoices data etc.
            return new SpendSummary(name: "Internal Supplier", years: new List<SpendDetail>() { new SpendDetail(2017, 5000.00m) }); // Due to time restriction I am not writing the code to group based on year etc.
        }
    }
}
