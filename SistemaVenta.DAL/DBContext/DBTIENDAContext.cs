using Microsoft.EntityFrameworkCore;
using SistemaVenta.Entity;

namespace SistemaVenta.DAL.DBContext
{
    public partial class DBTIENDAContext : DbContext
    {
        public DBTIENDAContext()
        {
        }

        public DBTIENDAContext(DbContextOptions<DBTIENDAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Articulo> Articulos { get; set; } = null!;
        public virtual DbSet<Categoria> Categoria { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Color> Colors { get; set; } = null!;
        public virtual DbSet<CondicionTributaria> CondicionTributaria { get; set; } = null!;
        public virtual DbSet<Configuracion> Configuracions { get; set; } = null!;
        public virtual DbSet<DetalleVenta> DetalleVenta { get; set; } = null!;
        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<Inventario> Inventarios { get; set; } = null!;
        public virtual DbSet<Marca> Marcas { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<NumeroCorrelativo> NumeroCorrelativos { get; set; } = null!;
        public virtual DbSet<Pago> Pagos { get; set; } = null!;
        public virtual DbSet<PuntoVenta> PuntoVenta { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<RolMenu> RolMenus { get; set; } = null!;
        public virtual DbSet<Sesion> Sesions { get; set; } = null!;
        public virtual DbSet<Sucursal> Sucursals { get; set; } = null!;
        public virtual DbSet<Talle> Talles { get; set; } = null!;
        public virtual DbSet<TipoComprobante> TipoComprobantes { get; set; } = null!;
        public virtual DbSet<TipoTalle> TipoTalles { get; set; } = null!;
        public virtual DbSet<TipoPago> TipoPagos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<Venta> Venta { get; set; } = null!;
        public virtual DbSet<Tarjeta> Tarjeta { get; set; } = null!;
        public virtual DbSet<AFIP> AFIP { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articulo>(entity =>
            {
                entity.HasKey(e => e.IdArticulo)
                    .HasName("PK__Articulo__AABB74221DCA65E6");

                entity.ToTable("Articulo");

                entity.Property(e => e.IdArticulo).HasColumnName("idArticulo");

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigoBarra");

                entity.Property(e => e.Costo)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("costo");

                entity.Property(e => e.Iva)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("iva");

                entity.Property(e => e.MargenGanancia)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("margenGanancia");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");

                entity.Property(e => e.IdMarca).HasColumnName("idMarca");

                entity.Property(e => e.IdTipoTalle).HasColumnName("idTipoTalle");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Articulos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK__Articulo__idCate__4F7CD00D");

                entity.HasOne(d => d.IdMarcaNavigation)
                    .WithMany(p => p.Articulos)
                    .HasForeignKey(d => d.IdMarca)
                    .HasConstraintName("FK__Articulo__idMarc__5070F446");

                entity.HasOne(d => d.IdTipoTalleNavigation)
                    .WithMany(p => p.Articulos)
                    .HasForeignKey(d => d.IdTipoTalle)
                    .HasConstraintName("FK__Articulo__idTipo__5165187F");
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK__Categori__8A3D240C44078973");

                entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__Cliente__885457EE1E034DD1");

                entity.ToTable("Cliente");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apellidos");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Domicilio)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("domicilio");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombres");

                entity.Property(e => e.NumeroDocumento)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("numeroDocumento");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.Property(e => e.IdCondicionTributaria).HasColumnName("idCondicionTributaria");

                entity.HasOne(d => d.IdCondicionTributariaNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.IdCondicionTributaria)
                    .HasConstraintName("FK__Cliente__idCondi__6A30C649");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.HasKey(e => e.IdColor)
                    .HasName("PK__Color__504A3B888B1FE94E");

                entity.ToTable("Color");

                entity.Property(e => e.IdColor).HasColumnName("idColor");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<CondicionTributaria>(entity =>
            {
                entity.HasKey(e => e.IdCondicionTributaria)
                    .HasName("PK__Condicio__9E34510FA2D40E6A");

                entity.Property(e => e.IdCondicionTributaria).HasColumnName("idCondicionTributaria");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.IdTipoComprobante).HasColumnName("idTipoComprobante");

                entity.HasOne(d => d.IdTipoComprobanteNavigation)
                    .WithMany(p => p.CondicionTributaria)
                    .HasForeignKey(d => d.IdTipoComprobante)
                    .HasConstraintName("FK_CondicionTributaria_idTipoComprobante");
            });

            modelBuilder.Entity<Configuracion>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Configuracion");

                entity.Property(e => e.Propiedad)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("propiedad");

                entity.Property(e => e.Recurso)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("recurso");

                entity.Property(e => e.Valor)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("valor");
            });

            modelBuilder.Entity<DetalleVenta>(entity =>
            {
                entity.HasKey(e => e.IdDetalleVenta)
                    .HasName("PK__DetalleV__BFE2843F3EB869B0");

                entity.Property(e => e.IdDetalleVenta).HasColumnName("idDetalleVenta");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.NombreArticulo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombreArticulo");

                entity.Property(e => e.TalleInventario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("talleInventario");

                entity.Property(e => e.ColorInventario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("colorInventario");

                entity.Property(e => e.IdInventario).HasColumnName("idInventario");

                entity.Property(e => e.IdVenta).HasColumnName("idVenta");

                entity.Property(e => e.NetoGravado)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("netoGravado");

                entity.Property(e => e.MargenGanancia)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("margenGanancia");

                entity.Property(e => e.Iva)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("iva");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total");

                entity.HasOne(d => d.IdInventarioNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdInventario)
                    .HasConstraintName("FK__DetalleVe__idInv__2180FB33");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdVenta)
                    .HasConstraintName("FK__DetalleVe__idVen__208CD6FA");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado)
                    .HasName("PK__Empleado__5295297CA4ABB0D5");

                entity.ToTable("Empleado");

                entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apellidos");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombres");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.HasOne(d => d.IdSucursalNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.IdSucursal)
                    .HasConstraintName("FK__Empleado__idSucu__30C33EC3");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Empleado__idUsua__7A672E12");
            });

            modelBuilder.Entity<Inventario>(entity =>
            {
                entity.HasKey(e => e.IdInventario)
                    .HasName("PK__Inventar__8F145B0DA9ACB99E");

                entity.ToTable("Inventario");

                entity.Property(e => e.IdInventario).HasColumnName("idInventario");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdArticulo).HasColumnName("idArticulo");

                entity.Property(e => e.IdColor).HasColumnName("idColor");

                entity.Property(e => e.IdTalle).HasColumnName("idTalle");

                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.Inventarios)
                    .HasForeignKey(d => d.IdArticulo)
                    .HasConstraintName("FK__Inventari__idArt__5BE2A6F2");

                entity.HasOne(d => d.IdColorNavigation)
                    .WithMany(p => p.Inventarios)
                    .HasForeignKey(d => d.IdColor)
                    .HasConstraintName("FK__Inventari__idCol__5CD6CB2B");

                entity.HasOne(d => d.IdTalleNavigation)
                    .WithMany(p => p.Inventarios)
                    .HasForeignKey(d => d.IdTalle)
                    .HasConstraintName("FK__Inventari__idTal__5DCAEF64");
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.HasKey(e => e.IdMarca)
                    .HasName("PK__Marca__70331812E80534E3");

                entity.ToTable("Marca");

                entity.Property(e => e.IdMarca).HasColumnName("idMarca");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.IdMenu)
                    .HasName("PK__Menu__C26AF483E5FF47E6");

                entity.ToTable("Menu");

                entity.Property(e => e.IdMenu).HasColumnName("idMenu");

                entity.Property(e => e.Controlador)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("controlador");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Icono)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("icono");

                entity.Property(e => e.IdMenuPadre).HasColumnName("idMenuPadre");

                entity.Property(e => e.PaginaAccion)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("paginaAccion");

                entity.HasOne(d => d.IdMenuPadreNavigation)
                    .WithMany(p => p.InverseIdMenuPadreNavigation)
                    .HasForeignKey(d => d.IdMenuPadre)
                    .HasConstraintName("FK__Menu__idMenuPadr__37A5467C");
            });

            modelBuilder.Entity<NumeroCorrelativo>(entity =>
            {
                entity.HasKey(e => e.IdNumeroCorrelativo)
                    .HasName("PK__NumeroCo__25FB547E738E643F");

                entity.ToTable("NumeroCorrelativo");

                entity.Property(e => e.IdNumeroCorrelativo).HasColumnName("idNumeroCorrelativo");

                entity.Property(e => e.CantidadDigitos).HasColumnName("cantidadDigitos");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaActualizacion");

                entity.Property(e => e.Gestion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("gestion");

                entity.Property(e => e.UltimoNumero).HasColumnName("ultimoNumero");
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.HasKey(e => e.IdPago)
                    .HasName("PK__Pago__BD2295AD6112C094");

                entity.ToTable("Pago");

                entity.Property(e => e.IdPago).HasColumnName("idPago");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("monto");

                entity.Property(e => e.IdTipoPago).HasColumnName("idTipoPago");

                entity.HasOne(d => d.IdTipoPagoNavigation)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.IdTipoPago)
                    .HasConstraintName("FK_Pago_idTipoPago");

            });

            modelBuilder.Entity<PuntoVenta>(entity =>
            {
                entity.HasKey(e => e.IdPuntoVenta)
                    .HasName("PK__PuntoVen__B49D1D36C54ABE9C");

                entity.Property(e => e.IdPuntoVenta).HasColumnName("idPuntoVenta");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdSesion).HasColumnName("idSesion");

                entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");

                entity.Property(e => e.Numero).HasColumnName("numero");

                entity.HasOne(d => d.IdSesionNavigation)
                    .WithMany(p => p.PuntoVenta)
                    .HasForeignKey(d => d.IdSesion)
                    .HasConstraintName("FK__PuntoVent__idSes__14270015");

                entity.HasOne(d => d.IdSucursalNavigation)
                    .WithMany(p => p.PuntoVenta)
                    .HasForeignKey(d => d.IdSucursal)
                    .HasConstraintName("FK__PuntoVent__idSuc__31B762FC");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Rol__3C872F76D79B24CA");

                entity.ToTable("Rol");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<RolMenu>(entity =>
            {
                entity.HasKey(e => e.IdRolMenu)
                    .HasName("PK__RolMenu__CD2045D8E3992A76");

                entity.ToTable("RolMenu");

                entity.Property(e => e.IdRolMenu).HasColumnName("idRolMenu");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdMenu).HasColumnName("idMenu");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.HasOne(d => d.IdMenuNavigation)
                    .WithMany(p => p.RolMenus)
                    .HasForeignKey(d => d.IdMenu)
                    .HasConstraintName("FK__RolMenu__idMenu__3F466844");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.RolMenus)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__RolMenu__idRol__3E52440B");
            });

            modelBuilder.Entity<Sesion>(entity =>
            {
                entity.HasKey(e => e.IdSesion)
                    .HasName("PK__Sesion__DB6C2DE675E7C70D");

                entity.ToTable("Sesion");

                entity.Property(e => e.IdSesion).HasColumnName("idSesion");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaFin")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaInicio")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Sesions)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Sesion__idUsuari__02FC7413");
            });

            modelBuilder.Entity<Sucursal>(entity =>
            {
                entity.HasKey(e => e.IdSucursal)
                    .HasName("PK__Sucursal__F707694C66C89F98");

                entity.ToTable("Sucursal");

                entity.Property(e => e.IdSucursal)
                    .ValueGeneratedNever()
                    .HasColumnName("idSucursal");

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ciudad");

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.Domicilio)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("domicilio");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.NumeroDocumento)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("numeroDocumento");            


                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.Property(e => e.IdCondicionTributaria).HasColumnName("idCondicionTributaria");

                entity.HasOne(d => d.IdCondicionTributariaNavigation)
                    .WithMany(p => p.Sucursales)
                    .HasForeignKey(d => d.IdCondicionTributaria)
                    .HasConstraintName("FK_Sucursal_CondicionTributaria");
            });

            modelBuilder.Entity<Talle>(entity =>
            {
                entity.HasKey(e => e.IdTalle)
                    .HasName("PK__Talle__74EEABF1084BDC14");

                entity.ToTable("Talle");

                entity.Property(e => e.IdTalle).HasColumnName("idTalle");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdTipoTalle).HasColumnName("idTipoTalle");

                entity.HasOne(d => d.IdTipoTalleNavigation)
                    .WithMany(p => p.Talles)
                    .HasForeignKey(d => d.IdTipoTalle)
                    .HasConstraintName("FK__Talle__idTipoTal__5535A963");
            });

            modelBuilder.Entity<TipoComprobante>(entity =>
            {
                entity.HasKey(e => e.IdTipoComprobante)
                    .HasName("PK__TipoComp__C77122F3C67EBAF5");

                entity.ToTable("TipoComprobante");

                entity.Property(e => e.IdTipoComprobante).HasColumnName("idTipoComprobante");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

            });

            modelBuilder.Entity<TipoTalle>(entity =>
            {
                entity.HasKey(e => e.IdTipoTalle)
                    .HasName("PK__TipoTall__C71D5769EBF96025");

                entity.ToTable("TipoTalle");

                entity.Property(e => e.IdTipoTalle).HasColumnName("idTipoTalle");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__645723A62C86685F");

                entity.ToTable("Usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.Clave)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("clave");

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__Usuario__idRol__4316F928");
            });

            modelBuilder.Entity<TipoPago>(entity =>
            {
                entity.HasKey(e => e.IdTipoPago)
                    .HasName("PK__TipoPago__AC5BA85BFA4D6DE2");

                entity.ToTable("TipoPago");

                entity.Property(e => e.IdTipoPago).HasColumnName("idTipoPago");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(e => e.IdVenta)
                    .HasName("PK__Venta__077D56148CC2F64E");

                entity.Property(e => e.IdVenta).HasColumnName("idVenta");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.IdPago).HasColumnName("idPago");

                entity.Property(e => e.IdPuntoVenta).HasColumnName("idPuntoVenta");

                entity.Property(e => e.IdTipoComprobante).HasColumnName("idTipoComprobante");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.NumeroComprobante)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("numeroComprobante");

                entity.Property(e => e.ImporteIva)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("importeIva");

                entity.Property(e => e.NetoGravado)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("netoGravado");

                entity.Property(e => e.Monto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK__Venta__idCliente__0A9D95DB");

                entity.HasOne(d => d.IdPagoNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdPago)
                    .HasConstraintName("FK__Venta__idPago__0B91BA14");

                entity.HasOne(d => d.IdPuntoVentaNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdPuntoVenta)
                    .HasConstraintName("FK__Venta__idPuntoVe__2DE6D218");

                entity.HasOne(d => d.IdTipoComprobanteNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdTipoComprobante)
                    .HasConstraintName("FK__Venta__idTipoCom__08B54D69");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Venta__idUsuario__09A971A2");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("observacion");

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("estado");
            });

            modelBuilder.Entity<Tarjeta>(entity =>
            {
                entity.HasKey(e => e.IdTarjeta)
                    .HasName("PK__Tarjeta__C456D53871DCA104");

                entity.ToTable("Tarjeta");

                entity.Property(e => e.IdTarjeta).HasColumnName("idTarjeta");

                entity.Property(e => e.NumeroTarjeta)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("numeroTarjeta");

                entity.Property(e => e.MesTarjeta)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("mesTarjeta");

                entity.Property(e => e.AnioTarjeta)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("anioTarjeta");

                entity.Property(e => e.CodigoSeguridadTarjeta)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigoSeguridadTarjeta");

                entity.Property(e => e.NombreTarjeta)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombreTarjeta");
            });

            modelBuilder.Entity<AFIP>(entity =>
            {
                entity.HasKey(e => e.IdAFIP)
                    .HasName("PK__AFIP__EC87626F6D9F50E7");

                entity.ToTable("AFIP");

                entity.Property(e => e.IdAFIP).HasColumnName("idAFIP");

                entity.Property(e => e.Token)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("token");

                entity.Property(e => e.Cae)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("cae");

                entity.Property(e => e.VencimientoToken)
                    .HasColumnType("datetime2")
                    .HasColumnName("vencimientoToken")
                    .HasDefaultValueSql("(getdate())");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
