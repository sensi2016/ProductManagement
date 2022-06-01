
using Microsoft.EntityFrameworkCore;

namespace ProductManagement.Application.Service
{
    public class ProductService : IProductService,IRegisterScoped
    {
        private readonly IDataBaseContext _dataBaseContext;
        private IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IWorkContextService _workContextService;
        public ProductService(IDataBaseContext dataBaseContext, IStringLocalizer<SharedResources> stringLocalizer, IWorkContextService workContextService)
        {
            _dataBaseContext = dataBaseContext;
            _stringLocalizer = stringLocalizer;
            _workContextService = workContextService;
        }


        public async Task<BaseResponseDto> Add(RequestProductDto requestProductDto)
        {
            var productValidation = new Validation.ProductValidation(_stringLocalizer);
            var result=  productValidation.Validate(requestProductDto);

            if (!result.IsValid)
            {
                return new BaseResponseDto
                {
                    Status = ResponseStatus.NotValid,
                    Errors = result.Errors.Select(g => g.ErrorMessage).ToList(),
                };
            }

            var map = Mapper.ProductMapper.Map(requestProductDto);
            map.UserId = _workContextService.UserId;

            _dataBaseContext.Products.Add(map);
            await _dataBaseContext.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
            };
        }

        public async Task<BaseResponseDto> Delete(int id)
        {
            var cur = await _dataBaseContext.Products.Where(d => d.Id == id).FirstOrDefaultAsync();

            _dataBaseContext.Products.Remove(cur);

            await _dataBaseContext.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
            };
        }

        public async Task<BaseResponseDto> Search(FilterProductDto filterProductDto)
        {
            var query = _dataBaseContext.Products.AsQueryable();

            if (!string.IsNullOrEmpty(filterProductDto.Title))
            {
                query = query.Where(d => EF.Functions.Like(d.Title, $"%{filterProductDto.Title}%"));
            }

            var map =await query.ToPagedQuery(filterProductDto)
                                        .Select(Mapper.ProductMapper.MapList)
                                        .ToListAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = new ResultListDto
                {
                    Count = await query.CountAsync(),
                    List = map
                }
            };
        }

        public async Task<BaseResponseDto> GetById(int id)
        {
            var cur = await _dataBaseContext.Products.Where(d => d.Id == id)
                .Select(g=>Mapper.ProductMapper.Map(g))
                .FirstOrDefaultAsync();


            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = cur
            };

        }

        public async Task<BaseResponseDto> Update(RequestProductDto requestProductDto)
        {
            var productValidation = new Validation.ProductValidation(_stringLocalizer);
            var result = productValidation.Validate(requestProductDto);

            
            if (!result.IsValid)
            {
                return new BaseResponseDto
                {
                    Status = ResponseStatus.NotValid,
                    Errors = result.Errors.Select(g => g.ErrorMessage).ToList(),
                };
            }

            var cur = await _dataBaseContext.Products.Where(d => d.Id == requestProductDto.Id).FirstOrDefaultAsync();
            var map = Mapper.ProductMapper.Map(requestProductDto, cur);

            _dataBaseContext.Products.Update(map);

            await _dataBaseContext.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
            };
        }
    }
}
