using System;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace TestSend
{
    public partial class Form1 : Form
    {
        string BaseURL = "http://aymanx1-001-site4.dtempurl.com/api/WhatappSender/"; // Replace with your API endpoint

        string filePath = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            openFileDialog.Title = "Select a File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
        }

        private void sendInvoice_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();   
            SendInvoieWithFile( txtReceiver.Text, txtSender.Text,txtShopName.Text,txtTotal.Text,txtFileName.Text);
        }


        private async Task SendInvoieWithFile(string Receiver,string Sender , string ShopName , string Total,string FilePath)
        {


            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization
                       = new AuthenticationHeaderValue("Bearer", txtToken.Text);
            using (var requ = new MultipartFormDataContent())
            {
                using (var filestram = File.OpenRead(filePath))
                {
                    requ.Add(new StreamContent(filestram), "file", "ahmed.pdf");
                    requ.Add(new StringContent(Receiver), "To");
                    requ.Add(new StringContent(Sender), "Phone");
                    requ.Add(new StringContent(ShopName), "ShopName");
                    requ.Add(new StringContent(Total), "InvocieTotal");
                    requ.Add(new StringContent(FilePath), "FileName");
                    var res = await client.PostAsync($"{BaseURL}SendTemplate", requ);
                    if (res.StatusCode == HttpStatusCode.OK)
                    {
                        MessageBox.Show("Sent Success");
                    }
                    else {
                        MessageBox.Show($"Error In Data {res.StatusCode} - {res.ReasonPhrase}" );
                    }
                    richTextBox1.Text = res.ToString();
                }
            }
        }


        private async Task SendInvoieData(string Receiver, string Sender, string ShopName, string Total)
        {


            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization
                       = new AuthenticationHeaderValue("Bearer", txtToken.Text);
            using (var requ = new MultipartFormDataContent())
            {
                requ.Add(new StringContent(Receiver), "To");
                requ.Add(new StringContent(Sender), "Phone");
                requ.Add(new StringContent(ShopName), "ShopName");
                requ.Add(new StringContent(Total), "InvocieTotal");
                var res = await client.PostAsync($"{BaseURL}SendTemplate", requ);
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    MessageBox.Show("Sent Success");
                }
                else
                {
                    MessageBox.Show($"Error In Data {res.StatusCode} - {res.ReasonPhrase}");
                }
                richTextBox1.Text = res.ToString();
            }
        }

    }
}





