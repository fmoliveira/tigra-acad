using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tigra
{
    /// <summary>
    /// Helper extensions.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Compare two arrays of bytes.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="buf"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the most inner exception of a given exception, so we have the most detailed error.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static Exception GetInnerstException(this Exception exception)
        {
            Exception e = exception;

            while (e.InnerException != null)
            {
                e = e.InnerException;
            }

            return e;
        }
    }
}