using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRCD.Models.Interfaces
{
    public interface IClinicaRepositorio : IRepositorio<Clinica>
    {
        Task<IEnumerable<Clinica>> GetTop3();
        Task<IEnumerable<Clinica>> Search(string name);
    }
}
