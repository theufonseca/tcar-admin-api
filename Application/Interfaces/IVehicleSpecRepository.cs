using Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IVehicleSpecRepository
    {
        Task<List<VehicleSpec>> GetAllAsync(CancellationToken cancellationToken);
        Task<VehicleSpec?> GetByIdAsync(string id, CancellationToken cancellationToken);
        Task UpsertAsync(VehicleSpec vehicleSpec, CancellationToken cancellationToken);
    }
}
