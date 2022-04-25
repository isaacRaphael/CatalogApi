﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Settings
{
    public class MongoDbSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }


        public string ConectionString { get
            {
                return $"mongodb://{Host}:{Port}";
            }
        }
    }
}
