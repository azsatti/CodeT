namespace ProArch.CodingTest.Services
{
    using Interfaces;
    using Models;

    /// <summary>
    /// Supplier service class to get supplier data. Note, we can use abstract/child classes to implement open/closed of SOLID instead of is external but not doing due to time constraint.
    /// </summary>
    public class SupplierService : ISupplierService
    {
        public Supplier GetById(int id)
        {
            return new Supplier(1, "Supplier - " + id , IsExternal(id));
        }   
        
        private bool IsExternal(int id)
        {
            return id % 2 != 0;            
        }
    }
}
