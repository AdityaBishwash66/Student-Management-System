﻿using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQL
{
	public class ProductRepository: IProductRepository
	{
		private readonly MarketContext db;

		public ProductRepository(MarketContext db)
		{
			this.db = db;
		}

		public void AddProduct(Product product)
		{
			db.Products.Add(product);
			db.SaveChanges();
		}

		public void Delete(int productId)
		{
			var product = db.Products.Find(productId);
			if (product == null) return;
			db.Remove(product);
			db.SaveChanges();
		}

		public Product GetProductById(int productId)
		{
			return db.Products.Find(productId);
		}

		public IEnumerable<Product> GetProducts()
		{
			return db.Products.ToList();
		}

		public IEnumerable<Product> GetProductsByCategory(int categoryId)
		{
			return db.Products.Where(x => x.CategoryId == categoryId).ToList();
		}

		public void Update(Product product)
		{
			var prod = db.Products.Find(product.ProductId);
			if (prod == null) return;

			prod.Category = product.Category;
			prod.Name = product.Name;
			prod.Price = product.Price;
			prod.Quantity = product.Quantity;

			db.SaveChanges();

		}
	}
}
