﻿using BinarySerialization;
using System.Text.Json.Serialization;
using Texnomic.DNS.Abstractions;
using Texnomic.DNS.Abstractions.Enums;

namespace Texnomic.DNS.Models
{
    public class Question : IQuestion
    {
        [FieldOrder(0)]
        [JsonIgnore]
        public IDomain Domain { get; set; }

        [Ignore]
        [JsonPropertyName("name")]
        public string Name
        {
            get => Domain.Name;
            set => Domain = Domain.FromString(value);
        }

        [FieldOrder(1)]
        [FieldBitLength(16)]
        [FieldEndianness(Endianness.Big)]
        [JsonPropertyName("type")]
        public RecordType Type { get; set; }

        [FieldOrder(2)]
        [FieldBitLength(16)]
        [FieldEndianness(Endianness.Big)]
        [JsonIgnore]
        public RecordClass Class { get; set; }
    }
}
