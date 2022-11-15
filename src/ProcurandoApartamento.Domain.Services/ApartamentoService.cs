using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;
using ProcurandoApartamento.Domain.Services.Interfaces;
using ProcurandoApartamento.Domain.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;

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

            //another approach (get all apartments for first match quantity with result or last match with first param)
            int countParams = parameters.Count;
            string resultNotify = string.Empty;

            while (countParams > 0)
            {
                countParams--;
                parameters.RemoveAt(countParams);

                list = await _apartamentoRepository.SearchBetterApartment(parameters);

                groupList = list.GroupBy(p => new { p.Quadra }, (key, group) => new
                {
                    key.Quadra,
                    Result = group.ToList()
                });

                itemsToReturn = groupList.Where(p => p.Result.Count == parameters.Count);

                if (itemsToReturn.Count() == 1)
                {
                    countParams = 0;
                    resultNotify = $@"QUADRA {itemsToReturn.Select(p => p.Quadra).FirstOrDefault()}";
                }

                if (itemsToReturn.Count() > 1 && parameters.Count > 1)
                {
                    countParams = 0;
                    resultNotify = $@"QUADRA {itemsToReturn.Select(p => p.Quadra).FirstOrDefault()}";
                } else if (itemsToReturn.Count() > 1)
                {
                    countParams = 0;
                    resultNotify = $@"QUADRA {itemsToReturn.Select(p => p.Quadra).LastOrDefault()}";
                }
            }

            return !string.IsNullOrWhiteSpace(resultNotify) ? resultNotify : "NENHUMA SUGESTAO DISPONIVEL" ;
        }
    }
}
