using SRCD.Models.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SRCD.Models
{
    public class DrogariaRepositorio : Repositorio<Drogaria>, IDrogariaRepositorio
    {
        private readonly ApplicationDbContext dbContext;

        public DrogariaRepositorio(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public override async Task UpdateAsync(Drogaria model)
        {
            var drogaria = await this.Find(x => x.Id == model.Id).SingleOrDefaultAsync();

            drogaria.Id = model.Id;
            drogaria.Nome = model.Nome;
            drogaria.Morada = model.Morada;
            drogaria.Email = model.Email;
            drogaria.Latitude = model.Latitude;
            drogaria.Longitude = model.Longitude;
            drogaria.Telefone1 = model.Telefone1;
            drogaria.Telefone2 = model.Telefone2;

            if(model.Imagem1 != null)
            {
                drogaria.Imagem1 = model.Imagem1;
                drogaria.Imagem1ContentType = model.Imagem1ContentType;
            }

            if (model.Imagem2 != null)
            {
                drogaria.Imagem2 = model.Imagem2;
                drogaria.Imagem2ContentType = model.Imagem2ContentType;
            }
            
            dbContext.Drogarias.Attach(drogaria);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Drogaria>> GetTop3()
        {
            return await dbContext.Drogarias.OrderByDescending(c => c.Id).Take(3).ToListAsync();
        }

        public async Task<IEnumerable<Drogaria>> Search(string name)
        {
            return await dbContext.Drogarias.Where(c => c.Nome.Contains(name)).ToListAsync();
        }
    }
}
