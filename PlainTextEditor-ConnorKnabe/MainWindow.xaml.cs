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
        private Boolean needsToSave { get; set; }
        private Boolean cancelSaveDialog { get; set; }
        private Boolean exitSaveDialog { get; set; }


        public MainWindow() {
            InitializeComponent();
        }

        private void MenuSave_Click(object sender, RoutedEventArgs e) {
            SaveAsHandler();
        }


        private void MenuSaveAs_Click(object sender, RoutedEventArgs e) {
            SaveAsHandler();
        }

        private void SaveAsHandler() {
            if (textDocument.hasBeenSaved()) {
                if (!textDocument.SaveFile(textDocument.textDocFileName, txtInput.Text)) {
                    // http://msdn.microsoft.com/en-us/library/Aa984357
                    System.Windows.Forms.MessageBox.Show("An error occurred saving the file.", "TextDocument", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                needsToSave = false;
            } else {
                saveFileDialog.Filter = "txt files (*.txt)|*.txt";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = textDocument.textDocFileName;

                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    if (!textDocument.SaveFile(saveFileDialog.FileName, txtInput.Text)) {
                        // http://msdn.microsoft.com/en-us/library/Aa984357
                        System.Windows.Forms.MessageBox.Show("An error occurred saving the file.", "TextDocument", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    needsToSave = false;
                }
            }

        }

        private void MenuOpen_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            openFileDialog.FileName = textDocument.textDocFileName;

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
            needsToSave = true;
        }

        private void MenuNew_Click(object sender, RoutedEventArgs e) {
            txtInput.Text = "";
            textDocument.textDocFileName = null;
            menuSave.IsEnabled = false;


        }

        private void MenuExit_Click(object sender, RoutedEventArgs e) {
            if (needsToSave) {
                saveDialog();
            } else {
                Environment.Exit(0);
            }
      
            if(exitSaveDialog) {
                Environment.Exit(0);
            } 
        }
        private void MenuAbout_Click(object sender, RoutedEventArgs e) {
            System.Windows.MessageBox.Show("Developed by Connor Knabe.  Studying to become a software developer at the University of Missouri Columbia.");

        }
        private void saveDialog() {
                MessageBoxResult result = System.Windows.MessageBox.Show("You have unsaved data.  Would you like to save?",
  "Confirmation", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes) {
                    SaveAsHandler();
                    exitSaveDialog = true;
                } else if (result == MessageBoxResult.No) {
                    exitSaveDialog = true;
                } else {
                    cancelSaveDialog = true;
                } 
        }

      
    }
}
