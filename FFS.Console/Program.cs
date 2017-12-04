using GenericCore.Support.Factory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFS.Console
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            ObjectsFactory objectsFactory = ObjectsFactory.New(ConfigurationManager.AppSettings["Dependencies"]);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(objectsFactory));
        }
    }
}
