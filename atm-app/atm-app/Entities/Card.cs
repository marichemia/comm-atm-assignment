using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atm_app.Entities
{
    internal class Card
    {
        public string CardNumber { get; set; }
        public string ExpDate { get; set; }
        public string CvcCode {  get; set; }
        public string PinCode { get; set; }
    }
}
