using System;
using System.Collections.Generic;

namespace bme280_viewer.Models
{
    public partial class Bme280
    {
        public int Id { get; set; }
        public decimal Temperature { get; set; }
        public decimal Pressure { get; set; }
        public decimal Hum { get; set; }
        public DateTime Created { get; set; }
    }
}
