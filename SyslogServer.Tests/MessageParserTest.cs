using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SyslogServer.Models;

namespace SyslogServer.Tests
{
    [TestClass]
    public class MessageParserTest
    {
        private const string Message1 = @"<191> 2015-12-10T22:14:15Z Hello World!";
        private const string Message2 = @"<14> 2015-12-10T05:14:15 Hello World!";
        private const string Message3 = @"2015-12-10T05:14:15 Hello World!";
        private const string Message4 = @"<165>1 Hello World!";
        private const string Message5 = @"Hello World!";

        [TestMethod]
        public void TestParse()
        {
            var mp = new MessageParser();
            var result = mp.Parse(Message1, IPAddress.Loopback);
            Assert.AreEqual(Facility.LocalUse7, result.Facility);
            Assert.AreEqual(Severity.Debug, result.Severity);
            Assert.AreEqual(new DateTimeOffset(2015,12,10,22,14,15,new TimeSpan(0,0,0,0)), result.DateTimeOffset);
            Assert.AreEqual("Hello World!", result.Message);

            result = mp.Parse(Message2, IPAddress.Loopback);
            Assert.AreEqual(SyslogMessage.DefaultFacility, result.Facility);
            Assert.AreEqual(SyslogMessage.DefaultSeverity, result.Severity);
            Assert.AreEqual(new DateTimeOffset(2015, 12, 10, 5, 14, 15, new TimeSpan(0, 1, 0, 0)), result.DateTimeOffset);
            Assert.AreEqual("Hello World!", result.Message);

            result = mp.Parse(Message3, IPAddress.Loopback);
            Assert.AreEqual(SyslogMessage.DefaultFacility, result.Facility);
            Assert.AreEqual(SyslogMessage.DefaultSeverity, result.Severity);
            Assert.AreEqual(new DateTimeOffset(2015, 12, 10, 5, 14, 15, new TimeSpan(0, 1, 0, 0)), result.DateTimeOffset);
            Assert.AreEqual("Hello World!", result.Message);

            result = mp.Parse(Message4, IPAddress.Loopback);
            Assert.AreEqual(Facility.LocalUse4, result.Facility);
            Assert.AreEqual(Severity.Notice, result.Severity);
            Assert.AreEqual("Hello World!", result.Message);

            result = mp.Parse(Message5, IPAddress.Loopback);
            Assert.AreEqual(SyslogMessage.DefaultFacility, result.Facility);
            Assert.AreEqual(SyslogMessage.DefaultSeverity, result.Severity);
            Assert.AreEqual("Hello World!", result.Message);

            result = mp.Parse(string.Empty, IPAddress.Loopback);
            Assert.AreEqual(SyslogMessage.DefaultFacility, result.Facility);
            Assert.AreEqual(SyslogMessage.DefaultSeverity, result.Severity);
            Assert.AreEqual(string.Empty, result.Message);

            result = mp.Parse(null, IPAddress.Loopback);
            Assert.AreEqual(SyslogMessage.DefaultFacility, result.Facility);
            Assert.AreEqual(SyslogMessage.DefaultSeverity, result.Severity);
            Assert.AreEqual(string.Empty, result.Message);

            result = mp.Parse(null, null);
            Assert.AreEqual(SyslogMessage.DefaultFacility, result.Facility);
            Assert.AreEqual(SyslogMessage.DefaultSeverity, result.Severity);
            Assert.AreEqual(string.Empty, result.Message);
        }
    }
}
