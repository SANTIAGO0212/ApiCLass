using Api_Sat_2023.DAL;
using Api_Sat_2023.DAL.Entities;
using Api_Sat_2023.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api_Sat_2023.Domain.Services
{
    public class CountryService : ICountryService
    {
        private readonly DateBaseContext _context;
        public CountryService(DateBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            return await _context.Countries.ToListAsync(); //Aquí lo que hago es traerme todos los datos que 
                                                           //tengo en mi tabla Countries.   
        }

        public async Task<Country> CreateCountryAsync(Country country)
        {
           try
           {
                country.Id = Guid.NewGuid();
                country.CreatedDate = DateTime.Now;

                _context.Countries.Add(country);
                await _context.SaveChangesAsync();
                return country;
           }
           catch (DbUpdateException dbUpdateException)
           {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
           }
        }
    }
}
