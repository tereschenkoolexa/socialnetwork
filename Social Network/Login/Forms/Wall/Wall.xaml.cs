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
        public Wall(int id)
        {
            InitializeComponent();
           
            HttpWebRequest myRequest = WebRequest.CreateHttp("https://localhost:44359/api/user");
            myRequest.Method = "GET";
            myRequest.ContentType = "application/json";
            try
            {
                WebResponse wr = myRequest.GetResponse();
                

            }
            catch (Exception)
            {
                MessageBox.Show("Не правильнi данi");
            }


        }
    }
}
