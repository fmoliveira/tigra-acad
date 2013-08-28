using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tigra
{
    public static class Extensions
    {
        public static bool CompareTo(this byte[] obj, byte[] buf)
        {
            if (obj.Length != buf.Length)
            {
                return false;
            }

            for (int i = 0; i < obj.Length; i++)
            {
                if (obj[i] != buf[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}