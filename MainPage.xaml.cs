using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BottomsUpList
{
    // Bindable class representing a single text message.
    public class TextMessage
    {
        public string Body { get; set; }
        public string DisplayTime { get; set; }
        public bool IsSent { get; set; }
        public bool IsReceived { get { return !IsSent; } }
        public IList<object> Placeholders { get; set; }
    }

    // Observable collection representing a text message conversation
    // that can load more items incrementally.
    public class Conversation : ObservableCollection<TextMessage>, ISupportIncrementalLoading
    {
        private uint messageCount = 0;

        public Conversation()
        {
        }

        public bool HasMoreItems { get; } = true;

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            this.CreateMessages(count);

            return Task.FromResult<LoadMoreItemsResult>(
                new LoadMoreItemsResult()
                {
                    Count = count
                }).AsAsyncOperation();
        }

        private void CreateMessages(uint count)
        {
            for (uint i = 0; i < count; i++)
            {
                this.Insert(0, new TextMessage()
                {
                    Body = $"{messageCount}: {CreateRandomMessage()}",
                    IsSent = 0 == messageCount++ % 2,
                    DisplayTime = DateTime.Now.ToString(),
                    Placeholders = CreateRandomPlaceholders()
                });
            }
        }

        private static Random rand = new Random();
        private static string fillerText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";

        public static string CreateRandomMessage()
        {
            return fillerText.Substring(0, rand.Next(5, fillerText.Length));
        }

        public static IList<object> CreateRandomPlaceholders()
        {
            int count = rand.Next(0, 5);
            var list = new List<object>();
            for (int i = 0; i < count; i++)
                list.Add(null);

            return list;
        }
    }

    public sealed partial class MainPage : Page
    {
        private readonly Conversation conversation = new Conversation();

        public MainPage()
        {
            this.InitializeComponent();

            //this.conversation.LoadMoreItemsAsync((int)10).GetResults();
            chatView.ItemsSource = this.conversation;
        }

        public static Windows.UI.Xaml.HorizontalAlignment GetItemAlignment(bool isSent)
        {
            return isSent ? Windows.UI.Xaml.HorizontalAlignment.Right : Windows.UI.Xaml.HorizontalAlignment.Left;
        }

        async void SendTextMessage()
        {
            if (MessageTextBox.Text.Length > 0)
            {
                this.conversation.Add(new TextMessage
                {
                    Placeholders = Conversation.CreateRandomPlaceholders(),
                    Body = MessageTextBox.Text,
                    DisplayTime = DateTime.Now.ToString(),
                    IsSent = true
                });
                MessageTextBox.Text = string.Empty;

                // Send a simulated reply after a brief delay.
                await Task.Delay(TimeSpan.FromSeconds(2));

                this.conversation.Add(new TextMessage
                {
                    Placeholders = Conversation.CreateRandomPlaceholders(),
                    Body = Conversation.CreateRandomMessage(),
                    DisplayTime = DateTime.Now.ToString(),
                    IsSent = false
                });
            }
        }

        private void MessageTextBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                this.SendTextMessage();
            }
        }
    }
}
