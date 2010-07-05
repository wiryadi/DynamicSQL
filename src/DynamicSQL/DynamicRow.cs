using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Data.SqlClient;

namespace DynamicSQL
{
    public class DynamicRow: DynamicObject
    {
        SqlDataReader sqlDataReader;

        public DynamicRow(SqlDataReader sqlDataReader)
        {
            this.sqlDataReader = sqlDataReader;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {

            try
            {
                var colIndex = sqlDataReader.GetOrdinal(binder.Name);
                result = sqlDataReader.GetValue(colIndex);

                return true;
            }
            catch (IndexOutOfRangeException)
            {
                return base.TryGetMember(binder, out result);
            }
        }

    }

    public static class DynamicRowExtension
    {
        public static DynamicRow ToDynamicRow(this SqlDataReader target)
        {
            return new DynamicRow(target);
        }
    }
}
