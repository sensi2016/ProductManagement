using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DTO.Product
{
    public class RequestProductDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Code { get; set; }
        public decimal? Price { get; set; }
        public int? Count { get; set; }
        public string? Brand { get; set; }
        public int? ProductTypeId { get; set; }
        public bool IsActive { get; set; }

    }

    public class ResponseProductDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Code { get; set; }
        public decimal? Price { get; set; }
        public int? Count { get; set; }
        public string? Brand { get; set; }
        public bool IsActive { get; set; }
        public int? ProductTypeId { get; set; }

        public string CreateDate { get; set; }
        public string ModifyDate { get; set; }

    }

    public class FilterProductDto : IPaging
    {
        public string Title { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set ; }
    }

    public class ResponseListProductDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public decimal? Price { get; set; }
        public int? Count { get; set; }
        public string? Brand { get; set; }
        public bool IsActive { get; set; }
        public string CreateDate { get; set; }
        public string ModifyDate { get; set; }

    }
}
