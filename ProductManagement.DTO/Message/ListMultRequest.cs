using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.DTO
{
    public class ListMultiRequest<T> 
    {
        public T Id { get; set; }
    }
    public class ListMultiResponse<T>
    {
        public T Id { get; set; }
        public string Title { get; set; }
    }
}
