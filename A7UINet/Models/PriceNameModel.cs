namespace A7UINet
{
    /// <summary>
    /// Наименование вида цены
    /// </summary>
    public class PriceNameModel : BaseModel
    {
        /// <summary>
        /// Идентификатор валюты 
        /// </summary>
        public int CurrencyId { get; set; }
        /// <summary>
        /// Формула
        /// </summary>
        public string Formula { get; set; }
        /// <summary>
        /// Признак
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// Идентификатор единицы измерения
        /// </summary>
        public int? UnitId { get; set; }
    }
}