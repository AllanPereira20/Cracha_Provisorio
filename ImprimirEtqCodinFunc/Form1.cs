using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using RawDataPrint;

namespace ImprimirEtqCodinFunc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection cnc = new SqlConnection(@"Data Source=brspjam-ap07;Initial Catalog=SGM_ONE;User ID=sa;Password=P@ssw0rd");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dt;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
           
            if (txtRegistro.Text == "")
            {
                MessageBox.Show("É necessário apresentar um registro", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Realize alguma ação caso o campo de texto esteja vazio

               

            }

            try
            {
                string pesquisa = txtRegistro.Text;

                string sql = "SELECT FUN_NOME, isNULL(FUN_CODIN,0), FUN_REGISTRO, FUN_STATUS FROM FUNCIONARIO WHERE FUN_REGISTRO = @pesquisa";

                SqlCommand cmd = new SqlCommand(sql, cnc);
                cmd.Parameters.AddWithValue("@pesquisa", pesquisa);

                cnc.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();

                        string funNome = reader.GetString(0);
                        string funCodin = reader.GetString(1);
                        int funReg = reader.GetInt32(2);
                        string funSituacao = reader.GetString(3);

                        lblNome.Text = funNome;
                        lblCodin.Text = funCodin;
                        lblReg.Text = funReg.ToString();
                        lblSituacao.Text = funSituacao;

                        if (lblSituacao.Text == "ATIVO")
                        {
                            
                        }
                        else
                        {
                            MessageBox.Show("Usuário desligado!", "Atenção!", MessageBoxButtons.OK);
                            txtRegistro.Focus();
                            txtRegistro.Clear();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Funcionário não encontrado!","Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtRegistro.Focus();
                        txtRegistro.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar o funcionário: " + ex.Message);
            }
            finally
            {
                cnc.Close();
            }
            
        }

        private void lblCodin_Click(object sender, EventArgs e)
        {

        }
        private void etiqueta()
        {
            Console.ReadLine();
            SqlCommand cmd = new SqlCommand("SELECT FUN_NOME, ISNULL(FUN_CODIN,0) as FUN_CODIN, FUN_REGISTRO, FUN_STATUS FROM FUNCIONARIO WHERE FUN_REGISTRO = @pesquisa;", cnc);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@pesquisa", txtRegistro.Text);

            cnc.Open();

            SqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.HasRows) // Verifica se há registros no dataReader
            {
                dataReader.Read();

                string dadosImpressao = "^XA";
                dadosImpressao += "~DG000.GRF,03072,012,";
                dadosImpressao += ",::::::::::::::::::::::::::::O03E0,O07F0,O0HF8,N01FFC,::::O0HF8,O07F0,P080,,:::::::::::::::::::::::::::::::::L075,L07F80,L07FF4,L07FHF80,L05FIF5,M03FIFC0,N057FHF4,O01FIF,P01FHF,R0HF,R05F,Q03FF,O015FHF,O07FIF,M017FIF4,M0KF80,L07FHFD0,L07FFE80,L07FF4,L07F80,L050,,::::L0540I015,L0780J0F,L07C0I01F,L0780J0F,L07C0I01F,L0780J0F,L07D5J5F,L07FLF,::::L07C0I01F,L0780J0F,L07C0I01F,L0780J0F,L07C0I01F,L0780J0F,L010J0H1,,::::L07C,L078,L07C,L078,L07C,L078,L07C,:L07FLF,:::L07D757575,L078,L07C,L078,L07C,L078,L07C,L078,L054,,::N054,M03FE,L017FF,M0IF80,L01FHFC0,L03F9FE0,L03F07F0,L03E03E0,L07C01F0,::L07800F0,L07C01F0,L07800F0,L07C01F0,L07E2BFA2A,L07FLF,:::L0N5,,:::S01,R017,R0HF,P057FF,O01FIF,N017FHFD,N0JFE0,L017FIF,L07FHF8F80,L07FFD0F,L07F800F80,L07D0H0F,L07FE00F80,L07FFD0F,L07FHFEF80,L017FIF40,N0KF8,N015FIF,P0JF,Q07FF,R0BF,R015,,:::::::::::::::::::::::::::::::::O03E0,O07F0,O0HF8,N01FFC,::::O0HF8,O07F0,P080,,:::::::::::::::^XA";
                dadosImpressao += "^MMT";
                dadosImpressao += "^PW945";
                dadosImpressao += "^LL1299";
                dadosImpressao += "^LS0";
                dadosImpressao += "^FT0,1088^XG000.GRF,1,1^FS";
                dadosImpressao += "^FT135,1127^A0B,37,36^FH\\^FD" + dataReader["FUN_NOME"].ToString() + "^FS";
                dadosImpressao += "^FT138,1255^A0B,42,40^FH\\^FDNome:^FS";
                dadosImpressao += "^FT209,1098^A0B,42,40^FH\\^FD" + dataReader["FUN_REGISTRO"].ToString() + "^FS";
                dadosImpressao += "^FT208,1256^A0B,42,40^FH\\^FDRegistro:^FS";
                dadosImpressao += "^BY4,3,101^FT889,1247^B2B,,N,N";
                dadosImpressao += "^FD" + dataReader["FUN_CODIN"].ToString() + "^FS";
                dadosImpressao += "^PQ1,0,1,Y^XZ";
                dadosImpressao += "^XA^ID000.GRF^FS^XZ";

                //string dadosImpressao = "^XA~";
                //dadosImpressao += "~DG000.GRF,03072,012,";
                //dadosImpressao += ",:::::::::::T0F8,S01FC,S03FE,S07FF,::::S03FE,S01FC,T020,,:::::::::::::::::::::::::::::::::P01D40,P01FE0,P01FFD,P01FHFE0,P017FHFD40,R0KF0,R015FHFD,T07FHFC0,U07FFC0,V03FC0,V017C0,V0HFC0,T057FFC0,S01FIFC0,R05FIFD,Q03FIFE0,P01FIF4,P01FHFA0,P01FFD,P01FE0,P014,,::::P0150J0540,P01E0J03C0,P01F0J07C0,P01E0J03C0,P01F0J07C0,P01E0J03C0,P01F5J57C0,P01FLFC0,::::P01F0J07C0,P01E0J03C0,P01F0J07C0,P01E0J03C0,P01F0J07C0,P01E0J03C0,Q040J0H40,,::::P01F,P01E,P01F,P01E,P01F,P01E,P01F,:P01FLFC0,:::P01F5D5D5D40,P01E,P01F,P01E,P01F,P01E,P01F,P01E,P015,,::R015,R0HF80,Q05FFC0,Q03FFE0,Q07FHF0,Q0FE7F8,Q0FC1FC,Q0F80F8,P01F007C,::P01E003C,P01F007C,P01E003C,P01F007C,P01F8AFE8A80,P01FLFC0,:::P015L540,,:::X040,W05C0,V03FC0,T015FFC0,T07FHFC0,S05FIF40,R03FIF8,Q05FIFC0,P01FHFE3E0,P01FHF43C0,P01FE003E0,P01F4003C0,P01FF803E0,P01FHF43C0,P01FIFBE0,Q05FIFD0,R03FIFE,S057FHFC0,T03FHFC0,U01FFC0,V02FC0,W0540,,:::::::::::::::::::::::::::::::::T0F8,S01FC,S03FE,S07FF,::::S03FE,S01FC,T020,,::::::::::::::::::::::::::::::::^XA";
                //dadosImpressao += "^MMT";
                //dadosImpressao += "^PW945";
                //dadosImpressao += "^LL0508";
                //dadosImpressao += "^LS0";
                //dadosImpressao += "^FT0,384^XG000.GRF,1,1^FS";
                //dadosImpressao += @"^FT184,477^A0B,25,24^FH\^FD" + dataReader["FUN_NOME"].ToString() + "^FS";
                //dadosImpressao += @"^FT306,481^A0B,25,24^FH\^FD" + dataReader["FUN_REGISTRO"].ToString() + "^FS";
                //dadosImpressao += @"^FT132,480^A0B,33,33^FH\^FDNome:^FS";
                //dadosImpressao += @"^FT253,479^A0B,33,33^FH\^FDRegistro:^FS";
                //dadosImpressao += "^BY3,3,103^FT928,467^B2B,,N,N";
                //dadosImpressao += "^FD" + dataReader["FUN_CODIN"].ToString() + "^FS";
                //dadosImpressao += "^PQ1,0,1,Y^XZ";
                //dadosImpressao += "^XA^ID000.GRF^FS^XZ";



                string printerName = "CRACHA";
                RawPrinterHelper.SendStringToPrinter(printerName, dadosImpressao);

               //// PrintDialog printDialog = new PrintDialog();
               // if (printDialog.ShowDialog() == DialogResult.OK)
               // {
               //     // Obtém a impressora selecionada pelo usuário
               //     //string printerName = printDialog.PrinterSettings.PrinterName;
               //     string printerName = "CRACHA";

               //     // Imprime na impressora selecionada
               //     if (RawPrinterHelper.SendStringToPrinter(printerName, dadosImpressao))
               //     {
               //         MessageBox.Show("Etiqueta gerada com sucesso!", "Etiqueta", MessageBoxButtons.OK, MessageBoxIcon.Information);
               //     }
               //     else
               //     {
               //         MessageBox.Show("Falha ao imprimir etiqueta", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
               //     }
               // }
            }
            else
            {
                MessageBox.Show("Nenhum registro encontrado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRegistro.Focus();
                        txtRegistro.Clear();
            }

            cnc.Close();
            dataReader.Close();
            cmd.Dispose();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            etiqueta();
        }

        private void txtRegistro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtRegistro.Text == "")
                {
                    MessageBox.Show("É necessário apresentar um registro", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Realize alguma ação caso o campo de texto esteja vazio




                }

                try
                {
                    string pesquisa = txtRegistro.Text;

                    string sql = "SELECT FUN_NOME, isNULL(FUN_CODIN,0), FUN_REGISTRO, FUN_STATUS FROM FUNCIONARIO WHERE FUN_REGISTRO = @pesquisa";

                    SqlCommand cmd = new SqlCommand(sql, cnc);
                    cmd.Parameters.AddWithValue("@pesquisa", pesquisa);

                    cnc.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            string funNome = reader.GetString(0);
                            string funCodin = reader.GetString(1);
                            int funReg = reader.GetInt32(2);
                            string funSituacao = reader.GetString(3);

                            lblNome.Text = funNome;
                            lblCodin.Text = funCodin;
                            lblReg.Text = funReg.ToString();
                            lblSituacao.Text = funSituacao;

                            if (lblSituacao.Text == "ATIVO")
                            {
                                
                            }
                            else
                            {
                                MessageBox.Show("Usuário desligado!", "Atenção!", MessageBoxButtons.OK);
                                txtRegistro.Focus();
                                txtRegistro.Clear();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Funcionário não encontrado!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtRegistro.Focus();
                            txtRegistro.Clear();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao pesquisar o funcionário: " + ex.Message);
                }
                finally
                {
                    cnc.Close();
                }

            }
        }
    }
    }
    

