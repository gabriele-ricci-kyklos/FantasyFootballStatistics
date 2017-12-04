using FFS.BE.Data;
using FFS.DAL;
using FFS.DAL.FantaGazzetta;
using GenericCore.Support;
using GenericCore.Support.Factory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFS.Console
{
    public partial class MainForm : Form
    {
        private ObjectsFactory _objectsFactory;
        private FFSDao _dao;

        private AbstractDataRetriever DataRetriever
        {
            get
            {
                return _objectsFactory.GetObject<AbstractDataRetriever>("FantaGazzettaDataRetriever");
            }
        }

        public MainForm(ObjectsFactory objectsFactory)
        {
            objectsFactory.AssertNotNull("objectsFactory");

            _objectsFactory = objectsFactory;

            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            DataResult result = DataRetriever.RetrieveData(@"C:\temp\");
            FFSDao dao = new FFSDao(result.LocalPath);
        }
    }
}
