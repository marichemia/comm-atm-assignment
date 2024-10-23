using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atm_app.Entities
{
    internal class Card
    {
        public required string CardNumber { get; set; }
        public required string ExpDate { get; set; }
        public required string CvcCode {  get; set; }
        public required string PinCode { get; set; }
    }
}
