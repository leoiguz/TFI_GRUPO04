using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaVenta.BLL.Implementacion;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Implementacion;
using SistemaVenta.DAL.Interfaces;

namespace SistemaVenta.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencia(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<DBTIENDAContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("CadenaSQL"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IVentaRepository, VentaRepository>();

            services.AddScoped<ICorreoService, CorreoService>();
            services.AddScoped<IFireBaseService, FireBaseService>();

            services.AddScoped<IUtilidadesService, UtilidadesService>();
            services.AddScoped<IRolService, RolService>();

            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ISucursalService, SucursalService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<ITipoTalleService, TipoTalleService>();
            services.AddScoped<IMarcaService, MarcaService>();
            services.AddScoped<IArticuloService, ArticuloService>();
            services.AddScoped<ITalleService, TalleService>();
            services.AddScoped<IInventarioService, InventarioService>();
            services.AddScoped<IClienteService, ClienteService>();

            services.AddScoped<ICondicionTributariaService, CondicionTributariaService>();
            services.AddScoped<ITipoComprobanteService, TipoComprobanteService>();
            services.AddScoped<IVentaService, VentaService>();
            services.AddScoped<ITipoPagoService, TipoPagoService>();
            services.AddScoped<IPagoService, PagoService>();

            services.AddScoped<IDashBoardService, DashBoardService>();
            services.AddScoped<IMenuService, MenuService>();

        }
    }
}
