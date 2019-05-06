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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextualRealityGameBookCreator.Interfaces;
using TextualRealityGameBookCreator.SectionPrimitives;

namespace TextualRealityGameBookCreator.Tests.Unit
{
    [TestClass]
    public class ParseFileTests
    {
        [TestMethod]
        public void LoadExampleFileLoadsNonEmptyFileAndReturnsArrayOfLines()
        {
            IParseFile parser = new ParseFile();
            var book = parser.Parse("/Examples/Example1 - BookName Only.gbc");

            Assert.IsNotNull(book);
            Assert.IsTrue(parser.RawFile.Count > 0);
        }

        [TestMethod]
        public void LoadExample1AndSetBookName()
        {
            IParseFile parser = new ParseFile();
            var book = parser.Parse("/Examples/Example1 - BookName Only.gbc");

            Assert.IsNotNull(book);
            Assert.AreEqual("The Strangest Pirate Story in the Galaxy!", book.BookName);
        }

        [TestMethod]
        public void LoadExample1ReportErrorsForInvalidDefinitionName()
        {
            IParseFile parser = new ParseFile();
            var book = parser.Parse("/Examples/Example1 - Invalid definition name.gbc");

            Assert.IsNotNull(book);
            Assert.IsTrue(parser.ErrorList.Count == 1);
            Assert.AreEqual("Invalid definition name found on line 2 <define wibble : The Strangest Pirate Story in the Galaxy!>.", parser.ErrorList[0]);
        }

        [TestMethod]
        public void LoadExample1AndSetBookNameAndOneSection()
        {
            IParseFile parser = new ParseFile();
            var book = parser.Parse("/Examples/Example1 - BookName and Single Section.gbc");

            Assert.IsNotNull(book);
            Assert.AreEqual("The Strangest Pirate Story in the Galaxy!", book.BookName);
            Assert.AreEqual(1, book.CountSections);
            Assert.IsTrue(book.SectionExists("prologue"));

          
            IBookSection prologue = book.GetSection("prologue");
            Assert.AreEqual(3, prologue.Count);
            Assert.AreEqual("This is the prologue to the book.", ((Paragraph)prologue.Primitives[0]).Text);
            Assert.AreEqual("./image.jpg", ((Image)prologue.Primitives[1]).FileName);
            Assert.AreEqual("This is the second paragraph of the prologue.", ((Paragraph)prologue.Primitives[2]).Text);
        }
    }
}
