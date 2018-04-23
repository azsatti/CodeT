namespace ProArch.CodingTest.Interfaces
{
    using ProArch.CodingTest.Collections;

    public interface IFailoverInvoiceService
    {
        FailoverInvoiceCollection GetInvoices(int supplierId);
    }
}
