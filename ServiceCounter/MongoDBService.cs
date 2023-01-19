using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;

namespace ServiceCounter
{
    public class MongoDBService
    {

        private readonly IMongoCollection<ServiceCounter> _serviceCounterCollection;

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _serviceCounterCollection = database.GetCollection<ServiceCounter>(mongoDBSettings.Value.CollectionName);
        }

        public async Task<ServiceCounter> ZadnjeKlicanaStoritev()
        {
            FilterDefinition<ServiceCounter> filter = Builders<ServiceCounter>.Filter.Empty;
            var sort = Builders<ServiceCounter>.Sort.Descending("casKlica");

            var lastCalled = _serviceCounterCollection.Find(filter).Sort(sort).FirstOrDefault().ToJson();
            System.Diagnostics.Debug.WriteLine(lastCalled);

            return await _serviceCounterCollection.Find(lastCalled).SingleAsync();
        }
        public async Task<ServiceCounter> NajpogostejeKlicanaStoritev()
        {
            FilterDefinition<ServiceCounter> filter = Builders<ServiceCounter>.Filter.Empty;
            var sort = Builders<ServiceCounter>.Sort.Descending("counter");

            var mostCalled = _serviceCounterCollection.Find(filter).Sort(sort).FirstOrDefault().ToJson();
            System.Diagnostics.Debug.WriteLine(mostCalled);

            return await _serviceCounterCollection.Find(mostCalled).SingleAsync();
        }
        public async Task<ServiceCounter> SteviloKlicev(string nameKletka)
        {
            FilterDefinition<ServiceCounter> filter = Builders<ServiceCounter>.Filter.Eq("izvedenKlic", nameKletka);

            return await _serviceCounterCollection.Find(filter).SingleAsync();
        }

        public async Task<ServiceCounter> DodajKlicKletke(string nameKletka)
        {
            FilterDefinition<ServiceCounter> filter = Builders<ServiceCounter>.Filter.Eq("izvedenKlic", nameKletka);

            var timeNow = DateTime.Now;

            UpdateDefinition<ServiceCounter> updateCas = Builders<ServiceCounter>.Update.Set("casKlica", timeNow);
            UpdateDefinition<ServiceCounter> updateCounter = Builders<ServiceCounter>.Update.Inc("counter", 1);
            await _serviceCounterCollection.UpdateOneAsync(filter, updateCas);
            await _serviceCounterCollection.UpdateOneAsync(filter, updateCounter);

            return await _serviceCounterCollection.Find(filter).SingleAsync();
        }
    }
}

