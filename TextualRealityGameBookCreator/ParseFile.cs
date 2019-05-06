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

namespace TextualRealityGameBookCreator
{
    public class ParseFile : IParseFile
    {
        List<string> _rawFile;

        public ParseFile()
        {
            _rawFile = new List<string>(); 
        }

        public ReadOnlyCollection<string> RawFile
        {
            get
            {
                return new ReadOnlyCollection<string>(_rawFile);
            }
        }

        public IBook Parse(List<string> rawFile)
        {
            return new Book();
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


    }
}
