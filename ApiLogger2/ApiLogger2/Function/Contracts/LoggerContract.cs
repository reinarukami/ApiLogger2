﻿using ApiLogger.Function.Default;
using ApiLogger.Function.Transaction;
using ApiLogger.Functions.Default;
using ApiLogger.Models.Default;
using ApiLogger.Models.DTO;
using ApiLogger.Models.JSON;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLogger.Function.Contracts
{
    public static class LoggerContract
    {
        private static Contract Contract;

        static LoggerContract()
        {
            Contract = DefaultContract.InitContract(DefaultConfiguration.GetContractConfig().GetSection("DefaultAccount").Value, DefaultConfiguration.GetContractConfig().GetSection("DefaultPassword").Value, DefaultConfiguration.GetContractConfig().GetSection("ContractAbi").Value, DefaultConfiguration.GetContractConfig().GetSection("ContractAddress").Value);
        }

        public static void Log(LogJSON logs)
        {
            int datetime = LogFunction.ConvertToUnixTime();
            var TaskLog = Contract.GetFunction("Log").SendTransactionAsync(DefaultConfiguration.GetContractConfig().GetSection("DefaultAccount").Value, logs.deviceID, logs.values, datetime);
            TaskLog.Wait();

            var TaskAddTransaction = Contract.GetFunction("LogTransactions").SendTransactionAsync(DefaultConfiguration.GetContractConfig().GetSection("DefaultAccount").Value,logs.deviceID, TaskLog.Result);
            TaskAddTransaction.Wait();
        }

        public static List<object> GetLogs(string _deviceID)
        {
            var transactionList = new List<string>();
            var tlist = new List<object>();

            var TaskGetCount = Contract.GetFunction("GetCount").CallAsync<int>(_deviceID, null, null);
            TaskGetCount.Wait();

            for (int i = 1; i <= TaskGetCount.Result; i++)
            {
                var TaskGetTransactions = Contract.GetFunction("GetTransactions").CallAsync<string>(_deviceID, null, null, i);
                TaskGetTransactions.Wait();
                transactionList.Add(TaskGetTransactions.Result);
            }

            var MasterEvent = Contract.GetEvent("LogEvent");

            var FilterMaster = MasterEvent.CreateFilterAsync();
            FilterMaster.Wait();

            var test = DefaultWeb3.InitializeWeb3();

            foreach (string transaction in transactionList)
            {
                var tasktest = test.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transaction);
                tasktest.Wait();

                var filterInput = MasterEvent.CreateFilterInput(new BlockParameter(tasktest.Result.BlockNumber), new BlockParameter(tasktest.Result.BlockNumber));

                var log = MasterEvent.GetAllChanges<LogDTO>(filterInput);
                log.Wait();

                tlist.Add(log.Result[0].Event);

            }

            return tlist;

        }
    }
}