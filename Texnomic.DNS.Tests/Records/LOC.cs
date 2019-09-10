﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Texnomic.DNS.Abstractions.Enums;
using Texnomic.DNS.Models;
using Texnomic.DNS.Resolvers;

namespace Texnomic.DNS.Tests.Records
{
    [TestClass]
    public class LOC
    {
        private ushort ID;
        private IResolver Resolver;
        private Message RequestMessage;
        private Message ResponseMessage;
        

        [TestInitialize]
        public void Initialize()
        {
            ID = (ushort)new Random().Next();

            Resolver = new UDP(IPAddress.Parse("8.8.8.8"));

            RequestMessage = new Message()
            {
                ID = ID,
                RecursionDesired = true,
                Questions =new List<Question>()
                {
                    new Question()
                    {
                        Domain = Domain.FromString("geekatlas.com"),
                        Class = RecordClass.Internet,
                        Type = RecordType.LOC
                    }
                }
            };
        }

        [TestMethod]
        public async Task QueryAsync()
        {
            ResponseMessage = await Resolver.ResolveAsync(RequestMessage);

            Assert.AreEqual(ID, ResponseMessage.ID);
            Assert.IsNotNull(ResponseMessage.Questions);
            Assert.IsNotNull(ResponseMessage.Answers);
            Assert.IsInstanceOfType(ResponseMessage.Answers.First().Record, typeof(DNS.Records.LOC));
        }
    }
}
