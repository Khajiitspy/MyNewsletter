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
    }
}