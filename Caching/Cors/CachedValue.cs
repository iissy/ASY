using System;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ASY.Iissy.Caching.Cors
{
    public class CachedValue<T>
    {
        protected string RefID { get; set; }

        private CachedOption cachedOption = CachedOption.None;
        protected CachedOption CachedOption
        {
            get { return cachedOption; }
            set { cachedOption = value; }
        }

        private int minute = 5;
        protected int Minute
        {
            get { return minute; }
            set { minute = value; }
        }

        private T value;
        public T Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        protected virtual T GetFromSource()
        {
            return default(T);
        }

        protected virtual T InternalGetCached()
        {
            return AbstractFactory<T>.GetInstance(this.CachedOption).InternalGetCached(this.CacheKey);
        }

        protected virtual bool InternalSetCached()
        {
            return AbstractFactory<T>.GetInstance(this.CachedOption).InternalSetCached(this.CacheKey, this.Value, this.Minute);
        }

        protected virtual string CacheKey
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "{0}-{1}", this.GetType().Name, this.RefID);
            }
        }

        protected M Parse<M>(int index, M def = default(M))
        {
            MatchCollection mc = Regex.Matches(this.CacheKey, @"\[-(.*?)-\]", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Singleline);
            int i = 1;
            string val = string.Empty;
            foreach (Match m in mc)
            {
                if (i == index)
                {
                    val = m.Groups[1].Value;
                    break;
                }
                else
                {
                    i++;
                }
            }

            if (string.IsNullOrWhiteSpace(val))
            {
                return def;
            }
            else
            {
                string str = val.ToString();
                M result = def;
                Type type = typeof(M);
                MethodInfo method = type.GetMethod("TryParse", new Type[] { typeof(String), typeof(M).MakeByRefType() });
                switch (type.ToString())
                {
                    case "System.Single":
                    case "System.Double":
                    case "System.Decimal":
                    case "System.Int32":
                    case "System.Int64":
                    case "System.DateTime":
                    case "System.Boolean":
                        object[] param = new object[2];
                        param[0] = str;
                        object item = method.Invoke(str, param);
                        if ((bool)item)
                        {
                            result = (M)param[1];
                        }
                        break;
                    default:
                        method = type.GetMethod("ToString", new Type[] { });
                        result = (M)method.Invoke(str, null);
                        break;
                }

                return result;
            }
        }
    }
}