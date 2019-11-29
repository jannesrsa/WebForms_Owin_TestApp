using SourceCode.Hosting.Client.BaseAPI;
using System;

namespace WebForms_Owin_TestApp.Helpers
{
    public static class ConnectionHelper
    {
        static ConnectionHelper()
        {
            SCConnectionStringBuilder.Host = Environment.MachineName;
            SCConnectionStringBuilder.Port = 5555;
            SCConnectionStringBuilder.Integrated = true;
            SCConnectionStringBuilder.IsPrimaryLogin = true;
        }

        internal static SCConnectionStringBuilder SCConnectionStringBuilder { get; } = new SCConnectionStringBuilder();

        internal static T GetServer<T>() where T : BaseAPI, new()
        {
            T server = new T();

            server.CreateConnection();
            server.Connection.Open(SCConnectionStringBuilder.ConnectionString);

            return server;
        }
    }
}