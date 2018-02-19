using System;
using System.Collections.Generic;
using System.Text;

namespace IGI_lab1
{
    class Call
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public string Route  { get; set; }
        public int TariffId { get; set; }
        public int CarId { get; set; }
        public string Dispatcher { get; set; }
        public virtual Car Car { get; set; }
        public virtual Tariff Tariff { get; set; }
    }
}
