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
    public record GetVehicleSpecByIdRequest(string Id) : IRequest<GetVehicleSpecByIdResponse>;
    public record GetVehicleSpecByIdResponse(VehicleSpec VehicleSpec);

    public class GetVehicleSpecByIdRequestHandler : IRequestHandler<GetVehicleSpecByIdRequest, GetVehicleSpecByIdResponse>
    {
        private readonly IVehicleSpecRepository vehicleSpecRepository;

        public GetVehicleSpecByIdRequestHandler(IVehicleSpecRepository vehicleSpecRepository)
        {
            this.vehicleSpecRepository = vehicleSpecRepository;
        }

        public async Task<GetVehicleSpecByIdResponse> Handle(GetVehicleSpecByIdRequest request, CancellationToken cancellationToken)
        {
            var vehicleSpec = await vehicleSpecRepository.GetByIdAsync(request.Id, cancellationToken);

            if (vehicleSpec is null)
                throw new ArgumentException("Vehicle not found");

            return new GetVehicleSpecByIdResponse(vehicleSpec);
        }
    }
}
