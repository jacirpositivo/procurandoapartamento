
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using ProcurandoApartamento.Infrastructure.Data;
using ProcurandoApartamento.Domain;
using ProcurandoApartamento.Domain.Repositories.Interfaces;
using ProcurandoApartamento.Test.Setup;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Xunit;
using System.Collections.Generic;

namespace ProcurandoApartamento.Test.Controllers
{
    public class ApartamentosControllerIntTest
    {
        public ApartamentosControllerIntTest()
        {
            _factory = new AppWebApplicationFactory<TestStartup>().WithMockUser();
            _client = _factory.CreateClient();

            _apartamentoRepository = _factory.GetRequiredService<IApartamentoRepository>();


            InitTest();
        }

        private const int DefaultQuadra = 1;
        private const int UpdatedQuadra = 2;

        private const bool DefaultApartamentoDisponivel = true;
        private const bool UpdatedApartamentoDisponivel = false;

        private const string DefaultEstabelecimento = "AAAAAAAAAA";
        private const string UpdatedEstabelecimento = "BBBBBBBBBB";

        private const bool DefaultEstabelecimentoExiste = true;
        private const bool UpdatedEstabelecimentoExiste = false;

        private readonly AppWebApplicationFactory<TestStartup> _factory;
        private readonly HttpClient _client;
        private readonly IApartamentoRepository _apartamentoRepository;

        private Apartamento _apartamento;


        private Apartamento CreateEntity()
        {
            return new Apartamento
            {
                Quadra = DefaultQuadra,
                ApartamentoDisponivel = DefaultApartamentoDisponivel,
                Estabelecimento = DefaultEstabelecimento,
                EstabelecimentoExiste = DefaultEstabelecimentoExiste,
            };
        }

        private void InitTest()
        {
            _apartamento = CreateEntity();
        }

        [Fact]
        public async Task CreateApartamento()
        {
            var databaseSizeBeforeCreate = await _apartamentoRepository.CountAsync();

            // Create the Apartamento
            var response = await _client.PostAsync("/api/apartamentos", TestUtil.ToJsonContent(_apartamento));
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            // Validate the Apartamento in the database
            var apartamentoList = await _apartamentoRepository.GetAllAsync();
            apartamentoList.Count().Should().Be(databaseSizeBeforeCreate + 1);
            var testApartamento = apartamentoList.Last();
            testApartamento.Quadra.Should().Be(DefaultQuadra);
            testApartamento.ApartamentoDisponivel.Should().Be(DefaultApartamentoDisponivel);
            testApartamento.Estabelecimento.Should().Be(DefaultEstabelecimento);
            testApartamento.EstabelecimentoExiste.Should().Be(DefaultEstabelecimentoExiste);
        }

        [Fact]
        public async Task CreateApartamentoWithExistingId()
        {
            var databaseSizeBeforeCreate = await _apartamentoRepository.CountAsync();
            // Create the Apartamento with an existing ID
            _apartamento.Id = 1L;

            // An entity with an existing ID cannot be created, so this API call must fail
            var response = await _client.PostAsync("/api/apartamentos", TestUtil.ToJsonContent(_apartamento));
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            // Validate the Apartamento in the database
            var apartamentoList = await _apartamentoRepository.GetAllAsync();
            apartamentoList.Count().Should().Be(databaseSizeBeforeCreate);
        }

        [Fact]
        public async Task GetAllApartamentos()
        {
            // Initialize the database
            await _apartamentoRepository.CreateOrUpdateAsync(_apartamento);
            await _apartamentoRepository.SaveChangesAsync();

            // Get all the apartamentoList
            var response = await _client.GetAsync("/api/apartamentos?sort=id,desc");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = JToken.Parse(await response.Content.ReadAsStringAsync());
            json.SelectTokens("$.[*].id").Should().Contain(_apartamento.Id);
            json.SelectTokens("$.[*].quadra").Should().Contain(DefaultQuadra);
            json.SelectTokens("$.[*].apartamentoDisponivel").Should().Contain(DefaultApartamentoDisponivel);
            json.SelectTokens("$.[*].estabelecimento").Should().Contain(DefaultEstabelecimento);
            json.SelectTokens("$.[*].estabelecimentoExiste").Should().Contain(DefaultEstabelecimentoExiste);
        }

        [Fact]
        public async Task GetApartamento()
        {
            // Initialize the database
            await _apartamentoRepository.CreateOrUpdateAsync(_apartamento);
            await _apartamentoRepository.SaveChangesAsync();

            // Get the apartamento
            var response = await _client.GetAsync($"/api/apartamentos/{_apartamento.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = JToken.Parse(await response.Content.ReadAsStringAsync());
            json.SelectTokens("$.id").Should().Contain(_apartamento.Id);
            json.SelectTokens("$.quadra").Should().Contain(DefaultQuadra);
            json.SelectTokens("$.apartamentoDisponivel").Should().Contain(DefaultApartamentoDisponivel);
            json.SelectTokens("$.estabelecimento").Should().Contain(DefaultEstabelecimento);
            json.SelectTokens("$.estabelecimentoExiste").Should().Contain(DefaultEstabelecimentoExiste);
        }

        [Fact]
        public async Task GetNonExistingApartamento()
        {
            var maxValue = long.MaxValue;
            var response = await _client.GetAsync("/api/apartamentos/" + maxValue);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        //[Fact]
        //public async Task UpdateApartamento()
        //{
        //    // Initialize the database
        //    await _apartamentoRepository.CreateOrUpdateAsync(_apartamento);
        //    await _apartamentoRepository.SaveChangesAsync();
        //    var databaseSizeBeforeUpdate = await _apartamentoRepository.CountAsync();

        //    // Update the apartamento
        //    var updatedApartamento = await _apartamentoRepository.QueryHelper().GetOneAsync(it => it.Id == _apartamento.Id);
        //    // Disconnect from session so that the updates on updatedApartamento are not directly saved in db
        //    //TODO detach
        //    updatedApartamento.Quadra = UpdatedQuadra;
        //    updatedApartamento.ApartamentoDisponivel = UpdatedApartamentoDisponivel;
        //    updatedApartamento.Estabelecimento = UpdatedEstabelecimento;
        //    updatedApartamento.EstabelecimentoExiste = UpdatedEstabelecimentoExiste;

        //    var response = await _client.PutAsync($"/api/apartamentos/{_apartamento.Id}", TestUtil.ToJsonContent(updatedApartamento));
        //    response.StatusCode.Should().Be(HttpStatusCode.OK);

        //    // Validate the Apartamento in the database
        //    var apartamentoList = await _apartamentoRepository.GetAllAsync();
        //    apartamentoList.Count().Should().Be(databaseSizeBeforeUpdate);
        //    var testApartamento = apartamentoList.Last();
        //    testApartamento.Quadra.Should().Be(UpdatedQuadra);
        //    testApartamento.ApartamentoDisponivel.Should().Be(UpdatedApartamentoDisponivel);
        //    testApartamento.Estabelecimento.Should().Be(UpdatedEstabelecimento);
        //    testApartamento.EstabelecimentoExiste.Should().Be(UpdatedEstabelecimentoExiste);
        //}

        //[Fact]
        //public async Task UpdateNonExistingApartamento()
        //{
        //    var databaseSizeBeforeUpdate = await _apartamentoRepository.CountAsync();

        //    // If the entity doesn't have an ID, it will throw BadRequestAlertException
        //    var response = await _client.PutAsync("/api/apartamentos/1", TestUtil.ToJsonContent(_apartamento));
        //    response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        //    // Validate the Apartamento in the database
        //    var apartamentoList = await _apartamentoRepository.GetAllAsync();
        //    apartamentoList.Count().Should().Be(databaseSizeBeforeUpdate);
        //}

        [Fact]
        public async Task DeleteApartamento()
        {
            // Initialize the database
            await _apartamentoRepository.CreateOrUpdateAsync(_apartamento);
            await _apartamentoRepository.SaveChangesAsync();
            var databaseSizeBeforeDelete = await _apartamentoRepository.CountAsync();

            var response = await _client.DeleteAsync($"/api/apartamentos/{_apartamento.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Validate the database is empty
            var apartamentoList = await _apartamentoRepository.GetAllAsync();
            apartamentoList.Count().Should().Be(databaseSizeBeforeDelete - 1);
        }

        [Fact]
        public void EqualsVerifier()
        {
            TestUtil.EqualsVerifier(typeof(Apartamento));
            var apartamento1 = new Apartamento
            {
                Id = 1L
            };
            var apartamento2 = new Apartamento
            {
                Id = apartamento1.Id
            };
            apartamento1.Should().Be(apartamento2);
            apartamento2.Id = 2L;
            apartamento1.Should().NotBe(apartamento2);
            apartamento1.Id = 0;
            apartamento1.Should().NotBe(apartamento2);
        }

        [Fact]
        public async Task SearchBetterApartmentFirstCase()
        {
            var apartmentNamesFilter = new List<string> { "ESCOLA", "ACADEMIA" };

            HttpResponseMessage response = await _client.PostAsync("/api/apartamentos/melhor-apartamento", TestUtil.ToJsonContent(apartmentNamesFilter));

            string result = await response.Content.ReadAsStringAsync();
            Assert.Equal("QUADRA 4", result);
        }

        [Fact]
        public async Task SearchBetterApartmentSecondCase()
        {
            var apartmentNamesFilter = new List<string> { "ESCOLA", "MERCADO", "ACADEMIA" };

            HttpResponseMessage response = await _client.PostAsync("/api/apartamentos/melhor-apartamento", TestUtil.ToJsonContent(apartmentNamesFilter));

            string result = await response.Content.ReadAsStringAsync();
            Assert.Equal("QUADRA 4", result);
        }
    }
}
