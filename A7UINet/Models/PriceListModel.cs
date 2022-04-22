namespace A7UINet
{
    /// <summary>
    /// Наименование прайс-листа 
    /// </summary>
    public class PriceListModel: BaseModel
    {
        /// <summary>
        /// Является основным прайс-листом
        /// </summary>
        public int IsMain { get; set; }
    }
}