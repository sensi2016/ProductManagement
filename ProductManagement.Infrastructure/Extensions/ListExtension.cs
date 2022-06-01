using ProductManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductManagement.Infrastructure.Extensions
{
   public static class ListExtension
    {
        public static bool TryAny<T>(this List<T> value)
        {
            return value?.Count > 0;
        }

        public static bool TryAny<T>(this ICollection<T> value)
        {
            return value?.Count > 0;
        }

        public static List<int> DistinctIntColumn<T>(this List<T> value,string propertyName)
        {
            return value.Select(x => x.GetIntProperty(propertyName)).Distinct().ToList();
        }

        public static List<long> DistinctLongColumn<T>(this List<T> value, string propertyName)
        {
            return value.Select(x => x.GetLongProperty(propertyName)).Distinct().ToList();
        }

        public static List<string> DistinctStringColumn<T>(this List<T> value, string propertyName)
        {
            return value.Where(x=> x.GetStringProperty(propertyName)!=string.Empty)
                .Select(x => x.GetStringProperty(propertyName)).Distinct().ToList();
        }

        public static List<T> ToPaging<T>(this List<T> value, int pageSize, int pageNumber)
        {
            return value.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
        }

        public static ResultListDto Set(this ResultListDto resultListDto,int count,object list)
        {
            resultListDto.Count = count;
            resultListDto.List = list;

            return resultListDto;
        }

        public static List<T> ToListOrderMoveLast<T>(this List<T> lst)
        {
            var curIndex = lst.FindIndex(d => d.GetType().GetProperty("Code").GetValue(d) == "Order");
            if(curIndex < 0)
                return lst;

            int lastIndex = lst.Count;
            var curItem= lst[curIndex];
            lst.RemoveAt(curIndex);
            lst.Insert(lastIndex, curItem);

            return lst;
        }
    }
}
