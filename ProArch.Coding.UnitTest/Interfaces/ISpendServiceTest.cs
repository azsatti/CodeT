namespace ProArch.Coding.UnitTest.Interfaces
{
    using Moq;
    using NUnit.Framework;
    using ProArch.CodingTest.Interfaces;
    using ProArch.CodingTest.Models;
    using ProArch.CodingTest.Services;
    using System.Collections.Generic;

    [TestFixture]
    public class ISpendServiceTest
    {
        [Test]
        public void Test()
        {
            var list = new List<SpendDetail>
                {
                    new SpendDetail (2017, 1000.0m),
                    new SpendDetail (2017, 2000.0m)
                };
            var spendSummary = new SpendSummary("Supplier1", list);

            var mock = new Mock<ISpendService>();
            mock.Setup(m => m.GetTotalSpend(1)).Returns(spendSummary);           
            mock.Verify();            
        }
    }
}
