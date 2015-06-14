using System.Collections.Generic;

namespace Organizations
{
    public interface IPaging<T>
    {
        List<T> GetEntitiesForOnePage(int pageNum, int pageSize, string sortType);
        int GetCount();
    }
}
