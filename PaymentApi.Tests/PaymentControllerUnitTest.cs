using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PaymentApi.Controllers;
using PaymentApi.Models;
using PaymentApi.Services;
using System;
using System.Threading.Tasks;

namespace PaymentApi.Tests
{
    public class PaymentControllerUnitTest
    {
        private Mock<IPaymentService> _paymentService;
        private PaymentViewModel paymentData;

        [SetUp]
        public void Setup()
        {
            _paymentService = new Mock<IPaymentService>();
            paymentData = new PaymentViewModel { Amount = 1000.00M, CardHolder = "Firstname Surname", CreditCardNumber = "1234567890123456", ExpirationDate = DateTime.Now.AddDays(1), SecurityCode = "123" };
        }

        [Test]
        public async Task ProcessPayment_ReturnsAnActionResult()
        {
            // Arrange
            var controller = new PaymentController(_paymentService.Object);

            // Act
            var result = await controller.ProcessPayment(paymentData);
            var objResult = result as ObjectResult;
            // Assert
            Assert.AreEqual(200, objResult.StatusCode);
        }
    }
}