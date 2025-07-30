using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class Respond
    {
        public Respond()
        {
            Message = string.Empty;
            Success = false;
        }
        public string Message { get; set; }
        public bool Success { get; set; }
        public Object? Results { get; set; }
    }
}
