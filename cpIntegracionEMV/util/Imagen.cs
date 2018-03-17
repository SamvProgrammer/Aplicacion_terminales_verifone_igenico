using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace cpIntegracionEMV.util
{
    public class Imagen
    {

        //Almacena los errores que se puedan presentar
        public string Error { get; set; }

        /// <summary>
        /// Se agrega un marca de agua a la imagen obtenida de la firma
        /// </summary>
        /// <param name="rutaImgOrig"></param>
        /// <param name="textoMarca"></param>
        /// <param name="rutaImgDest"></param>
        /// <returns></returns>
        //public static bool MarcaAguaTexto(string rutaImgOrig, string textoMarca, string rutaImgDest)
        public bool MarcaAguaTexto(string rutaImgOrig, string textoMarca, string rutaImgDest)
        {
            bool exito = false;

            try
            {
                // Cargamos la foto original
                Image imgPhoto = Image.FromFile(rutaImgOrig);

                // Obtenemos ancho y alto de la foto
                int phWidth = imgPhoto.Width;
                int phHeight = imgPhoto.Height;

                Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);
                Graphics grPhoto = Graphics.FromImage(bmPhoto);

                grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
                //grPhoto.DrawImage(imgPhoto,new Rectangle(0, 0, phWidth, phHeight),0,0,phWidth,phHeight,GraphicsUnit.Pixel);
                grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, phWidth, phHeight), 0, 0, phWidth, phHeight, GraphicsUnit.Pixel);

                //To maximize the size of the Copyright message we will test 7 different Font sizes 
                // to determine the largest possible size we can use for the width of our Photograph.
                int[] sizes = new int[] { 14, 12, 10, 8, 6, 4, 2 };
                Font crFont = null;
                SizeF crSize = new SizeF();
                for (int i = 0; i < 7; i++)
                {
                    crFont = new Font("arial", sizes[i], FontStyle.Bold);
                    crSize = grPhoto.MeasureString(textoMarca, crFont);

                    if ((ushort)crSize.Width < (ushort)phWidth)
                        break;
                }

                // Posición abajo centrada
                //int yPixlesFromBottom = 10; // (int)(phHeight * .05);
                float yPosFromBottom = 30; // ((phHeight - yPixlesFromBottom) - (crSize.Height / 2));
                float xCenterOfImg = 50; //(phWidth / 2);

                if (phWidth < 250)
                {
                    yPosFromBottom = 15;
                    xCenterOfImg = 30;
                }

                // Pintamos dos veces con el graphics el texto en la imagen con dos brush diferentes
                // con esto conseguiremos el efecto de sombra.
                StringFormat StrFormat = new StringFormat();
                StrFormat.Alignment = StringAlignment.Near;

                SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 160, 160, 160));
                //grPhoto.DrawString(textoMarca,crFont,semiTransBrush2,new PointF(xCenterOfImg + 1, yPosFromBottom + 1),StrFormat);
                grPhoto.DrawString(textoMarca, crFont, semiTransBrush2, new PointF(xCenterOfImg, yPosFromBottom), StrFormat);

                //SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));
                //grPhoto.DrawString(textoMarca, crFont, semiTransBrush, new PointF(xCenterOfImg, yPosFromBottom),StrFormat);

                // Vamos a cambiar la imagen por lo que la sacamos de memoria antes
                imgPhoto.Dispose();
                // Pasamos el bitmap a un Image para aprovecharnos de sus propiedades
                imgPhoto = bmPhoto;
                // Descargamos de memoria el graphics
                grPhoto.Dispose();
                // Guardamos la imagen
                imgPhoto.Save(rutaImgDest, ImageFormat.Png);


                imgPhoto.Dispose();

                exito = true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }

            return exito;
        }


        /// <summary>
        /// convierte los bytes de una imagen en Formato Hexadecimal
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public string BytesToHexadecimal(byte[] buffer)
        {
            var hex = new StringBuilder(buffer.Length * 2);
            foreach (byte b in buffer)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        /// <summary>
        /// Convierte una imagen en su Formato Hexadecimal
        /// </summary>
        /// <param name="pathFile">Ruta donde se busca la imagen</param>
        /// <param name="fileMarcaAgua">Archivo que se obtendrá en Hexadecimal</param>
        /// <returns></returns>
        public string ImagenToHexadecimal(string pathFile, string fileMarcaAgua)
        {
            string strHexadecimal = "";

            try
            {
                if (CarpetaExiste(pathFile))
                {
                    //se obtiene el hexadecimal de la imagen
                    //IImage objImage = new ImagenFirma();
                    byte[] image = File.ReadAllBytes(fileMarcaAgua);
                    //strHexadecimal = objImage.BytesToHexadecimal(image);
                    strHexadecimal = BytesToHexadecimal(image);
                }
                else
                    strHexadecimal = "Error: No existe el Path " + pathFile;
            }
            catch (Exception ex)
            {
                strHexadecimal = "Error: " + ex.Message;
            }

            return strHexadecimal;
        }


        /// <summary>
        /// Verifica si existe el path indicado
        /// </summary>
        /// <param name="userFilePath"></param>
        /// <returns></returns>
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



        /// <summary>
        /// Convierte un texto a imagen
        /// </summary>
        /// <param name="texto">Texto a generar imagen</param>
        /// <returns></returns>
        public bool ConvertirTextoAImagen(string rutaImgOrig, string rutaImgDest, string texto)
        {
            bool convierteImage = false;

            try
            {

                // Cargamos la foto original
                Image imgPhoto = Image.FromFile(rutaImgOrig);

                // Obtenemos ancho y alto de la foto
                int phWidth = 480; // imgPhoto.Width;
                int phHeight = 262; //imgPhoto.Height;

                Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);
                Graphics grPhoto = Graphics.FromImage(bmPhoto);

                grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
                //grPhoto.DrawImage(imgPhoto,new Rectangle(0, 0, phWidth, phHeight),0,0,phWidth,phHeight,GraphicsUnit.Pixel);
                grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, phWidth, phHeight), 0, 0, phWidth, phHeight, GraphicsUnit.Pixel);

                //To maximize the size of the Copyright message we will test 7 different Font sizes 
                // to determine the largest possible size we can use for the width of our Photograph.
                int[] sizes = new int[] { 18, 16, 14, 12, 10, 8, 6 };
                Font crFont = null;
                SizeF crSize = new SizeF();
                crFont = new Font("arial", 20, FontStyle.Bold);
                crSize = grPhoto.MeasureString(texto, crFont);

                //SizeF crSize = new SizeF();
                /*
                for (int i = 0; i < 7; i++)
                {
                    crFont = new Font("arial", sizes[i], FontStyle.Bold);
                    crSize = grPhoto.MeasureString(texto, crFont);

                    if ((ushort)crSize.Width < (ushort)phWidth)
                        break;
                }
                */

                // Posición abajo centrada
                int yPixlesFromBottom = (int)(phHeight * .05);
                float yPosFromBottom = 140; // ((phHeight - yPixlesFromBottom) - (crSize.Height / 2));
                float xCenterOfImg = (phWidth / 2);


                // Pintamos dos veces con el graphics el texto en la imagen con dos brush diferentes
                // con esto conseguiremos el efecto de sombra.
                StringFormat StrFormat = new StringFormat();
                StrFormat.Alignment = StringAlignment.Center;

                SolidBrush semiTransBrush2 = new SolidBrush(Color.Black);
                //grPhoto.DrawString(textoMarca,crFont,semiTransBrush2,new PointF(xCenterOfImg + 1, yPosFromBottom + 1),StrFormat);
                grPhoto.DrawString(texto, crFont, semiTransBrush2, new PointF(xCenterOfImg, yPosFromBottom), StrFormat);

                //SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));
                //grPhoto.DrawString(textoMarca, crFont, semiTransBrush, new PointF(xCenterOfImg, yPosFromBottom),StrFormat);

                // Vamos a cambiar la imagen por lo que la sacamos de memoria antes
                imgPhoto.Dispose();
                // Pasamos el bitmap a un Image para aprovecharnos de sus propiedades
                imgPhoto = bmPhoto;
                // Descargamos de memoria el graphics
                grPhoto.Dispose();
                // Guardamos la imagen
                imgPhoto.Save(rutaImgDest, ImageFormat.Png);


                imgPhoto.Dispose();

                convierteImage = true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }

            return convierteImage;



        }

    }
}
