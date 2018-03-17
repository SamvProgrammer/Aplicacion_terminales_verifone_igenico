using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using cpIntegracionEMV.util;

namespace cpIntegracionEMV.UI
{
    public partial class frmFirmaPanel : Form
    {
        private bool cursorDown;
        private Point[] puntos;
        private Pen lapiz;
        private Pen goma;
        private Bitmap bmp;
        private string accion;

        private bool _heDibujado = false;

        //Variable para guardar Errores
        public string ErrorFrm { get; set; }
        
        //Texto de marca de agua
        public string TextoMarcaAgua { get; set; }

        //Variable para el formato Hex de la imagen
        public string StrHexadecimal { get; set; }

        //Para arrastrar el objeto
        private Point mouseOffset;
        private bool isMouseDown = false;
        
        public frmFirmaPanel(string textoMarcaAgua)
        {
            InitializeComponent();
            TextoMarcaAgua = textoMarcaAgua;
        }

        private void cmdCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            cursorDown = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (cursorDown)
            {
                agregarPunto(new Point(e.X, e.Y));

                if (accion == "dibujar")
                    MDibujar();

                if (accion == "borrar")
                    MBorrar();

                _heDibujado = true;
            }

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            cursorDown = false;

            //Aqui reinicializamos a puntos para que no se unan las lineas
            //al volver a dibujar
            puntos = new Point[0];

            //Marcamos como transparente en la imagen donde estamos dibujando todo aquello que sea
            //del color de fondo, esto es necesario para que al cambiar de fondo no se vea lo que borramos
            bmp.MakeTransparent(pictureBox1.BackColor);

            //ponemos la imagen dibujada como fondo
            //esto lo hago aqui y no mientras se dibuja para que el trabajo de dibujar sea mas rapido.
            pictureBox1.Image = bmp;
        }

        private void agregarPunto(Point punto)
        {
            //agrega los puntos obtenidos al arreglo
            Point[] temp = new Point[puntos.Length + 1];
            puntos.CopyTo(temp, 0);
            puntos = temp;
            puntos[puntos.Length - 1] = punto;
        }

        private void MDibujar()
        {
            //Dibuja
            if (puntos.Length > 1)
            {
                //Aqui usamos dos Graphics, uno que dibuja directamente en el pictureBox y
                //otro que dibuja en la imagen.
                //Esto ayuda a que el dibujo sea rapido ya que si dibujamos solo la imagen
                //y la vamos colocando en el pictureBox puede ser mas lento.
                Graphics g1 = pictureBox1.CreateGraphics();
                Graphics g2 = Graphics.FromImage(bmp);
                g1.DrawLines(lapiz, puntos);
                g2.DrawLines(lapiz, puntos);
                g1.Dispose();
                g2.Dispose();
            }
        }

        private void MBorrar()
        {
            //Borra, es lo mismo que dibujar solo que el objeto Pen es otro
            if (puntos.Length > 1)
            {
                Graphics g1 = pictureBox1.CreateGraphics();
                Graphics g2 = Graphics.FromImage(bmp);
                g1.DrawLines(goma, puntos);
                g2.DrawLines(goma, puntos);
                g1.Dispose();
                g2.Dispose();
            }
        }

        private void frmFirmaPanel_Load(object sender, EventArgs e)
        {
            Inicializar();
            ErrorFrm = "";

            LimpiaPictureBox();
            cursorDown = false;

            cmdGuardar.Enabled = true;
            cmdLimpiar.Enabled = true;
            pictureBox1.Visible = true;
            pictureBox1.Enabled = true;
            lblFirma.Visible = true;

            this.Top -= 200;
        }


        private void Inicializar()
        {
            accion = "dibujar";
            puntos = new Point[0];

            //Creo el lapiz y la goma y defino el tipo de linea
            lapiz = new Pen(Color.Black, 1);
            lapiz.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            lapiz.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            lapiz.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            
            goma = new Pen(Color.White, 10);
            goma.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            goma.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            goma.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;

            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            //creo el cursor del lapiz a partir de una imagen que tengo como recurso
            IntPtr intprCursor = cpIntegracionEMV.Properties.Resources.LapizC.GetHicon();
            pictureBox1.Cursor = new Cursor(intprCursor);
            
        }

        private void cmdObtiene_Click(object sender, EventArgs e)
        {
            LimpiaPictureBox();
            cursorDown = false;

            cmdGuardar.Enabled = true;
            cmdLimpiar.Enabled = true;
            pictureBox1.Visible = true;
            pictureBox1.Enabled = true;
            lblFirma.Visible = true;
        }

        private void cmdGuardar_Click(object sender, EventArgs e)
        {

            try
            {
                
                  if (_heDibujado)
                  { 
                        Bitmap fondo = new Bitmap(bmp.Width, bmp.Height);
                        Graphics g = Graphics.FromImage(fondo);
                        g.FillRectangle(new SolidBrush(pictureBox1.BackColor), 0, 0, bmp.Width, bmp.Height);
                        g.Dispose();
                        Unir(fondo);
                
                        //se guarda la imagen
                        string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                        string userFilePath = Path.Combine(localAppData, "MIT");
                        string file = userFilePath + "\\firmaPanel.png";
                        string fileAgua = userFilePath + "\\firmaPanelAgua.png";

                        if (CarpetaExiste(userFilePath))
                        {
                            bmp.Save(file, ImageFormat.Png);
                            
                            //Imagen.MarcaAguaTexto(file, TextoMarcaAgua, fileAgua);
                            Imagen objImage = new Imagen();
                            objImage.MarcaAguaTexto(file, TextoMarcaAgua, fileAgua);
                            //pictureBox1.Load(fileAgua);

                            //se obtiene el hexadecimal de la imagen
                            byte[] image = File.ReadAllBytes(fileAgua);
                            //StrHexadecimal = Imagen.BytesToHexadecimal(image);
                            StrHexadecimal = objImage.BytesToHexadecimal(image);

                            //Se inhabilitan los controles
                            pictureBox1.Enabled = false;
                            cmdLimpiar.Enabled = false;
                            cmdGuardar.Enabled = false;

                            this.Close();
                        }
                }
                else
                  MessageBox.Show("Debes firmar en el recuadro blanco.");

            }
            catch(Exception ex)
            {
                ErrorFrm = ex.Message;
            }
        }


        private void Unir(Bitmap fondo)
        {
            //Este metodo une un fondo con lo que se ha dibujado.
            //esto es necesario ya que lo que dibujamos se dibuja sobre un fondo transparente
            //y si no lo unimos puede traer problemas al salvar la imagen o al cambiarle el tamaño
            //al area de dibujo.
            Graphics g = Graphics.FromImage(fondo);
            g.DrawImage(bmp, 0, 0);
            bmp = new Bitmap(fondo);
            g.Dispose();
            fondo.Dispose();
        }

        private void cmdLimpiar_Click(object sender, EventArgs e)
        {
            LimpiaPictureBox();
            _heDibujado = false;
        }

        private void LimpiaPictureBox()
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;
        }

        

        private void CrearGoma()
        {
            //en realidad este metodo lo que hace es crear un cursor para la goma a partir de dos
            //elipses, de esta manera el cursor queda como un circulo
            int diametroG = Convert.ToInt32(goma.Width);
            Bitmap Goma = new Bitmap(diametroG, diametroG);

            Graphics gGoma = Graphics.FromImage(Goma);
            gGoma.FillRectangle(Brushes.Magenta, 0, 0, diametroG, diametroG);
            gGoma.FillEllipse(Brushes.White, 0, 0, diametroG - 1, diametroG - 1);
            gGoma.DrawEllipse(new Pen(Color.Black, 1), 0, 0, diametroG - 1, diametroG - 1);
            Goma.MakeTransparent(Color.Magenta);
            gGoma.Dispose();

            IntPtr intprCursorGoma = Goma.GetHicon();
            pictureBox1.Cursor = new Cursor(intprCursorGoma);

        }



        private bool CarpetaExiste(string userFilePath)
        {
            bool existe = false;

            if (!Directory.Exists(userFilePath))
            {
                Directory.CreateDirectory(userFilePath);
                existe = true;
            }
            else
                existe = true;

            return existe;
        }

        private void frmFirmaPanel_MouseDown(object sender, MouseEventArgs e)
        {
            int xOffset;
            int yOffset;

            if (e.Button == MouseButtons.Left)
            {
                xOffset = -e.X - SystemInformation.FrameBorderSize.Width;
                yOffset = -e.Y - SystemInformation.CaptionHeight -
                    SystemInformation.FrameBorderSize.Height;
                mouseOffset = new Point(xOffset, yOffset);
                isMouseDown = true;
            }   
        }

        private void frmFirmaPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }

        private void frmFirmaPanel_MouseUp(object sender, MouseEventArgs e)
        {
            // Changes the isMouseDown field so that the form does
            // not move unless the user is pressing the left mouse button.
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }






    }
}
