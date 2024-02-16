using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.Entity;
using System.Globalization;
using AutoMapper;

namespace SistemaVenta.AplicacionWeb.Utilidades.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            #region Rol
            CreateMap<Rol, VMRol>().ReverseMap();
            #endregion Rol

            #region Usuario
            CreateMap<Usuario, VMUsuario>()
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                )
                .ForMember(destino =>
                destino.NombreRol,
                opt => opt.MapFrom(origen => origen.IdRolNavigation.Descripcion)
                );

            CreateMap<VMUsuario, Usuario>()
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                )
                .ForMember(destino =>
                destino.IdRolNavigation,
                opt => opt.Ignore()
                );
            #endregion Usuario

            #region Sucursal
            CreateMap<Sucursal, VMSucursal>()
                .ForMember(destino =>
                destino.PorcentajeImpuesto,
                opt => opt.MapFrom(origen => Convert.ToString(origen.PorcentajeImpuesto.Value, new CultureInfo("es-AR")))
                );

            CreateMap<VMSucursal, Sucursal>()
                .ForMember(destino =>
                destino.PorcentajeImpuesto,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PorcentajeImpuesto.Value, new CultureInfo("es-AR")))
                );
            #endregion Sucursal

            #region Categoria
            CreateMap<Categoria, VMCategoria>()
                .ForMember(destino =>
                destino.esActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );

            CreateMap<VMCategoria, Categoria>()
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.esActivo == 1 ? true : false)
                );
            #endregion Categoria

            #region Marca
            CreateMap<Marca, VMMarca>()
                .ForMember(destino =>
                destino.esActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );

            CreateMap<VMMarca, Marca>()
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.esActivo == 1 ? true : false)
                );
            #endregion Marca

            #region Color
            CreateMap<Color, VMColor>()
                .ForMember(destino =>
                destino.esActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );

            CreateMap<VMColor, Color>()
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.esActivo == 1 ? true : false)
                );
            #endregion Color

            #region CondicionTributaria
            CreateMap<CondicionTributaria, VMCondicionTributaria>()
                .ForMember(destino =>
                destino.esActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );

            CreateMap<VMCondicionTributaria, CondicionTributaria>()
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.esActivo == 1 ? true : false)
                );
            #endregion CondicionTributaria

            #region TipoTalle
            CreateMap<TipoTalle, VMTipoTalle>()
                .ForMember(destino =>
                destino.esActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );

            CreateMap<VMTipoTalle, TipoTalle>()
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.esActivo == 1 ? true : false)
                );
            #endregion TipoTalle

            #region Talle
            CreateMap<Talle, VMTalle>()
                .ForMember(destino =>
                destino.esActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                )
                .ForMember(destino =>
                destino.NombreTipoTalle,
                opt => opt.MapFrom(origen => origen.IdTipoTalleNavigation.Descripcion)
                );

            CreateMap<VMTalle, Talle>()
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.esActivo == 1 ? true : false)
                )
                .ForMember(destino =>
                destino.IdTipoTalleNavigation,
                opt => opt.Ignore()
                );

            #endregion Talle

            #region Articulo

            CreateMap<Articulo, VMArticulo>()
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                )
                .ForMember(destino =>
                destino.NombreCategoria,
                opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.Descripcion)
                )
                .ForMember(destino =>
                destino.NombreMarca,
                opt => opt.MapFrom(origen => origen.IdMarcaNavigation.Descripcion)
                )
                .ForMember(destino =>
                destino.NombreTipoTalle,
                opt => opt.MapFrom(origen => origen.IdTipoTalleNavigation.Descripcion)
                )
                .ForMember(destino =>
                destino.Costo,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Costo.Value, new CultureInfo("es-AR")))
                );

            CreateMap<VMArticulo, Articulo>()
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                )
                .ForMember(destino =>
                destino.IdCategoriaNavigation,
                opt => opt.Ignore()
                )
                .ForMember(destino =>
                destino.IdMarcaNavigation,
                opt => opt.Ignore()
                )
                .ForMember(destino =>
                destino.IdTipoTalleNavigation,
                opt => opt.Ignore()
                )
                .ForMember(destino =>
                destino.Costo,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Costo, new CultureInfo("es-AR")))
                );
            #endregion Articulo

            #region Inventario
            CreateMap<Inventario, VMInventario>()
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                )
                .ForMember(destino =>
                destino.NombreArticulo,
                opt => opt.MapFrom(origen => origen.IdArticuloNavigation.Descripcion)
                )
                .ForMember(destino =>
                destino.NombreColor,
                opt => opt.MapFrom(origen => origen.IdColorNavigation.Descripcion)
                )
                .ForMember(destino =>
                destino.NombreTalle,
                opt => opt.MapFrom(origen => origen.IdTalleNavigation.Descripcion)
                )
                .ForMember(destino =>
                destino.Cantidad,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Cantidad.Value, new CultureInfo("es-AR")))
                )
                .ForMember(destino =>
                destino.PrecioArticulo,
                opt => opt.MapFrom(origen => Convert.ToString(origen.IdArticuloNavigation.Costo.Value, new CultureInfo("es-AR")))
                );

            CreateMap<VMInventario, Inventario>()
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                )
                .ForMember(destino =>
                destino.IdArticuloNavigation,
                opt => opt.Ignore()
                )
                .ForMember(destino =>
                destino.IdColorNavigation,
                opt => opt.Ignore()
                )
                .ForMember(destino =>
                destino.IdTalleNavigation,
                opt => opt.Ignore()
                )
                .ForMember(destino =>
                destino.Cantidad,
                opt => opt.MapFrom(origen => Convert.ToInt32(origen.Cantidad, new CultureInfo("es-AR")))
                );
            #endregion Inventario

            #region Cliente

            CreateMap<Cliente, VMCliente>()
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                )
                .ForMember(destino =>
                destino.NombreCondicionTributaria,
                opt => opt.MapFrom(origen => origen.IdCondicionTributariaNavigation.Nombre)
                );

            CreateMap<VMCliente, Cliente>()
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                )
                .ForMember(destino =>
                destino.IdCondicionTributariaNavigation,
                opt => opt.Ignore()
                );
            #endregion Cliente

            #region TipoComprobante
            CreateMap<TipoComprobante, VMTipoComprobante>().ReverseMap();
            #endregion TipoComprobante

            #region Venta
            CreateMap<Venta, VMVenta>()
            .ForMember(destino =>
            destino.TipoComprobante,
            opt => opt.MapFrom(origen => origen.IdTipoComprobanteNavigation.Descripcion)
            )
            .ForMember(destino =>
            destino.Usuario,
            opt => opt.MapFrom(origen => origen.IdUsuarioNavigation.Nombre)
            )
            .ForMember(destino =>
            destino.ImpuestoTotal,
            opt => opt.MapFrom(origen => Convert.ToString(origen.ImpuestoTotal.Value, new CultureInfo("es-AR")))
            )
            .ForMember(destino =>
            destino.Total,
            opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-AR")))
            )
            .ForMember(destino =>
            destino.IdPuntoVenta,
            opt => opt.MapFrom(origen => origen.IdPuntoVentaNavigation.IdPuntoVenta)
            )
            .ForMember(destino =>
            destino.FechaRegistro,
            opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy"))
            );

            CreateMap<VMVenta, Venta>()
            .ForMember(destino =>
            destino.ImpuestoTotal,
            opt => opt.MapFrom(origen => Convert.ToDecimal(origen.ImpuestoTotal, new CultureInfo("es-AR")))
            )
            .ForMember(destino =>
            destino.Total,
            opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-AR")))
            );
            #endregion Venta

            #region DetalleVenta
            CreateMap<DetalleVenta, VMDetalleVenta>()
            .ForMember(destino =>
            destino.Precio,
            opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE")))
            )
            .ForMember(destino =>
            destino.Total,
            opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE")))
            );

            CreateMap<VMDetalleVenta, DetalleVenta>()
            .ForMember(destino =>
            destino.Precio,
            opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-PE")))
            )
            .ForMember(destino =>
            destino.Total,
            opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-PE")))
            );

            CreateMap<DetalleVenta, VMReporteVenta>()
            .ForMember(destino =>
            destino.FechaRegistro,
            opt => opt.MapFrom(origen => origen.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy"))
            )
            .ForMember(destino =>
            destino.NumeroVenta,
            opt => opt.MapFrom(origen => origen.IdVentaNavigation.NumeroVenta)
            )
            .ForMember(destino =>
            destino.TipoComprobante,
            opt => opt.MapFrom(origen => origen.IdVentaNavigation.IdTipoComprobanteNavigation.Descripcion)
            )
            .ForMember(destino =>
            destino.DocumentoCliente,
            opt => opt.MapFrom(origen => origen.IdVentaNavigation.IdCliente)
            )
            .ForMember(destino =>
            destino.NombreCliente,
            opt => opt.MapFrom(origen => origen.IdVentaNavigation.IdClienteNavigation.Nombres)//Puede fallar
            )
            .ForMember(destino =>
            destino.ImpuestoTotalVenta,
            opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.ImpuestoTotal.Value, new CultureInfo("es-AR")))
            )
            .ForMember(destino =>
            destino.TotalVenta,
            opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Total.Value, new CultureInfo("es-AR")))
            )
            .ForMember(destino =>
            destino.Articulo,
            opt => opt.MapFrom(origen => origen.DescripcionArticulo)
            )
            .ForMember(destino =>
            destino.Precio,
            opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-AR")))
            )
            .ForMember(destino =>
            destino.Total,
            opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-AR")))
            )
            ;

            #endregion DetalleVenta

            #region Menu
            CreateMap<Menu, VMMenu>()
            .ForMember(destino =>
            destino.SubMenus,
            opt => opt.MapFrom(origen => origen.InverseIdMenuPadreNavigation)
            );
            #endregion Menu
        }
    }
}
