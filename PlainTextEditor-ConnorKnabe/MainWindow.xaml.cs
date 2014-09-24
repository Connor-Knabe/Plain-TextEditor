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
        SaveFileDialog saveFileDialog = new SaveFileDialog();


        public MainWindow() {
            InitializeComponent();
        }

        private void MenuSave_Click(object sender, RoutedEventArgs e) {

            if (textDocument.hasBeenSaved()) {
                if (!textDocument.SaveFile(textDocument.textDocFileName, txtInput.Text)) {
                    // http://msdn.microsoft.com/en-us/library/Aa984357
                    System.Windows.Forms.MessageBox.Show("An error occurred saving the file.", "TextDocument", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            } else {
                SaveAsHandler();
            }

        }


        private void MenuSaveAs_Click(object sender, RoutedEventArgs e) {
            SaveAsHandler();
        }

        private void SaveAsHandler() {
            saveFileDialog.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;


            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                if (!textDocument.SaveFile(saveFileDialog.FileName, txtInput.Text)) {
                    // http://msdn.microsoft.com/en-us/library/Aa984357
                    System.Windows.Forms.MessageBox.Show("An error occurred saving the file.", "TextDocument", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void MenuOpen_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            // http://msdn.microsoft.com/en-us/library/db5x7c0d(v=VS.85).aspx
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                textDocument.filePath = openFileDialog.FileName;
                if (textDocument.OpenFile()) {
                    txtInput.Text = textDocument.textBoxString;

                }
            }
        }

        private void TxtInput_TextChanged(object sender, RoutedEventArgs e) {
            menuSave.IsEnabled = true;
        }

        private void MenuNew_Click(object sender, RoutedEventArgs e) {
            txtInput.Text = "";
            textDocument.textDocFileName = null;
            menuSave.IsEnabled = false;


        }


      
    }
}
