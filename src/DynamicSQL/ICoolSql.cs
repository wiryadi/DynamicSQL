using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicSQL
{
    public interface IDynamicSQL
    {
        void EachRow(string queryString, Action<dynamic> act);
        void FirstRow(string queryString, Action<dynamic> act);
    }
}
