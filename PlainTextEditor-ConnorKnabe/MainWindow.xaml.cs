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
using System.ComponentModel;


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
        //When you click the save button it calls the savehandler
        private void MenuSave_Click(object sender, RoutedEventArgs e) {
            SaveHandler(false);
        }
        //When you click the save as button it calls the savehandler
        private void MenuSaveAs_Click(object sender, RoutedEventArgs e) {
            SaveHandler(true);
        }
        //Checks to see if you need to save before opening the open window
        private void MenuOpen_Click(object sender, RoutedEventArgs e) {
            if (needsToSave) {
                SaveDialog();
                if (exitSaveDialog) {
                    OpenHandler();
                }
            } else {
                OpenHandler();
            }
        }
        //Checks to see if the textbox text has been changed and if so enable the save button and now it needs to save (for opening exiting etc)
        private void TxtInput_TextChanged(object sender, RoutedEventArgs e) {
            menuSave.IsEnabled = true;
            needsToSave = true;
        }
        //When you attempt to open a new document it will verify you don't have changes you need to save
        private void MenuNew_Click(object sender, RoutedEventArgs e) {
            if (needsToSave) {
                SaveDialog();
                if (exitSaveDialog) {
                    NewHandler();
                }
            }
            if (textDocument.HasBeenSaved()) {
                NewHandler();
            }
        }
        //This is not in TextDocument class because it disables the menuSave button
        private void NewHandler() {
            txtInput.Text = "";
            textDocument.textDocFileName = null;
            menuSave.IsEnabled = false;
            needsToSave = false;
        }
        private void MenuExit_Click(object sender, RoutedEventArgs e) {
            if (needsToSave) {
                SaveDialog();
            } else {
                Environment.Exit(0);
            }
            if(exitSaveDialog) {
                Environment.Exit(0);
            } 
        }
        private void MenuAbout_Click(object sender, RoutedEventArgs e) {
            System.Windows.MessageBox.Show("Developed by Connor Knabe who is studying to become a software developer at the University of Missouri Columbia.","About Me");
        }
        //Shows the dialog window to allow the user to open a file
        private void OpenHandler() {
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
                    needsToSave = false;
                }
            }
        }
        //The SaveHandler is not in the TextDocument class because it opens a window the user can see to save documents
        private void SaveHandler(Boolean isSaveAs) {
            if (textDocument.HasBeenSaved() && !isSaveAs) {
                if (!textDocument.SaveFile(textDocument.textDocFileName, txtInput.Text)) {
                    // http://msdn.microsoft.com/en-us/library/Aa984357
                    System.Windows.Forms.MessageBox.Show("An error occurred saving the file.", "TextDocument", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                needsToSave = false;
                menuSave.IsEnabled = false;
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
                    menuSave.IsEnabled = false;
                }
            }

        }
        //Displays to the user alerting them that they need to save or will lose data
        private void SaveDialog() {
                MessageBoxResult result = System.Windows.MessageBox.Show("You have unsaved data.  Would you like to save?",
  "Confirmation", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes) {
                    if (textDocument.HasBeenSaved()) {
                        SaveHandler(false);
                        exitSaveDialog = true;

                    } else {
                        SaveHandler(true);
                        exitSaveDialog = true;

                    }
                } else if (result == MessageBoxResult.No) {
                    exitSaveDialog = true;
                } else {
                    cancelSaveDialog = true;
                } 
        }
        //If the user clicks the x button it will check to see if they need to save
        protected override void OnClosing(CancelEventArgs e) {
            if (needsToSave) {
                SaveDialog();
            } else {
                Environment.Exit(0);
            }
            if (exitSaveDialog) {
                Environment.Exit(0);
            } 
            e.Cancel = true;
        }

    }
}
