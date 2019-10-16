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
        private PrintingLine printingLines = new PrintingLine();
        private const int PAPER_WIDTH = 27;

        public CustomPrinter()
        {
            printDoc.PrintController = printController;
            printDoc.PrinterSettings = ps;
        }

        public void AddLine(String Line, Font font)
        {
            printingLines.AddLine(Line, font);
        }

        public void AddBlankLine()
        {
            printingLines.AddLine(" ", new Font("Courier New", 5, FontStyle.Bold));
        }

        public void AddCenteredLine(string line, Font font)
        {
            int num = PAPER_WIDTH - line.Length;
            if ((num % 2) != 0) num--;
            num /= 2;
            String spaces = new String(' ', num);
            String centeredLine = spaces + line + spaces;
            printingLines.AddLine(centeredLine, font);
        }

        public void AddSeparateLine(String str1, String str2, Font font)
        {
            try
            {
                int num = ((PAPER_WIDTH - str1.Length) - str2.Length);
                String spaces = new String(' ', num);
                String separatedLine = str1 + spaces + str2;
                printingLines.AddLine(separatedLine, font);
            }
            catch (Exception)
            {
                printingLines.AddLine(str1 + " " + str2, font);
            }

        }

        public void AddLineEnd(String str, Font font)
        {
            int num = PAPER_WIDTH - str.Length;
            String spaces = new String(' ', num);
            String endLine = spaces + str;
            printingLines.AddLine(endLine, font);
        }

        public void Print()
        {

            printDoc.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
            {
                float yPos = 0;
                for (int i = 0; i < printingLines.Length(); i++)
                {
                    yPos = 20 + (i * printingLines.FontAt(i).GetHeight(e1.Graphics));
                    e1.Graphics.DrawString(printingLines.LineAt(i), printingLines.FontAt(i), new SolidBrush(Color.Black), new RectangleF(0, yPos, printDoc.DefaultPageSettings.PrintableArea.Width, printDoc.DefaultPageSettings.PrintableArea.Width));
                }

            };

            //printDoc.DefaultPageSettings.PaperSize = new PaperSize("Custom", 315, printingLines.Length() * 10);
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

    public class PrintingLine
    {
        private List<String> printing_lines = new List<string>();
        private List<Font> line_font = new List<Font>();

        public String LineAt(int index)
        {
            return printing_lines[index];
        }

        public Font FontAt(int index)
        {
            return line_font[index];
        }

        public void AddLine(String str, Font font) 
        {
            printing_lines.Add(str);
            line_font.Add(font);
        }

        public int Length()
        {
            return printing_lines.Count();
        }
    }
}
