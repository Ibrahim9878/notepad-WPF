using System.IO;
using System.IO.Enumeration;
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

namespace WpfApp3
{
    public partial class MainWindow : Window
    {
        string data = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            for (double i = 8; i < 73; i++)
            {
                SizeBox.Items.Add(i);
            }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                FilePath.Text = dialog.FileName;
            }
            FlowDocument myFlowDoc = new FlowDocument();
            text.Document = myFlowDoc;


            TextRange textRange = new TextRange(
                text.Document.ContentStart,
                text.Document.ContentEnd
            );
            textRange.Text = File.ReadAllText(dialog.FileName);
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            FlowDocument myFlowDoc = new FlowDocument();
            text.Document = myFlowDoc;
            TextRange textRange = new TextRange(
                text.Document.ContentStart,
                text.Document.ContentEnd
            );
            if (FilePath.Text == string.Empty)
            {
                MessageBox.Show("Write File name to textbox");
                return;
            }
            File.WriteAllText(FilePath.Text, textRange.Text);
            textRange.Text = string.Empty;
            FilePath.Text = string.Empty;

        }

        private void text_TextChanged(object sender, TextChangedEventArgs e)
        {

            string richText = new TextRange(text.Document.ContentStart, text.Document.ContentEnd).Text;
            if (checkBox.IsChecked == true)
            {
                if (FilePath.Text == string.Empty)
                {
                    MessageBox.Show("Write File name to textbox");
                    return;
                }
                File.WriteAllText(FilePath.Text, richText);
                
            }

        }

        private void cutButton_Click(object sender, RoutedEventArgs e)
        {

            text.Cut();
        }

        private void copyButton_Click(object sender, RoutedEventArgs e)
        {
            text.Copy();
        }

        private void PasteButton_Click(object sender, RoutedEventArgs e)
        {
            text.Paste();
        }

        private void selectAllButton_Click(object sender, RoutedEventArgs e)
        {
            text.SelectAll();
            MessageBox.Show("Everything Selected");
       
        }

        private void SizeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            text.FontSize = Convert.ToDouble(SizeBox.SelectedItem);
        }
    }
}