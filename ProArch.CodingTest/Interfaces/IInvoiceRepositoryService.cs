namespace ProArch.CodingTest.Interfaces
{
    using ProArch.CodingTest.Models;
    using System.Linq;    

    public interface IInvoiceRepositoryService
    {
        IQueryable<Invoice> Get(int supplierId);
       
    }
}
