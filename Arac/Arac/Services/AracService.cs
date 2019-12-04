using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Arac.Services
{
    public class AracService
    {
        private readonly IMongoCollection<Arac> _araclar;
        private readonly IConfiguration _configuration;

        public AracService(IConfiguration configuration)
        {
            _configuration = configuration;
            var client = new MongoClient(_configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(_configuration["DatabaseSettings:DatabaseName"]);
            _araclar = database.GetCollection<Arac>(_configuration["DatabaseSettings:CollectionName"]);
        }

        public List<Arac> Get() =>
            _araclar.Find(arac => true).ToList();

        public Arac Get(string id) =>
            _araclar.Find<Arac>(arac => arac.Id == id).FirstOrDefault();

        public Arac Create(Arac arac)
        {
            _araclar.InsertOne(arac);
            return arac;
        }

        public void Update(string id, Arac aracIn) =>
            _araclar.ReplaceOne(arac => arac.Id == id, aracIn);

        public void Remove(Arac aracIn) =>
            _araclar.DeleteOne(arac => arac.Id == aracIn.Id);

        public void Remove(string id) => 
            _araclar.DeleteOne(arac => arac.Id == id);
    }
}