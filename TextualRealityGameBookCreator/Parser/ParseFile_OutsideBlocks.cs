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
namespace TextualRealityGameBookCreator.Parser
{
    public partial class ParseFile
    {
        private void ProcessOutsideBlocks(string strippedLine)
        {
            if (strippedLine.ToLower().StartsWith("define", StringComparison.Ordinal))
            {
                // remove 'define' from start of line
                var removeDefine = strippedLine.Substring("define".Length).Trim();

                // check if this define is a bookname
                if (removeDefine.ToLower().StartsWith("bookname", StringComparison.Ordinal))
                {
                    ProcessBookName(strippedLine, removeDefine);
                    return;
                }

                // check if this define is a section
                if (removeDefine.ToLower().StartsWith("section", StringComparison.Ordinal))
                {
                    ProcessSection(strippedLine, removeDefine);
                    return;
                }

                // check if this define is a section
                if (removeDefine.ToLower().StartsWith("contents", StringComparison.Ordinal))
                {
                    if (_book.GetContents().Count == 0)
                    {
                        ProcessContents(strippedLine, removeDefine);
                        return;
                    }

                    ErrorAndThrow("Duplicate contents section found on line " + _lineCounter + ".");

                }

                ErrorAndThrow("Invalid definition name found on line " + _lineCounter + " <" + strippedLine + ">.");
            }
            else
            {
                ErrorAndThrow("'define' keyword expcted but found on line " + _lineCounter + " <" + strippedLine + ">.");
            }
        }
    }
}
