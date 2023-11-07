using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;
using TechnicalTest.Models;

namespace TechnicalTest.Controllers
{
    public class CalculateTaxController : ApiController
    {
        #region CalculateSalesTax
        /// <summary>
        /// CalculateSalesTax
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage CalculateSalesTax(string requestMessage)
        {
            try
            {
                decimal total = 0;
                decimal salesTax = 0;
                decimal grandTotal = 0;
                int salesTaxPercentage = 18;
                string costCentre = "";

                XmlDocument xmltest = new XmlDocument();
                xmltest.LoadXml("<body>" + requestMessage + "</body>");
                XmlNodeList nodetotal = xmltest.GetElementsByTagName("total");
                XmlNodeList nodecostcentre = xmltest.GetElementsByTagName("cost_centre");
                total = nodetotal.Count > 0 ? Convert.ToDecimal(nodetotal[0].InnerText) : 0;
                costCentre = nodecostcentre.Count > 0 ? nodecostcentre[0].InnerText : "UNKNOWN";

                if (total > 0)
                {
                    salesTax = (total * salesTaxPercentage) / 100;
                    grandTotal = total - salesTax;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Rejected");
                }

                return Request.CreateResponse(HttpStatusCode.OK, new ReturnModel { Total = total, SalesTax = salesTax, GrandTotal = grandTotal, CostCentre = nodecostcentre.Count > 0 ? costCentre : "UNKNOWN" });
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Rejected");
            }
        }
        #endregion
    }
}
