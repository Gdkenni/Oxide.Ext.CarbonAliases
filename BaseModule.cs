﻿namespace Oxide.Ext.CarbonAliases
{
    public class BaseModule
    {
        public static T GetModule<T>()
        {
            return (T)(object)new ImageDatabaseModule();
        }
    }
}
