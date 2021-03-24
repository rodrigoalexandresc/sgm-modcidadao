using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<IList<IPTU>> GetByImpostoQuery(ImpostoQuery impostoQuery) {
            try
            {
                var data = dbContext.IPTUs.Where(w => 
                    ( !string.IsNullOrEmpty(impostoQuery.CPFouCNPJ) ? impostoQuery.CPFouCNPJ == w.CPFOuCNPJ : true) 
                    && (!string.IsNullOrEmpty(impostoQuery.InscricaoImovel) ? impostoQuery.InscricaoImovel == w.InscricaoImovel : true)
                    && (impostoQuery.DataConsulta.HasValue ? impostoQuery.DataConsulta >= w.DataVencimento : true));
                    
                return await data.ToListAsync();                
            }
            catch (System.Exception ex)
            {                
                var innerEx = (ex.InnerException != null) ? ex.InnerException.Message : "";

                var excecao = $" {dbContext.Database.GetConnectionString()} \n {ex.Message} \n {innerEx} ";
                throw new System.Exception(excecao);
            }

        }
    }
}