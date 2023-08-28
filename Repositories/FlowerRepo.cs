using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class FlowerRepo : IFlowerRepo
    {
        public void AddAFlower(FlowerBouquet flower)
            => FlowerBouquetDAO.Instance.AddAFlower(flower);

        public void DeleteAFlower(int id) => FlowerBouquetDAO.Instance.DeleteAFlower(id);

        public List<FlowerBouquet> GetAllFlowers() => FlowerBouquetDAO.Instance.GetAllFlowers();

        public List<Category> GetCategories() => FlowerBouquetDAO.Instance.GetCategories();

        public FlowerBouquet GetFlowerById(int id)
            => FlowerBouquetDAO.Instance.GetFlowerById(id);

        public List<Supplier> GetSuppliers() => FlowerBouquetDAO.Instance.GetSuppliers();

        public void UpdateAFlower(FlowerBouquet flower)
            => FlowerBouquetDAO.Instance.UpdateAFlower(flower);
    }
}
