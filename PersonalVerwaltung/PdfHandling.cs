using Aspose.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalVerwaltung
{
    public static class PdfHandling
    {
        public static Document createPayrollPdf(Employee employee, string monat, int jahr)
        {


            Document lohnabrechnung = new Document();
            Aspose.Pdf.Page seite1 = lohnabrechnung.Pages.Add();

            seite1.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(employee.Firstname + " " + employee.Lastname));
            seite1.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(employee.Street));
            seite1.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(employee.Zipcode + " " + new Ort(employee.Country, employee.Zipcode).Ortsname));
            seite1.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(employee.Email));

            seite1.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("\n \n \nIhre Lohnabrechnung für den Zeitraum " + monat + " " + jahr + ":" + "\n\n\n\n"));

            //Tabelle
            Aspose.Pdf.Table table = new Aspose.Pdf.Table();
            table.Border = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.LightGray));
            table.DefaultCellBorder = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.LightGray));

            //Zeile für Monatliche Arbeitszeit
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Monatliche Arbeitszeit:");
            row1.Cells.Add("0,00");
            //Zeile für Stundenlohn
            Row row2 = table.Rows.Add();
            row2.Cells.Add("Stundenlohn:");
            row2.Cells.Add("0,00");
            //Zeile für Bruttolohn
            Row row3 = table.Rows.Add();
            row3.Cells.Add("Bruttolohn:");
            row3.Cells.Add("0,00");
            //Zeile für Steuerabzüge
            Row row4 = table.Rows.Add();
            row4.Cells.Add("Steuerabzüge:");
            row4.Cells.Add("0,00");
            //Zeile für Krankenversicherungsabzüge
            Row row5 = table.Rows.Add();
            row5.Cells.Add("Krankenversicherung:");
            row5.Cells.Add("0,00");
            //Zeile für Nettolohn
            Row row6 = table.Rows.Add();
            row6.Cells.Add("Nettolohn:");
            row6.Cells.Add("0,00");


            //Tabbele einfügen
            lohnabrechnung.Pages[1].Paragraphs.Add(table);


            return lohnabrechnung;
        }
    }
}
