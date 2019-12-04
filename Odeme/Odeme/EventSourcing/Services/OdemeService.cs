using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Odeme.Services
{
    public class OdemeService
    {
        private readonly IMongoCollection<Odeme> _odemeler;
        private readonly IConfiguration _configuration;

        public OdemeService(IConfiguration configuration)
        {
            _configuration = configuration;
            var client = new MongoClient(_configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(_configuration["DatabaseSettings:DatabaseName"]);
            _odemeler = database.GetCollection<Odeme>(_configuration["DatabaseSettings:CollectionName"]);
        }

        public List<Odeme> Get() =>
            _odemeler.Find(ucus => true).ToList();

        public Odeme Get(string id) =>
            _odemeler.Find<Odeme>(ucus => ucus.Id == id).FirstOrDefault();

        public Odeme Create(Odeme ucus)
        {
            _odemeler.InsertOne(ucus);
            return ucus;
        }

        public void Update(string id, Odeme ucusIn) =>
            _odemeler.ReplaceOne(ucus => ucus.Id == id, ucusIn);

        public void Remove(Odeme ucusIn) =>
            _odemeler.DeleteOne(ucus => ucus.Id == ucusIn.Id);

        public void Remove(string id) => 
            _odemeler.DeleteOne(ucus => ucus.Id == id);
    }
}