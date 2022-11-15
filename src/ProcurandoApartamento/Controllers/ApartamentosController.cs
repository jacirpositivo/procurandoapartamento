// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using JHipsterNet.Core.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProcurandoApartamento.Crosscutting.Exceptions;
using ProcurandoApartamento.Domain;
using ProcurandoApartamento.Domain.Services.Interfaces;
using ProcurandoApartamento.Web.Extensions;
using ProcurandoApartamento.Web.Filters;
using ProcurandoApartamento.Web.Rest.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpPost("melhor-apartamento")]
        public async Task<ActionResult<string>> SearchBetterApartment([FromBody] List<string> parameters)
        {
            _log.LogDebug($"REST request to search|find the better apartment : {parameters}");

            var result = await _apartamentoService.SearchBetterApartment(parameters);
            return ActionResultUtil.WrapOrNotFound(result);
        }
    }
}
