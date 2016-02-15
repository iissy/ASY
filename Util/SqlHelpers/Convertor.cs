using System;
using System.Xml.Linq;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace ASY.Iissy.Util.SqlHelpers
{
    internal static class Convertor
    {
        public static XElement DataReaderToXml(IDataReader dr, int colNum)
        {
            XElement tableXml = new XElement("Table");
            while (dr.Read())
            {
                XElement rowXml = new XElement("Row");
                for (int i = 0; i < colNum; i++)
                {
                    Type type = dr.GetFieldType(i);
                    string name = dr.GetName(i);
                    string value = string.Empty;
                    object obj = dr.GetValue(i);
                    if (obj == DBNull.Value)
                    {
                        continue;
                    }
                    switch (type.ToString())
                    {
                        case "System.Boolean":
                            value = obj.ToString().ToLowerInvariant();
                            break;
                        case "System.Single":
                            value = ((float)obj).ToString(CultureInfo.InvariantCulture);
                            break;
                        case "System.Double":
                            value = ((double)obj).ToString(CultureInfo.InvariantCulture);
                            break;
                        case "System.Decimal":
                            value = ((decimal)obj).ToString(CultureInfo.InvariantCulture);
                            break;
                        case "System.Int16":
                            value = ((short)obj).ToString(CultureInfo.InvariantCulture);
                            break;
                        case "System.Int32":
                            value = ((int)obj).ToString(CultureInfo.InvariantCulture);
                            break;
                        case "System.Int64":
                            value = ((long)obj).ToString(CultureInfo.InvariantCulture);
                            break;
                        case "System.DateTime":
                            value = ((DateTime)obj).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                            break;
                        default:
                            value = obj.ToString();
                            break;
                    }

                    rowXml.Add(new XElement(name, value));
                }

                tableXml.Add(rowXml);
            }

            return tableXml;
        }

        public static T DataToEntity<T>(IDataReader dr) where T : new()
        {
            T item = new T();
            PropertyInfo[] properties = item.GetType().GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    if (prop.Name.ToLowerInvariant() == dr.GetName(i).ToLowerInvariant())
                    {
                        if (dr[prop.Name] == DBNull.Value)
                            prop.SetValue(item, null);
                        else if (prop.PropertyType.Name == "String" && dr.GetFieldType(i).Name == "DateTime")
                        {
                            prop.SetValue(item, ((DateTime)dr[prop.Name]).ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        else
                        {
                            prop.SetValue(item, dr[prop.Name]);
                        }
                        continue;
                    }
                }
            }

            return item;
        }

        public static T DataToEntity<T>(DataRow row) where T : new()
        {
            T item = new T();
            PropertyInfo[] properties = item.GetType().GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                if (row.Table.Columns.Contains(prop.Name))
                {
                    if (row[prop.Name] == DBNull.Value)
                        prop.SetValue(item, null);
                    else if (prop.PropertyType.Name == "String" && row.Table.Columns[prop.Name].DataType.Name == "DateTime")
                    {
                        prop.SetValue(item, ((DateTime)row[prop.Name]).ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    else
                        prop.SetValue(item, row[prop.Name]);
                    continue;
                }
            }

            return item;
        }
    }
}