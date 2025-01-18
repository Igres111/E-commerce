using E_commerce.DTOs.ProductDtos;
using E_commerce.Models;

namespace E_commerce.Repositories.ProductRepos
{
    public interface IProduct
    {
        public Task<List<GetProductDto>> GetProductsList();
        public Task<List<GetProductDto>> GetDiscounted();
        public Task<List<GetProductDto>> GetSeller();
        public Task<List<GetProductDto>> GetExplore();
    }
}
