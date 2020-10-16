using GBTaxSystemWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBTaxSystemWebAPI.Services
{
    public interface IPurchaseService
    {
        /// <summary>
        /// Calculates net, gross and VAT amount
        /// </summary>
        /// <param name="purchaseData">Input Purchase data</param>
        /// <returns>Calculated purchase data</returns>
        Task<PurchaseDataModel> CalculatePurchaseInfo(PurchaseDataModel purchaseData);
    } 
}
