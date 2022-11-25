using API.Repository.Interface;
using API.Services.Interface;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Document = DocumentFormat.OpenXml.Wordprocessing.Document;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

namespace API.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly IContractRepository _contractRepository;

        public TemplateService(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public static string NumberToText(long number)
        {
            string[] unitNumbers = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] placeValues = new string[] { "", "nghìn", "triệu", "tỷ" };
            bool isNegative = false;

            string sNumber = number.ToString("#");
            if (number < 0)
            {
                number = -number;
                sNumber = number.ToString();
                isNegative = true;
            }

            int ones, tens, hundreds;

            int positionDigit = sNumber.Length;   // last -> first

            string result = " ";

            if (positionDigit == 0)
                result = unitNumbers[0] + result;
            else
            {
                // 0:       ###
                // 1: nghìn ###,###
                // 2: triệu ###,###,###
                // 3: tỷ    ###,###,###,###
                int placeValue = 0;

                while (positionDigit > 0)
                {
                    // Check last 3 digits remain ### (hundreds tens ones)
                    tens = hundreds = -1;
                    ones = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                    positionDigit--;
                    if (positionDigit > 0)
                    {
                        tens = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                        positionDigit--;
                        if (positionDigit > 0)
                        {
                            hundreds = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                            positionDigit--;
                        }
                    }

                    if ((ones > 0) || (tens > 0) || (hundreds > 0) || (placeValue == 3))
                        result = placeValues[placeValue] + result;

                    placeValue++;
                    if (placeValue > 3) placeValue = 1;

                    if ((ones == 1) && (tens > 1))
                        result = "một " + result;
                    else
                    {
                        if ((ones == 5) && (tens > 0))
                            result = "lăm " + result;
                        else if (ones > 0)
                            result = unitNumbers[ones] + " " + result;
                    }
                    if (tens < 0)
                        break;
                    else
                    {
                        if ((tens == 0) && (ones > 0)) result = "linh " + result;
                        if (tens == 1) result = "mười " + result;
                        if (tens > 1) result = unitNumbers[tens] + " mươi " + result;
                    }
                    if (hundreds < 0) break;
                    else
                    {
                        if ((hundreds > 0) || (tens > 0) || (ones > 0))
                            result = unitNumbers[hundreds] + " trăm " + result;
                    }
                    result = " " + result;
                }
            }
            result = result.Trim();
            if (isNegative) result = "Âm " + result;
            else result = result[0].ToString().ToUpper() + result.Substring(1);
            return result;
        }

        public void ReplaceBookmarkTable(string fileName)
        {
            using (WordprocessingDocument doc
                = WordprocessingDocument.Open(fileName, true))
            {
                foreach (BookmarkStart bookmark in doc.MainDocumentPart.Document.Body.Descendants<BookmarkStart>())
                {
                    // Get name of bookmark
                    string bookmarkNameOriginal = bookmark.Name;

                    // Get bookmark text from parent elements text
                    string bookmarkText = bookmark.Parent.InnerText;
                    Console.WriteLine($"{bookmarkNameOriginal} {bookmarkText} {bookmark.Id}");
                    if (bookmarkNameOriginal == "row")
                    {
                        TableRow tr = (TableRow)bookmark.Parent.Parent.Parent;
                        TableRow newTr = (TableRow)tr.Clone();
                        BookmarkStart bookmarkStart = newTr.Descendants<BookmarkStart>().First();
                        bookmarkStart.Remove();
                        // Find the first cell in the row.
                        TableCell cell = newTr.Elements<TableCell>().ElementAt(0);

                        // Find the first paragraph in the table cell.
                        Paragraph p = cell.Elements<Paragraph>().First();

                        // Find the first run in the paragraph.
                        Run r = p.Elements<Run>().First();

                        // Set the text for the run.
                        Text t = r.Elements<Text>().First();
                        t.Text = "them hang moi";

                        Table table = doc.MainDocumentPart.Document.Body.Elements<Table>().First();
                        table.Append(newTr);
                    }
                }

                doc.Save();
            }
        }

        public static void ReplaceBookmark(string fileName, object obj)
        {
            using (WordprocessingDocument doc
                = WordprocessingDocument.Open(fileName, true))
            {

                foreach (BookmarkStart bookmark in doc.MainDocumentPart.Document.Body.Descendants<BookmarkStart>())
                {
                    // Get name of bookmark
                    string bookmarkNameOriginal = bookmark.Name;

                    // Get bookmark text from parent elements text
                    if (obj.GetType().GetProperty(bookmarkNameOriginal) != null)
                    {
                        bookmark.NextSibling<Run>().Elements<Text>().First().Text = obj.GetType().GetProperty(bookmarkNameOriginal).GetValue(obj, null).ToString();
                    }
                }
            }
        }

        // Insert a table into a word processing document.
        public void CreateTable(string fileName)
        {
            // Use the file name and path passed in as an argument
            // to open an existing Word 2007 document.

            using (WordprocessingDocument doc
                = WordprocessingDocument.Open(fileName, true))
            {
                // Create an empty table.
                Table table = new Table();

                // Create a TableProperties object and specify its border information.
                TableProperties tblProp = new TableProperties(
                    new TableBorders(
                        new TopBorder()
                        {
                            Val =
                            new EnumValue<BorderValues>(BorderValues.Dashed),
                            Size = 24
                        },
                        new BottomBorder()
                        {
                            Val =
                            new EnumValue<BorderValues>(BorderValues.Dashed),
                            Size = 24
                        },
                        new LeftBorder()
                        {
                            Val =
                            new EnumValue<BorderValues>(BorderValues.Dashed),
                            Size = 24
                        },
                        new RightBorder()
                        {
                            Val =
                            new EnumValue<BorderValues>(BorderValues.Dashed),
                            Size = 24
                        },
                        new InsideHorizontalBorder()
                        {
                            Val =
                            new EnumValue<BorderValues>(BorderValues.Dashed),
                            Size = 24
                        },
                        new InsideVerticalBorder()
                        {
                            Val =
                            new EnumValue<BorderValues>(BorderValues.Dashed),
                            Size = 24
                        }
                    )
                );

                // Append the TableProperties object to the empty table.
                table.AppendChild<TableProperties>(tblProp);

                // Create a row.
                TableRow tr = new TableRow();

                // Create a cell.
                TableCell tc1 = new TableCell();

                // Specify the width property of the table cell.
                tc1.Append(new TableCellProperties(
                    new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));

                // Specify the table cell content.
                tc1.Append(new Paragraph(new Run(new Text("some text"))));

                // Append the table cell to the table row.
                tr.Append(tc1);

                // Create a second table cell by copying the OuterXml value of the first table cell.
                TableCell tc2 = new TableCell(tc1.OuterXml);

                // Append the table cell to the table row.
                tr.Append(tc2);

                // Append the table row to the table.
                table.Append(tr);

                // Append the table to the document.
                doc.MainDocumentPart.Document.Body.Append(table);
            }
        }

        public void CreateWordprocessingDocument(string filepath)
        {
            // Create a document by supplying the filepath.
            using (WordprocessingDocument wordDocument =
                WordprocessingDocument.Create(filepath, WordprocessingDocumentType.Document))
            {
                // Add a main document part.
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                // Create the document structure and add some text.
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());
                Paragraph para = body.AppendChild(new Paragraph());
                Run run = para.AppendChild(new Run());
                run.AppendChild(new Text("Create text in body - CreateWordprocessingDocument"));
            }
        }

        // Change the text in a table in a word processing document.
        public void ChangeTextInCell(string filepath, string txt)
        {
            // Use the file name and path passed in as an argument to
            // open an existing document.
            using (WordprocessingDocument doc =
                WordprocessingDocument.Open(filepath, true))
            {
                // Find the first table in the document.
                Table table =
                    doc.MainDocumentPart.Document.Body.Elements<Table>().First();

                // Find the second row in the table.
                TableRow row = table.Elements<TableRow>().ElementAt(1);

                // Find the first cell in the row.
                TableCell cell = row.Elements<TableCell>().ElementAt(0);

                // Find the first paragraph in the table cell.
                Paragraph p = cell.Elements<Paragraph>().First();

                // Find the first run in the paragraph.
                Run r = p.Elements<Run>().First();

                // Set the text for the run.
                Text t = r.Elements<Text>().First();
                t.Text = txt;
            }
        }

        public void AppendRowToTable(string filepath, string[,] data)
        {
            using (WordprocessingDocument doc =
            WordprocessingDocument.Open(filepath, true))
            {
                // Find the first table in the document.
                Table table =
                    doc.MainDocumentPart.Document.Body.Elements<Table>().First();

                for (var i = 0; i <= data.GetUpperBound(0); i++)
                {
                    var tr = new TableRow();
                    for (var j = 0; j <= data.GetUpperBound(1); j++)
                    {
                        var tc = new TableCell();
                        tc.Append(new Paragraph(new Run(new Text(data[i, j]))));

                        // Assume you want columns that are automatically sized.
                        tc.Append(new TableCellProperties(
                            new TableCellWidth { Type = TableWidthUnitValues.Auto }));

                        tr.Append(tc);
                    }
                    table.Append(tr);
                }
                doc.Save();
            }
        }

        public void AddTable(string fileName, string[,] data)
        {
            using (var document = WordprocessingDocument.Open(fileName, true))
            {
                var doc = document.MainDocumentPart.Document;

                Table table = new Table();

                TableProperties props = new TableProperties(
                    new TableBorders(
                    new TopBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 12
                    },
                    new BottomBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 12
                    },
                    new LeftBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 12
                    },
                    new RightBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 12
                    },
                    new InsideHorizontalBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 12
                    },
                    new InsideVerticalBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 12
                    }));

                table.AppendChild(props);

                for (var i = 0; i <= data.GetUpperBound(0); i++)
                {
                    var tr = new TableRow();
                    for (var j = 0; j <= data.GetUpperBound(1); j++)
                    {
                        var tc = new TableCell();
                        tc.Append(new Paragraph(new Run(new Text(data[i, j]))));

                        // Assume you want columns that are automatically sized.
                        tc.Append(new TableCellProperties(
                            new TableCellWidth { Type = TableWidthUnitValues.Auto }));

                        tr.Append(tc);
                    }
                    table.Append(tr);
                }
                doc.Body.Append(table);
                doc.Save();
            }
        }

        public async Task<string> ExportContract(Guid id)
        {
            var contract = await _contractRepository.GetById(id);
            return contract.Name;
        }
    }
}