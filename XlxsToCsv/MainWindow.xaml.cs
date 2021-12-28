using System;
using System.Collections.Generic;
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

namespace XlxsToCsv
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

        private void HandleFileDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // Assuming you have one file that you care about, pass it off to whatever
                // handling code you have defined.

                if (files[0].EndsWith(".xlsx", StringComparison.CurrentCultureIgnoreCase))
                {
                    outputTextBox.Text = Converter.ConvertToCsv(files[0]);
                    if ((bool)clipboardCheckBox.IsChecked)
                    {
                        Clipboard.SetText(outputTextBox.Text);
                    }
                    return;
                }

            }

            outputTextBox.Text = "Okänd fil typ. Vänligen ge en .xlsx fil";

        }


        private void TextBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Handled = true;
            }
        }



    }
}
