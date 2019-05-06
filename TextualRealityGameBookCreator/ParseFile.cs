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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using TextualRealityGameBookCreator.Interfaces;
using TextualRealityGameBookCreator.SectionPrimitives;

namespace TextualRealityGameBookCreator
{
    public enum ParserState
    {
        OutsideDefine = 0,
        BookName = 1,
        Section = 2,
        Contents = 3,
        Paragraph = 4
    }

    public class ParseFile : IParseFile
    {
        private List<string> _rawFile;
        private IBook _book;
        private List<string> _errors;
        private int _lineCounter = 0;
        private ParserState _parserState = ParserState.OutsideDefine;
        private IBookSection _currentParsedSection;

        public ParseFile()
        {
            _book = new Book();
            _rawFile = new List<string>();
            _errors = new List<string>();
            _lineCounter = 0;
            _parserState = ParserState.OutsideDefine;
        }

        public ReadOnlyCollection<string> RawFile
        {
            get
            {
                return new ReadOnlyCollection<string>(_rawFile);
            }
        }

        public ReadOnlyCollection<string> ErrorList
        {
            get
            {
                return new ReadOnlyCollection<string>(_errors);
            }
        }

        public IBook Parse(List<string> rawFile)
        {
            _book = new Book();
            try
            {
                foreach (var line in _rawFile)
                {
                    _lineCounter++;

                    // TODO: Look for more complete way of stripping whitepace from the front.
                    var strippedLine = line.TrimStart(' ');

                    if (string.IsNullOrEmpty(strippedLine))
                    {
                        continue;
                    }

                    if (CheckForComments(strippedLine))
                    {
                        continue;
                    }

                    ProcessDefine(strippedLine);
                }
            }
            catch (InvalidOperationException)
            {

            }

            return _book;
        }

        public IBook Parse(string fileName)
        {
            var path = Path.GetFullPath(Environment.CurrentDirectory);
            var fullFilename = Path.GetFullPath(path + fileName);

            if (File.Exists(fullFilename))
            { 
                _rawFile = new List<string>(File.ReadAllLines(fullFilename));

                return Parse(_rawFile);
            }

            throw new FileNotFoundException("Input file not found.", fileName);
        }

        private bool CheckForComments(string line)
        {
            if (line.StartsWith("//", StringComparison.Ordinal))
            {
                return true;
            }

            return false;
        }

        private void ProcessDefine(string strippedLine)
        {
            switch (_parserState)
            {
                case ParserState.OutsideDefine:
                    if (strippedLine.ToLower().StartsWith("define", StringComparison.Ordinal))
                    {
                        // remove 'define' from start of line
                        var removeDefine = strippedLine.Substring("define".Length).TrimStart(' ');

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

                        ErrorAndThrow("Invalid definition name found on line "  + _lineCounter + " <" + strippedLine + ">.");
                    }
                    else
                    {
                        ErrorAndThrow("'define' keyword expcted but found on line "  + _lineCounter + " <" + strippedLine + ">.");
                    }
                    break;
                case ParserState.Section:
                    ProcessInsideSection(strippedLine);
                    break;
            }

        }

        private void ProcessInsideSection(string strippedLine)
        {
            if (strippedLine.ToLower().StartsWith("paragraph", StringComparison.Ordinal))
            {
                string[] split = strippedLine.Split('=');

                if (split.Length != 2)
                {
                    ErrorAndThrow("Error on line " + _lineCounter + " <" + strippedLine + ">.");
                }

                var firstToken = split[0].TrimStart(' ').TrimEnd(' ').ToLower();
                if (firstToken == ("paragraph"))
                {
                    ISectionPrimitive paragraph = new Paragraph(split[1].TrimStart(' '));
                    _currentParsedSection.Add(paragraph);

                }
                return;
            }

            if (strippedLine.ToLower().StartsWith("image", StringComparison.Ordinal))
            {
                string[] split = strippedLine.Split('=');

                if (split.Length != 2)
                {
                    ErrorAndThrow("Error on line " + _lineCounter + " <" + strippedLine + ">.");
                }

                var firstToken = split[0].TrimStart(' ').TrimEnd(' ').ToLower();
                if (firstToken == ("image"))
                {
                    ISectionPrimitive paragraph = new Image(split[1].TrimStart(' '));
                    _currentParsedSection.Add(paragraph);

                }
                return;
            }

            ErrorAndThrow("Invalid paragraph definition found on line " + _lineCounter + " <" + strippedLine + ">.");

        }

        private void ProcessSection(string strippedLine, string removeDefine)
        {
            _parserState = ParserState.Section;

            string[] split = removeDefine.Split(':');

            if (split.Length != 2)
            {
                ErrorAndThrow("Error on line " + _lineCounter + " <" + strippedLine + ">.");
            }

            var firstToken = split[0].TrimStart(' ').TrimEnd(' ').ToLower();
            if (firstToken == ("section"))
            {
                IBookSection section = new BookSection(split[1].TrimStart(' '));
                _currentParsedSection = section;
                _book.AddSection(section);
            }
        }



        private void ProcessBookName(string strippedLine, string removeDefine)
        {
            _parserState = ParserState.BookName;

            string[] split = removeDefine.Split(':');

            if (split.Length != 2)
            {
                ErrorAndThrow("Error on line " + _lineCounter + " <" + strippedLine + ">.");
            }

            var firstToken = split[0].TrimStart(' ').TrimEnd(' ').ToLower();
            if (firstToken == ("bookname"))
            {
                _book.BookName = split[1].TrimStart(' ');
            }

            _parserState = ParserState.OutsideDefine;
        }

        private void ErrorAndThrow(string errorMessage)
        {
            _errors.Add(errorMessage);
            throw new InvalidOperationException(errorMessage);
        }
    }
}
