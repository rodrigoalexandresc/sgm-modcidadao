using System;
using System.Collections.Generic;

namespace ModCidadao.Models {
    public class ImpostoQuery {
        public string CPFouCNPJ { get; set; }
        public string InscricaoImovel { get; set; }
        public DateTime? DataConsulta { get; set; }

        public bool IsValid() {
            return !string.IsNullOrEmpty(CPFouCNPJ)
                || !string.IsNullOrEmpty(InscricaoImovel)
                || DataConsulta.HasValue;
        }

        public IEnumerable<string> Errors() {
            if (IsValid()) return null;

            var errors = new List<string>();

            errors.Add("Preencha pelo um dos campos para busca");

            return errors;            
        }
    }
}