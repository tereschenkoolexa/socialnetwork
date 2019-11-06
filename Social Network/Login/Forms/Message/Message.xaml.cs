using Login.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Interaction logic for Message.xaml
    /// </summary>
    public partial class Message : Window
    {
        public Message(int id)
        {
            InitializeComponent();

            HttpWebRequest myRequest = WebRequest.CreateHttp($"https://localhost:44359/api/message/getchat/{id}");
            myRequest.Method = "GET";
            myRequest.ContentType = "application/json";

            WebResponse wr = myRequest.GetResponse();
            try
                {
                List<ChatModel> listChatModel = new List<ChatModel>();
                using (StreamReader reader = new StreamReader(wr.GetResponseStream()))
                {
                    string responseFromServer = reader.ReadToEnd();
                    listChatModel = JsonConvert.DeserializeObject<List<ChatModel>>(responseFromServer);
                }
                DataGridChats.ItemsSource = listChatModel;
                }
            catch(Exception ex)
            { MessageBox.Show(ex.ToString()); }
            }
        private void ButtonMessage_Click(object sender, RoutedEventArgs e)
        {



        }

        private void DataGridChats_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {



        }
    }
}
