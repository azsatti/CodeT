namespace ProArch.CodingTest.Models
{
    public class SpendDetail
    {
        public SpendDetail(int year, decimal totalSpend)
        {
            this.Year = year;
            this.TotalSpend = totalSpend;
        }

        public int Year { get; set; }
        public decimal TotalSpend { get; set; }
    }
}