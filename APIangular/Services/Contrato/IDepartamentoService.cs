using BackEndApiSistema.Models;

namespace BackEndApiSistema.Services.Contrato
{
    public interface IDepartamentoService
    {
        Task<List<Departamento>> GetList();

    }
}
