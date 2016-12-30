using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace Multi_Language.DataApi.Tasks
{
    public static class ProcessorAndRamUsageTask
    {
        static PerformanceCounter _cpuCounter, _memUsageCounter;

        public static void CallWebApi()
        {
            // Hello!
            Console.WriteLine("CPU Info Client: Reporting your CPU usage today!");

            try
            {
                _cpuCounter = new PerformanceCounter();
                _cpuCounter.CategoryName = "Processor";
                _cpuCounter.CounterName = "% Processor Time";
                _cpuCounter.InstanceName = "_Total";

                _memUsageCounter = new PerformanceCounter("Memory", "Available KBytes");

                // Create a new thread to start polling and sending the data
                RunPollingThread();

                Console.WriteLine("Press a key to stop and exit");

                Console.WriteLine("Stopping thread..");


            }
            catch (Exception)
            {
                throw;
            }
        }

        static void RunPollingThread()
        {

            double cpuTime;
            ulong memUsage, totalMemory;

            // Get the stuff we need to send
            GetMetrics(out cpuTime, out memUsage, out totalMemory);

            // Send the data
            var postData = new
            {
                MachineName = System.Environment.MachineName,
                Processor = cpuTime,
                MemUsage = memUsage,
                TotalMemory = totalMemory
            };

            var json = JsonConvert.SerializeObject(postData);
            // Post the data to the server
            var serverUrl = new Uri(ConfigurationManager.AppSettings["ServerUrl"]);

            var client = new WebClient();
            client.Headers.Add("Content-Type", "application/json");
            client.UploadString(serverUrl, json);

        }


        static void GetMetrics(out double processorTime, out ulong memUsage, out ulong totalMemory)
        {
            processorTime = (double)_cpuCounter.NextValue();
            memUsage = (ulong)_memUsageCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            processorTime = (double)_cpuCounter.NextValue();
            memUsage = (ulong)_memUsageCounter.NextValue();

            totalMemory = 0;

            // Get total memory from WMI
            ObjectQuery memQuery = new ObjectQuery("SELECT * FROM CIM_OperatingSystem");

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(memQuery);

            foreach (ManagementObject item in searcher.Get())
            {
                totalMemory = (ulong)item["TotalVisibleMemorySize"];
            }
        }

    }
}