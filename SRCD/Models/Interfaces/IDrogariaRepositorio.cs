using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRCD.Models.Interfaces
{
    public interface IDrogariaRepositorio : IRepositorio<Drogaria>
    {
        Task<IEnumerable<Drogaria>> GetTop3();
        Task<IEnumerable<Drogaria>> Search(string name);
    }
}
