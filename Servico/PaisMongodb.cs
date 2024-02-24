using api_desafio21dias.Models;
using DnsClient.Protocol;
using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace api_desafio21dias.Servicos
{
    public class PaisMongodb
    {
        private IMongoDatabase mongoDatabase;
        private IConfiguration _configuration;

        public PaisMongodb(IConfiguration configuration)
        {
            _configuration = configuration;
            //var cnn = Program.MongoCnn!.Split('#');
            var cnn = _configuration["ConnectionStrings:MongoCnn"]!.Split('#');
            this.mongoDatabase = new MongoClient(cnn[0]).GetDatabase(cnn[1]);
        }

        private IMongoCollection<Pai> mongoCollection()
        {
            return this.mongoDatabase.GetCollection<Pai>("pais");
        }

        public async void Inserir(Pai pai) 
        {
            await mongoCollection().InsertOneAsync(pai);
        }

        public async void Atualizar(Pai pai) 
        {
            
            //var filter = Builders<Pai>.Filter.Eq(c => c.Id.Equals(pai.Id), true);
            //await mongoCollection().UpdateOneAsync(filter, new ObjectUpdateDefinition<Pai>(pai));
            var filter = Builders<Pai>.Filter.Eq(c => c.Id, pai.Id);
            await mongoCollection().UpdateOneAsync(filter, Builders<Pai>.Update.Set(p => p.Nome, pai.Nome).Set(p => p.AlunoId, pai.AlunoId));
        }

        public async void RemovePorId(ObjectId id) 
        {
            await mongoCollection().DeleteOneAsync(pai => pai.Id == id);
        }

        public async Task<IList<Pai>> Todos()
        {
            IMongoQueryable<Pai> queryAblePais = mongoCollection().AsQueryable();
            return await queryAblePais.ToListAsync();
        }

        public async Task<Pai> BuscaPorId(ObjectId id)
        {
            return await this.mongoCollection().AsQueryable().Where(pai => pai.Id.Equals(id)).FirstAsync();
        }
    }
}