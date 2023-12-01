namespace EducationHub.Web.Tests
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Remote;

    using Xunit;

    public class SeleniumTests : IClassFixture<SeleniumServerFactory<Program>>
    {
        private readonly SeleniumServerFactory<Program> server;
        private readonly IWebDriver browser;

        public SeleniumTests(SeleniumServerFactory<Program> server)
        {
            this.server = server;
            server.CreateClient();
            var opts = new ChromeOptions();
            opts.AddArguments("--headless", "--ignore-certificate-errors");
            this.browser = new RemoteWebDriver(opts);
        }

        [Fact(Skip = "Example test. Disabled for CI.")]
        public void FooterOfThePageContainsPrivacyLink()
        {
            this.browser.Navigate().GoToUrl(this.server.RootUri);
            Assert.Contains(
                this.browser.FindElements(By.CssSelector("footer a")),
                x => x.GetAttribute("href").EndsWith("/Home/Privacy"));
        }
    }
}
