using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CssToJson
{
    class Program
    {
        static readonly Dictionary<string, string> NeededTypographyElements = new Dictionary<string, string>
        {
            {  "DisplayBig","/* Display/Big */"},
            {  "DisplaySmall","/* Display/Small */"},
            {  "HeadingH1","/* Heading/H1 */"},
            {  "HeadingH2","/* Heading/H2 */"},
            {  "HeadingH3","/* Heading/H3 */"},
            {  "HeadingH4","/* Heading/H4 */"},
            {  "HeadingH5","/* Heading/H5 */"},
            {  "HeadingH6","/* Heading/H6 */"},
            {  "BodySmallLight","/* Body/Small/Light */"},
            {  "BodySmallLightUnderline","/* Body/Small/Light underline */"},
            {  "BodySmallLightItalic","/* Body/Small/Light italic */"},
            {  "BodySmallLightItalicUnderline","/* Body/Small/Light italic underline */"},
            {  "BodySmall","/* Body/Small/Regular */"},
            {  "BodySmallUnderline","/* Body/Small/Regular underline */"},
            {  "BodySmallItalic","/* Body/Small/Regular italic */"},
            {  "BodySmallItalicUnderline","/* Body/Small/Regular italic underline */"},
            {  "BodyMedium","/* Body/Medium/Regular */"},
            {  "BodyMediumUnderline","/* Body/Medium/Regular underline */"},
            {  "BodyMediumItalic","/* Body/Medium/Regular italic */"},
            {  "BodyMediumItalicUnderline","/* Body/Medium/Regular italic underline */"},
            {  "BodyMediumBold","/* Body/Medium/Bold */"},
            {  "BodyMediumBoldUnderline","/* Body/Medium/Bold underline */"},
            {  "BodyMediumBoldItalic","/* Body/Medium/Bold italic */"},
            {  "BodyMediumBoldItalicUnderline","/* Body/Medium/Bold italic underline */"},
            {  "BodyLarge","/* Body/Large/Regular */"},
            {  "BodyLargeUnderline","/* Body/Large/Regular underline */"},
            {  "BodyLargeItalic","/* Body/Large/Regular italic */"},
            {  "BodyLargeItalicUnderline","/* Body/Large/Regular italic underline */"},
            {  "BodyLargeBold","/* Body/Large/Bold */"},
            {  "BodyLargeBoldUnderline","/* Body/Large/Bold underline */"},
            {  "BodyLargeBoldItalic","/* Body/Large/Bold italic */"},
            {  "BodyLargeBoldItalicUnderline","/* Body/Large/Bold italic underline */"},
            {  "SupportOverline","/* Support/Overline */"},
            {  "Button","/* Button/Bold */" }
        };

        static readonly Dictionary<string, string> CSSParameters = new Dictionary<string, string>
        {
            {"Fonts","font-family" },
            {"FontSizes","font-size" },
            {"LineHeight","line-height" },
            {"Weight","font-weight" }
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Started");

            string textFile = @"../../../Typography.txt";
            //string textFile1 = @"~\Typography.txt";

            if (File.Exists(textFile))
            {
                var text = File.ReadAllLines(path: textFile);
                
                var fontSizeDict = new Dictionary<string, string>(); fontSizeDict.Add( "FontSizes","font-size" );
                var LineHeightDict = new Dictionary<string, string>(); LineHeightDict.Add("LineHeight", "line-height");
                var WeightDict = new Dictionary<string, string>();WeightDict.Add("Weight", "font-weight");


                foreach (var item in NeededTypographyElements)
                {
                    var cssParamsOfItem = text.ToList().SkipWhile(x => !x.Equals(item.Value)).Skip(1).Take(5);

                    fontSizeDict.Add(item.Key,
                        "\"" + (Convert.ToDecimal(
                            cssParamsOfItem.ToList().FirstOrDefault(x => x.Contains("font-size")).Split(":")[1].Trim().Replace("px;", "")
                            ) / 10)
                            .ToString() + "rem\"");

                    LineHeightDict.Add(item.Key,
                        "\"" + (Convert.ToDecimal(
                            cssParamsOfItem.ToList().FirstOrDefault(x => x.Contains("line-height")).Split(":")[1].Trim().Replace("px;", "")
                            ) / 10)
                            .ToString() + "rem\"");

                    WeightDict.Add(item.Key,
                        "\"" +
                            cssParamsOfItem.ToList()
                            .FirstOrDefault(x => x.Contains("font-weight"))
                            .Split(":")[1]
                            .Trim()
                            .Replace(";", "")
                            .ToString() + "\"");
                            
                }

                var fontsizeText = GetLine(fontSizeDict);
                var lineHeightText = GetLine(LineHeightDict);
                var weightText = GetLine(WeightDict);

                if (File.Exists(@"../../../Outcome.txt"))
                {
                    File.Delete(@"../../../Outcome.txt");
                }
                File.WriteAllLines(@"../../../Outcome.txt", new string[] { fontsizeText, lineHeightText, weightText });
              
            }

            static string GetLine(Dictionary<string, string> data)
            {
                // Step 2: build up the string data.
                StringBuilder builder = new StringBuilder();
                foreach (var pair in data)
                {
                    builder.Append(pair.Key).Append(":").Append(pair.Value).Append(",\n");
                }
                string result = builder.ToString();
                // Remove the end comma.
                result = result.TrimEnd(',');
                return result;
            }

        }



    }
}
