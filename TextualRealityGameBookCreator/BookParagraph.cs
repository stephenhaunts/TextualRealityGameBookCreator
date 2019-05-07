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
using System.Collections.ObjectModel;
using TextualRealityGameBookCreator.Interfaces;

namespace TextualRealityGameBookCreator
{
    public class BookParagraph : IBookParagraph
    {
        public string Name { get; private set; }
        private List<ISectionPrimitive> _primitives;
        private List<IChoice> _choices;

        public BookParagraph()
        {
            Name = string.Empty;
            _primitives = new List<ISectionPrimitive>();
            _choices = new List<IChoice>();
        }

        public BookParagraph(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            _primitives = new List<ISectionPrimitive>();
            _choices = new List<IChoice>();
        }

        public ReadOnlyCollection<ISectionPrimitive> Primitives
        {
            get
            {
                return new ReadOnlyCollection<ISectionPrimitive>(_primitives);
            }
        }

        public void Add(ISectionPrimitive primitive)
        {
            if (primitive == null)
            {
                throw new ArgumentNullException(nameof(primitive));
            }

            _primitives.Add(primitive);
        }

        public int Count
        {
            get
            {
                return _primitives.Count;
            }
        }

        public ReadOnlyCollection<IChoice> Choices
        {
            get
            {
                return new ReadOnlyCollection<IChoice>(_choices);
            }
        }

        public void Add(IChoice choices)
        {
            if (choices == null)
            {
                throw new ArgumentNullException(nameof(choices));
            }

            _choices.Add(choices);
        }

        public int ChoicesCount
        {
            get
            {
                return _choices.Count;
            }
        }


    }
}
