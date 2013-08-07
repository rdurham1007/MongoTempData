using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MongoTempDataProvider.Tests
{
    [TestClass]
    public class ProviderTests
    {
        [TestMethod]
        public void should_save_temp_data()
        {
            var provider = new MongoTempDataProvider("mongodb://localhost/TestDB");

            var data = new Dictionary<string, object>()
            {
                {"Test1", "SomeValue"}
            };

            var request = new Mock<HttpRequestBase>();
            request.Setup(r => r.UserHostAddress).Returns("SomeUniqueString");
            
            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(c => c.Request).Returns(request.Object);

            var controllerContext = new ControllerContext(httpContext.Object, new RouteData(),
                                                          new Mock<ControllerBase>().Object);
            provider.SaveTempData(controllerContext, data);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void should_retrieve_temp_data()
        {
            var provider = new MongoTempDataProvider("mongodb://localhost/TestDB");

            var request = new Mock<HttpRequestBase>();
            request.Setup(r => r.UserHostAddress).Returns("SomeUniqueString");

            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(c => c.Request).Returns(request.Object);

            var controllerContext = new ControllerContext(httpContext.Object, new RouteData(),
                                                          new Mock<ControllerBase>().Object);
            var data = provider.LoadTempData(controllerContext);

            Assert.IsTrue(true);
            
        }
    }
}
