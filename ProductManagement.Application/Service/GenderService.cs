
using Microsoft.EntityFrameworkCore;

namespace ProductManagement.Application.Service
{
    public class GenderService:IGenderService
    {
        private IDataBaseContext _dataBaseContext;
        public GenderService(IDataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public async Task<BaseResponseDto> GetAll()
        {
            var lst = await _dataBaseContext.Genders.ToListAsync();
            var map = lst.Adapt<List<ListMultiResponse<int>>>();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = map
            };
        }
    }
}
