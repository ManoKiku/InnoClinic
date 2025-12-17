using InnoClinic.Offices.Application.Models;
using InnoClinic.Offices.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace InnoClinic.Offices.Application.Data;

public class ApplicationDbContext
{
    private readonly MongoClient _mongoClient;
    private readonly IMongoDatabase _database;

    public IMongoCollection<Office> Offices { get; }

    public ApplicationDbContext(IOptions<MongoConfig> mongoConfig)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        BsonSerializer.RegisterSerializer(new DateTimeSerializer(DateTimeKind.Utc));
        
        var config = mongoConfig.Value;
        
        _mongoClient = new MongoClient(config.ConnectionString);
        _database = _mongoClient.GetDatabase(config.Database);
        Offices = _database.GetCollection<Office>(config.Collection);
    }
}