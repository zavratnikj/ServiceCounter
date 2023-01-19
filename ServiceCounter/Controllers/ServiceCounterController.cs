using Microsoft.AspNetCore.Mvc;

namespace ServiceCounter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCounterController : Controller
    {

        private readonly MongoDBService _mongoDbService;

        public ServiceCounterController(MongoDBService mongoDBService)
        {
            _mongoDbService = mongoDBService;
        }

        [HttpGet]
        [Route("getZadnjiKlic")]
        public async Task<ServiceCounter> GetKlic()
        {
            return await _mongoDbService.ZadnjeKlicanaStoritev();
        }

        [HttpGet]
        [Route("getNajpogostejeKlicanaStoritev")]
        public async Task<ServiceCounter> GetNajpogostejsiKlic()
        {
            return await _mongoDbService.NajpogostejeKlicanaStoritev();
        }
        [HttpGet]
        [Route("steviloKlicev")]
        public async Task<ServiceCounter> GetSteviloKlicev(string nameKletka)
        {
            return await _mongoDbService.SteviloKlicev(nameKletka);
        }

        [HttpPut]
        [Route("updateCounter")]
        public async Task<ServiceCounter> PutKletka(string nameKletka)
        {
            return await _mongoDbService.DodajKlicKletke(nameKletka);            
        }

    }
}
