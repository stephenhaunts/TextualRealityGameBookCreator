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
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TextualRealityGameBookCreator.Tests.Unit
{
    [TestClass]
    public class ParseFile
    {
        const string EXAMPLES_PATH = "/Examples";
        const string EXAMPLE1 = "/Game Book Example.gbc";

        [TestMethod]
        public void LoadExampleFileLoadsNonEmptyFileAndReturnsArrayOfLines()
        {
            var file = LoadExampleFile(EXAMPLE1);

            Assert.IsNotNull(file);
            Assert.IsTrue(file.Length > 0);
        }

        private static string[] LoadExampleFile(string filename)
        {
            var path = Path.GetFullPath(Environment.CurrentDirectory + EXAMPLES_PATH);
            var fullFilename = Path.GetFullPath(path + filename);

            if (File.Exists(fullFilename))
            {
                return File.ReadAllLines(fullFilename);
            }

            return null;
        }
    }
}
