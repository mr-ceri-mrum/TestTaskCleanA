using CleanA.Domain.Сonstants;

namespace CleanA.Domain.ActionResponse;

public class ActionMethodResult
{
     /// <summary>
        /// Флаг результата
        /// </summary>
        public bool IsSuccessed { get; set; }
        /// <summary>
        /// Код результата.<br/>
        /// Параметры: SUCCESS, ERROR, SERVER_ERROR, INVALID_DATA
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Данные
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Исключение
        /// </summary>
        public object Exception { get; set; }
        
        protected ActionMethodResult()
        {
        }

        protected ActionMethodResult(string code)
        {
            this.IsSuccessed = code == ActionMethodConstants.Results.Success;
            this.Code = code;
        }

        protected ActionMethodResult(string code, object data) : this(code)
        {
            this.Data = data;
        }

        protected ActionMethodResult(string code, string title, string text) : this(code)
        {
            this.Title = title;
            this.Text = text;
        }

        protected ActionMethodResult(string code, string title, string text, object data = null) : this(code, title,
            text)
        {
            this.Data = data;
        }
        
        public static ActionMethodResult Success() => new(ActionMethodConstants.Results.Success);
        public static ActionMethodResult Success(object data) => new(ActionMethodConstants.Results.Success, data);

        public static ActionMethodResult Success(object data, string text) =>
            new(ActionMethodConstants.Results.Success, "", text, data);
        

        public static ActionMethodResult Success(string title, string text, object data = null)
        {
            return new ActionMethodResult(ActionMethodConstants.Results.Success, title, text, data);
        }

        public static ActionMethodResult Error(string code, string title, string text)
        {
            return new ActionMethodResult(code, title, text);
        }

        public static ActionMethodResult Error(object exception)
        {
            return new ActionMethodResult(ActionMethodConstants.Results.Error) { Exception = exception };
        }

        public static ActionMethodResult Error(string title, string text, object data, object exception = null)
        {
            return new ActionMethodResult(ActionMethodConstants.Results.Error, title, text, data)
            {
                Exception = exception
            };
        }

        public static ActionMethodResult Error(string title, string text, object exception = null)
        {
            return new ActionMethodResult(ActionMethodConstants.Results.Error, title, text) { Exception = exception };
        }
}