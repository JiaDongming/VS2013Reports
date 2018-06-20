using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public static class Util
    {

        public static List<int> ConvertToIntList(string str)
        {
            List<int> ids = new List<int>();
            if (string.IsNullOrWhiteSpace(str))
                return ids;

            string[] p = str.Split(',');
            foreach (string token in p)
            {
                if (string.IsNullOrWhiteSpace(token))
                    continue;
                ids.Add(token.ToInt32());
            }
            return ids;
        }

        public static string ConvertToStringList(List<int> ids)
        {
            if (ids == null || ids.Count == 0)
                return null;
            StringBuilder sb = new StringBuilder();
            foreach (int id in ids)
            {
                sb.Append(id).Append(",");
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}
