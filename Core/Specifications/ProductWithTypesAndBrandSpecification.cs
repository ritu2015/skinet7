using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithTypesAndBrandSpecification : BaseSpecification<Product>
    {
        public ProductWithTypesAndBrandSpecification()
        {
            AddInclude(x=> x.ProductType);
            AddInclude(X=> X.ProductBrand);
        }

        public ProductWithTypesAndBrandSpecification(int id) : base(x=>x.Id==id)
        {
            AddInclude(x=> x.ProductType);
            AddInclude(X=> X.ProductBrand);
        }
    }
}