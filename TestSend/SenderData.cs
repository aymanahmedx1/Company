using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace TestSend
{
    internal class SenderData
    {
        public string To { get; set; }
        public string Phone { get; set; }
        public string Link { get; set; }
        public string ShopName { get; set; }
        public string InvocieTotal { get; set; }
        public string FileName { get; set; } = "فاتوره";
        public StreamContent file { get; set; }
    }
}
