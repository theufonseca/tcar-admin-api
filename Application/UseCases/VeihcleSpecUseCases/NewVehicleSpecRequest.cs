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
    public record NewVehicleSpecRequest(VehicleSpec VehicleSpec) : IRequest<NewVehicleSpecResponse>;

    public record NewVehicleSpecResponse(string Id);

    public class NewVehicleSpecHandler : IRequestHandler<NewVehicleSpecRequest, NewVehicleSpecResponse>
    {
        private readonly IVehicleSpecRepository vehicleSpecRepository;
        private readonly IVehicleSpecPub vehicleSpecPub;

        public NewVehicleSpecHandler(IVehicleSpecRepository vehicleSpecRepository,
            IVehicleSpecPub vehicleSpecPub)
        {
            this.vehicleSpecRepository = vehicleSpecRepository;
            this.vehicleSpecPub = vehicleSpecPub;
        }

        public async Task<NewVehicleSpecResponse> Handle(NewVehicleSpecRequest request, CancellationToken cancellationToken)
        {
            ValidRequest(request);
            await vehicleSpecRepository.UpsertAsync(request.VehicleSpec, cancellationToken);
            await vehicleSpecPub.SendAsync(new VehicleSpecCreated(request.VehicleSpec), cancellationToken);
            return new NewVehicleSpecResponse(request.VehicleSpec.Id);
        }

        private void ValidRequest(NewVehicleSpecRequest request)
        {
            if (request is null || !request.VehicleSpec.IsValid())
                throw new ArgumentException();
        }
    }
}
