using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProcurandoApartamento.Domain
{
    [Table("apartamento")]
    public class Apartamento : BaseEntity<long>
    {
        public int Quadra { get; set; }
        public bool ApartamentoDisponivel { get; set; }
        public string Estabelecimento { get; set; }
        public bool EstabelecimentoExiste { get; set; }

        // jhipster-needle-entity-add-field - JHipster will add fields here, do not remove

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var apartamento = obj as Apartamento;
            if (apartamento?.Id == null || apartamento?.Id == 0 || Id == 0) return false;
            return EqualityComparer<long>.Default.Equals(Id, apartamento.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public override string ToString()
        {
            return "Apartamento{" +
                    $"ID='{Id}'" +
                    $", Quadra='{Quadra}'" +
                    $", ApartamentoDisponivel='{ApartamentoDisponivel}'" +
                    $", Estabelecimento='{Estabelecimento}'" +
                    $", EstabelecimentoExiste='{EstabelecimentoExiste}'" +
                    "}";
        }
    }
}
