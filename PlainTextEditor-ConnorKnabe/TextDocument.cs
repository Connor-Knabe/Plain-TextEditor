using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace PlainTextEditor_ConnorKnabe {
    class TextDocument {

        public String filePath { get; set; }
        public String textBoxString { get; set; }
        public String textDocFileName { get; set; }


        public Boolean hasBeenSaved() {

            if (textDocFileName == null) {
                return false;
            } else {
                return true;
            }
        }

        public Boolean SaveFile(String fileName, String text) {
            try {
                using (StreamWriter sw = new StreamWriter(fileName)) {
                    sw.WriteLine(text);
                }

                textDocFileName = fileName;
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public Boolean OpenFile() {

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
