using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class ProductService : List<ProductModel>
    {
        public ProductService()
        {
            this.AddRange(new ProductModel[]
            {
                new ProductModel() {Id =1, Name = "abc1", Price = 245551},
                new ProductModel() {Id =2, Name = "abc2", Price = 245552},
                new ProductModel() {Id =3, Name = "abc3", Price = 245553},
                new ProductModel() {Id =4, Name = "abc4", Price = 245554},
                new ProductModel() {Id =5, Name = "abc5", Price = 245555}
            });
        }
    }
}
