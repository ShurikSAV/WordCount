using System;
using System.Collections.Generic;

namespace WordCount.Interface
{
    interface ITextParser
    {
        Action RunWorkerStarted { get; set; }
        Action RunWorkerCompleted { get; set; }

        Dictionary<string, int> DoWork();
    }
}
