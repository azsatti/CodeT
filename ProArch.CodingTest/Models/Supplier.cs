namespace ProArch.CodingTest.Models
{
    /// <summary>
    /// Note, we can use abstract/child classes to implement open/closed of SOLID instead of is external but not doing due to time constraint.
    /// </summary>

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