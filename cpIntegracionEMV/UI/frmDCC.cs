using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using cpIntegracionEMV.data;
using cpIntegracionEMV.util;

namespace cpIntegracionEMV.UI
{
    public partial class frmDCC : Form
    {
        public frmDCC()
        {
            InitializeComponent();
        }

        private void frmDCC_Load(object sender, EventArgs e)
        {
            TRINP.dcc_type = "LOCAL";
            listViewOrig.Clear();
            
            if(TRINP.Tx_Tip.Equals(""))
            {
                listViewOrig.Items.Add(" ");
                listViewOrig.Items.Add(" ");
                listViewOrig.Items.Add(" ");
                listViewOrig.Items.Add(" Amount: $" + TRINP.Tx_Amount + " " + TRINP.Tx_Currency);
            }
            else
            {
                double temp1, temp2, total;
                double.TryParse(TRINP.Tx_Amount, out temp1);
                double.TryParse(TRINP.Tx_Tip, out temp2);
                total = temp1 + temp2;

                listViewOrig.Items.Add(" ");
                listViewOrig.Items.Add(" ");
                listViewOrig.Items.Add("SubTotal: $ " + TRINP.Tx_Amount + " " + TRINP.Tx_Currency);
                listViewOrig.Items.Add("Tip: $ " + utilidadesMIT.FormatoNumero(TRINP.Tx_Tip) + " " + TRINP.Tx_Currency);
                listViewOrig.Items.Add("Total: $ " + utilidadesMIT.FormatoNumero(total.ToString()) + " " + TRINP.Tx_Currency);
            }

            listViewCardholder.Items.Add(" ");
            listViewCardholder.Items.Add("Amount: " + Simbolo(TRRSP.cc_nbCurrencyCode) + " " + TRRSP.dcc_amount + " " + TRRSP.cc_nbCurrencyCode);
            listViewCardholder.Items.Add("Currency: " + TRRSP.cc_nbCurrency);
            listViewCardholder.Items.Add("Rate: " + TRRSP.rate);
            if(!TRRSP.nu_markup.Equals(""))
            {
                listViewCardholder.Items.Add("Exchange rate mark-up: " + TRRSP.nu_markup);                
            }
        }
        private String Simbolo(String dcc_currency)
        {
            if(dcc_currency.Trim().Equals("EUR"))
            {
                return "€";
            }
            else if(dcc_currency.Trim().Equals("GBP"))
            {
                return "£";
            }
            else if (dcc_currency.Trim().Equals("JPY"))
            {
                return "¥";
            }
            return "$";
        }
        private void btnOrig_Click(object sender, EventArgs e)
        {
            TRINP.dcc_type = "LOCAL";
            this.Close();
        }

        private void btnCardholder_Click(object sender, EventArgs e)
        {
            TRINP.dcc_type = "EXTRANJERO";
            this.Close();
        }

        private void listViewOrig_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        } 
    }
}
