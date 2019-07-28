using SRCD.Models;
using SRCD.Models.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SRCD.Models.Repositorio
{
    public class ClinicaRepositorio : Repositorio<Clinica>, IClinicaRepositorio
    {
        private readonly ApplicationDbContext dbContext;

        public ClinicaRepositorio(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public override async Task UpdateAsync(Clinica model)
        {
            var clinica = await this.Find(x => x.Id == model.Id).SingleOrDefaultAsync();

            clinica.Id = model.Id;
            clinica.Nome = model.Nome;
            clinica.Morada = model.Morada;
            clinica.Email = model.Email;
            clinica.Latitude = model.Latitude;
            clinica.Longitude = model.Longitude;
            clinica.Telefone1 = model.Telefone1;
            clinica.Telefone2 = model.Telefone2;

            if(model.Imagem1 != null)
            {
                clinica.Imagem1 = model.Imagem1;
                clinica.Imagem1ContentType = model.Imagem1ContentType;
            }

            if (model.Imagem2 != null)
            {
                clinica.Imagem2 = model.Imagem2;
                clinica.Imagem2ContentType = model.Imagem2ContentType;
            }

            dbContext.Entry(clinica).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Clinica>> GetTop3()
        {
            return await dbContext.Clinicas.OrderByDescending(c => c.Id).Take(3).ToListAsync();
        }

        public async Task<IEnumerable<Clinica>> Search(string name)
        {
            return await dbContext.Clinicas.Where(c => c.Nome.Contains(name)).ToListAsync();
        }
    }
}
