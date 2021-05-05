using ChatApp.DTO;
using ChatApp.DTO.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatAppMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        private readonly string userName;
        private readonly HubConnection hubConnection;
        private ObservableCollection<UserChatMessage> Messages { get; } = new ObservableCollection<UserChatMessage>();

        public ChatPage(string userName)
        {
            InitializeComponent();
            this.userName = userName;
            btnSend.Clicked += BtnSend_Clicked;
            lvMessages.ItemsSource = Messages;

            hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/Chathub")
                .Build();

            hubConnection.On<UserChatMessage>(Consts.RECEIVE_MESSAGE, ReceiveMessage_Event);
            hubConnection.StartAsync();

        }

        private void ReceiveMessage_Event(UserChatMessage obj)
        {
            Messages.Insert(0, obj);
        }

        private async void BtnSend_Clicked(object sender, EventArgs e)
        {
            var message = txtMessage.Text;
            if (string.IsNullOrWhiteSpace(message))
            {
                await DisplayAlert("Validation errors", "The message is required", "OK");
                return;
            }
            await hubConnection.SendAsync(Consts.SEND_MESSAGE, new UserChatMessage
            {
                Message = message,
                Username = userName

            });
        }
    }
}