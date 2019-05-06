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
    public class BookTests
    {
        [TestMethod]
        public void ConstructorCreateBookWithEmptyBookSections()
        {
            IBook book = new Book();
            Assert.AreEqual(0, book.CountSections);
        }

        [TestMethod]
        public void AddSectionAddsSectionToBook()
        {
            const string PROLOGUE = "This is the prologue to my book.";
            const string PROLOGUE2 = "I wrote this book for fun.";

            ISectionPrimitive prologue = new Paragraph(PROLOGUE);
            ISectionPrimitive prologue2 = new Paragraph(PROLOGUE2);

            IBookSection section = new BookSection("Prologue");
            section.Add(prologue);
            section.Add(prologue2);

            IBook book = new Book();
            book.AddSection(section);

            Assert.AreEqual(1, book.CountSections);
        }

        [TestMethod]
        public void AddSectionAddsMultipleSectionsToBook()
        {
            IBook book = BookWithTwoSections();

            Assert.AreEqual(2, book.CountSections);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddSectionThrowsInvalidOperationExceptionIfYouAddDuplicateSectionName()
        {
            IBook book = BookWithTwoSections();

            // Rules
            const string RULES = "Read the book and pick choices.";

            ISectionPrimitive rulesParagraph = new Paragraph(RULES);

            IBookSection rules = new BookSection("Rules");
            rules.Add(rulesParagraph);

            book.AddSection(rules);          
        }

        private static IBook BookWithTwoSections()
        {
            // Prologue
            const string PROLOGUE = "This is the prologue to my book.";
            const string PROLOGUE2 = "I wrote this book for fun.";

            ISectionPrimitive prologue = new Paragraph(PROLOGUE);
            ISectionPrimitive prologue2 = new Paragraph(PROLOGUE2);

            IBookSection section = new BookSection("Prologue");
            section.Add(prologue);
            section.Add(prologue2);

            // Rules
            const string RULES = "Read the book and pick choices.";

            ISectionPrimitive rules = new Paragraph(RULES);

            IBookSection section2 = new BookSection("Rules");
            section2.Add(rules);


            IBook book = new Book();
            book.AddSection(section);
            book.AddSection(section2);
            return book;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddSectionThrowsInvalidOperationExceptionIfSectionDoesntHaveAName()
        {
            const string PROLOGUE = "This is the prologue to my book.";
            const string PROLOGUE2 = "I wrote this book for fun.";

            ISectionPrimitive prologue = new Paragraph(PROLOGUE);
            ISectionPrimitive prologue2 = new Paragraph(PROLOGUE2);

            IBookSection section = new BookSection();
            section.Add(prologue);
            section.Add(prologue2);

            IBook book = new Book();
            book.AddSection(section);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddSectionThrowsArgumentNullExceptionIfSectionIsNull()
        {
            IBook book = new Book();
            book.AddSection(null);    
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetSectionThrowsArgumentNullExceptionIsNameIsNull()
        {
            IBook book = BookWithTwoSections();
            book.GetSection(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetSectionThrowsArgumentNullExceptionIsNameIsEmpty()
        {
            IBook book = BookWithTwoSections();
            book.GetSection(string.Empty);
        }

        [TestMethod]
        public void GetSectionPrologueReturnsSectionByName()
        {
            IBook book = BookWithTwoSections();

            IBookSection prologue = book.GetSection("Prologue");

            Assert.IsNotNull(prologue);
            Assert.AreEqual("Prologue", prologue.Name);
        }

        [TestMethod]
        public void GetSectionRulesReturnsSectionByName()
        {
            IBook book = BookWithTwoSections();

            IBookSection rules = book.GetSection("Rules");

            Assert.IsNotNull(rules);
            Assert.AreEqual("Rules", rules.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetSectionThrowsArgumentNullExceptionIfSectionDoesntExist()
        {
            IBook book = BookWithTwoSections();
            IBookSection rules = book.GetSection("Does Not Exist");
        }

        [TestMethod]
        public void CountSectionsReturnsZeroForNewBook()
        {
            IBook book = new Book();
            Assert.AreEqual(0, book.CountSections);
        }

        [TestMethod]
        public void CountSectionsReturns2ForNewBookWithTwoSections()
        {
            IBook book = BookWithTwoSections();
            Assert.AreEqual(2, book.CountSections);
        }
    }
}
