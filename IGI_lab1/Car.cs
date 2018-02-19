using System;
using System.Collections.Generic;
using System.Text;

namespace IGI_lab1
{
    class Car
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int CarModelId { get; set; }
        public DateTime YearCreation { get; set; }
        public double Mileage{ get; set; }
        public string Driver { get; set; }
        public DateTime LastTO { get; set; }
        public string Mechanic { get; set; }
        public virtual CarModel CarModel { get; set; }
        public virtual ICollection<Call> Calls { get; set; }
        public Car()
        {
            Calls = new List<Call>();
        }
    }
}
