namespace ProArch.CodingTest.Services
{
    using ProArch.CodingTest.Interfaces;
    using ProArch.CodingTest.Collections;

    public class FailoverInvoiceService : IFailoverInvoiceService
    {
        public FailoverInvoiceCollection GetInvoices(int supplierId)
        {
            return new FailoverInvoiceCollection();
        }
    }
}
