// ReSharper disable once CheckNamespace
namespace ProductManagement.DTO
{
    public interface IPaging
    {
        int PageSize { get; set; }
        int PageNumber { get; set; }
    }
}
