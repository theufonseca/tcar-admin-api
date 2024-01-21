using Application.Interfaces;
using Domain.Aggregate;
using Infra.Data.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class VehicleSpecRepository : IVehicleSpecRepository
    {
        private readonly IMongoCollection<VehicleSpec> _vehicleSpecsCollection;
        public VehicleSpecRepository(IOptions<VehicleSpecDatabaseSettings> vehicleSpecDatabaseSettings)
        {
            var mongoClient = new MongoClient(vehicleSpecDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(vehicleSpecDatabaseSettings.Value.DatabaseName);
            _vehicleSpecsCollection = mongoDatabase.GetCollection<VehicleSpec>(vehicleSpecDatabaseSettings.Value.CollectionName);
        }

        public async Task<List<VehicleSpec>> GetAllAsync(CancellationToken cancellationToken)
            => await _vehicleSpecsCollection.Find(_ => true).ToListAsync(cancellationToken);


        public async Task<VehicleSpec?> GetByIdAsync(string id, CancellationToken cancellationToken)
            => await _vehicleSpecsCollection.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

        public async Task UpsertAsync(VehicleSpec vehicleSpec, CancellationToken cancellationToken)
        {
            var filter = Builders<VehicleSpec>.Filter.Eq(x => x.Id, vehicleSpec.Id);
            await _vehicleSpecsCollection.ReplaceOneAsync(filter, vehicleSpec, new ReplaceOptions { IsUpsert = true }, cancellationToken);
        }
    }
}
