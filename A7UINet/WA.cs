using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A7UINet
{
    /// <summary>
    /// Основная рабочая область
    /// </summary>
    public class WA
    {
        /// <summary>
        /// Строка соединения
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// Коллекция всех наименований прайс-листов
        /// </summary>
        /// <returns></returns>
        public List<PriceListModel> GetPriceLists()
        {
            List<PriceListModel> coll = new List<PriceListModel>();
            using (var cnn = new SqlConnection(ConnectionString))
            {
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "select PRL_ID, PRL_GUID, PRL_NAME, PRL_MEMO, PRL_FLAGS from dbo.PRL_LISTS";
                    cmd.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        PriceListModel v = new PriceListModel{WA = this};
                        v.Id = reader.GetInt32(0);
                        v.Guid = reader.GetGuid(1);
                        v.Name = reader.GetString(2);
                        v.Memo = reader.IsDBNull(3) ? null : reader.GetString(3);
                        v.IsMain = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                        coll.Add(v);
                    }
                }
            }

            return coll;
        }
        /// <summary>
        /// Коллекция всех наименований видов цен
        /// </summary>
        /// <returns></returns>
        public List<PriceNameModel> GetPriceNames()
        {
            List<PriceNameModel> coll = new List<PriceNameModel>();
            using (var cnn = new SqlConnection(ConnectionString))
            {
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "SELECT PRC_ID, PRC_GUID, PRC_NAME, PRC_MEMO, CRC_ID, PRC_FORMULA, PRC_TAG, UN_ID  FROM dbo.PRC_NAMES";
                    cmd.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        PriceNameModel v = new PriceNameModel { WA = this }; 
                        v.Id = reader.GetInt32(0);
                        v.Guid = reader.GetGuid(1);
                        v.Name = reader.GetString(2);
                        v.Memo = reader.IsDBNull(3) ? null : reader.GetString(3);
                        v.CurrencyId = reader.GetInt32(4);
                        v.Formula = reader.IsDBNull(5) ? null : reader.GetString(5);
                        v.Tag = reader.IsDBNull(6) ? null : reader.GetString(6);
                        v.UnitId = reader.IsDBNull(7) ? (int?)null : reader.GetInt32(7);
                        coll.Add(v);
                    }
                }
            }

            return coll;
        }
    }
}
