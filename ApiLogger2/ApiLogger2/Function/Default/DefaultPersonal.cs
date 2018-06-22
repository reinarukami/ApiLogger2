using ApiLogger.Functions.Default;
using Nethereum.RPC.Eth;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLogger.Function.Default
{
    public static class DefaultPersonal
    {

        public static string CreateAccount(string password)
        {
            var taskCreateAccount =  DefaultWeb3.InitializeWeb3().Personal.NewAccount.SendRequestAsync(password);
            taskCreateAccount.Wait();

            return taskCreateAccount.Result;
        }

    }
}
