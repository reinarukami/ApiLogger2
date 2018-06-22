using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Hex.HexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLogger.Models.DTO
{
    [FunctionOutput]
    public class LogDTO
    {

        [Parameter("address", "sender", 1)]
        public string sender { get; set; }

        [Parameter("string", "values", 2)]
        public string values { get; set; }

        [Parameter("uint256", "logdate", 3)]
        public int logdate { get; set; }

    }
}
