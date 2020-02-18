using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WordCount.Interface;
using WordCount.View;

namespace WordCount
{
    class Program
    {
        /// <summary>
        /// Минимальная длинна слова
        /// </summary>
        private static int _wordLongMin = 0;

        private static IFileManager _fileManager = new FileManager();
        private static IView _view = new ConsoleView();
        

        /// <summary>
        /// Максимальное количество потоков
        /// </summary>
        private static int _threadsMaxCount = 10;

        [STAThread]
        static void Main()
        {

            _view.MessageOutput("Программа для подсчёта повторяемости слов в тексте");

            ReceiveWordLongMin();

            ReceiveFolder();

            
            
            Thread.CurrentThread.Name = "main";

            var files = _fileManager.GetFiles("*.txt");

            var semaphore = new Semaphore(_threadsMaxCount, _threadsMaxCount);
            var threads = new List<Thread>(files.Length);
            
            var wordsRating = new List<Dictionary<string, int>>();

            foreach (var fileFullName in files)
            {
                var thread = new Thread(() => {
                    semaphore.WaitOne();

                    try
                    {
                        ITextParser textParser = new TextParser(
                            _fileManager.FileOpen(fileFullName),
                            (string s) => s.Length > _wordLongMin
                            );

                        textParser.RunWorkerStarted += () => _view.MessageOutput($"+Запустил парсинг файла: {fileFullName}");
                        textParser.RunWorkerCompleted += () => _view.MessageOutput($"-Завершил парсинг файла: {fileFullName}");

                        wordsRating.Add(textParser.DoWork());
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                });

                threads.Add(thread);

                thread.Start();

                
            }

            foreach (var thread in threads)
                thread.Join();

                       

            _view.MessageOutput("Топ 10 слов:"); 
            
            _view.MessageOutput("\tСлово \t кол. повторов");

            foreach (var word in wordsRating
                                    .Where(x => x != null)
                                    .SelectMany(x => x)
                                    .ToLookup(x => x.Key, x => x.Value)
                                    .ToDictionary(d => d.Key,d => d.Sum())
                                    .OrderByDescending(x => x.Value)
                                    .Take(10)
                )
            {
                _view.MessageOutput($"\t {word.Key} \t {word.Value}");
            }


            //Dictionary<DataReader, Thread> readers = new Dictionary<DataReader, Thread>();

            _view.TextInput("Нажмите Enter для завершения ...");
        }

        private static void ReceiveFolder()
        {
            _view.MessageOutput("Выберите директрию:");
            while (string.IsNullOrWhiteSpace(_fileManager.FolderSelect()))
            {
                _view.MessageErrorOutput("повторите выбор директории");
            }
        }

        private static void ReceiveWordLongMin()
        {
            while (_wordLongMin <= 0)
            {
                int.TryParse(_view.TextInput("Введите минимальную длину просматриваемых слов:"), out _wordLongMin);
                if (_wordLongMin == 0)
                {
                    _view.MessageErrorOutput("повторите ввод");
                }
            }
        }
    }
}
