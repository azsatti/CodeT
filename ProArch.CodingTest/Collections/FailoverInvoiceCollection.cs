namespace ProArch.CodingTest.Collections
{
    using System;
    using ProArch.CodingTest.External;

    public class FailoverInvoiceCollection
    {
        public DateTime Timestamp { get; set; }
        public ExternalInvoice[] Invoices { get; set; }

        public FailoverInvoiceCollection()
        {
            this.Invoices = new ExternalInvoice[0];
        }
    }
}