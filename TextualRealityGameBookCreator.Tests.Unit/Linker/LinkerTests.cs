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
using TextualRealityGameBookCreator.Parser;
using TextualRealityGameBookCreator.SectionPrimitives;

namespace TextualRealityGameBookCreator.Tests.Unit.Linker
{
    [TestClass]
    public class LinkerTests
    {
        [TestMethod]
        public void LoadAndLinkExample4()
        {
            IParseFile parser = new ParseFile();
            var book = parser.Parse("/Examples/Example4.gbc");

            ValidateBook(book);
        }

        private static void ValidateBook(IBook book)
        {
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

            Assert.AreEqual("start", book.GetParagraph("start").Name);
            Assert.AreEqual("You are standing at the start of a winding path. There is a split in the road going left or right.", ((Paragraph)book.GetParagraph("start").Primitives[0]).Text);
            Assert.AreEqual("./image.jpg", ((Image)book.GetParagraph("start").Primitives[1]).FileName);
            Assert.AreEqual("What will you do?", ((Paragraph)book.GetParagraph("start").Primitives[2]).Text);
            Assert.AreEqual(2, book.GetParagraph("start").ChoicesCount);
            Assert.AreEqual("left", book.GetParagraph("start").Choices[0].LinkToId);
            Assert.AreEqual("Take the left hand path.", book.GetParagraph("start").Choices[0].Text);
            Assert.AreEqual("right", book.GetParagraph("start").Choices[1].LinkToId);
            Assert.AreEqual("Take the right hand path.", book.GetParagraph("start").Choices[1].Text);

            Assert.AreEqual("left", book.GetParagraph("left").Name);
            Assert.AreEqual("You see an old man standing in the road. He looks frail and unwelcoming. The path continues into the distance.", ((Paragraph)book.GetParagraph("left").Primitives[0]).Text);
            Assert.AreEqual("./image.jpg", ((Image)book.GetParagraph("left").Primitives[1]).FileName);
            Assert.AreEqual("What will you do?", ((Paragraph)book.GetParagraph("left").Primitives[2]).Text);
            Assert.AreEqual("talktoman", book.GetParagraph("left").Choices[0].LinkToId);
            Assert.AreEqual("Ask the old man how to get to the castle.", book.GetParagraph("left").Choices[0].Text);
            Assert.AreEqual("takepath", book.GetParagraph("left").Choices[1].LinkToId);
            Assert.AreEqual("Ignore the old man and continue down the path.", book.GetParagraph("left").Choices[1].Text);

            Assert.AreEqual("right", book.GetParagraph("right").Name);
            Assert.AreEqual("You are eaten by an ogre with big teeth that are dripping blood onto the floor. You can start the quest again.", ((Paragraph)book.GetParagraph("right").Primitives[0]).Text);
            Assert.AreEqual("./image.jpg", ((Image)book.GetParagraph("right").Primitives[1]).FileName);
            Assert.AreEqual("start", book.GetParagraph("right").Choices[0].LinkToId);
            Assert.AreEqual("If you want to try again, please restart the adventure.", book.GetParagraph("right").Choices[0].Text);

            Assert.AreEqual("talktoman", book.GetParagraph("talktoman").Name);
            Assert.AreEqual("The old man looks at you agitated and then pulls out a knife that he plunges into your heart. As the life drains out of your body, you stare into his cold, dead, eyes. He cackles to himself as he licks the blood from his knife.", ((Paragraph)book.GetParagraph("talktoman").Primitives[0]).Text);
            Assert.AreEqual("start", book.GetParagraph("talktoman").Choices[0].LinkToId);
            Assert.AreEqual("If you want to try again, please restart the adventure.", book.GetParagraph("talktoman").Choices[0].Text);

            Assert.AreEqual("takepath", book.GetParagraph("takepath").Name);
            Assert.AreEqual("You walk down the path for what feels like hours and arrive at the gates of a castle. There is a bell next to the gate.", ((Paragraph)book.GetParagraph("takepath").Primitives[0]).Text);
            Assert.AreEqual("left", book.GetParagraph("takepath").Choices[0].LinkToId);
            Assert.AreEqual("Retreat from the castle and go back down the pathway.", book.GetParagraph("takepath").Choices[0].Text);
        }
    }
}
