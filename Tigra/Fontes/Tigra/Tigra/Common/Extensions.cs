using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Tigra.Database;

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

        /// <summary>
        /// Get display name of a given user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetDisplayName(this UserAccount user)
        {
            if (user.UserProfile != null && user.UserProfile.FullName != null && user.UserProfile.FullName.Trim().Length != 0)
            {
                return user.UserProfile.FullName;
            }
            else
            {
                return user.Email;
            }
        }

        /// <summary>
        /// Get cell from its tag.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static Cell GetCell(this Entities ctx, object cell)
        {
            if (cell != null)
            {
                string s = cell.ToString();
                return (from i in ctx.Cells where i.Tag == s select i).FirstOrDefault();
            }
            return null;
        }

        /// <summary>
        /// Get cell ID from its tag.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static int GetCellID(this Entities ctx, object cell)
        {
            Cell item = ctx.GetCell(cell);
            if (item != null)
            {
                return item.CellID;
            }
            return 0;
        }

        /// <summary>
        /// Get cell ID from its tag.
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static Cell GetCell(this object cell)
        {
            using (var ctx = new Entities())
            {
                return ctx.GetCell(cell);
            }
        }

        /// <summary>
        /// Get cell ID from its tag.
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static int GetCellID(this object cell)
        {
            using (var ctx = new Entities())
            {
                return ctx.GetCellID(cell);
            }
        }
    }
}