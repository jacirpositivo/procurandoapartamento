
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;
using ProcurandoApartamento.Domain;
using ProcurandoApartamento.Crosscutting.Exceptions;
using ProcurandoApartamento.Web.Extensions;
using ProcurandoApartamento.Web.Filters;
using ProcurandoApartamento.Web.Rest.Utilities;
using ProcurandoApartamento.Domain.Repositories.Interfaces;
using ProcurandoApartamento.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ProcurandoApartamento.Controllers
{
    
    [Route("api/apartamentos")]
    [ApiController]
    public class ApartamentosController : ControllerBase
    {
        private const string EntityName = "apartamento";
        private readonly ILogger<ApartamentosController> _log;
        private readonly IApartamentoService _apartamentoService;

        public ApartamentosController(ILogger<ApartamentosController> log,
        IApartamentoService apartamentoService)
        {
            _log = log;
            _apartamentoService = apartamentoService;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<Apartamento>> CreateApartamento([FromBody] Apartamento apartamento)
        {
            _log.LogDebug($"REST request to save Apartamento : {apartamento}");
            if (apartamento.Id != 0)
                throw new BadRequestAlertException("A new apartamento cannot already have an ID", EntityName, "idexists");

            await _apartamentoService.Save(apartamento);
            return CreatedAtAction(nameof(GetApartamento), new { id = apartamento.Id }, apartamento)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, apartamento.Id.ToString()));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Apartamento>>> GetAllApartamentos(IPageable pageable)
        {
            _log.LogDebug("REST request to get a page of Apartamentos");
            var result = await _apartamentoService.FindAll(pageable);
            return Ok(result.Content).WithHeaders(result.GeneratePaginationHttpHeaders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApartamento([FromRoute] long id)
        {
            _log.LogDebug($"REST request to get Apartamento : {id}");
            var result = await _apartamentoService.FindOne(id);
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApartamento([FromRoute] long id)
        {
            _log.LogDebug($"REST request to delete Apartamento : {id}");
            await _apartamentoService.Delete(id);
            return NoContent().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, id.ToString()));
        }
    }
}
