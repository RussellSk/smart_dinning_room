using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;

namespace WpfApp1
{
    public class CustomPrinter
    {
        private readonly PrinterSettings ps = new PrinterSettings();
        private PrintDocument printDoc = new PrintDocument();
        private PrintController printController = new StandardPrintController();
        private List<String> PrintingLines = new List<string>();
        private const int PAPER_WIDTH = 23;

        public CustomPrinter()
        {
            printDoc.PrintController = printController;
            printDoc.PrinterSettings = ps;
        }

        public void AddLine(String Line)
        {
            PrintingLines.Add(Line);
            PrintingLines.Add(" ");
        }

        public void AddBlankLine()
        {
            PrintingLines.Add(" ");
        }

        public void AddCenteredLine(string line)
        {
            int num = PAPER_WIDTH - line.Length;
            if ((num % 2) != 0) num--;
            num /= 2;
            String spaces = new string(' ', num);
            String centeredLine = spaces + line + spaces;
            PrintingLines.Add(centeredLine);
            PrintingLines.Add(" ");
        }

        public void AddSeparateLine(String str1, String str2)
        {
            try
            {
                int num = ((PAPER_WIDTH - str1.Length) - str2.Length) - 2;
                String spaces = new string(' ', num);
                String separatedLine = str1 + spaces + str2;
                PrintingLines.Add(separatedLine);
            }
            catch (Exception)
            {
                PrintingLines.Add(str1 + " " + str2);
            }

            PrintingLines.Add(" ");
        }

        public void Print()
        {

            printDoc.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
            {
                float yPos = 0;
                int count = 0;
                Font printFont = new Font("Courier New", 12, FontStyle.Bold);

                foreach (String str in PrintingLines)
                {
                    yPos = 20 + (count * printFont.GetHeight(e1.Graphics));
                    e1.Graphics.DrawString(str, printFont, new SolidBrush(Color.Black), new RectangleF(0, yPos, printDoc.DefaultPageSettings.PrintableArea.Width, printDoc.DefaultPageSettings.PrintableArea.Width));
                    count++;
                }

            };

            printDoc.DefaultPageSettings.PaperSize = new PaperSize("Custom", 315, 300);
            try
            {
                printDoc.Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine("CustomPrinter Error: " + ex.Message);
            }
        }
    }
}
