using FFS.BE.Data;
using FFS.DAL;
using FFS.DAL.FantaGazzetta;
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
            DataResult result = new FantaGazzettaDataRetriever().RetrieveData(@"C:\temp\");
            FFSDao dao = new FFSDao(result.LocalPath);
        }
    }
}
