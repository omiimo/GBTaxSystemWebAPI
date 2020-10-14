using GBTaxSystemWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBTaxSystemWebAPI.Services
{
    public interface IPurchaseService
    {
        Task<PurchaseDataModel> CalculatePurchaseInfo(PurchaseDataModel purchaseData);
    } 
}
