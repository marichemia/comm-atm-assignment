﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atm_app.Entities
{
    internal class Transaction
    {
        public required string TransactionDate { get; set; }
        public required string TransactionType { get; set; }
        public decimal AmountGEL { get; set; }
        public decimal AmountUSD { get; set; }
        public decimal AmountEUR { get; set; }
    }
}
