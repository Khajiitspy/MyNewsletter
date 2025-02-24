using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MimeKit;
using MimeKit.Utils;
using MyNewsletter.Tools;

namespace MyNewsletter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public Brush SelectedTheme = Brushes.GhostWhite;

        private void PageTypeChoice(object sender, MouseButtonEventArgs e)
        {
            Border b= sender as Border;
            TextBlock tB = b.Child as TextBlock;

            StackPanel SP = b.Parent as StackPanel;
            foreach(Border c in SP.Children)
            {
                if (c.Background != Brushes.Transparent)
                {
                    TextBlock cB = c.Child as TextBlock;
                    cB.Foreground = c.Background;
                    c.Background = Brushes.Transparent;
                }
            }

            SelectedTheme = tB.Foreground;
            b.Background = SelectedTheme;
            tB.Foreground = Brushes.GhostWhite;

            SendButton.Background = SelectedTheme;
            TextBlock SB = SendButton.Child as TextBlock;
            SB.Foreground = Brushes.GhostWhite;

            TypeChoiceError.Foreground = SelectedTheme;
            EmailError.Foreground = SelectedTheme;
        }

        bool DefaultTextBox = true;

        private void SendTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!DefaultTextBox)
            {
                if (SendToBox.Text == "")
                {
                    DefaultTextBox = true;
                    SendToBox.Text = "Recipient@Example.com";
                    SendToBox.Foreground = Brushes.LightGray;
                }
            }
        }

        private void SendToBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (DefaultTextBox)
            {
                SendToBox.Text = "";
                SendToBox.Foreground = Brushes.Black;
                DefaultTextBox = false;
            }
        }

        private void SendButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (EmailSender.IsValidEmail(SendToBox.Text))
            {
                if (SelectedTheme != Brushes.GhostWhite)
                {
                    bool EmailSent = false;
                    if (SelectedTheme == Brushes.HotPink)
                    {
                        BodyBuilder body = new BodyBuilder();
                        string mainDirectory = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
                        var imagePath = mainDirectory + "/Images/BirthdayBack.jpg";
                        var image = body.LinkedResources.Add(imagePath);
                        image.ContentId = MimeUtils.GenerateMessageId(); // Generate a unique Content-ID


                        body.HtmlBody = 
                            $"<div style=\" width: 600px; height: 350px; background-color: lightblue; background-image: url(cid:{image.ContentId}); background-repeat: no-repeat; background-size: cover;\">" +
                                $"\r<h1 style=\"display: flex; justify-content: center; align-items: center; padding: 20px; text-shadow: -1px -1px 0px White, 1px -1px 0px White, -1px 1px 0px White, 1px 1px 0px White; color: #ffd800\">Happy Birthday {SendToBox.Text}!</h1>" +
                            $"\r</div>";


                        EmailSender.SendEmail("User", SendToBox.Text, "Happy Birthday!", body);
                        EmailSent = true;
                    }
                    else if (SelectedTheme == Brushes.ForestGreen)
                    {
                        BodyBuilder body = new BodyBuilder();

                        body.HtmlBody =
                            $"<div style=\" width: 600px; height: 350px; background-color: ForestGreen; background-repeat: no-repeat; background-size: cover;\">" +
                                $"\r<h1 style=\"display: flex; justify-content: center; align-items: center; padding: 20px; text-shadow: -1px -1px 0px White, 1px -1px 0px White, -1px 1px 0px White, 1px 1px 0px White; color: #ffd800\">{SendToBox.Text}, Don't Miss the new Sale!</h1>" +
                            $"\r</div>";

                        EmailSender.SendEmail("User", SendToBox.Text, "Sale", body);
                        EmailSent = true;
                    }
                    else if (SelectedTheme == Brushes.DarkGoldenrod)
                    {
                        BodyBuilder body = new BodyBuilder();
                        string mainDirectory = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
                        var imagePath = mainDirectory + "/Images/Wildfire.jpg";
                        var image = body.LinkedResources.Add(imagePath);
                        image.ContentId = MimeUtils.GenerateMessageId(); // Generate a unique Content-ID


                        body.HtmlBody =
                            $"<div style=\" width: 600px; height: 350px; background-color: ForestGreen; background-image: url(cid:{image.ContentId}); background-repeat: no-repeat; background-size: cover;\">" +
                                $"\r<h1 style=\"display: flex; justify-content: center; align-items: center; padding: 20px; text-shadow: -1px -1px 0px White, 1px -1px 0px White, -1px 1px 0px White, 1px 1px 0px White; color: #ffd800\">{SendToBox.Text}, Wildfires broke out near you!</h1>" +
                            $"\r</div>";
                        EmailSender.SendEmail("User", SendToBox.Text, "NewsLetter", body);
                        EmailSent = true;
                    }
                    if (EmailSent == true)
                    {
                        MessageBox.Show("Email Has Been Sent Successfully!", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong on our end... We are sorry.", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    TypeChoiceError.Text = "Please pick what you would like to send!";
                }
            }
            else
            {
                EmailError.Text = "Invalid Email!";
            }
        }
    }
}