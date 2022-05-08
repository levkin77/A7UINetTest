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

        public List<SelectorModel> GetPriceListSelector()
        {
            List<SelectorModel> coll = new List<SelectorModel>();
            using (var cnn = new SqlConnection(ConnectionString))
            {
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "select PRL_ID, PRL_NAME from dbo.PRL_LISTS order by PRL_FLAGS desc, PRL_NAME;select n.PRC_ID, n.PRC_NAME, c.PRL_ID from dbo.PRL_PRICES c inner join dbo.PRC_NAMES n on n.PRC_ID = c.PRC_ID order by c.PRL_ID, c.PRC_NO DESC; ";
                    cmd.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        SelectorModel v = new SelectorModel();
                        v.Id = reader.GetInt32(0);
                        v.Name = reader.GetString(1);
                        v.Kind = 0;
                        coll.Add(v);
                    }
                    reader.NextResult();
                    while (reader.Read())
                    {
                        SelectorModel v= new SelectorModel();
                        v.Id = reader.GetInt32(0);
                        v.Name = reader.GetString(1);
                        v.Kind = 1;
                        int pid = reader.GetInt32(2);
                        var parent = coll.FirstOrDefault(s => s.Id == pid);
                        if (parent != null)
                        {
                            v.Parent = parent;
                            parent.Elements.Add(v);
                        }
                    }
                }
            }

            return coll;
        }

        public static string GetParamFactKind2(int basekind, int? refKind=null)
        {
            if(refKind.HasValue && refKind!=0)
            {
                switch (refKind)
                {
                    case 1: return "Папка документов";
                    case 2: return "Счет";
                    case 3: return "Корреспондент";
                    case 4: return "Товар";
                    case 5: return "Аналитика";
                    case 6: return "Подшивка";
                    case 7: return "Шаблон";
                    case 15: return "Форма";
                    case 18: return "Перечисление";
                    case 20: return "Автонумерация";
                    case 27: return "Вид цены";
                    case 28: return "Единица измерения";
                    case 29: return "Валюта";
                    case 30: return "Прайс лист";
                    default:
                        return refKind.ToString();
                }
            }
            else
            {
                switch (basekind)
                {
                    case 3: return "Целое";
                    case 5: return "Вещественное";
                    case 6: return "Денежное";
                    case 7: return "Дата";
                    case 8: return "Строка";
                    default:
                        break;
                }
            }

            return string.Empty;
        }
        public static string GetParamFactKind(object baseKindObj, object refKindObj = null)
        {
            return GetParamFactKind2((int)baseKindObj, (int?)refKindObj);
        }
    }
}
