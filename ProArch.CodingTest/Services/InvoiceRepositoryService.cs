namespace ProArch.CodingTest.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;
    using ProArch.CodingTest.Models;

    public class InvoiceRepository : IInvoiceRepositoryService
    {
        public IQueryable<Invoice> Get(int supplierId)
        {
            return new List<Invoice>().AsQueryable();
        }
    }
}
