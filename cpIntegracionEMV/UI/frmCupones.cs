using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace cpIntegracionEMV.UI
{
    public partial class frmCupones : Form
    {
        private int topControl;
        private int contador;
        private string strXML;

        public frmCupones()
        {
            InitializeComponent();
        }

        public frmCupones(string XML)
        {
            InitializeComponent();
            contador = 0;
            strXML = XML;
        }

        public int NumeroCupon { get; set; }
        public string Respuesta { get; set; }

        private void frmCupones_Load(object sender, EventArgs e)
        {
            CreaCupones();
            this.Top += 20;
            NumeroCupon = -1;

            tabCupon.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabCupon.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabCupon_DrawItem);
        }


        private void CreaCupones()
        {
            TabPage myTabPage;
            RadioButton rb;
            XmlDocument docXML;
            XmlNodeList xnList;
            int contTabs = 0;
            int elementosAgregados = 0;
            bool agregaTab = false;

            docXML = new XmlDocument();
            docXML.LoadXml(strXML);
            xnList = docXML.SelectNodes("/PNPRESPONSE/datos/coupon");

            topControl = 20;

            myTabPage = new TabPage("Página " + (contTabs + 1));
            myTabPage.BackColor = Color.White;

            for (int i = 0; i < xnList.Count; i++)
            {

                if (agregaTab)
                {
                    myTabPage = new TabPage("Página " + (contTabs + 1));
                    myTabPage.BackColor = Color.White;
                    agregaTab = false;
                }
                else
                    agregaTab = false;
                
                rb = new RadioButton();
                rb.Name = "miBoton" + contador;

                rb.Text = xnList[i].ChildNodes[1].InnerText.Replace("|", "\n");
                rb.Top = topControl;
                rb.Tag = contador;
                rb.Visible = true;
                rb.BackColor = Color.White;
                rb.Width = 200;
                rb.Height = 40;
                rb.Left = 10;
                rb.Click += new EventHandler(RB_Click);

                myTabPage.Controls.Add(rb);

                contador++;
                topControl += 40; 
                elementosAgregados++;

                if (elementosAgregados > 4 )
                {
                    tabCupon.TabPages.Add(myTabPage);
                    contTabs++;
                    agregaTab = true;

                    //se reinician las posiciones
                    topControl = 20;
                    elementosAgregados = 0;
                    myTabPage = null;
                }
                

            }

            if (elementosAgregados > 0)
            {
                tabCupon.TabPages.Add(myTabPage);
                contTabs++;
            }

            
        }

        protected void RB_Click(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            int code;
            int.TryParse(rb.Tag.ToString(), out code);
            NumeroCupon = code;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Respuesta = "1";
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Respuesta = "2";

            if (NumeroCupon != -1)
                this.Close();
            else
                MessageBox.Show("Debes seleccionar un cupón.");
        }



        private void tabCupon_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            TabPage tp = tabCupon.TabPages[e.Index];
            Brush br;
            StringFormat sf = new StringFormat();
            RectangleF r = new RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height - 2);

            sf.Alignment = StringAlignment.Center;

            String strTitle = tp.Text;
            //If the current index is the Selected Index, change the color
            if (tabCupon.SelectedIndex == e.Index)
            {
                //this is the background color of the tabpage
                //you could make this a stndard color for the selected page
                br = new SolidBrush(Color.White);
                //this is the background color of the tab page
                g.FillRectangle(br, e.Bounds);
                //this is the background color of the tab page
                //you could make this a stndard color for the selected page
                br = new SolidBrush(Color.Black);
                g.DrawString(strTitle, tabCupon.Font, br, r, sf);
            }
            else
            {
                //these are the standard colors for the unselected tab pages
                br = new SolidBrush(Color.WhiteSmoke);
                g.FillRectangle(br, e.Bounds);
                br = new SolidBrush(Color.Black);
                g.DrawString(strTitle, tabCupon.Font, br, r, sf);
            }




        }



    }
}
