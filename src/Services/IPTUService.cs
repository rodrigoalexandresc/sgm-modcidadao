using System.Linq;
using System.Threading.Tasks;
using ModCidadao.Models;
using ModCidadao.Repositories;

namespace ModCidadao.Services {
    public class IPTUService {

        private readonly ModCidadaoDbContext dbContext;
        public IPTUService(ModCidadaoDbContext dbContext)
        {            
            this.dbContext = dbContext;
        }

        public void AtualizarImposto(IPTU iPTU) {
            var iPTUGravado = dbContext.IPTUs.FirstOrDefault(w => w.Chave == iPTU.Chave);

            iPTU.CPFouCNPJ = "35837615881";
            if (iPTUGravado != null) {
                iPTUGravado.AreaConstruida = iPTU.AreaConstruida;
                iPTUGravado.AreaTerreno = iPTU.AreaTerreno;
                iPTUGravado.DataVencimento = iPTU.DataVencimento;
                iPTUGravado.Descricao = iPTU.Descricao;
                iPTUGravado.CPFouCNPJ = iPTU.CPFouCNPJ;
                iPTUGravado.InscricaoImovel = iPTU.InscricaoImovel;
                iPTUGravado.Valor = iPTU.Valor;
            }
            else {
                dbContext.IPTUs.Add(iPTU);
            }                             
            dbContext.SaveChanges();          
        }
    }
}