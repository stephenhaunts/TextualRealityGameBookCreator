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
    public class BookContents : IBookContents
    {
        readonly Dictionary<string, string> _contentItems;

        public BookContents()
        {
            _contentItems = new Dictionary<string, string>();
        }

        public int Count
        {
            get
            {
                return _contentItems.Count;
            }
        }

        public bool Exists(string contentItem)
        {
            return (_contentItems.ContainsKey(contentItem.ToLower()));
        }

        public void Add(string contentsItem)
        {
            if (string.IsNullOrEmpty(contentsItem))
            {
                throw new ArgumentNullException(nameof(contentsItem));
            }

            if (_contentItems.ContainsKey(contentsItem.ToLower()))
            {
                throw new InvalidOperationException(nameof(contentsItem));
            }

            _contentItems.Add(contentsItem.ToLower(), contentsItem.ToLower());
        }
    }
}
