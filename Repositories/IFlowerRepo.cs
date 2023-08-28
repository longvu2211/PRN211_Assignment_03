using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IFlowerRepo
    {
        public List<FlowerBouquet> GetAllFlowers();

        public FlowerBouquet GetFlowerById(int id);

        public List<Category> GetCategories();

        public List<Supplier> GetSuppliers();

        public void AddAFlower(FlowerBouquet flower);

        public void UpdateAFlower(FlowerBouquet flower);

        public void DeleteAFlower(int id);
    }
}
