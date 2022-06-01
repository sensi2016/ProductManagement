
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace ProductManagement.DTO
{
    public class BaseDto
    {
        /// <summary>
      
        /// </summary>
        public int Id { get; set; }

        /// <summary>
      
        /// </summary>
        public string Title { get; set; }

        public string TitleLang2 { get; set; }
        public string Note { get; set; }
        public string NoteLang2 { get; set; }


        public string Code1 { get; set; }
        public string Code2 { get; set; }
        public string Arrange { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
    }

    public class RequestBaseDto: BaseDto, IPaging
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class RequestBaseFilterDto : BaseRequestPaging
    {
       
        public List<int> IdList { get; set; }

       
        public List<int> Ids { get; set; }
    }
}

