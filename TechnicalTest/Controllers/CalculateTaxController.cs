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
        #region CalculatedSalesTaxGrandTotal
        /// <summary>
        /// Calculate Sales Tax Grand Total
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage CalculatedSalesTaxGrandTotal(RequestModel objRequest)
        {
            try
            {
                int salesTaxPercentage = 18;
                ReturnModel objReturnModel = new ReturnModel();

                //Convert the input text to XML document
                XmlDocument xmltest = new XmlDocument();
                xmltest.LoadXml("<body>" + objRequest.Request + "</body>");

                //Extract Tag using Node List
                XmlNodeList nodetotal = xmltest.GetElementsByTagName("total");
                XmlNodeList nodecostcentre = xmltest.GetElementsByTagName("cost_centre");

                objReturnModel.Total = nodetotal.Count > 0 ? Convert.ToDecimal(nodetotal[0].InnerText) : 0;
                objReturnModel.CostCentre = nodecostcentre.Count > 0 ? nodecostcentre[0].InnerText : "UNKNOWN";

                if (objReturnModel.Total > 0)
                {
                    objReturnModel.SalesTax = (objReturnModel.Total * salesTaxPercentage) / 100;
                    objReturnModel.GrandTotal = objReturnModel.Total - objReturnModel.SalesTax;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Rejected");
                }

                return Request.CreateResponse(HttpStatusCode.OK, objReturnModel);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Rejected");
            }
        }
        #endregion
    }
}
