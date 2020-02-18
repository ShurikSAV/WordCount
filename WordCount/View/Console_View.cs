using System;
using System.Windows.Forms;
using WordCount.Interface;

namespace WordCount.View
{
    class Console_View : IView

    {
        public string Folder_Select(string message)
        {
            Message_output(message);
            var FolderDialog = new FolderBrowserDialog();

            if (FolderDialog.ShowDialog() != DialogResult.OK || string.IsNullOrWhiteSpace(FolderDialog.SelectedPath))
            {
                return string.Empty;
            }
            return FolderDialog.SelectedPath;
        }

        /// <summary>
        /// вывод сообщения об ошибке
        /// </summary>
        /// <param name="message">текст сообщения</param>
        public void Message_error_output(string message)
        {

            var background_Color = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.BackgroundColor = background_Color;
        }

        /// <summary>
        /// вывод сообщения на экран
        /// </summary>
        /// <param name="message">текст сообщения</param>
        public void Message_output(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Запрос пользователя ввести текс
        /// </summary>
        /// <param name="message">текст сообщения</param>
        public string Text_input(string message)
        {
            Message_output(message);

            return Console.ReadLine();
        }
    }
}
