using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Cells;

namespace XlxsToCsv
{
    internal static class Converter
    {
        public static string ConvertToCsv(string path)
        {
            Workbook wb = new Workbook(path);

            using Stream stream = new MemoryStream();
            wb.Save(stream, SaveFormat.Csv);
            stream.Position = 0;
            StreamReader streamReader = new StreamReader(stream);
            string output = streamReader.ReadToEnd();
            int evalIndex = output.LastIndexOf('\n');
            //Remove evla message
            return output.Remove(evalIndex);


        }

        
    }



    
}
