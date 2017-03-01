using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Bot.Connector.DirectLine;
using System.Collections.ObjectModel;

namespace marsbotclient.UWP
{
    class BotConnection
    {
        public DirectLineClient Client = new DirectLineClient("gQOq2rjyHhA.cwA.uvY.ImygdI-9DYorPqhZ1YFq5PfHkdvhCpxhVuMH2FR2diI");
        public Conversation MainConversation;
        public ChannelAccount Account;

        public BotConnection(string AccountName)
        {
            MainConversation = Client.Conversations.StartConversation();
            Account = new ChannelAccount { Id = AccountName, Name = AccountName };
        }

        public void SendMessage(string message)
        {
            Activity activity = new Activity
            {
                From = Account,
                Text = message,
                Type = ActivityTypes.Message
            };

            Client.Conversations.PostActivity(MainConversation.ConversationId, activity);
        }

        public async Task GetMessagesAsync(ObservableCollection<MessageListItem> collection)
        {
            string watermark = null;

            while (true)
            {
                var activitySet = await Client.Conversations.GetActivitiesAsync(MainConversation.ConversationId, watermark);

                watermark = activitySet?.Watermark;

                foreach (var activity in activitySet.Activities)
                {
                    if (activity.From.Name == "marsbotboxonical")
                        collection.Add(new MessageListItem(activity.Text, activity.From.Name));
                }

                await Task.Delay(3000);
            }
        }
    }
}
