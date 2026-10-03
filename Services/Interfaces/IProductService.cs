using Práctica_Controller_Service_Repository.Models.DTO.Reponses;
using Práctica_Controller_Service_Repository.Models.DTO.Requests;
using Práctica_Controller_Service_Repository.Models.DTO.Reponses;
using Práctica_Controller_Service_Repository.Models.DTO.Requests;

namespace Práctica_Controller_Service_Repository.Services.Interfaces
{
    public interface IProductService
    {
        List<ProductForReadDto> GetAllProducts();
        ProductForReadDto? GetProductById(int id);
        ProductForReadDto CreateProduct(ProductForCreateDto dto);
        void UpdateProduct(int id, ProductForUpdateDto dto);
        void DeleteProduct(int id);
        List<ProductForReadDto> SearchProductsByName(string name);
        ProductStatsDto GetStats();
    }
}

