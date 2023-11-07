using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnicalTest.Models
{
    public class ReturnModel
    {
        #region Total
        /// <summary>
        /// Total
        /// </summary>
        public decimal Total { get; set; }
        #endregion

        #region SalesTax
        /// <summary>
        /// SalesTax
        /// </summary>
        public decimal SalesTax { get; set; }
        #endregion

        #region GrandTotal
        /// <summary>
        /// GrandTotal
        /// </summary>
        public decimal GrandTotal { get; set; }
        #endregion

        #region CostCentre
        /// <summary>
        /// CostCentre
        /// </summary>
        public string CostCentre { get; set; }
        #endregion               
    }
}