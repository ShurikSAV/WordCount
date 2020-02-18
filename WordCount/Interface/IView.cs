namespace WordCount.Interface
{
    /// <summary>
    /// Интерфейс для вывода сообщений пользователю
    /// </summary>
    interface IView
    {
        void MessageOutput(string message);
        string TextInput(string message);
        void MessageErrorOutput(string message);
    }
}
