using Application.Interfaces;
using Application.Shared;
using Domain.Aggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.VeihcleSpecUseCases
{
    public record UpdateVehicleSpecRequest(VehicleSpec VehicleSpec) : IRequest<UpdateVehicleSpecResponse>;
    public record UpdateVehicleSpecResponse(VehicleSpec VehicleSpec);

    public class UpdateVehicleSpecHandler : IRequestHandler<UpdateVehicleSpecRequest, UpdateVehicleSpecResponse>
    {
        private readonly IVehicleSpecRepository vehicleSpecRepository;
        private readonly IVehicleSpecPub vehicleSpecPub;

        public UpdateVehicleSpecHandler(IVehicleSpecRepository vehicleSpecRepository,
            IVehicleSpecPub vehicleSpecPub)
        {
            this.vehicleSpecRepository = vehicleSpecRepository;
            this.vehicleSpecPub = vehicleSpecPub;
        }

        async public Task<UpdateVehicleSpecResponse> Handle(UpdateVehicleSpecRequest request, CancellationToken cancellationToken)
        {
            ValidRequest(request);
            await vehicleSpecRepository.UpsertAsync(request.VehicleSpec, cancellationToken);
            await vehicleSpecPub.SendAsync(new VehicleSpecCreated(request.VehicleSpec), cancellationToken);
            return new UpdateVehicleSpecResponse(request.VehicleSpec);
        }

        private void ValidRequest(UpdateVehicleSpecRequest request)
        {
            if (request is null || !request.VehicleSpec.IsValid())
                throw new ArgumentException();
        }
    }
}
