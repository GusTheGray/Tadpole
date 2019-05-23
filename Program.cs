using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace Tadpole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get username for tadpoles
            Console.Write("User name: ");
            var user = Console.ReadLine();
            //Get password for tadpoles
            Console.Write("Password: ");
            var pass = Console.ReadLine();
            var count = 0;
            Console.Clear();

            //starts the webpage
            IWebDriver driver = new ChromeDriver(@"..\netcoreapp2.2");
            driver.Url = "https://www.tadpoles.com/home_or_work";
            
            //Log in to site
            driver.FindElement(By.XPath("//*[@id=\"app\"]/div[2]/div[2]/div[2]/div/div[1]/div/h1/span[1]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"app\"]/div[2]/div[2]/div[3]/div/div/div/div[1]/div[1]/div/div[2]/div[1]/img")).Click();
            driver.FindElement(By.XPath("//*[@id=\"app\"]/div[2]/div[2]/div[3]/div/div/div/div[1]/div[3]/div/div[1]/form/div[1]/div/input")).SendKeys(user);
            driver.FindElement(By.XPath("//*[@id=\"app\"]/div[2]/div[2]/div[3]/div/div/div/div[1]/div[3]/div/div[1]/form/div[2]/div/input")).SendKeys(pass);
            driver.FindElement(By.XPath("//*[@id=\"app\"]/div[2]/div[2]/div[3]/div/div/div/div[1]/div[3]/div/div[1]/form/div[3]/div[1]/button")).Click();

            //wait a little bit
            Thread.Sleep(1000);

            //get the list of months
            var monthList = driver.FindElement(By.XPath("//*[@id=\"app\"]/div[3]/div[1]/ul"));
            var months = monthList.FindElements(By.TagName("li"));

            foreach (var month in months)
            {
                //Select the given month
                month.Click();
                //wait for pics to load
                Thread.Sleep(5000);

                //get list of pics
                var liList = driver.FindElement(By.XPath("//*[@id=\"app\"]/div[3]/div[2]/ul"));
                var items = liList.FindElements(By.TagName("li"));

                foreach (var item in items)
                {
                   //click on the picture
                    item.Click();
                    //wait for it to load
                    Thread.Sleep(3000);
                    //look to see if the item is available to download
                    var icon = driver.FindElements(By.Id("sharetray-map"));
                    if (icon.Count>0)
                    {
                        //keep running total of pictures downloaded
                        count++;
                        //Click the download button
                        driver.FindElement(By.CssSelector("map[id='sharetray-map'] area:nth-child(3)")).Click();
                    }
                    //This is here for a slideshow effect, can remove to make process faster
                    Thread.Sleep(3000);

                    //close the pic
                    driver.FindElement(By.ClassName("btnClose")).Click();
                    Thread.Sleep(1000);
                }
            }

            driver.Close();

            Console.WriteLine(string.Format("You downloaded {0} pictures! Enjoy!",count));
        }
    }
}
