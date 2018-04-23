namespace ProArch.CodingTest.Models
{
    using System;

    public class Invoice
    {
        public int CustomerId { get; set; }

        public DateTime InvoiceDate { get; set; }

        public decimal Amount { get; set; }
    }
}
