using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StudentAdvertsTests
{
    [TestFixture]
    public class SeleniumTest
    {
        private IWebDriver driver;
        private string URL;

        [TearDown]
        public void tearDownTests()
        {
            driver.Close();
        }

        [SetUp]
        public void setUpTests()
        { 
            URL = "http://localhost:4200/";
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        [Test]
        public void searchTest_GoogleConfirmed()
        {
            driver.Navigate().GoToUrl(URL);
            Assert.AreSame("http://localhost:4200/", URL);
        }

        [Test]
        public void register()
        {
            driver.Navigate().GoToUrl(URL + "/register");
            var email = driver.FindElement(By.XPath("//*[@id='inputEmail']"));
            email.SendKeys("testowyemail124@zupa.com");
            var password = driver.FindElement(By.XPath("//*[@id='inputPassword']"));
            password.SendKeys("zaq1@WSX");
            var repassword = driver.FindElement(By.XPath("//*[@id='inputConfirmPassword']"));
            repassword.SendKeys("zaq1@WSX");

            repassword.Submit();
            System.Threading.Thread.Sleep(5000);
            Assert.That(driver.Url, Is.EqualTo("http://localhost:4200/home"));
        }

        [Test]
        public void login()
        {
            driver.Navigate().GoToUrl(URL + "/logging");
            var email = driver.FindElement(By.XPath("//*[@id='inputEmail']"));
            email.SendKeys("testowyemail124@zupa.com");
            var password = driver.FindElement(By.XPath("//*[@id='inputPassword']"));
            password.SendKeys("zaq1@WSX");

            password.Submit();
            System.Threading.Thread.Sleep(5000);
            Assert.That(driver.Url, Is.EqualTo("http://localhost:4200/home"));
        }


        [Test]
        public void addAdvert()
        {
            driver.Navigate().GoToUrl(URL + "/logging");
            var email = driver.FindElement(By.XPath("//*[@id='inputEmail']"));
            email.SendKeys("testowyemail124@zupa.com");
            var password = driver.FindElement(By.XPath("//*[@id='inputPassword']"));
            password.SendKeys("zaq1@WSX");

            password.Submit();
            System.Threading.Thread.Sleep(2000);

            driver.Navigate().GoToUrl(URL + "/add");
            var title = driver.FindElement(By.XPath("//*[@id='title']"));
            title.SendKeys("TestoweOgloszenie Selenium 2");
            var email2 = driver.FindElement(By.XPath("//*[@id='email']"));
            email2.SendKeys("testowyemail123@zupa.com");
            var number = driver.FindElement(By.XPath("//*[@id='phone']"));
            number.SendKeys("123456789");
            var desc = driver.FindElement(By.XPath("//*[@id='description']"));
            desc.SendKeys("Description test");
            var price = driver.FindElement(By.XPath("//*[@id='price']"));
            price.SendKeys("2137");

            price.Submit();
        }
    }
}
