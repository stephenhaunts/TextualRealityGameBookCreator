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
    public class BookSectionTests
    {
        [TestMethod]
        public void ConstructorCreatesEmptyBookSection()
        {
            IBookSection section = new BookSection();

            Assert.AreEqual(string.Empty, section.Name);
            Assert.AreEqual(0, section.Primitives.Count);
        }

        [TestMethod]
        public void OveriddenConstructorCreatesEmptyBookSection()
        {
            const string NAME = "Prologue";
            IBookSection section = new BookSection(NAME);

            Assert.AreEqual(NAME, section.Name);
            Assert.AreEqual(0, section.Primitives.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OveriddenConstructorThrowsArgumentNullExceptionIfBookNameIsNull()
        {
            IBookSection section = new BookSection(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OveriddenConstructorThrowsArgumentNullExceptionIfBookNameIsEmpty()
        {
            IBookSection section = new BookSection(string.Empty);
        }

        [TestMethod]
        public void AddParagraphToBookSection()
        {
            const string PROLOGUE = "This is the prologue to my book.";
            ISectionPrimitive prologue = new Paragraph(PROLOGUE);

            IBookSection section = new BookSection("Prologue");
            section.Add(prologue);

            Assert.AreEqual(1, section.Count);
            Assert.AreEqual(1, section.Primitives.Count);
            Assert.IsInstanceOfType(section.Primitives[0], typeof(Paragraph));
            Assert.AreEqual(PROLOGUE, ((Paragraph)section.Primitives[0]).Text);
        }

        [TestMethod]
        public void AddParagraphsToBookSection()
        {
            const string PROLOGUE = "This is the prologue to my book.";
            const string PROLOGUE2 = "I wrote this book for fun.";

            ISectionPrimitive prologue = new Paragraph(PROLOGUE);
            ISectionPrimitive prologue2 = new Paragraph(PROLOGUE2);

            IBookSection section = new BookSection("Prologue");
            section.Add(prologue);
            section.Add(prologue2);

            Assert.AreEqual(2, section.Count);
            Assert.AreEqual(2, section.Primitives.Count);

            Assert.IsInstanceOfType(section.Primitives[0], typeof(Paragraph)); 
            Assert.IsInstanceOfType(section.Primitives[1], typeof(Paragraph));

            Assert.AreEqual(PROLOGUE, ((Paragraph)section.Primitives[0]).Text);
            Assert.AreEqual(PROLOGUE2, ((Paragraph)section.Primitives[1]).Text);
        }

        [TestMethod]
        public void AddParagraphsAndImageFileToBookSection()
        {
            const string PROLOGUE = "This is the prologue to my book.";
            const string PROLOGUE2 = "I wrote this book for fun.";
            const string IMAGE = "./image.png";

            ISectionPrimitive prologue = new Paragraph(PROLOGUE);
            ISectionPrimitive prologue2 = new Paragraph(PROLOGUE2);
            ISectionPrimitive image = new Image(IMAGE);

            IBookSection section = new BookSection("Prologue");
            section.Add(prologue);
            section.Add(image);
            section.Add(prologue2);

            Assert.AreEqual(3, section.Count);
            Assert.AreEqual(3, section.Primitives.Count);

            Assert.IsInstanceOfType(section.Primitives[0], typeof(Paragraph));
            Assert.IsInstanceOfType(section.Primitives[1], typeof(Image));
            Assert.IsInstanceOfType(section.Primitives[2], typeof(Paragraph));

            Assert.AreEqual(PROLOGUE, ((Paragraph)section.Primitives[0]).Text);
            Assert.AreEqual(IMAGE, ((Image)section.Primitives[1]).FileName);
            Assert.AreEqual(PROLOGUE2, ((Paragraph)section.Primitives[2]).Text);
        }
    }
}
