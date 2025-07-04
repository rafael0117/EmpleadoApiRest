using Microsoft.EntityFrameworkCore;
using BackEndApiSistema.Models;
using BackEndApiSistema.Services.Contrato;

namespace BackEndApiSistema.Services.Implementacion
{
    public class DepartamentoService : IDepartamentoService
    {
        private DbempleadoContext _dbContext;

        public DepartamentoService(DbempleadoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Departamento>> GetList()
        {
            try{
                List<Departamento> lista = new List<Departamento>();    
                lista = await _dbContext.Departamentos.ToListAsync();
                return lista;
            }
            catch(Exception e){
                throw e;
            
            }
        }
    }
}
