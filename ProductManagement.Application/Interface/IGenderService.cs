

namespace ProductManagement.Application.Interface
{
    public interface IGenderService
    {
        Task<BaseResponseDto> GetAll();
    }
}
