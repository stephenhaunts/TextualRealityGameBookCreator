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
using TextualRealityGameBookCreator.Interfaces;

namespace TextualRealityGameBookCreator
{
    public class Book : IBook
    {
        private readonly Dictionary<string, IBookSection> _bookSections;

        public Book()
        {
            BookName = string.Empty;
            _bookSections = new Dictionary<string, IBookSection>();
        }

        public string BookName { get; set; }

        public int CountSections
        {
            get
            {
                return _bookSections.Count;
            }
        }

        public IBookSection GetSection(string sectionName)
        {
            if (string.IsNullOrEmpty(sectionName))
            {
                throw new ArgumentNullException(sectionName);
            }

            if (!_bookSections.ContainsKey(sectionName.ToLower()))
            {
                throw new InvalidOperationException("A book section of the name >" + sectionName.ToLower() + "> does not exist.");
            }

            return _bookSections[sectionName.ToLower()];
        }

        public void AddSection(IBookSection section)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            if (string.IsNullOrEmpty(section.Name))
            {
                throw new InvalidOperationException("Section name can not be null or empty.");
            }

            if (_bookSections.ContainsKey(section.Name.ToLower()))
            {
                throw new InvalidOperationException("A book section of the name >" + section.Name.ToLower() + "> already exists in the book.");
            }

            _bookSections.Add(section.Name.ToLower(), section);
        }
    }
}
