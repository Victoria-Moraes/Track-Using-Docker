using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System.Drawing;
using System;
using System.IO;

namespace Track
{
    class Program
    {
        static void Main(string[] args)
        {
            using (FileStream fileStreamPath = new FileStream(Path.GetFullPath(@"/Users/victoria/Projects/Track-Changes-Docker/Track-Changes-Docker/Documento.docx"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (WordDocument document = new WordDocument(fileStreamPath, FormatType.Automatic))
                {

                    TextSelection[] textSelection = document.FindAll("obra", false, false);

                    foreach (TextSelection selection in textSelection)
                    {

                        foreach (WSection section in document.Sections)
                        {

                            IWTextRange textRange = selection.GetAsOneRange();
                            textRange.CharacterFormat.HighlightColor = Syncfusion.Drawing.Color.Yellow;

                            IWParagraph paragraph = section.AddParagraph();
                            textRange.Text = "Texto Alterado pelo Robo";
                            textRange.CharacterFormat.FontSize = 14;

                            paragraph = textRange.OwnerParagraph;
                            textRange.CharacterFormat.Italic = true;

                            TextBodyPart bodyPart = new TextBodyPart(selection);
                            document.Replace("Paragrafo", bodyPart, false, true, false);
                            document.TrackChanges = true;


                            WComment comment = paragraph.AppendComment("Teste Comentário");
                            comment.Format.User = "Robo";
                            comment.Format.UserInitials = "Mrs";
                            comment.Format.DateTime = DateTime.Now;


                        }


                    }


                    using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"/Users/victoria/Projects/Track-Changes-Docker/Track-Changes-Docker/Resultado.docx"), FileMode.Create, FileAccess.ReadWrite))

                    {
                        document.Save(outputFileStream, FormatType.Docx);
                    }
                }
            }
        }
    }
}

