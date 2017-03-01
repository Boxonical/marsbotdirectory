using marsbotclient.UWP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace marsbotclient
{
	public partial class MainPage : ContentPage
	{
        BotConnection connection = new BotConnection("James");
        ObservableCollection<MessageListItem> messageList = new ObservableCollection<MessageListItem>();

		public MainPage()
		{
            InitializeComponent();
            MessageListView.ItemsSource = messageList;
            var messageTask = connection.GetMessagesAsync(messageList);
		}

        public void Send(object sender,EventArgs args)
        {
            var message = ((Entry)sender).Text = "";

            if (message.Length > 0)
            {
                var messageListItem = new MessageListItem(message, connection.Account.Name);
                messageList.Add(messageListItem);

                connection.SendMessage(message);
            }
        }
	}
}
