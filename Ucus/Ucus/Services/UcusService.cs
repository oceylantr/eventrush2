using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Ucus.Services
{
    public class UcusService
    {
        private readonly IMongoCollection<Ucus> _ucuslar;
        private readonly IConfiguration _configuration;

        public UcusService(IConfiguration configuration)
        {
            _configuration = configuration;
            var client = new MongoClient(_configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(_configuration["DatabaseSettings:DatabaseName"]);
            _ucuslar = database.GetCollection<Ucus>(_configuration["DatabaseSettings:CollectionName"]);
        }


        public List<Ucus> Get() =>
            _ucuslar.Find(ucus => true).ToList();

        public Ucus Get(string id) =>
            _ucuslar.Find<Ucus>(ucus => ucus.Id == id).FirstOrDefault();

        public Ucus Create(Ucus ucus)
        {
            _ucuslar.InsertOne(ucus);
            return ucus;
        }

        public void Update(string id, Ucus ucusIn) =>
            _ucuslar.ReplaceOne(ucus => ucus.Id == id, ucusIn);

        public void Remove(Ucus ucusIn) =>
            _ucuslar.DeleteOne(ucus => ucus.Id == ucusIn.Id);

        public void Remove(string id) => 
            _ucuslar.DeleteOne(ucus => ucus.Id == id);
    }
}