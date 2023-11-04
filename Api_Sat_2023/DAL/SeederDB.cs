using Api_Sat_2023.DAL.Entities;

namespace Api_Sat_2023.DAL
{
    public class SeederDB
    {
        private readonly DateBaseContext _context;
        public SeederDB(DateBaseContext context)
        {
            _context = context;
        }

        //Crearemos un método llamado Seeder Async
        //Este método es una especie de MAIN()
        //Este método tendrá la responsabilidad de prepoblar las tablas de mi BD

        public async Task SeederAsync()
        {
            //Primero: Agregaré un método propio de EF que hace las veces de comando 'update-database'
            //En otras palabras: un método creará la BD inmediatamente ponga en ejecucuión mi API
            await _context.Database.EnsureCreatedAsync();

            //A partir de aquí vamos a ir creando métodos que me sirvan para pepobrar mi BD
            await PopulateCountriesAsync();
            await _context.SaveChangesAsync(); //Esta línea me guarda ls datos en BD
        }

        private async Task PopulateCountriesAsync()
        {
            //El método Any() me indica si la tabla Countries tiene al menos un registro
            //El método Any negado (!) me indica que no hay ningun registro en mi tabla countries
            if (!_context.Countries.Any())
            {
                //Así creo yo un objeto país con sus respectivos estados
                _context.Countries.Add(new Country
                {
                    CreatedDate = DateTime.Now,
                    Name = "Colombia",
                    States = new List<State>()
                    {
                        new State
                        {
                            CreatedDate = DateTime.Now,
                            Name = "Antioquia"
                        },

                        new State
                        {
                            CreatedDate = DateTime.Now,
                            Name = "Cundinamarca"
                        }
                    }
                });
                _context.Countries.Add(new Country
                {
                    CreatedDate = DateTime.Now,
                    Name = "Argentina",
                    States = new List<State>()
                    {
                        new State
                        {
                            CreatedDate = DateTime.Now,
                            Name = "Buenos Aires"
                        },

                        new State
                        {
                            CreatedDate = DateTime.Now,
                            Name = "Ezeiza"
                        }
                    }
                });

            }
        }
    }
}
