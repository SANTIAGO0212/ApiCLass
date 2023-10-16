using Api_Sat_2023.DAL;
using Api_Sat_2023.DAL.Entities;
using Api_Sat_2023.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

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

        public async Task<Country> GetCountryByIdAsync(Guid id)
        {
            //return await _context.Countries.FindAsync(id); // FindAsync es un método propio de DbContext
            //return await _context.Countries.FirstAsync(x=>x.Id==id); //First Async es un método de EntityFramwork CORE
            return await _context.Countries.FirstOrDefaultAsync(c => c.Id == id); //Es parte de entityFramework CORE

        }

        public async Task<Country> GetCountryByNameAsync(string name)
        {
            return await _context.Countries.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<Country> EditCountryAsync(Country country)
        {
            try
            {
                country.ModifiedBase = DateTime.Now;
                _context.Countries.Update(country); //El método Update que es de EF me sirve para Actualizar un objeto
                await _context.SaveChangesAsync();
                return country;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<Country> DeleteCountryAsync(Guid id)
        {
            try
            {
                //Aquí, con el ID que traigo desde el controller, estoy recuperando el país que luego voy a eliminar
                //Ese país que recupero lo guardo en la variable country
                var country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);
                if (country == null) return null; //Si el país no exíste, entonces retore NULL

                _context.Countries.Remove(country);
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
