﻿using BinarySerialization;
using Texnomic.DNS.Abstractions;

namespace Texnomic.DNS.Records
{
    public class CName : IRecord
    {
        [FieldOrder(0)]
        public IDomain Domain { get; set; }
    }
}
