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
    /// Interaction logic for Friends.xaml
    /// </summary>
    public partial class Friends : Window
    {
        int _id;
        public Friends(int id)
        {
            _id = id;
            InitializeComponent();

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

        private void DataGridFriends_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            UserModel chatModel = (UserModel)DataGridFriends.SelectedItem;

            
            WallFriend WindowProg = new WallFriend(chatModel.Id,_id);
            WindowProg.Show();
            this.Close();

        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonWall_Click(object sender, RoutedEventArgs e)
        {
            Wall WindowProg = new Wall(_id);
            WindowProg.Show();
            this.Close();
        }

        private void ButtonMessage_Click(object sender, RoutedEventArgs e)
        {
            Chat WindowProg = new Chat(_id);
            WindowProg.Show();
            this.Close();
        }
    }
}
