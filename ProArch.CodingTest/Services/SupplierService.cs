namespace ProArch.CodingTest.Services
{
    using Interfaces;
    using ProArch.CodingTest.Models;

    public class SupplierService : ISupplierService
    {
        public Supplier GetById(int id)
        {
            return new Supplier(1, "Supplier - " + id.ToString() , IsExternal(id));
        }   
        
        private bool IsExternal(int id)
        {
            return id % 2 != 0;            
        }
    }
}
