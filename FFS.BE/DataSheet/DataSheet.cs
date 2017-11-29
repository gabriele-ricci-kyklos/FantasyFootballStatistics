using GenericCore.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFS.BE.DataSheet
{
    public class DataSheet
    {
        public string Name { get; private set; }
        public IList<DataSheetRow> Rows { get; private set; }

        public DataSheet(string name, IList<DataSheetRow> rows)
        {
            name.AssertNotNull("name");
            rows.AssertNotNull("rows");

            Name = name;
            Rows = rows;
        }
    }
}
