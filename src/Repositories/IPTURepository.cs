using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModCidadao.Models;

namespace ModCidadao.Repositories {
    public class IPTURepository {
        private readonly ModCidadaoDbContext dbContext;
        public IPTURepository(ModCidadaoDbContext dbContext)
        {            
            this.dbContext = dbContext;
        }

        public void Add(IPTU iPTU) {
            dbContext.IPTUs.Add(iPTU);
            dbContext.SaveChanges();
        }        

        public void Update(IPTU iPTU) {
            var reg = dbContext.Update(iPTU);
            dbContext.SaveChanges();
        }

        public IPTU GetByKey(string chave) {
            return dbContext.IPTUs.FirstOrDefault(w => w.Chave == chave);
        }

        public async Task<IList<IPTU>> GetByImpostoQuery(ImpostoQuery impostoQuery) {
            var data = dbContext.IPTUs.Where(w => impostoQuery.InscricaoImovel == w.InscricaoImovel && impostoQuery.DataConsulta >= w.DataVencimento);
            return await data.ToListAsync();
        }
    }
}