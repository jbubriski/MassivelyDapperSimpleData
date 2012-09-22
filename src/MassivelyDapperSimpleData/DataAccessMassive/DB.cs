using System;
using System.Configuration;

namespace MassivelyDapperSimpleData.DataAccessMassive
{
    /// <summary>
    /// Convenience class for opening/executing data
    /// </summary>
    public static class DB
    {
        public static DynamicModel Current
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings.Count > 1)
                {
                    return new DynamicModel(ConfigurationManager.ConnectionStrings[1].Name);
                }

                throw new InvalidOperationException("Need a connection string name - can't determine what it is");
            }
        }
    }
}