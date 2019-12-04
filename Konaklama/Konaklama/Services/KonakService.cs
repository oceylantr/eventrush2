using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Konak.Services
{
    public class KonakService
    {
        private readonly IMongoCollection<Konak> _konaklar;
        private readonly IConfiguration _configuration;

        public KonakService(IConfiguration configuration)
        {
            _configuration = configuration;
            var client = new MongoClient(_configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(_configuration["DatabaseSettings:DatabaseName"]);
            _konaklar = database.GetCollection<Konak>(_configuration["DatabaseSettings:CollectionName"]);
        }

        public List<Konak> Get() =>
            _konaklar.Find(konak => true).ToList();

        public Konak Get(string id) =>
            _konaklar.Find<Konak>(konak => konak.Id == id).FirstOrDefault();

        public Konak Create(Konak konak)
        {
            _konaklar.InsertOne(konak);
            return konak;
        }

        public void Update(string id, Konak konakIn) =>
            _konaklar.ReplaceOne(konak => konak.Id == id, konakIn);

        public void Remove(Konak konakIn) =>
            _konaklar.DeleteOne(konak => konak.Id == konakIn.Id);

        public void Remove(string id) => 
            _konaklar.DeleteOne(konak => konak.Id == id);
    }
}