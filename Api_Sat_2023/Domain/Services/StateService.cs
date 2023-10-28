using Api_Sat_2023.DAL;
using Api_Sat_2023.DAL.Entities;
using Api_Sat_2023.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Api_Sat_2023.Domain.Services
{
    public class StateService : IStateService
    {
        private readonly DateBaseContext _context;
        public StateService(DateBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<State>> GetStatesByCountryIdAsync(Guid countryId)
        {
            return await _context.States.
                Where(s => s.CountryId == countryId).
                ToListAsync(); //Aquí lo que hago es traerme todos los datos que 
                                                           //tengo en mi tabla Countries.   
        }

        public async Task<State> CreateStateAsync(State state, Guid countryId)
        {
           try
           {
                state.Id = Guid.NewGuid();
                state.CreatedDate = DateTime.Now;
                state.CountryId = countryId;
                state.Country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == countryId);
                state.ModifiedBase = null;

                _context.States.Add(state);
                await _context.SaveChangesAsync();
                return state;
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

        public async Task<State> GetStateByIdAsync(Guid id)
        {
            //return await _context.Countries.FindAsync(id); // FindAsync es un método propio de DbContext
            //return await _context.Countries.FirstAsync(x=>x.Id==id); //First Async es un método de EntityFramwork CORE
            return await _context.States.FirstOrDefaultAsync(s => s.Id == id); //Es parte de entityFramework CORE

        }

        public async Task<State> EditStateAsync(State state, Guid id)
        {
            try
            {
                state.ModifiedBase = DateTime.Now;
                _context.States.Update(state); //El método Update que es de EF me sirve para Actualizar un objeto
                await _context.SaveChangesAsync();
                return state;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<State> DeleteStateAsync(Guid id)
        {
            try
            {
                //Aquí, con el ID que traigo desde el controller, estoy recuperando el país que luego voy a eliminar
                //Ese país que recupero lo guardo en la variable country
                var state = await _context.States.FirstOrDefaultAsync(s => s.Id == id);
                if (state == null) return null; //Si el país no exíste, entonces retore NULL

                _context.States.Remove(state);
                await _context.SaveChangesAsync();
                return state;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
            
        }
    }
}
