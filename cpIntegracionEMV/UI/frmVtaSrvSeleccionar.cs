using cpIntegracionEMV.data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using cpIntegracionEMV.util;

namespace cpIntegracionEMV.UI
{
    public partial class frmVtaSrvSeleccionar : Form
    {
        private string AuxXML;
        private string AuxCat;

        private string productoXML;
        private string carrier;

        private string idCategoria, idProducto, idProveedor;

        public frmVtaSrvSeleccionar()
        {
            InitializeComponent();
        }

        private void CmdAceptar_Click(object sender, EventArgs e)
        {
            if (CboProductos.SelectedIndex != -1)
            {
                idProducto = utilidadesMIT.GetDataXML("id", utilidadesMIT.GetDataXML("producto" + (CboProductos.SelectedIndex + 1), AuxCat));
                idProveedor = utilidadesMIT.GetDataXML("id_proveedor", utilidadesMIT.GetDataXML("producto" + (CboProductos.SelectedIndex + 1), AuxCat));
                TRINP.TAEidCategoria = idCategoria;
                TRINP.TAEidProducto = idProducto;
                TRINP.TAEidProveedor = idProveedor;

                carrier = utilidadesMIT.GetDataXML("descripcion", AuxCat);
                productoXML = utilidadesMIT.GetDataXML("descripcion", utilidadesMIT.GetDataXML("producto" + (CboProductos.SelectedIndex + 1), AuxCat));
                productoXML = productoXML.Replace(carrier,"").Replace(" ","");
                TRINP.TAEAmount = productoXML;

                this.Close();
            }
            else
            {
                MessageBox.Show("Selecciona un producto", "Centro de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            
        }

        private void frmVtaSrvSeleccionar_Load(object sender, EventArgs e)
        {
            AuxXML = TypeUsuario.RESPRODUCTOS;
            this.ObtenerCategorias();
        }

        private void ObtenerCategorias()
        {
            string StrCategoria;
            int i = 1;
            CboCategoria.Items.Clear();
            //CboCategoria.Items.Add("Selecciona una categoría");


            while (!utilidadesMIT.GetDataXML("categoria" + i, AuxXML).Equals(""))
            {
                StrCategoria = utilidadesMIT.GetDataXML("descripcion", utilidadesMIT.GetDataXML("categoria" + i, AuxXML));
                CboCategoria.Items.Add(StrCategoria);
                i+=1;
            }

        }

        private void CboCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            fraProducto.Visible = true;
            AuxCat = utilidadesMIT.GetDataXML("categoria" + (CboCategoria.SelectedIndex + 1), AuxXML);
            idCategoria = utilidadesMIT.GetDataXML("id_categoria", AuxCat);
            this.ObtenerProductos(AuxCat);
        }

        private void CboCategoria_Click(object sender, EventArgs e)
        {
            fraProducto.Visible = false;
        }

        private void ObtenerProductos(string productos)
        {
            string StrProducto, strProveedor;
            int i = 1;
            
            CboProductos.Items.Clear();

            while (!utilidadesMIT.GetDataXML("producto" + i, productos).Equals(""))
            {
                StrProducto = utilidadesMIT.GetDataXML("descripcion", utilidadesMIT.GetDataXML("producto" + i, productos));
                strProveedor = utilidadesMIT.GetDataXML("desc_proveedor", utilidadesMIT.GetDataXML("producto" + i, productos));
                CboProductos.Items.Add(StrProducto + "   ---   " + strProveedor);
                i += 1;
            }

        }

        

    }
}
