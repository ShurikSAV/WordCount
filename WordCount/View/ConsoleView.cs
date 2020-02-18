using System;
using System.Windows.Forms;
using WordCount.Interface;

namespace WordCount.View
{
    class ConsoleView : IView

    {


        /// <summary>
        /// вывод сообщения об ошибке
        /// </summary>
        /// <param name="message">текст сообщения</param>
        public void MessageErrorOutput(string message)
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
        public void MessageOutput(string message)
        {
            Console.WriteLine(message);
        }

        
        /// <summary>
        /// Запрос пользователя ввести текс
        /// </summary>
        /// <param name="message">текст сообщения</param>
        public string TextInput(string message)
        {
            MessageOutput(message);

            return Console.ReadLine();
        }
    }
}
