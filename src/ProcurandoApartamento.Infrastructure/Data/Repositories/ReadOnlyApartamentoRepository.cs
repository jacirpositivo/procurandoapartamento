using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JHipsterNet.Core.Pagination;
using JHipsterNet.Core.Pagination.Extensions;
using ProcurandoApartamento.Domain;
using ProcurandoApartamento.Domain.Repositories.Interfaces;
using ProcurandoApartamento.Infrastructure.Data.Extensions;

namespace ProcurandoApartamento.Infrastructure.Data.Repositories
{
    public class ReadOnlyApartamentoRepository : ReadOnlyGenericRepository<Apartamento, long>, IReadOnlyApartamentoRepository
    {
        public ReadOnlyApartamentoRepository(IUnitOfWork context) : base(context)
        {
        }
    }
}
