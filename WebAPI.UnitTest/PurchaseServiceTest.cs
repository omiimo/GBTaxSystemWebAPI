using GBTaxSystemWebAPI.Exceptions;
using GBTaxSystemWebAPI.Models;
using GBTaxSystemWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using WebAPI.UnitTest.TestData;
using Xunit;

namespace WebAPI.UnitTest
{
    public class PurchaseServiceTest
    {
        /// <summary>
        /// Test will validate processed purchase data with supplied sample purchase data
        /// </summary>
        /// <param name="purchaseData">A Valid pre-calculated input purchase data</param>
        [Theory]
        [ValidPurchaseInfoTestData]
        public async void Should_CalculateAndComparePurchaseData_With_PreCalculatedPurchaseData(PurchaseData purchaseData)
        {
            // Arrange
            IPurchaseService purchaseService = new PurchaseService();

            // Act
            var resultGrossAmount = await purchaseService.CalculatePurchaseInfo(new PurchaseData { VATRate = purchaseData.VATRate, GrossAmount = purchaseData.GrossAmount });
            var resultVATAmount = await purchaseService.CalculatePurchaseInfo(new PurchaseData { VATRate = purchaseData.VATRate, VATAmount = purchaseData.VATAmount });
            var resultNetAmount = await purchaseService.CalculatePurchaseInfo(new PurchaseData { VATRate = purchaseData.VATRate, NetAmount = purchaseData.NetAmount });

            // Assert
            Assert.Equal(purchaseData.VATRate, resultGrossAmount.VATRate);
            Assert.Equal(purchaseData.GrossAmount, resultGrossAmount.GrossAmount);
            Assert.Equal(purchaseData.VATAmount, resultGrossAmount.VATAmount);
            Assert.Equal(purchaseData.NetAmount, resultGrossAmount.NetAmount);

            Assert.Equal(purchaseData.VATRate, resultVATAmount.VATRate);
            Assert.Equal(purchaseData.GrossAmount, resultVATAmount.GrossAmount);
            Assert.Equal(purchaseData.VATAmount, resultVATAmount.VATAmount);
            Assert.Equal(purchaseData.NetAmount, resultVATAmount.NetAmount);

            Assert.Equal(purchaseData.VATRate, resultNetAmount.VATRate);
            Assert.Equal(purchaseData.GrossAmount, resultNetAmount.GrossAmount);
            Assert.Equal(purchaseData.VATAmount, resultNetAmount.VATAmount);
            Assert.Equal(purchaseData.NetAmount, resultNetAmount.NetAmount);

        }

        /// <summary>
        /// Test should check for valid and invalid VAT rate (10, 13, 20) and compare with expectedResult.
        /// </summary>
        /// <param name="vatRate">VAT Rate</param>
        /// <param name="expectedResult">Input True if VAT Rate is a valid value (10, 13, 20) else false</param>
        [Theory]
        [InlineData(13, true)]
        [InlineData(10, true)]
        [InlineData(20, true)]
        [InlineData(50, false)]
        [InlineData(16, false)]
        public async void Should_ValidateInputVatRate_With_ExpectedResult(decimal vatRate, bool expectedResult)
        {

            // Arrange
            IPurchaseService purchaseService = new PurchaseService();

            //Act - assert
            if (expectedResult == false)
                await Assert.ThrowsAsync<ValidationException>(async () => await purchaseService.CalculatePurchaseInfo(new PurchaseData { VATRate = vatRate, GrossAmount = 100 }));
            else
            {
                var result = await purchaseService.CalculatePurchaseInfo(new PurchaseData { VATRate = vatRate, GrossAmount = 100 });
                Assert.IsType<PurchaseData>(result);
                Assert.Equal(vatRate, result.VATRate);
            }

        }

        /// <summary>
        /// Validation test for input PurchaseData.
        /// </summary>
        /// <param name="purchaseData">Sample data for validation</param>
        /// <param name="expectedResult">Expected result for the purchaseData</param>
        [Theory]
        [PurchaseInfoInputTestData]
        public async void Should_ValidateInputPurchaseDataModel_With_ExpectedResult(PurchaseData purchaseData, bool expectedResult)
        {
            //Arrange
            IPurchaseService purchaseService = new PurchaseService();

            //Act & Assert
            if (expectedResult == false)
            {
                await Assert.ThrowsAsync<ValidationException>(async () => await purchaseService.CalculatePurchaseInfo(new PurchaseData { VATRate = purchaseData.VATRate, GrossAmount = purchaseData.GrossAmount, NetAmount = purchaseData.NetAmount, VATAmount = purchaseData.VATAmount }));
            }
            else
            {
                var result = await purchaseService.CalculatePurchaseInfo(new PurchaseData { VATRate = purchaseData.VATRate, GrossAmount = purchaseData.GrossAmount, NetAmount = purchaseData.NetAmount, VATAmount = purchaseData.VATAmount });
                Assert.IsType<PurchaseData>(result);
                Assert.Equal(purchaseData.VATRate, result.VATRate);
            }

        }

    }
}
