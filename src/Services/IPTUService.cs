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

        public void AtualizarImposto(IPTU iPTU) {
            var iPTUGravado = iPTURepository.GetByKey(iPTU.Chave);
            if (iPTUGravado == null) 
                this.iPTURepository.Add(iPTU);            
            else 
                this.iPTURepository.Update(iPTU);
        }
    }
}