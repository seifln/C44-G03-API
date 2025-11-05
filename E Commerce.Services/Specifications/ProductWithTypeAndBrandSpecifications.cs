using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{
    internal class ProductWithTypeAndBrandSpecifications : BaseSpecifications<Product, int>
    {

        public ProductWithTypeAndBrandSpecifications(int id) : base(P => P.Id == id)
        {
            AddInclude(P => P.ProductType);
            AddInclude(P => P.ProductBrand);
        }
        // Get All Products
        public ProductWithTypeAndBrandSpecifications(ProductQueryParams queryParams)
            : base(ProductSpecificationsHelper.GetProductCriteria(queryParams))
        {
            AddInclude(P => P.ProductType);
            AddInclude(P => P.ProductBrand);

            switch (queryParams.Sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy( P => P.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending( P => P.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy( P => P.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending( P => P.Price);
                    break;
                default:
                    AddOrderBy( X => X.Id);
                    break;
            }

            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);

        }
    }
}
