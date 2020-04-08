using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Api.Interfaces;
using System;

namespace Api.Services
{
    public class Vote
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
