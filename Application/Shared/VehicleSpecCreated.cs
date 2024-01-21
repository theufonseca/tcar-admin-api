using Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class VehicleSpecCreated
    {
        public VehicleSpec VehicleSpec { get; private set; }

        public VehicleSpecCreated(VehicleSpec vehicleSpec)
        {
            VehicleSpec = vehicleSpec;
        }
    }
}
