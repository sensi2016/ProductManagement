using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

// ReSharper disable once CheckNamespace
namespace ProductManagement.DTO
{
    public class BaseRequestDelete<T>
    {
        public T Id { get; set; }
    }    

    public class BaseRequestPost<T> : BaseRequestPaging
    {
       
        public T Id { get; set; }
    }

    public class BaseListRequestPost<T> : BaseRequestPaging
    {
        public List<T> List { get; set; }

    }

    public class BaseRequestPagingPost : BaseRequestPaging
    {
     
        public override int PageSize { get; set; }


        public override int PageNumber { get; set; }
    }

    public abstract class BaseRequest<T> : BaseRequestPaging
    {
        public virtual T Id { get; set; }

        private int _pageSize;
        public override int PageSize { get => _pageSize == 0 ? 10 : _pageSize; set => _pageSize = value; }

        public override int PageNumber { get; set; }
    }

    public abstract class BaseRequestPaging : IPaging
    {
   
        public virtual int PageSize { get; set; }
        public virtual int PageNumber { get; set; }
    }


}

