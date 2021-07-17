using Microsoft.VisualStudio.TestTools.UnitTesting;
using YJ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YJ.Utility.Tests
{
    [TestClass()]
    public class StrHelperTests
    {
        [TestMethod()]
        public void toNumTest()
        {
            var ret = "2.5k".ToNumUnit();
            Console.WriteLine("{0}  {1}",ret.Num, ret.Unit);
            ret = "y y y 2.5kg".ToNumUnit();
            Console.WriteLine("{0} {1}", ret.Num, ret.Unit);
            ret = "约等于200包".ToNumUnit();
            Console.WriteLine("{0} {1}", ret.Num, ret.Unit);
            ret = "约等于八万零六十四包".ToNumUnit();
            Console.WriteLine("{0} {1}", ret.Num, ret.Unit);
            ret = "约等于一亿八千万零六十四".ToNumUnit();
            Console.WriteLine("{0} {1}", (int)ret.Num, ret.Unit);
        }

        [TestMethod()]
        public void toDateTest()
        {
            Console.WriteLine("19.08.28".ToDateTimeOrNull());
            //Assert.Fail();
            var ret = "保质期至：19.08.28".ToExpireDate(DateTime.Now);
            Console.WriteLine(ret.ToString());

            ret = "1年1个月".ToExpireDate(DateTime.Now);
            Console.WriteLine(ret.ToString());
            ret = "十二个月".ToExpireDate(DateTime.Now);
            Console.WriteLine(ret.ToString());

            ret = "1年".ToExpireDate(DateTime.Now);
            Console.WriteLine(ret.ToString());
        }
    }
}