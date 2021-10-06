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
                CreatTable();
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

        private void CreatTable()
        {
            string[] headers = new string[]
            {
                 "Kód",
                 "Eladó",
                 "Oldal",
                 "Kerület",
                 "Lift",
                 "Szobák száma",
                 "Alapterület (m2)",
                 "Ár (mFt)",
                 "Négyzetméter ár (Ft/m2)"
            };

            for (int i = 1; i < headers.Length + 1; i++)
            {
                xlSheet.Cells[1, i] = headers[i - 1];
            }

            object[,] values = new object[flats.Count, headers.Length];

            int counter = 0;
            foreach (Flat f in flats)
            {
                values[counter, 0] = f.Code;
                values[counter, 1] = f.Vendor;
                values[counter, 2] = f.Side;
                values[counter, 3] = f.District;
                values[counter, 4] = f.Elevator;
                values[counter, 5] = f.NumberOfRooms;
                values[counter, 6] = f.FloorArea;
                values[counter, 7] = f.Price / f.FloorArea;
                values[counter, 8] = "";
                counter++;
            }

            xlSheet.get_Range(
                    GetCell(2, 1),
                    GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;

            Exel.Range headerRange = xlSheet.get_Range(GetCell(1, 1), GetCell(1, headers.Length));
            headerRange.Font.Bold = true;
            headerRange.VerticalAlignment = Exel.XlVAlign.xlVAlignCenter;
            headerRange.HorizontalAlignment = Exel.XlHAlign.xlHAlignCenter;
            headerRange.EntireColumn.AutoFit();
            headerRange.RowHeight = 40;
            headerRange.Interior.Color = Color.LightBlue;
            headerRange.BorderAround2(Exel.XlLineStyle.xlContinuous, Exel.XlBorderWeight.xlThick);


            Exel.Range bodyRange = xlSheet.get_Range(GetCell(2, 1), GetCell(1 + values.GetLength(0), values.GetLength(1)));
            bodyRange.BorderAround2(Exel.XlLineStyle.xlContinuous, Exel.XlBorderWeight.xlThick);

            Exel.Range firstcolume = xlSheet.get_Range(GetCell(2, 1), GetCell(values.GetLength(0),1));
            firstcolume.Font.Bold = true;
            firstcolume.Interior.Color = Color.LightYellow;

            Exel.Range lastcolume = xlSheet.get_Range(GetCell(2, values.GetLength(1)), GetCell(1 + values.GetLength(0), values.GetLength(1)));
            lastcolume.Interior.Color = Color.LightGreen;
        }


        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;
        }

        private void LoadData()
        {
            flats = context.Flat.ToList();
        }
    }
}
