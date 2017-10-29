using FFS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFS.Console
{
    class Program
    {
        static void Main()
        {
            var result = new WebDataRetriever().RetrieveData(@"C:\temp\");
        }
    }
}
