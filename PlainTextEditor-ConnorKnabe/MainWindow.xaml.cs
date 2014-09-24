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

using System.Windows.Forms;
using System.IO;


namespace PlainTextEditor_ConnorKnabe {   
    public partial class MainWindow : Window {
        TextDocument textDocument = new TextDocument();


        public MainWindow() {
            InitializeComponent();
        }


        private void btnSave_Click(object sender, RoutedEventArgs e) {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                if (!textDocument.saveFile(saveFileDialog.FileName, txtInput.Text)) {
                    // http://msdn.microsoft.com/en-us/library/Aa984357
                    System.Windows.Forms.MessageBox.Show("An error occurred saving the report.", "Grader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void menuOpen_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            // http://msdn.microsoft.com/en-us/library/db5x7c0d(v=VS.85).aspx
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                textDocument.filePath = openFileDialog.FileName;
                if (textDocument.openFile()) {
                    txtInput.Text = textDocument.textBoxString;

                }
            }
        }

        private void txtInput_TextChanged_1(object sender, RoutedEventArgs e) {
            

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {

        }

      
    }
}
