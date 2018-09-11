using System;

namespace Template.WebAPI.Interfaces
{
    public interface IAppSettings
    {
        string ServiceName { get; }

        string RedisServer { get; }
        byte RedisDbIndex { get; }

        string ConnectorUrl { get; }
        string ConnectorClientId { get; }
        string ConnectorUserKey { get; }

        void Validate();
    }
}
