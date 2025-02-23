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

            b.Background = tB.Foreground;
            tB.Foreground = Brushes.GhostWhite;
        }

        DropShadowEffect DSE = new DropShadowEffect() { Color = Colors.LightGray, BlurRadius = 10, ShadowDepth = 0 };
        private void TypeChoice_MouseEnter(object sender, MouseEventArgs e)
        {
            Border b = sender as Border;
            DSE.BlurRadius = 20;
            b.Effect = DSE.Clone();
        }

        private void TypeChoice_MouseLeave(object sender, MouseEventArgs e)
        {
            Border b = sender as Border;
            DSE.BlurRadius = 10;
            b.Effect = DSE.Clone();
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