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
using TextualRealityGameBookCreator.SectionPrimitives;

namespace TextualRealityGameBookCreator.Tests.Unit.SectionPrimitives
{
    [TestClass]
    public class ParagraphTests
    {
        [TestMethod]
        public void DefaultConstructorDefeultsTextToEmptyString()
        {
            Paragraph paragraph = new Paragraph();

            Assert.IsNotNull(paragraph.Text);
        }

        [TestMethod]
        public void OverrideConstructorAssignsSpecifiedText()
        {
            const string Text = "This is my paragraph text.";
            Paragraph paragraph = new Paragraph(Text);

            Assert.IsNotNull(paragraph.Text);
            Assert.AreEqual(Text, paragraph.Text);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OverrideConstructorThrowsArgumentNullExceptionIfSpecifiedTextIsNull()
        {
            Paragraph paragraph = new Paragraph(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OverrideConstructorThrowsArgumentNullExceptionIfSpecifiedTextIsEmptyString()
        {
            Paragraph paragraph = new Paragraph(string.Empty);
        }
    }
}
