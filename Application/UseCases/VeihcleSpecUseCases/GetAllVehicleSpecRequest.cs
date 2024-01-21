using Application.Interfaces;
using Domain.Aggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.VeihcleSpecUseCases
{
    public record GetAllVehicleSpecRequest() : IRequest<GetAllVehicleSpecResponse>;

    public record GetAllVehicleSpecResponse(IEnumerable<VehicleSpec> VehicleSpecs);

    public class GetAllVehicleSpecHandler : IRequestHandler<GetAllVehicleSpecRequest, GetAllVehicleSpecResponse>
    {
        private readonly IVehicleSpecRepository vehicleSpecRepository;

        public GetAllVehicleSpecHandler(IVehicleSpecRepository vehicleSpecRepository)
        {
            this.vehicleSpecRepository = vehicleSpecRepository;
        }

        public async Task<GetAllVehicleSpecResponse> Handle(GetAllVehicleSpecRequest request, CancellationToken cancellationToken)
        {
            var vehicleSpecs = await vehicleSpecRepository.GetAllAsync(cancellationToken);
            return new GetAllVehicleSpecResponse(vehicleSpecs);
        }
    }
}
