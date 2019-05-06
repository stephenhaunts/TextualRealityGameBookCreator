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

namespace TextualRealityGameBookCreator.Tests.Unit
{
    [TestClass]
    public class BookContentsTests
    {
        [TestMethod]
        public void NewContentItemsContainsNoItems()
        {
            IBookContents contents = new BookContents();
            Assert.AreEqual(0, contents.Count);
        }

        [TestMethod]
        public void AddInsertsNewContentItem()
        {
            IBookContents contents = new BookContents();
            contents.Add("Prologue");

            Assert.AreEqual(1, contents.Count);
            Assert.IsTrue(contents.Exists("Prologue"));
        }

        [TestMethod]
        public void AddInsertsNew2ContentItem()
        {
            IBookContents contents = new BookContents();
            contents.Add("Prologue");
            contents.Add("Rules");

            Assert.AreEqual(2, contents.Count);
            Assert.IsTrue(contents.Exists("Prologue"));
            Assert.IsTrue(contents.Exists("Rules"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddThrowsInvalidOperationExceptionForDuplicateContentItem()
        {
            IBookContents contents = new BookContents();
            contents.Add("Prologue");
            contents.Add("Prologue");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddThrowsArgumentNullExceptionForNullContentItem()
        {
            IBookContents contents = new BookContents();
            contents.Add(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddThrowsArgumentNullExceptionForEmptyContentItem()
        {
            IBookContents contents = new BookContents();
            contents.Add(string.Empty);
        }
    }
}
