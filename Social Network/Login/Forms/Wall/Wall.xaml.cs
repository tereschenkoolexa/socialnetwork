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
    /// Interaction logic for Wall.xaml
    /// </summary>
    public partial class Wall : Window
    {
        int _id;
        public Wall(int id)
        {
            InitializeComponent();
            _id = id;
            HttpWebRequest myRequestUser = WebRequest.CreateHttp($"https://localhost:44359/api/user/get/{id}");
            myRequestUser.Method = "GET";
            myRequestUser.ContentType = "application/json";
            try
            {
                WebResponse wr = myRequestUser.GetResponse();
                UserModel userModel = new UserModel();
                using (StreamReader reader = new StreamReader(wr.GetResponseStream()))
                {
                    string responseFromServer = reader.ReadToEnd();
                    userModel = JsonConvert.DeserializeObject<UserModel>(responseFromServer);
                }
                FirstAndSecondNameTextBox.Content = userModel.Name;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            HttpWebRequest myRequestWall = WebRequest.CreateHttp($"https://localhost:44359/api/wall/get/{id}");
            myRequestWall.Method = "GET";
            myRequestWall.ContentType = "application/json";
            try
            {
                WebResponse wr = myRequestWall.GetResponse();
                WallModel wallModel = new WallModel();
                using (StreamReader reader = new StreamReader(wr.GetResponseStream()))
                {
                    string responseFromServer = reader.ReadToEnd();
                    wallModel = JsonConvert.DeserializeObject<WallModel>(responseFromServer);
                }
                StatusTextBox.Text = wallModel.Status;
                AgeLabel.Content = wallModel.Age;
                CityLabel.Content = wallModel.City;
                CountryLabel.Content = wallModel.Country;

            }
            catch (Exception ex)
            {
                MessageBox.Show("У вас Бан");
            }



        }

        private void ButtonMessage_Click(object sender, RoutedEventArgs e)
        {
            Chat WindowProg = new Chat(_id);
            WindowProg.Show();
            this.Close();


        }

        private void ButtonFriends_Click(object sender, RoutedEventArgs e)
        {
            Friends WindowProg = new Friends(_id);
            WindowProg.Show();
            this.Close();
        }
    }
}
