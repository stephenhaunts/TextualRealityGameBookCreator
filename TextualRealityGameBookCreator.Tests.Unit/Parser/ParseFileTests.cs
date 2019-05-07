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
using TextualRealityGameBookCreator.Parser;
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

        [TestMethod]
        public void LoadExample1ReportErrorForInvalidAttributeInSection()
        {
            IParseFile parser = new ParseFile();
            var book = parser.Parse("/Examples/Example1 - Invalid attribute in section.gbc");

            Assert.AreEqual("Invalid attribute found in section on line 10 <wibble>.", parser.ErrorList[0]);
        }

        [TestMethod]
        public void LoadExample1Loads3Sections()
        {
            IParseFile parser = new ParseFile();
            var book = parser.Parse("/Examples/Example1.gbc");

            Assert.IsNotNull(book);
            Assert.AreEqual("The Strangest Pirate Story in the Galaxy!", book.BookName);
            Assert.AreEqual(3, book.CountSections);

            Assert.IsTrue(book.SectionExists("prologue"));
            IBookSection prologue = book.GetSection("prologue");
            Assert.AreEqual(3, prologue.Count);
            Assert.AreEqual("This is the prologue to the book.", ((Paragraph)prologue.Primitives[0]).Text);
            Assert.AreEqual("./image.jpg", ((Image)prologue.Primitives[1]).FileName);
            Assert.AreEqual("This is the second paragraph of the prologue.", ((Paragraph)prologue.Primitives[2]).Text);

            Assert.IsTrue(book.SectionExists("dedication"));
            IBookSection dedication = book.GetSection("dedication");
            Assert.AreEqual(1, dedication.Count);
            Assert.AreEqual("This book is dedicated to Amanda, Amy and Daniel.", ((Paragraph)dedication.Primitives[0]).Text);

            Assert.IsTrue(book.SectionExists("rules"));
            IBookSection rules = book.GetSection("rules");
            Assert.AreEqual(1, rules.Count);
            Assert.AreEqual("Navigate through the book by making decisions at the decision points which will make you turn to different paragraphs.", ((Paragraph)rules.Primitives[0]).Text);

        }

        [TestMethod]
        public void LoadExample2Loads3SectionsAndContents()
        {
            IParseFile parser = new ParseFile();
            var book = parser.Parse("/Examples/Example2.gbc");

            Assert.IsNotNull(book);
            Assert.AreEqual("The Strangest Pirate Story in the Galaxy!", book.BookName);
            Assert.AreEqual(3, book.CountSections);

            Assert.IsTrue(book.SectionExists("prologue"));
            IBookSection prologue = book.GetSection("prologue");
            Assert.AreEqual(3, prologue.Count);
            Assert.AreEqual("This is the prologue to the book.", ((Paragraph)prologue.Primitives[0]).Text);
            Assert.AreEqual("./image.jpg", ((Image)prologue.Primitives[1]).FileName);
            Assert.AreEqual("This is the second paragraph of the prologue.", ((Paragraph)prologue.Primitives[2]).Text);

            Assert.IsTrue(book.SectionExists("dedication"));
            IBookSection dedication = book.GetSection("dedication");
            Assert.AreEqual(1, dedication.Count);
            Assert.AreEqual("This book is dedicated to Amanda, Amy and Daniel.", ((Paragraph)dedication.Primitives[0]).Text);

            Assert.IsTrue(book.SectionExists("rules"));
            IBookSection rules = book.GetSection("rules");
            Assert.AreEqual(1, rules.Count);
            Assert.AreEqual("Navigate through the book by making decisions at the decision points which will make you turn to different paragraphs.", ((Paragraph)rules.Primitives[0]).Text);

            Assert.AreEqual(3, book.GetContents().Count);
            Assert.IsTrue(book.GetContents().Exists("Prologue"));
            Assert.IsTrue(book.GetContents().Exists("Dedication"));
            Assert.IsTrue(book.GetContents().Exists("Rules"));
        }

        [TestMethod]
        public void LoadExample2ReportsErrorForDuplicateContents()
        {
            IParseFile parser = new ParseFile();
            var book = parser.Parse("/Examples/Example2 - Duplicate Contents.gbc");

            Assert.AreEqual("Duplicate contents section found on line 32.", parser.ErrorList[0]);
        }

        [TestMethod]
        public void LoadExample3()
        {
            IParseFile parser = new ParseFile();
            var book = parser.Parse("/Examples/Example3.gbc");

            Assert.IsNotNull(book);
            Assert.AreEqual("The Strangest Pirate Story in the Galaxy!", book.BookName);
            Assert.AreEqual(3, book.CountSections);

            Assert.IsTrue(book.SectionExists("prologue"));
            IBookSection prologue = book.GetSection("prologue");
            Assert.AreEqual(3, prologue.Count);
            Assert.AreEqual("This is the prologue to the book.", ((Paragraph)prologue.Primitives[0]).Text);
            Assert.AreEqual("./image.jpg", ((Image)prologue.Primitives[1]).FileName);
            Assert.AreEqual("This is the second paragraph of the prologue.", ((Paragraph)prologue.Primitives[2]).Text);

            Assert.IsTrue(book.SectionExists("dedication"));
            IBookSection dedication = book.GetSection("dedication");
            Assert.AreEqual(1, dedication.Count);
            Assert.AreEqual("This book is dedicated to Amanda, Amy and Daniel.", ((Paragraph)dedication.Primitives[0]).Text);

            Assert.IsTrue(book.SectionExists("rules"));
            IBookSection rules = book.GetSection("rules");
            Assert.AreEqual(1, rules.Count);
            Assert.AreEqual("Navigate through the book by making decisions at the decision points which will make you turn to different paragraphs.", ((Paragraph)rules.Primitives[0]).Text);

            Assert.AreEqual(4, book.GetContents().Count);
            Assert.IsTrue(book.GetContents().Exists("Prologue"));
            Assert.IsTrue(book.GetContents().Exists("Dedication"));
            Assert.IsTrue(book.GetContents().Exists("Rules"));
            Assert.IsTrue(book.GetContents().Exists("Start"));

            Assert.AreEqual(5, book.CountParagraph);

        }
    }
}
