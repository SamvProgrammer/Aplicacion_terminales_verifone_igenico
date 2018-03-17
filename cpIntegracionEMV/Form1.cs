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
using cpIntegracionEMV.security;
namespace cpIntegracionEMV
{
    public partial class frmTest : Form
    {
        clsCpIntegracionEMV cp = new clsCpIntegracionEMV();
        public frmTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cp.dbgSetUrl("https://dev3.mitec.com.mx");
            cp.dbgLoginUser("A415MIUS0", "TEMPORAL1");
            cp.dbgSetUrlIpKeyWeb("https://dev10.mitec.com.mx");
            cp.ObtieneLlavePublicaRSA();
            /*cp.dbgInitDUKPT();
            cp.dbgSetDUKPT(Info.ksn,Info.kcv,Info.ipek);*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cp.dbgStartTxEMV("10.00"))
            {
                //get merchant
                cp.dbgGetMerchantBanda("9");
            }            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cp.sndVtaDirectaEMV(TypeUsuario.User,
                                TypeUsuario.Pass,
                                        "",
                                TypeUsuario.Id_Company,
                                TypeUsuario.Id_Branch,
                                TypeUsuario.country,
                                        "V/MC",
                                TRINP.Tx_Merchant,//merchant
                                DateTime.Now.ToLongTimeString().Replace(":", "").Substring(0, 6),//reference
                                "9",
                                "MXN"
                                      );
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            cp.dbgPrintVoucher(TRRSP.voucher_comercio);
            cp.dbgPrintVoucher(TRRSP.voucher_cliente);            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            cp.sndCancelacion(  TypeUsuario.User,
                                TypeUsuario.Pass,
                                "",
                                TypeUsuario.Id_Company,
                                TypeUsuario.Id_Branch,
                                TypeUsuario.country,
                                "10.00",
                                textBox1.Text.Trim(),
                                textBox2.Text.Trim()
                              );
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            cp.sndVtaRecompensasDirecto(TypeUsuario.User,
                                        TypeUsuario.Pass,
                                        TypeUsuario.Id_Company,
                                        TypeUsuario.Id_Branch,
                                        TypeUsuario.country,
                                        DateTime.Now.ToLongTimeString().Replace(":", "").Substring(0, 6),//reference
                                        "00.01",
                                        "101");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            cp.sndVtaRecompensasDirecto(TypeUsuario.User,
                                        TypeUsuario.Pass,
                                        TypeUsuario.Id_Company,
                                        TypeUsuario.Id_Branch,
                                        TypeUsuario.country,
                                        DateTime.Now.ToLongTimeString().Replace(":", "").Substring(0, 6),//reference
                                        "200.00",
                                        "102");
        }

        private void button8_Click(object sender, EventArgs e)
        {

            String x = RC4.Decrypt("1DE8FC91C316F30B912C106CDE20FCCE7DD196C5A418D0E5247A0F1A348E9D1439954C0F18706370A8E04A8713C54896A1FC25D0491BA19185A9283C3868B462A5DE11BC1B5D1B035A19EA040DF21F32FC1A12416149C2E29CA6456B90FC1B726A79ADCFDDEF647AF7F9475DDA5D622BBF0096542145E6D7F456E1D653090E3DF6B19617ABA8C58A902024E167F3FEB9CA7B9899476B55D4A449BC1EABD5B36232A68C4DA578E322AF24E5283CBB45FEC7007DB810D76AB885CC4159577C8673F770F3EA9DF6E4A3EBF412CE4D705E9208BFE1414AA5C008BF884A895FF873E2A05DCB6FCFC687108FC16DC356578AAC903D2BD64DF71D864B7127972BF97BBDB1F45C95A7FCBA9BA38BC79954E755D17E2E9A8DB758F87AA5A400BD9C89B884CDDF019849DB84146BBC02AD0B234DB43C9994CCCB19F3290FC2B5FF1969BBC2B3A83188FDBB93CD11CB13C463E213EF4A1579DEBF28B4AD850131F0A89A72E43CE1AD7DCDF4D2EF52969CFE71A05382301A3351582484D31DEE4B6109302C3BFB989385794AA8D9C82D81FC01AB2B0EB98B229CC1303D6BA75099A04983561AFA182EF3C640B41A578F4CCD56584BAF11E2E14752EF2CEA960B88C5211D0A126515CCC001A1703174C33ADA90390F9F27DE219CAA46F1927951AC09D7466264F1F104C0E98D20FF761C26281501E5684DA2A6F7ED71850EC240853A6F7598BC0A7DB7FC60A0B513BEA7724EF8C950B4828A6CFC0CF989EE3D035A0D4F8CBE7BD9D1AEACE2A7F6A7FFC7F702253CD40AE70AAF5B1BFF6530FD16551281585EB088C487A9310CF281F247F00CDA4F43FD039439A57F31E24A032399EC201D09BEBB83BF1A50716A2D616EAB94ABD87D5ED79E3902C56215D1216244C35FD151F8AF64E7CF37F2AAEE37D13E698F26190FD3E7AC87FB3FF1637535FFA926C510DD5BEDC8076FC859973EB48B4EE32BEFC1010A3DF4AF99AFE0A0A42B8B3E3A2D3A7EF8C993ADFBFE31877744F25FADBFB6D367847DB959E684DC8352902C18CB335752182C295D2E557A1A1249A9421CA2690EF114219906548260FFCDE18740806F3C9823DFA0A8EAA4443DF1A529410E1E6DE90378821A946F4B6876AB3300CB5502214F5939805A6475C15DDBCE1DF33FF19620793FA84B67BAB8D813E8C0A52B2DFF1E50BA82FD9D90BC394B46EE244A3B7E8276EB3112F4DF5416601EB06DC206536D4CDE00F1890FC12D8E128FF3B3CBF0FE3EA9F405B95DAAFED5FB020941EDD2B4A2E083A427D311E547558946161A4F0A7B419567B888ED9795E9481A362612D4A87F47CA149855454200C2579C88509B5AC348213E30123C", Info.RC4Key);
            x = "1";
        }
    }
}
