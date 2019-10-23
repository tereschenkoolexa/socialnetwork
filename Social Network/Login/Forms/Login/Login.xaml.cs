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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            UserModel userModel = new UserModel() { Gmail = LoginTextBox.Text, Password = PasswordTextBox.Text};
            HttpWebRequest myRequest = WebRequest.CreateHttp("https://localhost:44359/api/user/login");
            myRequest.Method = "POST";
            myRequest.ContentType = "application/json";
            using(StreamWriter writer = new StreamWriter(myRequest.GetRequestStream()))
            {
                writer.Write(JsonConvert.SerializeObject(userModel));
            }
            try
            {
                WebResponse wr = myRequest.GetResponse();
                int id;
                using (StreamReader reader = new StreamReader(wr.GetResponseStream()))
                {
                    id = int.Parse(reader.ReadToEnd());
                }

                Wall WindowProg = new Wall(id);
                WindowProg.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не правильнi данi");
            }





        }
    }
}
