using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.DTO
{
    public class ListResponseDto:BaseResponseDto
    {
        public int Count { get; set; }
        public int PageSize { get; set; }
    }
}
