using StackExchange.Redis;
using System;

namespace DockerRedisExample.Redis
{
    public class RedisHelper
    {
        static RedisHelper()
        {
            _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect("localhost:6379");
            });
        }

        private static readonly Lazy<ConnectionMultiplexer> _lazyConnection;

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return _lazyConnection.Value;
            }
        }
    }
}