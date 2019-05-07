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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextualRealityGameBookCreator.Interfaces;
using TextualRealityGameBookCreator.SectionPrimitives;

namespace TextualRealityGameBookCreator.Tests.Unit
{
    [TestClass]
    public class BookParagraphTests
    {
        [TestMethod]
        public void ConstructorCreatesEmptyBookParagraph()
        {
            IBookParagraph paragraph = new BookParagraph();

            Assert.AreEqual(string.Empty, paragraph.Name);
            Assert.AreEqual(0, paragraph.Primitives.Count);
        }

        [TestMethod]
        public void OveriddenConstructorCreatesEmptyBookParagraph()
        {
            const string NAME = "Paragraph";
            IBookParagraph paragraph = new BookParagraph(NAME);

            Assert.AreEqual(NAME, paragraph.Name);
            Assert.AreEqual(0, paragraph.Primitives.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OveriddenConstructorThrowsArgumentNullExceptionIfNameIsNull()
        {
            IBookParagraph paragraph = new BookParagraph(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OveriddenConstructorThrowsArgumentNullExceptionIfNameIsEmpty()
        {
            IBookParagraph paragraph = new BookParagraph(string.Empty);
        }

        [TestMethod]
        public void AddParagraphToBookParagraph()
        {
            const string TEXT = "This is the paragraph.";
            ISectionPrimitive prologue = new Paragraph(TEXT);

            IBookParagraph paragraph = new BookParagraph("Castle");
            paragraph.Add(prologue);

            Assert.AreEqual(1, paragraph.Count);
            Assert.AreEqual(1, paragraph.Primitives.Count);
            Assert.IsInstanceOfType(paragraph.Primitives[0], typeof(Paragraph));
            Assert.AreEqual(TEXT, ((Paragraph)paragraph.Primitives[0]).Text);
        }

        [TestMethod]
        public void AddParagraphsToBookParagraphBlock()
        {
            const string TEXT = "This is the prologue to my book.";
            const string TEXT2 = "I wrote this book for fun.";

            ISectionPrimitive prologue = new Paragraph(TEXT);
            ISectionPrimitive prologue2 = new Paragraph(TEXT2);

            IBookParagraph paragraph = new BookParagraph("Paragraph");
            paragraph.Add(prologue);
            paragraph.Add(prologue2);

            Assert.AreEqual(2, paragraph.Count);
            Assert.AreEqual(2, paragraph.Primitives.Count);

            Assert.IsInstanceOfType(paragraph.Primitives[0], typeof(Paragraph)); 
            Assert.IsInstanceOfType(paragraph.Primitives[1], typeof(Paragraph));

            Assert.AreEqual(TEXT, ((Paragraph)paragraph.Primitives[0]).Text);
            Assert.AreEqual(TEXT2, ((Paragraph)paragraph.Primitives[1]).Text);
        }

        [TestMethod]
        public void AddParagraphsAndImageFileToBookParagraph()
        {
            const string TEXT = "This is the prologue to my book.";
            const string TEXT2 = "I wrote this book for fun.";
            const string IMAGE = "./image.png";

            ISectionPrimitive prologue = new Paragraph(TEXT);
            ISectionPrimitive prologue2 = new Paragraph(TEXT2);
            ISectionPrimitive image = new Image(IMAGE);

            IBookParagraph paragraph = new BookParagraph("Paragraph");
            paragraph.Add(prologue);
            paragraph.Add(image);
            paragraph.Add(prologue2);

            Assert.AreEqual(3, paragraph.Count);
            Assert.AreEqual(3, paragraph.Primitives.Count);

            Assert.IsInstanceOfType(paragraph.Primitives[0], typeof(Paragraph));
            Assert.IsInstanceOfType(paragraph.Primitives[1], typeof(Image));
            Assert.IsInstanceOfType(paragraph.Primitives[2], typeof(Paragraph));

            Assert.AreEqual(TEXT, ((Paragraph)paragraph.Primitives[0]).Text);
            Assert.AreEqual(IMAGE, ((Image)paragraph.Primitives[1]).FileName);
            Assert.AreEqual(TEXT2, ((Paragraph)paragraph.Primitives[2]).Text);
        }


        [TestMethod]
        public void NewParagraphHasZeroChoices()
        {
            IBookParagraph paragraph = new BookParagraph("Paragraph");

            Assert.AreEqual(0, paragraph.ChoicesCount);
        }

        [TestMethod]
        public void AddChoiceToParagraph()
        {
            IBookParagraph paragraph = new BookParagraph("Paragraph");

            IChoice choice = new Choice
            {
                LinkToId = "TalkToMan",
                LinkTo = new BookParagraph()
            };

            paragraph.Add(choice);

            Assert.AreEqual(1, paragraph.ChoicesCount);
            Assert.AreEqual("TalkToMan", paragraph.Choices[0].LinkToId);
            Assert.IsNotNull(paragraph.Choices[0].LinkTo);
        }

        [TestMethod]
        public void AddChoicesToParagraph()
        {
            IBookParagraph paragraph = new BookParagraph("Paragraph");

            IChoice choice = new Choice
            {
                LinkToId = "TalkToMan",
                LinkTo = new BookParagraph()
            };

            IChoice choice2 = new Choice
            {
                LinkToId = "RingBell",
                LinkTo = new BookParagraph()
            };

            paragraph.Add(choice);
            paragraph.Add(choice2);

            Assert.AreEqual(2, paragraph.ChoicesCount);
            Assert.AreEqual("TalkToMan", paragraph.Choices[0].LinkToId);
            Assert.IsNotNull(paragraph.Choices[0].LinkTo);

            Assert.AreEqual("RingBell", paragraph.Choices[1].LinkToId);
            Assert.IsNotNull(paragraph.Choices[1].LinkTo);
        }
    }
}
