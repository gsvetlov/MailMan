using MailMan.Services.MailSenderService;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailMan.Lib.Tests
{
    [TestClass]
    public class MailManSimpleSmtpServiceTest
    {
        [TestMethod]
        public void Constructor_Returns_Singleton()
        {
            var serviceInstance = SimpleSmtpService.GetService();
            var otherServiceInstance = SimpleSmtpService.GetService();

            Assert.IsNotNull(serviceInstance, $"{nameof(serviceInstance)} is null");
            Assert.IsNotNull(otherServiceInstance, $"{nameof(otherServiceInstance)} is null");
            Assert.AreSame(serviceInstance, otherServiceInstance);
        }
    }
}
