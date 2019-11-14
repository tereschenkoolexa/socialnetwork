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
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class Chat : Window
    {
        int _idU, _idC;
        public Chat(int id)
        {
            InitializeComponent();
            _idU = id;
            HttpWebRequest myRequest = WebRequest.CreateHttp($"https://localhost:44359/api/message/getchat/{id}");
            myRequest.Method = "GET";
            myRequest.ContentType = "application/json";
            WebResponse wr = myRequest.GetResponse();
            List<ChatModel> listChatModel = new List<ChatModel>();
            try
            {
                using (StreamReader reader = new StreamReader(wr.GetResponseStream()))
                {
                    string responseFromServer = reader.ReadToEnd();
                    listChatModel = JsonConvert.DeserializeObject<List<ChatModel>>(responseFromServer);
                }
                DataGridChats.ItemsSource = listChatModel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonWall_Click(object sender, RoutedEventArgs e)
        {
            Wall WindowProg = new Wall(_idU);
            WindowProg.Show();
            this.Close();
        }

        private void ButtonFriends_Click(object sender, RoutedEventArgs e)
        {
            Friends WindowProg = new Friends(_idU);
            WindowProg.Show();
            this.Close();
        }

        private void DataGridChats_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChatModel chatModel = (ChatModel)DataGridChats.SelectedItem;

            _idC = chatModel.Id;
            Message WindowProg = new Message(_idC, _idU);
            WindowProg.Show();
            this.Close();

        }
    }
}
