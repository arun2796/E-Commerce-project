using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InputParams
{
    public class CloudinarySetting
    {

        public required string CloudName { get; set; }

        public required string ApiKey { get; set; }

        public required string ApiSecret { get; set; }
    }
}
