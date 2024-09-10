using System.Net.Http;
using System.Text;
using System.Windows;
using Newtonsoft.Json; // Install Newtonsoft.Json via NuGet Package Manager

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            sen();
        }



        private async void sen() {

            var accessToken = "EAAVeni00TlEBOZCVZBG42xMinuGA0ZA4ZBjyM3EEhugzhW4AoCZAvQAORtFZBYXe5g3FCQ2yEhebZBziJnGZAynwR8XijdkWwCXA3ty6WUqy5andALl25UyLPMQrZAsVgEYjchpIBUgHmAlOLJrIZCwRUIFUoPfBHgLD8h7jjgN0n5EceQ30FQ40E18Ngu3dQqPMh87hB7yzrwB9xVFUPicdmFO1OWnHLQL3b715NrtOaO";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization
                    = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

                var body = new Class1()
                {
                    messaging_product = "whatsapp",
                    to = "966559786796",
                    type = "template",
                    template = new Template()
                    {
                        name = "orders",
                        language = new Language() { code = "en" },
                        components = [
                            new Component(){
                                type="header",
                                parameters =[
                                new Parameter(){
                                type="document"
                                ,document= new Document(){
                                filename="file"
                                ,
                                link = link.Text.ToString()
                                }
                                }]},
                                new Component(){
                                type="body",
                                parameters =[
                                new Parameter(){
                                type="text"
                                ,text = text1.Text.ToString(),
                                },new Parameter(){
                                type="text"
                                ,text = text2.Text.ToString(),
                                },new Parameter(){
                                type="text"
                                ,text = text3.Text.ToString(),
                                }]},
                            ]
                    },


                };

                string json = JsonConvert.SerializeObject(body);

                // Prepare the content to be sent in the HTTP request
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                mess.Text = json.ToString();    
                // Send the HTTP POST request
                var response = await client.PostAsync($"https://graph.facebook.com/v17.0/338960399307002/messages", content);


                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Message sent successfully!");
                    mess2.Text = "Message sent successfully!"; 
                }
                else
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to send message. Status: {response.StatusCode}. Response: {responseContent}");
                    mess2.Text += $"Failed to send message. Status: {response.StatusCode}. Response: {responseContent}";

                }
            }
        }
    }
}