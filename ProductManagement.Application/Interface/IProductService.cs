
namespace ProductManagement.Application.Interface
{
    public interface IProductService
    {
        Task<BaseResponseDto> Search(FilterProductDto filterProductDto);
        Task<BaseResponseDto> GetById(int id);
        Task<BaseResponseDto> Add(RequestProductDto  requestProductDto);
        Task<BaseResponseDto> Update(RequestProductDto requestProductDto);
        Task<BaseResponseDto> Delete(int id);
    }
}
