using System;
using AutoRss.Models;
using Microsoft.WindowsAzure;

namespace AutoRss.Configuration
{
    public class Configuration : IConfiguration
    {
        public bool UseMockMediaRepository
        {
            get
            {
                var setting = CloudConfigurationManager.GetSetting("UseMockMediaRepository");
                bool result;
                Boolean.TryParse(setting, out result);
                return result;
            }
        }
    }
}
