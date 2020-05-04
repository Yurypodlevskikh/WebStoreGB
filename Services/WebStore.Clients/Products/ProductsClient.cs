using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Clients.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(IConfiguration Configuration) : base(Configuration, WebAPI.Products)
        {
        }

        public BrandDTO GetBrandById(int id) => Get<BrandDTO>($"{_ServiceAddress}/brands/{id}");

        public IEnumerable<Brand> GetBrands() => Get<List<Brand>>($"{_ServiceAddress}/brands");

        public ProductDTO GetProductById(int id) => Get<ProductDTO>($"{_ServiceAddress}/{id}");

        public PagedProductsDTO GetProducts(ProductFilter Filter = null) =>
            Post(_ServiceAddress, Filter ?? new ProductFilter())
            .Content
            .ReadAsAsync<PagedProductsDTO>()
            .Result;

        public SectionDTO GetSectionById(int id) => Get<SectionDTO>($"{_ServiceAddress}/sections/{id}");

        public IEnumerable<Section> GetSections() => Get<List<Section>>($"{_ServiceAddress}/sections");
    }
}
