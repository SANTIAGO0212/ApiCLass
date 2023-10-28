using Api_Sat_2023.DAL.Entities;
using Api_Sat_2023.Domain.Interfaces;
using Api_Sat_2023.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api_Sat_2023.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatesController : Controller
    {
        private readonly IStateService _stateService;
        public StatesController(IStateService stateService)
        {
            _stateService = stateService;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get")] //Aquí concateno la URL inical: URL = api/countries/Get

        public async Task<ActionResult<IEnumerable<State>>> GetStatesByCountryIdAsync(Guid countryId)
        {
            var states = await _stateService.GetStatesByCountryIdAsync(countryId); //Aquí estoy yendo a mi capa de DOMAIN para traerme +
                                                                       //la lista de países

            if (states == null || !states.Any()) //El método Any() significa si hay al menos un elemento
                                                       //El método !Any() significa si no hay absolutamente nada.
            {
                return NotFound(); // Not Found = 404
            }
            return Ok(states); // Ok = 200 Status Code
        }
        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateStateAsync(State state, Guid countryId)
        {
            try
            {
                var createdState = await _stateService.CreateStateAsync(state,countryId);

                if (createdState == null)
                {
                    return NotFound();
                }

                return Ok(createdState);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                {
                    return Conflict(String.Format("El país {0} ya exíste", state.Name));
                }
                return Conflict(ex.Message);
            }
        }
        [HttpGet, ActionName("GetById")]
        [Route("GetById/{id}")] //Aquí concateno la URL inical: URL = api/countries/Get

        public async Task<ActionResult<IEnumerable<State>>> GetStateByIdAsync(Guid id)
        {
            if (id == null) return BadRequest("El id es requerido");
            var state = await _stateService.GetStateByIdAsync(id); //Aquí estoy yendo a mi capa de DOMAIN para traerme +
                                                                           //la lista de países

            if (state == null) //El método Any() significa si hay al menos un elemento
                                   //El método !Any() significa si no hay absolutamente nada.
            {
                return NotFound(); // Not Found = 404
            }
            return Ok(state); // Ok = 200 Status Code
        }
        [HttpPut, ActionName("Edit")]
        [Route("Edit")]
        public async Task<ActionResult<State>> EditStateAsync(State state, Guid id)
        {
            try
            {
                var editedState = await _stateService.EditStateAsync(state,  id);
                return Ok(editedState);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                {
                    return Conflict(String.Format("El país {0} ya exíste", state.Name));
                }
                return Conflict(ex.Message);
            }
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete")]
        public async Task<ActionResult<State>> DeleteStateAsync(Guid id)
        {
            if (id == null) return BadRequest("El Id es requerido!");
            var deletedState = await _stateService.DeleteStateAsync(id);
            if (deletedState == null) return NotFound("País no encontrado!");
            return Ok(deletedState);
        }

    }
}
