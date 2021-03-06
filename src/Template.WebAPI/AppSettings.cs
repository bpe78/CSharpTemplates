﻿using System;
using Microsoft.Extensions.Configuration;
using Template.WebAPI.Interfaces;

namespace Template.WebAPI
{
    class AppSettings : IAppSettings
    {
        private readonly IConfiguration _configuration;

        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string ServiceName { get; } = "TestService";

        public string RedisServer
        {
            get
            {
                const string key = "redis:serverAddress";
                var value = _configuration.GetValue(key, string.Empty);
                
                if (string.IsNullOrEmpty(value))
                    throw new InvalidOperationException($"Could not read setting [{key}]");

                return value;
            }
        }

        public byte RedisDbIndex
        {
            get
            {
                const string key = "redis:dbIdx";
                var value = _configuration.GetValue(key, string.Empty);
                if (string.IsNullOrWhiteSpace(value))
                    return 0;
                var dbId = byte.Parse(value);
                if (dbId > 15)
                    throw new InvalidOperationException($"[{key}] index out of bounds: {dbId}");

                return dbId;
            }
        }

        public string ConnectorUrl => _configuration.GetValue<string>("connector:Url") ?? throw new InvalidOperationException("connector url is missing");
        public string ConnectorClientId => "ABCDEFGHIJKLMNOPQRSTUVXYZ-1234567890";
        public string ConnectorUserKey => _configuration.GetValue<string>("connector:userKey") ?? throw new InvalidOperationException("UserKey is missing");

        /// <summary>
        /// This method should be called at startup, to validate all settings in a meaningful way
        /// </summary>
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(ServiceName))
                throw new ApplicationException("Invalid value for [ServiceName]");
            if(string.IsNullOrWhiteSpace(RedisServer))
                throw new ApplicationException("Invalid value for [RedisServer]");
            if ((RedisDbIndex < 0) || (RedisDbIndex > 15))
                throw new ApplicationException("Invalid value for [RedisDbIndex]");
            if (string.IsNullOrWhiteSpace(ConnectorUrl))
                throw new ApplicationException("Invalid value for [ConnectorUrl]");
            if (string.IsNullOrWhiteSpace(ConnectorClientId))
                throw new ApplicationException("Invalid value for [ConnectorClientId]");
            if (string.IsNullOrWhiteSpace(ConnectorUserKey))
                throw new ApplicationException("Invalid value for [ConnectorUserKey]");
        }
    }
}
