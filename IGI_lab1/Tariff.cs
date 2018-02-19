using System;
using System.Collections.Generic;
using System.Text;

namespace IGI_lab1
{
    class Tariff
    {
        public int Id { get; set; }
        public string TariffName { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public virtual ICollection<Call> Calls { get; set; }
        public Tariff()
        {
            Calls = new List<Call>();
        }
    }
}
