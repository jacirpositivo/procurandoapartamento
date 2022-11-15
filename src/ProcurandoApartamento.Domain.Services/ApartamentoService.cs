using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;
using ProcurandoApartamento.Domain.Services.Interfaces;
using ProcurandoApartamento.Domain.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ProcurandoApartamento.Domain.Services
{
    public class ApartamentoService : IApartamentoService
    {
        protected readonly IApartamentoRepository _apartamentoRepository;

        public ApartamentoService(IApartamentoRepository apartamentoRepository)
        {
            _apartamentoRepository = apartamentoRepository;
        }

        public virtual async Task<Apartamento> Save(Apartamento apartamento)
        {
            await _apartamentoRepository.CreateOrUpdateAsync(apartamento);
            await _apartamentoRepository.SaveChangesAsync();
            return apartamento;
        }

        public virtual async Task<IPage<Apartamento>> FindAll(IPageable pageable)
        {
            var page = await _apartamentoRepository.QueryHelper()
                .GetPageAsync(pageable);
            return page;
        }

        public virtual async Task<Apartamento> FindOne(long id)
        {
            var result = await _apartamentoRepository.QueryHelper()
                .GetOneAsync(apartamento => apartamento.Id == id);
            return result;
        }

        public virtual async Task Delete(long id)
        {
            await _apartamentoRepository.DeleteByIdAsync(id);
            await _apartamentoRepository.SaveChangesAsync();
        }

        public virtual async Task<string> SearchBetterApartment(List<string> parameters)
        {
            parameters = parameters.Select(p => p.ToString().ToUpper()).ToList();

            IEnumerable<Apartamento> list = await _apartamentoRepository.SearchBetterApartment(parameters);

            var groupList = list.GroupBy(p => new { p.Quadra }, (key, group) => new
            {
                key.Quadra,
                Result = group.ToList()
            });

            var itemsToReturn = groupList.Where(p => p.Result.Count == parameters.Count);

            if (itemsToReturn.Any())
            {
                return $@"QUADRA {itemsToReturn.Select(p => p.Quadra).LastOrDefault()}";
            }

            string stringToResult = string.Empty;
            int lastQuarter = 0;

            foreach (string currentApartment in parameters)
            {
                List<Apartamento> apartments = list.Where(p => p.Estabelecimento == currentApartment).ToList();
                int _currentQuarter = 0;

                if (apartments.Count >= 1)
                {
                    _currentQuarter = apartments.Select(p => p.Quadra).LastOrDefault();
                }

                lastQuarter = _currentQuarter >= lastQuarter ? _currentQuarter : lastQuarter;
            }

            return lastQuarter > 0 ? stringToResult += $@"QUADRA {lastQuarter}" : "NENHUMA SUGESTAO DISPONIVEL";
        }
    }
}
