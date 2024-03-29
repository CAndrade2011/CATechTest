﻿using MongoDB.Bson;
using MongoDB.Driver;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace Infra.DataFromMongo.Repository;

public class BaseContext : IDisposable
{
    private const string DATABASE_NAME = "ca-tech-test-db";
    protected readonly IMongoDatabase _database;
    protected const bool SUCCESS_RETURN = true;
    protected const bool ERROR_RETURN = false;

    public BaseContext(){}

    protected BaseContext(Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
        var settings = MongoClientSettings.FromConnectionString(configuration["ConnectionStrings:MongoDB"]);
        settings.Credential = MongoCredential.CreateCredential("admin", "root", "SenhaAdmin2024!");
        var client = new MongoClient(settings);
        _database = client.GetDatabase(DATABASE_NAME);
    }

    protected IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }

    protected static ObjectId ToDbId(string value)
    {
        return new ObjectId(value);
    }

    protected static string ToSystemId(ObjectId value)
    {
        return value.ToString();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
