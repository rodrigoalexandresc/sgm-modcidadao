using System;

namespace ModCidadao.Models {

    [Serializable]
    public class IPTU {
        public IPTU()
        {
            
        }

        public int Id { get; set; }
        public decimal Valor { get; set; }
        public string InscricaoImovel { get; set; }
        public DateTime DataVencimento { get; set; }
        public string Chave { get; set; }
        public string Descricao { get; set; }
        public decimal AreaConstruida { get; set; }
        public decimal AreaTerreno { get; set; }
    }

    public class IPTUChave
    {
        public static string GerarChave(string InscricaoImovel, DateTime DataVencimento)
        {
            return InscricaoImovel + DataVencimento.ToString("yyyyMMdd");
        }
    }
}