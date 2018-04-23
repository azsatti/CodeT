namespace ProArch.CodingTest.Interfaces
{
    using ProArch.CodingTest.Models;

    public interface ISpendService
    {
        SpendSummary GetTotalSpend(int supplierId);
    }
}
