using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Exel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace _4.gyak_IRF
{
    public partial class Form1 : Form
    {
        RealEstateEntities context = new RealEstateEntities();
        List<Flat> flats;
        Exel.Application xlApp;
        Exel.Workbook xlWB;
        Exel.Worksheet xlSheet;
        public Form1()
        {
            InitializeComponent();
            LoadData();
            CreateExel();
        }

        private void CreateExel()
        {
            try
            {
                xlApp = new Exel.Application();
                xlWB = xlApp.Workbooks.Add(Missing.Value);
                xlSheet = xlWB.ActiveSheet;

                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception)
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}");
                MessageBox.Show(errMsg, "Error");

                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        }

        private void LoadData()
        {
            flats = context.Flat.ToList();
        }
    }
}
