using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;


using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using OpenQA.Selenium.Support.UI;
using System.Linq;

namespace TechTestHGR
{
    class Program
    {
        static void Main(string[] args)
        {
            // Run selenium
            ChromeDriver cd = new ChromeDriver(Environment.CurrentDirectory);
            cd.Url = @"https://app.hustlegotreal.com/Account/Login";
            cd.Navigate();
            IWebElement e = cd.FindElementById("Email");
            e.SendKeys("testing@hustlegotreal.com");
            e = cd.FindElementById("Password");
            e.SendKeys("HGR2021");
            //e = cd.FindElementByXPath(@"//*[@id=""main""]/div/div/div[2]/table/tbody/tr/td[1]/div/form/fieldset/table/tbody/tr[6]/td/button");
            e.Click();

            CookieContainer cc = new CookieContainer();

            //Get the cookies
            foreach (OpenQA.Selenium.Cookie c in cd.Manage().Cookies.AllCookies)
            {
                string name = c.Name;
                string value = c.Value;
                cc.Add(new System.Net.Cookie(name, value, c.Path, c.Domain));
            }

            //Fire off the request
            HttpWebRequest hwr = (HttpWebRequest)HttpWebRequest.Create("https://app.hustlegotreal.com/Home");
            hwr.CookieContainer = cc;
            hwr.Method = "POST";
            hwr.ContentType = "application/x-www-form-urlencoded";
            StreamWriter swr = new StreamWriter(hwr.GetRequestStream());
            //swr.Write("feeds=35");
            swr.Close();

            WebResponse wr = hwr.GetResponse();
            string s = new System.IO.StreamReader(wr.GetResponseStream()).ReadToEnd();
            Console.WriteLine(s);

        }

    }
}


