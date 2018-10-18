using System.Web.Http.Results;
using ConversionService.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConversionService.Test
{
    [TestClass]
    public class NumberToWordTest
    {
        [TestMethod]
        public void TestWithZero()
        {
            var controller = new NumberToWordsController();

            var result = controller.GetNumberToWord(0);
            var contentResult = result as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(contentResult.Content, "ZERO");
        }
        [TestMethod]
        public void TestWithDecimalData()
        {
            var controller = new NumberToWordsController();

            var result = controller.GetNumberToWord((decimal)123.45);
            var contentResult = result as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(contentResult.Content, "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS");
        }

        [TestMethod]
        public void TestWithoutDecimalData()
        {
            var controller = new NumberToWordsController();

            var result = controller.GetNumberToWord(100);
            var contentResult = result as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(contentResult.Content, "ONE HUNDRED DOLLARS");
        }

        [TestMethod]
        public void TestWithInvalidNumber()
        {
            var controller = new NumberToWordsController();

            var result = controller.GetNumberToWord(-100);
            var contentResult = result as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(contentResult.Content, "INVALID NUMBER");
        }
        
        [TestMethod]
        public void TestWithZeroDecimal()
        {
            var controller = new NumberToWordsController();

            var result = controller.GetNumberToWord((decimal)0.87);
            var contentResult = result as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(contentResult.Content, "ZERO DOLLARS AND EIGHTY-SEVEN CENTS");
        }

        [TestMethod]
        public void TestWithBillion()
        {
            var controller = new NumberToWordsController();

            var result = controller.GetNumberToWord(23000000);
            var contentResult = result as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(contentResult.Content, "TWO BILLION THREE MILLION DOLLARS");
        }

        [TestMethod]
        public void TestWithMillion()
        {
            var controller = new NumberToWordsController();

            var result = controller.GetNumberToWord(4760000);
            var contentResult = result as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(contentResult.Content, "FOUR MILLION SEVEN HUNDRED AND SIXTY THOUSAND DOLLARS");
        }

        [TestMethod]
        public void TestWithThousands()
        {
            var controller = new NumberToWordsController();

            var result = controller.GetNumberToWord(960000);
            var contentResult = result as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(contentResult.Content, "NINE HUNDRED AND SIXTY THOUSAND DOLLARS");
        }
    }
}
