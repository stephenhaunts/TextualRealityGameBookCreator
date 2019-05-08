/*
MIT License

Copyright (c) 2019 

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using TextualRealityGameBookCreator.Interfaces;
using TextualRealityGameBookCreator.SectionPrimitives;

namespace TextualRealityGameBookCreator.Parser
{
    public partial class ParseFile
    {
        private void ProcessParagraph(string strippedLine, string removeDefine)
        {
            _parserState = ParserState.Paragraph;

            string[] split = removeDefine.Split(':');

            if (split.Length != 2)
            {
                ErrorAndThrow("Error on line " + _lineCounter + " <" + strippedLine + ">.");
            }

            var firstToken = split[0].Trim().ToLower();
            if (firstToken == ("paragraph"))
            {
                IBookParagraph paragraph = new BookParagraph(split[1].Trim());
                _currentParsedParagraph = paragraph;
                _book.AddParagraph(paragraph);
            }
        }

        private void ProcessInsideParagraph(string strippedLine)
        {
            if (strippedLine.ToLower().StartsWith("paragraph", StringComparison.Ordinal))
            {
                ExtractParagraph(strippedLine);
                return;
            }

            if (strippedLine.ToLower().StartsWith("image", StringComparison.Ordinal))
            {
                ExtractImage(strippedLine);
                return;
            }

            // choice.left = Retreat from the castle and go back down the pathway.
            if (strippedLine.ToLower().StartsWith("choice", StringComparison.Ordinal))
            {
                ExtractChoice(strippedLine);
                return;
            }

            if (strippedLine.ToLower().StartsWith("end", StringComparison.Ordinal))
            {
                _parserState = ParserState.OutsideDefine;
                return;
            }

            ErrorAndThrow("Invalid attribute found in paragraph on line " + _lineCounter + " <" + strippedLine + ">.");

        }

        private void ExtractChoice(string strippedLine)
        {
            string[] split = strippedLine.Split('=');

            if (split.Length != 2)
            {
                ErrorAndThrow("Error on line " + _lineCounter + " <" + strippedLine + ">.");
            }

            var firstToken = split[0].Trim().ToLower();
            string[] choiceSplit = firstToken.Split('.');

            if (choiceSplit[0] == ("choice"))
            {
                var secondToken = split[1].Trim().ToLower();
                IChoice choice = new Choice
                {
                    LinkToId = choiceSplit[1],
                    Text = secondToken
                };

                _currentParsedParagraph.Add(choice);
            }
        }

        private void ExtractImage(string strippedLine)
        {
            string[] split = strippedLine.Split('=');

            if (split.Length != 2)
            {
                ErrorAndThrow("Error on line " + _lineCounter + " <" + strippedLine + ">.");
            }

            var firstToken = split[0].Trim().ToLower();
            if (firstToken == ("image"))
            {
                ISectionPrimitive paragraph = new Image(split[1].Trim());
                _currentParsedParagraph.Add(paragraph);
            }
        }

        private void ExtractParagraph(string strippedLine)
        {
            string[] split = strippedLine.Split('=');

            if (split.Length != 2)
            {
                ErrorAndThrow("Error on line " + _lineCounter + " <" + strippedLine + ">.");
            }

            var firstToken = split[0].Trim().ToLower();
            if (firstToken == ("paragraph"))
            {
                ISectionPrimitive paragraph = new Paragraph(split[1].Trim());
                _currentParsedParagraph.Add(paragraph);
            }
        }
    }
}
