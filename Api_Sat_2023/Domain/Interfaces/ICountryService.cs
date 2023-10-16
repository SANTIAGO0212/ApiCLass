using Api_Sat_2023.DAL.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Api_Sat_2023.Domain.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>>GetCountriesAsync(); //Una firma de método
        Task<Country>CreateCountryAsync(Country country);
        Task<Country> GetCountryByIdAsync(Guid id);
        Task<Country> GetCountryByNameAsync(string name);
        Task<Country> EditCountryAsync(Country country);
        Task<Country> DeleteCountryAsync(Guid id);

    }
}
