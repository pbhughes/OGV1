using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Collections;

namespace OPGOV.AgendaServices
{
    public class CompareByFileDate: IComparer
    {


        #region IComparer Members
        /// <summary>
        /// Current set to sort descending
        /// </summary>
        /// <param name="x">First file info object</param>
        /// <param name="y">Next file info object</param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            FileInfo a = new FileInfo((string)x);
            FileInfo b = new FileInfo((string)y);

            DateTime aDte = a.CreationTime;
            DateTime bDte = b.CreationTime;

            return DateTime.Compare(aDte, bDte) * -1;
        }

        #endregion
    }
}
