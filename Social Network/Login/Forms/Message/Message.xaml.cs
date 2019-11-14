using Login.Models;
using Microsoft.AspNetCore.SignalR.Client;
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

        private HubConnection _hubConnection;

        private async void Connect(int id)
        {
            _hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:44359/chat").Build();
            try
            {
                await _hubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            _hubConnection.On<string>("sendAll", new Action<string>((message) =>
            {
            HttpWebRequest myRequestUser = WebRequest.CreateHttp($"https://localhost:44359/api/user/get/{id}");
            myRequestUser.Method = "GET";
            myRequestUser.ContentType = "application/json";
            WebResponse wrNameUser = myRequestUser.GetResponse();
            UserModel userModel = new UserModel();
                using (StreamReader reader = new StreamReader(wrNameUser.GetResponseStream()))
                {
                    string responseFromServer = reader.ReadToEnd();

                }
            ChatTextBox.Text += "\n" + userModel.Name + " : " + message;
            }));

            await _hubConnection.InvokeAsync("Connect", id);
        }
        int _idChat, _idUser;
        public Message(int idC, int idU)
        {
            InitializeComponent();
            _idChat = idC;
            _idUser = idU;
            Connect(_idUser);

            HttpWebRequest myRequest = WebRequest.CreateHttp($"https://localhost:44359/api/message/getMessage/{idC}");
            myRequest.Method = "GET";
            myRequest.ContentType = "application/json";
            WebResponse wr = myRequest.GetResponse();

            List<MessageModel> listChatModel = new List<MessageModel>();
            using (StreamReader reader = new StreamReader(wr.GetResponseStream()))
            {
                string responseFromServer = reader.ReadToEnd();
                listChatModel = JsonConvert.DeserializeObject<List<MessageModel>>(responseFromServer);
            }

            
            foreach (var item in listChatModel)
            {
                int Id_ = item.IdUser;
                HttpWebRequest myRequestUser = WebRequest.CreateHttp($"https://localhost:44359/api/user/get/{Id_}");
                myRequestUser.Method = "GET";
                myRequestUser.ContentType = "application/json";
                WebResponse wrNameUser = myRequestUser.GetResponse();
                UserModel userModel = new UserModel();
                using (StreamReader reader = new StreamReader(wrNameUser.GetResponseStream()))
                {
                    string responseFromServer = reader.ReadToEnd();
                    userModel = JsonConvert.DeserializeObject<UserModel>(responseFromServer);
                }


                ChatTextBox.Text += "\n" + userModel.Name + " : " + item.Context;


            }
        }

        private async void ButtonMessage_Click(object sender, RoutedEventArgs e)
        {

            MessageModel messageModel = new MessageModel() { IdChat = _idChat, Context = MesstextBox.Text, IdUser = _idUser };
            HttpWebRequest myRequest = WebRequest.CreateHttp($"https://localhost:44359/api/message/PostMessage");
            myRequest.Method = "POST";
            myRequest.ContentType = "application/json";
            using (StreamWriter writer = new StreamWriter(myRequest.GetRequestStream()))
            {
                writer.Write(JsonConvert.SerializeObject(messageModel));
            }
            WebResponse wrNewWall = myRequest.GetResponse();
            await _hubConnection.InvokeAsync("sendAll", MesstextBox.Text);

        }


    }
}
