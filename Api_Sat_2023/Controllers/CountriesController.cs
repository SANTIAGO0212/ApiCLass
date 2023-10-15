using Api_Sat_2023.DAL.Entities;

using Api_Sat_2023.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api_Sat_2023.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //Esta es la primera parte de la URL de esta API: URL = api/countries
    public class CountriesController : Controller
    {
        private readonly ICountryService _countryService;
        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }
        //En un controlador los métodos cambian denombre, y realmente se llaman ACCIONES (ACTIONS) +
        //Si es una API, se le denomina ENDPOINT
        //Todo Endpoint retorna un ActionResult, significa que retorna el resultado de una ACCIÓN.
        [HttpGet,ActionName("Get")]
        [Route("Get")] //Aquí concateno la URL inical: URL = api/countries/Get
        
        public async Task<ActionResult<IEnumerable<Country>>> GetActionResultAsync()
        {
            var countries = await _countryService.GetCountriesAsync(); //Aquí estoy yendo a mi capa de DOMAIN para traerme +
                                                                       //la lista de países

            if(countries == null || !countries.Any()) //El método Any() significa si hay al menos un elemento
                                                      //El método !Any() significa si no hay absolutamente nada.
            {
                return NotFound(); // Not Found = 404
            }
            return Ok(countries); // Ok = 200 Status Code
        }
        [HttpPost,ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateCountryAsync(Country country)
        {
            try
            {
                var createdCountry = await _countryService.CreateCountryAsync(country);
                
                if(createdCountry == null)
                {
                    return NotFound();
                }

                return Ok(createdCountry);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate")){
                    return Conflict(String.Format("El país {0} ya exíste", country.Name));
                }
                return Conflict(ex.Message);
            }
        }
    }
}
