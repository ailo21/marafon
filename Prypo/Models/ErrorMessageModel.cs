using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Prypo.Models
{
    /// <summary>
    /// Ошибка
    /// </summary>
    public class ErrorMessage
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Информация
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// Кнопки
        /// </summary>
        public ErrorMessageBtnModel[] Buttons { get; set; }
    }
    /// <summary>
    /// Сообщение об ошибках
    /// </summary>
    public class ErrorMessageBtnModel
    {
        /// <summary>
        /// Ссылка
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Тексь
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Стиль
        /// </summary>
        [DefaultValue("default")]
        public string Style { get; set; }

        /// <summary>
        /// Действие
        /// </summary>
        public string Action { get; set; }
    }
}