namespace ProArch.CodingTest.Models
{
    using System.Collections.Generic;

    public class SpendSummary
    {
        public SpendSummary(string name, List<SpendDetail> years)
        {
            this.Name = name;
            this.Years = years;
        }

        public string Name { get; set; }

        public List<SpendDetail> Years { get; set; }
    }
}