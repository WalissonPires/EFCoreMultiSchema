using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreMultiSchema
{
    public class ConnectionStringBuild
    {
        private readonly Dictionary<string, string> _connectionParams;


        public ConnectionStringBuild(string connectionString)
        {
            _connectionParams = new Dictionary<string, string>();

            ParseConnectionString(connectionString);
        }


        public string GetConnectionString()
        {
            return String.Join(";", _connectionParams.Select(x => x.Key + "=" + x.Value));
        }

        public ConnectionStringBuild SetParam(string param, string value)
        {
            if (_connectionParams.ContainsKey(param))
                _connectionParams[param] = value;
            else
                _connectionParams.Add(param, value);

            return this;
        }

        public string GetParam(string param)
        {
            if (_connectionParams.ContainsKey(param))
                return _connectionParams[param];

            return null;
        }


        private void ParseConnectionString(string connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException(nameof(connectionString));

            var connSplited = connectionString.Split(";", StringSplitOptions.RemoveEmptyEntries);
            foreach(var pair in connSplited)
            {
                var pairSplited = pair.Split("=");
                if (pairSplited.Length != 2)
                    throw new ArgumentException("Badly formatted connection string");

                _connectionParams.Add(pairSplited[0], pairSplited[1]);
            };
        }
    }
}
