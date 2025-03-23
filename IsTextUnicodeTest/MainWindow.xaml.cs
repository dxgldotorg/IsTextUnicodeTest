using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace IsTextUnicodeTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("Advapi32", SetLastError = false)]
        static extern bool IsTextUnicode(byte[] buf, int len, ref int opt);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            int flags = ~0;
            byte[] ansitext = Encoding.Default.GetBytes(TextInput.Text);
            bool isunicode = IsTextUnicode(ansitext, ansitext.Length, ref flags);
            if (isunicode)
            {
                OutputLabel.Content = "Text (detected as Unicode)";
                TextOutput.Text = System.Text.Encoding.Unicode.GetString(ansitext);
            }
            else
            {
                OutputLabel.Content = "Text (detected as ANSI)";
                TextOutput.Text = System.Text.Encoding.Default.GetString(ansitext);
            }
        }
    }
}