using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace PlainTextEditor_ConnorKnabe {
    class TextDocument {

        public String filePath;
        //public List<String> textBoxString = new List<String>();
        public String textBoxString;
        public Boolean saveFile(String fileName, String text) {
            try {
                using (StreamWriter sw = new StreamWriter(fileName)) {
                    sw.WriteLine(text);
                }
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public Boolean openFile() {

            try {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(filePath)) {
                    // http://msdn.microsoft.com/en-us/library/ms228388(v=vs.80).aspx
                    // http://msdn.microsoft.com/en-us/library/ms131448.aspx
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    textBoxString = sr.ReadToEnd();

                }

                return true;

            } catch (Exception ex) {
                return false;
            }
        }













    }
}
