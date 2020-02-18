
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using WordCount.Interface;

namespace WordCount
{
    class TextParser : ITextParser
    {
        private StreamReader _streamReader;
        private Func<string, bool> _checkСondition;

        public TextParser(StreamReader streamReader, Func<string, bool> checkСondition)
        {
            _streamReader = streamReader;
            _checkСondition = checkСondition;
        }

        public Action RunWorkerStarted { get ; set ; }
        public Action RunWorkerCompleted { get; set; }

        public Dictionary<string, int> DoWork()
        {
            if (_streamReader == null) return null;
            RunWorkerStarted?.Invoke();

            var wordReg = new Regex("[^a-zA-Z0-9а-яА-Я]");
            var result = new Dictionary<string, int>();

            string lineText;
            while ((lineText = _streamReader.ReadLine()) != null)
            {
                foreach (var word in wordReg.Split(lineText))
                {
                    var wordTrim = word.Trim();

                    if (!_checkСondition(wordTrim))
                    {
                        continue;
                    }

                    if (result.ContainsKey(wordTrim))
                    {
                        result[wordTrim]++;
                    }
                    else
                    {
                        result.Add(wordTrim, 1);
                    }
                }
            }

            RunWorkerCompleted?.Invoke();
            return result;
        }

    }
}
