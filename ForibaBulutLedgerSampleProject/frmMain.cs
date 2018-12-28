using ForibaBuluteDefterTestProject.ServiceRef;
using Ionic.Zip;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ForibaBuluteDefterTestProject
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private string GetAuthorization(string username, string pass)
        {
            string authorization = username + ":" + pass; //kullanıcı adı ve şifre. aralarında : karakteri olacak.
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(authorization);
            string base64authorization = Convert.ToBase64String(byteArray);

            return string.Format("Basic {0}", base64authorization);
        }

        private string GetHashInfo(byte[] file)
        {
            using (var md5 = MD5.Create())
            {
                byte[] aa = md5.ComputeHash(file);

                var hash = BitConverter.ToString(aa).Replace("-", "").ToLower();

                return hash;
            }
        }

        private byte[] IncludeZip(string file, string fileName)
        {
            byte[] ziplenecekData = Encoding.UTF8.GetBytes(file);

            MemoryStream zipStream = new MemoryStream();

            using (ZipFile zip = new Ionic.Zip.ZipFile())
            {
                ZipEntry zipEleman = zip.AddEntry(fileName + ".txt", ziplenecekData);

                zip.Save(zipStream);
            }

            zipStream.Seek(0, SeekOrigin.Begin);
            zipStream.Flush();

            zipStream.Position = 0;
            return zipStream.ToArray();
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            try
            {
                string defterPath = Application.StartupPath + @"\doc\12345678011233132433.txt";

                string defter = (File.ReadAllText(defterPath)).Replace("#VKN#", txtTcVkn.Text.Trim());

                byte[] zipData = IncludeZip(defter, Guid.NewGuid().ToString());

                ELedgerCloudServiceClient wsClient = new ELedgerCloudServiceClient();

                using (new System.ServiceModel.OperationContextScope((System.ServiceModel.IClientChannel)wsClient.InnerChannel))
                {
                    System.ServiceModel.Web.WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization, GetAuthorization(txtKullanici.Text.Trim(), txtSifre.Text.Trim()));

                    var req = new SaveRequest()
                    {
                        company = new Company
                        {
                            identifier = txtTcVkn.Text.Trim(),
                        },

                        source = new LedgerStream
                        {
                            fileName = "12345678011233132433.zip",
                            hash = GetHashInfo(zipData),
                            binaryData = zipData
                        }
                    };

                    var result = wsClient.save(req);

                    txtDefterID.Text = result.ledgerId.ToString();

                    MessageBox.Show(result.message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSorgula_Click(object sender, EventArgs e)
        {
            try
            {
                long defid;

                if (long.TryParse(txtDefterID.Text.Trim(), out defid))
                {
                    ELedgerCloudServiceClient wsClient = new ELedgerCloudServiceClient();

                    using (new System.ServiceModel.OperationContextScope((System.ServiceModel.IClientChannel)wsClient.InnerChannel))
                    {
                        System.ServiceModel.Web.WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization, GetAuthorization(txtKullanici.Text.Trim(), txtSifre.Text.Trim()));

                        var req = new StatusRequest()
                        {
                            company = new Company
                            {
                                identifier = txtTcVkn.Text.Trim()
                            },

                            ledgerId = defid
                        };

                        var result = wsClient.status(req);

                        MessageBox.Show(result.code + " - " + result.message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Geçersiz e-Defter ID", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
