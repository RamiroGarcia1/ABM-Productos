using Práctica_Controller_Service_Repository.Entities;
using Práctica_Controller_Service_Repository.Models.DTO.Reponses;
using Práctica_Controller_Service_Repository.Models.DTO.Requests;
using Práctica_Controller_Service_Repository.Repositories.Interfaces;
using Práctica_Controller_Service_Repository.Services.Interfaces;

namespace Práctica_Controller_Service_Repository.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public List<ProductForReadDto> GetAllProducts()
        {
            var products = _repository.GetAllProducts();
            var result = new List<ProductForReadDto>();

            foreach (var p in products)
            {
                result.Add(new ProductForReadDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                });
            }
            return result;
        }

        public ProductForReadDto? GetProductById(int id)
        {
            var product = _repository.GetProductById(id);

            if (product == null)
            {
                return null;
            }

            return new ProductForReadDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

        public ProductForReadDto CreateProduct(ProductForCreateDto dto)
        {
            var existe = _repository.GetAllProducts().Any(p => p.Name.ToLower() == dto.Name.ToLower());

            if (existe)
            {
                throw new InvalidOperationException("Ya existe un producto con ese nombre.");
            }

            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price
            };

            _repository.AddProduct(product);

            return new ProductForReadDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

        public void UpdateProduct(int id, ProductForUpdateDto dto)
        {
            var product = new Product
            {
                Id = id,
                Name = dto.Name,
                Price = dto.Price
            };

            _repository.UpdateProduct(product);
        }

        public void DeleteProduct(int id)
        {
            _repository.DeleteProduct(id);
        }

        public List<ProductForReadDto> SearchProductsByName(string name)
        {
            var products = _repository.SearchProductsByName(name);
            var result = new List<ProductForReadDto>();

            foreach (var p in products)
            {
                result.Add(new ProductForReadDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                });
            }
            return result;
        }

        public ProductStatsDto GetStats()
        {
            var products = _repository.GetAllProducts();

            if (products.Count == 0)
            {
                return new ProductStatsDto
                {
                    Total = 0,
                    AveragePrice = 0,
                    MostExpensiveName = ""
                };
            }

            return new ProductStatsDto
            {
                Total = products.Count,
                AveragePrice = products.Select(p => p.Price).Average(),
                MostExpensiveName = products.OrderByDescending(p => p.Price).First().Name
            };
        }
    }
}