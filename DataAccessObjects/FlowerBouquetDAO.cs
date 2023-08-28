using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class FlowerBouquetDAO
    {
        private FlowerBouquetDAO() { }
        private static readonly object instanceLock = new object();
        private static FlowerBouquetDAO instance;
        public static FlowerBouquetDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new FlowerBouquetDAO();
                    }
                    return instance;
                }
            }
        }

        public List<FlowerBouquet> GetAllFlowers()
        {
            try
            {
                List<FlowerBouquet> flowers = null;
                var context = new FUFlowerBouquetManagementV4Context();
                flowers = context.FlowerBouquets.Include(f => f.Category).
                    Include(f => f.Supplier).ToList();
                return flowers;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public FlowerBouquet GetFlowerById(int id)
        {
            try
            {
                FlowerBouquet flower = null;
                var context = new FUFlowerBouquetManagementV4Context();
                flower = context.FlowerBouquets.Include(f => f.Category).Include(f => f.Supplier).
                    FirstOrDefault(f => f.FlowerBouquetId == id);
                return flower;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Category> GetCategories()
        {
            try
            {
                List<Category> categories = null;
                var context = new FUFlowerBouquetManagementV4Context();
                categories = context.Categories.ToList();
                return categories;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Supplier> GetSuppliers()
        {
            try
            {
                List<Supplier> suppliers = null;
                var context = new FUFlowerBouquetManagementV4Context();
                suppliers = context.Suppliers.ToList();
                return suppliers;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddAFlower(FlowerBouquet flower)
        {
            if (flower == null)
            {
                throw new Exception("Flower is undefined!");
            }
            var context = new FUFlowerBouquetManagementV4Context();
            var checkDuplicate = context.FlowerBouquets.
                FirstOrDefault(f => f.FlowerBouquetId == flower.FlowerBouquetId);
            if (checkDuplicate != null)
            {
                throw new Exception("Flower existed!");
            }
            context.FlowerBouquets.Add(flower);
            context.SaveChanges();
        }

        public void UpdateAFlower(FlowerBouquet flower)
        {
            if (flower == null)
            {
                throw new Exception("Flower is undefined!");
            }
            if (flower.UnitPrice <= 0)
            {
                throw new Exception("Price MUST be positive!");
            }
            if (flower.UnitsInStock <= 0)
            {
                throw new Exception("Unit in stock MUST be positive!");
            }
            var context = new FUFlowerBouquetManagementV4Context();
            context.FlowerBouquets.Update(flower);
            context.SaveChanges();
        }

        public void DeleteAFlower(int id)
        {
            var context = new FUFlowerBouquetManagementV4Context();
            var deletedFlower = context.FlowerBouquets.Find(id);
            var checkExisted = context.OrderDetails.Any
                (ord => ord.FlowerBouquetId == deletedFlower.FlowerBouquetId);
            if (!checkExisted)
            {
                if (deletedFlower != null)
                {
                    deletedFlower.FlowerBouquetStatus = 0;
                    context.SaveChanges();
                }
            }
            else
            {
                throw new Exception("Cannot delete! The flower had in the order");
            }
        }
    }
}
