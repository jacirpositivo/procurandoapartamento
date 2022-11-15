using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JHipsterNet.Core.Pagination;
using JHipsterNet.Core.Pagination.Extensions;
using ProcurandoApartamento.Domain;
using ProcurandoApartamento.Domain.Repositories.Interfaces;
using ProcurandoApartamento.Infrastructure.Data.Extensions;

namespace ProcurandoApartamento.Infrastructure.Data.Repositories
{
    public class ApartamentoRepository : GenericRepository<Apartamento, long>, IApartamentoRepository
    {
        public ApartamentoRepository(IUnitOfWork context) : base(context)
        {
        }

        public async Task<IEnumerable<Apartamento>> SearchBetterApartment(List<string> parameters)
        {
            var list = await _context.Set<Apartamento>()
                                     .Where(p => parameters.Contains(p.Estabelecimento) &&
                                            p.EstabelecimentoExiste)
                                     .OrderBy(p => p.Quadra)
                                     .ToListAsync();

            return list;
        }
    }
}
