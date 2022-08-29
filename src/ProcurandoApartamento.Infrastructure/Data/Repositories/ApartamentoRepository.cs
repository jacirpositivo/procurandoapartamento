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

    }
}
