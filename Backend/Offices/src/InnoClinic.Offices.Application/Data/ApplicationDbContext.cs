using InnoClinic.Offices.Application.Models;
using InnoClinic.Offices.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace InnoClinic.Offices.Application.Data;

public class ApplicationDbContext
{
    private readonly MongoClient _mongoClient;
    private readonly IMongoDatabase _database;

    public IMongoCollection<Office> Offices { get; }

    ApplicationDbContext(IOptions<MongoConfig> mongoConfig)
    {
        var config = mongoConfig.Value;
        
        _mongoClient = new MongoClient(config.ConnectionString);
        _database = _mongoClient.GetDatabase(config.Database);
        Offices = _database.GetCollection<Office>(config.Collection);
    }
}