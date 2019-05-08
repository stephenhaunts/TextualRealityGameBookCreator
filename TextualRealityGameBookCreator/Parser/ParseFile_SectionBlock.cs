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
        private void ProcessSection(string strippedLine, string removeDefine)
        {
            _parserState = ParserState.Section;

            var split = SplitInnerLine(':', removeDefine);
            var firstToken = split[0].Trim().ToLower();

            if (firstToken == ("section"))
            {
                IBookSection section = new BookSection(split[1].Trim());
                _currentParsedSection = section;
                _book.AddSection(section);
            }
        }

        private void ProcessInsideSection(string strippedLine)
        {
            if (strippedLine.ToLower().StartsWith("paragraph", StringComparison.Ordinal))
            {
                ExtractSectionParagraph(strippedLine);
                return;
            }

            if (strippedLine.ToLower().StartsWith("image", StringComparison.Ordinal))
            {
                ExtractSectionImage(strippedLine);
                return;
            }

            if (strippedLine.ToLower().StartsWith("end", StringComparison.Ordinal))
            {
                _parserState = ParserState.OutsideDefine;
                return;
            }

            ErrorAndThrow("Invalid attribute found in section on line " + _lineCounter + " <" + strippedLine + ">.");
        }

        private void ExtractSectionImage(string strippedLine)
        {
            var split = SplitInnerLine('=', strippedLine);
            var firstToken = split[0].Trim().ToLower();

            if (firstToken == ("image"))
            {
                ISectionPrimitive paragraph = new Image(split[1].Trim());
                _currentParsedSection.Add(paragraph);
            }
        }

        private void ExtractSectionParagraph(string strippedLine)
        {
            var split = SplitInnerLine('=', strippedLine);
            var firstToken = split[0].Trim().ToLower();

            if (firstToken == ("paragraph"))
            {
                ISectionPrimitive paragraph = new Paragraph(split[1].Trim());
                _currentParsedSection.Add(paragraph);
            }
            return;
        }
    }
}
