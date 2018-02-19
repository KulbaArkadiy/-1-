using System;
using System.Collections.Generic;
using System.Text;

namespace IGI_lab1
{
    class CarModel
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string Specifications { get; set; }
        public decimal Cost { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
        public CarModel ()
        {
            Cars = new List<Car>();
        }
    }
}
