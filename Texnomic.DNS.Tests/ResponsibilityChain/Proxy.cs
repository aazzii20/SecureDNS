﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PipelineNet.MiddlewareResolver;
using Texnomic.DNS.Abstractions.Enums;
using Texnomic.DNS.Models;
using Texnomic.DNS.Resolvers;
using Texnomic.DNS.ResponsibilityChain;

namespace Texnomic.DNS.Tests.ResponsibilityChain
{
    [TestClass]
    public class Proxy
    {
        private ushort ID;
        private Message RequestMessage;
        private Message ResponseMessage;


        [TestInitialize]
        public void Initialize()
        {
            ID = (ushort) new Random().Next();

            RequestMessage = new Message()
            {
                ID = ID,
                RecursionDesired = true,
                Questions = new List<Question>()
                {
                    new Question()
                    {
                        Domain = Domain.FromString("google.com"),
                        Class = RecordClass.Internet,
                        Type = RecordType.A
                    }
                }
            };
        }

        [TestMethod]
        public async Task RunAsync()
        {
            var ActivatorMiddlewareResolver = new ActivatorMiddlewareResolver();
            var ProxyResponsibilityChain = new ProxyResponsibilityChain(ActivatorMiddlewareResolver);
            ResponseMessage = await ProxyResponsibilityChain.Execute(RequestMessage);

            Assert.AreEqual(ID, ResponseMessage.ID);
            Assert.IsNotNull(ResponseMessage.Questions);
            Assert.IsNotNull(ResponseMessage.Answers);
            Assert.IsInstanceOfType(ResponseMessage.Answers.First().Record, typeof(DNS.Records.A));
        }
    }
}
