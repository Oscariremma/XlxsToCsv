using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace XlsxToCsv
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            inFile_tbx.Text = Settings.StaticSettings.lastPath;
            clipboardCheckBox.IsChecked = Settings.StaticSettings.copyResult;

            Closing += OnWindowClosing;
        }

        private void HandleFileDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // Assuming you have one file that you care about, pass it off to whatever
                // handling code you have defined.

                if (ConvertFile(files[0])) return;
            }



        }

        /// <summary>
        /// Converts a XLSX file to CSV
        /// </summary>
        /// <param name="file">Path to file</param>
        /// <returns>Returns true if success otherwise false</returns>
        private bool ConvertFile(string file)
        {
            if (!file.EndsWith(".xlsx", StringComparison.CurrentCultureIgnoreCase))
            {
                outputTextBox.Text = "Okänd fil typ. Vänligen ge en .xlsx fil";
                return false;
            }
                
            
            if (!File.Exists(file))
            {
                outputTextBox.Text = $"Kan inte hitta filen: {file}";
                return false;
            };
            
            outputTextBox.Text = Converter.ConvertToCsv(file);
            if (clipboardCheckBox.IsChecked == true)
            {
                Clipboard.SetText(outputTextBox.Text);
            }

            return true;

        }


        private void TextBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Handled = true;
            }
        }


        private void BrowseFile_Btn(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel|*.xlsx";
            ofd.ShowDialog();
            inFile_tbx.Text = ofd.FileName;

        }

        private void Run_Btn(object sender, RoutedEventArgs e)
        {
            string file = inFile_tbx.Text;

            ConvertFile(file);

        }

        private void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            Settings.StaticSettings.lastPath = inFile_tbx.Text;
            Settings.StaticSettings.copyResult = clipboardCheckBox.IsChecked ?? false;
            Settings.SaveSettings();
        }
    }
}
