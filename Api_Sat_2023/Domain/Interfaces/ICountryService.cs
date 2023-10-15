using Api_Sat_2023.DAL.Entities;

namespace Api_Sat_2023.Domain.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>>GetCountriesAsync(); //Una firma de método
        Task<Country>CreateCountryAsync(Country country);
    }
}
