namespace ProArch.Coding.UnitTest.Services
{
    using Microsoft.Extensions.Logging;
    using Moq;
    using NUnit.Framework;
    using ProArch.CodingTest.Interfaces;
    using CodingTest.Models;
    using FluentAssertions;
    using ProArch.CodingTest.Services;

    [TestFixture]
    public class SpendServiceTest
    {
        [Test]
        public void Test_ExternalSupplierWithoutFailOver()
        {
           var loggerFactory = new LoggerFactory().AddConsole(LogLevel.Information);
           var supplier = new Supplier(1, "External Supplier Without failover", true);
           var mockSupplier = new Mock<ISupplierService>();
           mockSupplier.Setup(m => m.GetById(1)).Returns(supplier);
           mockSupplier.Verify();
           var mockRepository = new Mock<ISpendService>();
           var mockFailover = new Mock<IFailoverInvoiceService>();

           var spendService = new SpendService(mockSupplier.Object,
                mockRepository.Object,
                mockFailover.Object,
               loggerFactory);  // We can replace with other loggers etc

            var result = spendService.GetTotalSpend(1);
            result.Name.Should().Be("External Supplier Without failover");
        }

        [Test]
        public void Test_InternalSupplier()
        {
            var loggerFactory = new LoggerFactory().AddConsole(LogLevel.Information);
            var supplier = new Supplier(2, "Internal Supplier", false);
            var mockSupplier = new Mock<ISupplierService>();
            mockSupplier.Setup(m => m.GetById(2)).Returns(supplier);
            mockSupplier.Verify();

            var mockRepository = new Mock<ISpendService>();
            var mockFailover = new Mock<IFailoverInvoiceService>();

            var spendService = new SpendService(mockSupplier.Object,
                mockRepository.Object,
                mockFailover.Object,
                loggerFactory); // We can replace with other loggers etc

            var result = spendService.GetTotalSpend(2);
            result.Name.Should().Be("Internal Supplier");
        }
    }
}
