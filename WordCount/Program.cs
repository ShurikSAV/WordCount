using System;
using WordCount.Interface;
using WordCount.View;

namespace WordCount
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            IView view = new Console_View();
            
            view.Message_output("Программа для подсчёта повторяемости слов");

            int word_leng_min = 0;

            while (word_leng_min <= 0)
            {
                int.TryParse(view.Text_input("Введите минимальную длину просматриваемых слов:"), out word_leng_min);
                if (word_leng_min == 0)
                {
                    view.Message_error_output("повторите ввод");
                }
            }

            while (string.IsNullOrWhiteSpace(view.Folder_Select("Выберите директрию:")))
            {
                view.Message_error_output("повторите выбор директории");
            }

            


        }
    }
}
