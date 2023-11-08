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
        #region GetCalculatedSalesTaxGrandTotal
        /// <summary>
        /// Get Calculated Sales Tax Grand Total value
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetCalculatedSalesTaxGrandTotal(string requestMessage)
        {
            try
            {
                int salesTaxPercentage = 18;
                ReturnModel objReturnModel = new ReturnModel();

                XmlDocument xmltest = new XmlDocument();
                xmltest.LoadXml("<body>" + requestMessage + "</body>"); //Convert the input text to XML document

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
