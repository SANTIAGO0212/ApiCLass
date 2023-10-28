using Api_Sat_2023.DAL.Entities;

namespace Api_Sat_2023.Domain.Interfaces
{
    public interface IStateService
    {
        Task<IEnumerable<State>> GetStatesByCountryIdAsync(Guid countryId); //Una firma de método
        Task<State> CreateStateAsync(State state, Guid countryId);
        Task<State> GetStateByIdAsync(Guid id);
        Task<State> EditStateAsync(State state, Guid id);
        Task<State> DeleteStateAsync(Guid id);

    }
}
