namespace WordCount.Interface
{
    /// <summary>
    /// Интерфейс для вывода сообщений пользователю
    /// </summary>
    interface IView
    {
        void Message_output(string message);

        string Text_input(string message);
        void Message_error_output(string message);
        string Folder_Select(string message);
    }
}
