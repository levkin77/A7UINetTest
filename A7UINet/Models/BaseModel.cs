using System;

namespace A7UINet
{
    public abstract class WAModel
    {
        public WA WA { get; set; }
    }
    public class BaseModel: WAModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Глобальный идентификатор
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Memo { get; set; }
    }
}