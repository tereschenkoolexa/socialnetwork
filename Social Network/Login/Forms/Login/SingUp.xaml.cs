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

namespace UI.Forms
{
    /// <summary>
    /// Interaction logic for SingUp.xaml
    /// </summary>
    public partial class SingUp : Window
    {
        public SingUp()
        {
            InitializeComponent();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {

            UserModel userModelSignUp = new UserModel() { Gmail = LoginTextBox.Text, Password = PasswordTextBox.Text, Name = NameTextBox.Text };
            HttpWebRequest myRequestSignUp = WebRequest.CreateHttp("https://localhost:44359/api/user/signup");
            myRequestSignUp.Method = "POST";
            myRequestSignUp.ContentType = "application/json";
            using (StreamWriter writer = new StreamWriter(myRequestSignUp.GetRequestStream()))
            {
                writer.Write(JsonConvert.SerializeObject(userModelSignUp));
            }
            WebResponse wrSignUp = myRequestSignUp.GetResponse();

            UserModel userModel = new UserModel() { Gmail = LoginTextBox.Text, Password = PasswordTextBox.Text };
            HttpWebRequest myRequest = WebRequest.CreateHttp("https://localhost:44359/api/user/login");
            myRequest.Method = "POST";
            myRequest.ContentType = "application/json";
            using (StreamWriter writer = new StreamWriter(myRequest.GetRequestStream()))
            {
                writer.Write(JsonConvert.SerializeObject(userModel));
            }
            WebResponse wr = myRequest.GetResponse();
            int id;
            using (StreamReader reader = new StreamReader(wr.GetResponseStream()))
            {
                id = int.Parse(reader.ReadToEnd());
            }

            WallModel wallModel = new WallModel()
            {
                Age = int.Parse(AgeTextBox.Text),
                City = CityTextBox.Text,
                Country = CountryTextBox.Text,
                IdUser = id
            };
            HttpWebRequest myRequestNewWall = WebRequest.CreateHttp("https://localhost:44359/api/wall/newwall");
            myRequestNewWall.Method = "POST";
            myRequestNewWall.ContentType = "application/json";
            using (StreamWriter writer = new StreamWriter(myRequestNewWall.GetRequestStream()))
            {
                writer.Write(JsonConvert.SerializeObject(wallModel));
            }
            WebResponse wrNewWall = myRequestNewWall.GetResponse();

            Wall WindowProg = new Wall(id);
            WindowProg.Show();
            this.Close();

        }
    }
}
