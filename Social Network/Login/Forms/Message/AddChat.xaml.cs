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
    /// Interaction logic for AddChat.xaml
    /// </summary>
    public partial class AddChat : Window
    {
        public List<int> idUsers = new List<int>();
        int _id;
        public AddChat(int id)
        {
            InitializeComponent();

            _id = id;
            InitializeComponent();
            idUsers.Add(id);
            HttpWebRequest myRequest = WebRequest.CreateHttp($"https://localhost:44359/api/user/getusers/{_id}");
            myRequest.Method = "GET";
            myRequest.ContentType = "application/json";
            WebResponse wr = myRequest.GetResponse();
            List<UserModel> listFriendsModel = new List<UserModel>();
            try
            {
                using (StreamReader reader = new StreamReader(wr.GetResponseStream()))
                {
                    string responseFromServer = reader.ReadToEnd();
                    listFriendsModel = JsonConvert.DeserializeObject<List<UserModel>>(responseFromServer);
                }
                DataGridFriends.ItemsSource = listFriendsModel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            ChatModelAdd chatModel = new ChatModelAdd() { Name = NameChat.Text, idUsers = idUsers }; 
            HttpWebRequest myRequest = WebRequest.CreateHttp($"https://localhost:44359/api/message/PostChat");
            myRequest.Method = "POST";
            myRequest.ContentType = "application/json";
            using (StreamWriter writer = new StreamWriter(myRequest.GetRequestStream()))
            {
                writer.Write(JsonConvert.SerializeObject(chatModel));
            }
            WebResponse wr = myRequest.GetResponse();

        }

        private void DataGridFriends_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UserModel userModel = (UserModel)DataGridFriends.SelectedItem;
            
                    idUsers.Add(userModel.Id);

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Chat WindowProg = new Chat(_id);
            WindowProg.Show();
            this.Close();
        }
    }
}
