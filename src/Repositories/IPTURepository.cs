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

        public async Task Add(IPTU iPTU) {
            dbContext.IPTUs.Add(iPTU);
            await dbContext.SaveChangesAsync();
        }        

        public async Task Update(IPTU iPTU) {
            var reg = dbContext.Update(iPTU);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IPTU> GetByKey(string chave) {
            return await dbContext.IPTUs.FirstOrDefaultAsync(w => w.Chave == chave);
        }

        public async Task<IList<IPTU>> GetByImpostoQuery(ImpostoQuery impostoQuery) {
            var data = dbContext.IPTUs.Where(w => impostoQuery.InscricaoImovel == w.InscricaoImovel && impostoQuery.DataConsulta >= w.DataVencimento);
            return await data.ToListAsync();
        }
    }
}