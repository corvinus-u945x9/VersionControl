using IRF_week06.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRF_week06
{
    public partial class Form1 : Form
    {
        BindingList<RateDate> rates = new BindingList<RateDate>();
        
        
        public Form1()
        {
            InitializeComponent();
            Fügvény();
            dataGridView1.DataSource = rates;
        }

        private void Fügvény()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };

            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;
        }
    }
}
