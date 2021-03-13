using System.Threading.Tasks;
using ModCidadao.Models;
using ModCidadao.Repositories;

namespace ModCidadao.Services {
    public class IPTUService {
        private readonly IPTURepository iPTURepository;
        public IPTUService(IPTURepository iPTURepository)
        {
            this.iPTURepository = iPTURepository;
        }

        public async Task AtualizarImposto(IPTU iPTU) {
            var iPTUGravado = await iPTURepository.GetByKey(iPTU.Chave);
            if (iPTUGravado == null) 
                await this.iPTURepository.Add(iPTU);            
            else 
                await this.iPTURepository.Update(iPTU);
        }
    }
}