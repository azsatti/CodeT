namespace ProArch.CodingTest.Models
{
    public class Supplier
    {
        public Supplier(int id, string name, bool isExternal)
        {
            Id = id;
            Name = name;
            IsExternal = isExternal;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsExternal { get; set; }
    }
}