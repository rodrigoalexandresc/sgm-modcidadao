using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModCidadao.Models;

namespace ModCidadao.Repositories {
    public class IPTUMap {
        public IPTUMap(EntityTypeBuilder<IPTU> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.ToTable("iptu");

            entityBuilder.Property(x => x.Id).HasColumnName("id");
            entityBuilder.Property(x => x.CPFOuCNPJ).HasColumnName("cpfoucnpj");
            entityBuilder.Property(x => x.DataVencimento).HasColumnName("datavencimento");
            entityBuilder.Property(x => x.AreaConstruida).HasColumnName("areaconstruida");
            entityBuilder.Property(x => x.AreaTerreno).HasColumnName("areaterreno");
            entityBuilder.Property(x => x.Chave).HasColumnName("chave");
            entityBuilder.Property(x => x.Descricao).HasColumnName("descricao");
            entityBuilder.Property(x => x.InscricaoImovel).HasColumnName("inscricaoimovel");
            entityBuilder.Property(x => x.Valor).HasColumnName("valor");
        }        
    }
}