using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Restaurante.Data.DBModels;

#nullable disable

namespace Restaurant.Web
{
    public partial class restauranteContext : DbContext
    {
        public restauranteContext()
        {
        }

        public restauranteContext(DbContextOptions<restauranteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Complemento> Complementos { get; set; }
        public virtual DbSet<Configuracionsistema> Configuracionsistemas { get; set; }
        public virtual DbSet<Cuenta> Cuentas { get; set; }
        public virtual DbSet<Mesa> Mesas { get; set; }
        public virtual DbSet<Permiso> Permisos { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Proveedore> Proveedores { get; set; }
        public virtual DbSet<RelCuentaProducto> RelCuentaProductos { get; set; }
        public virtual DbSet<RelProductoComplemento> RelProductoComplementos { get; set; }
        public virtual DbSet<RelRolesPermiso> RelRolesPermisos { get; set; }
        public virtual DbSet<Reportemovimiento> Reportemovimientos { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Tipocomplemento> Tipocomplementos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Venta> Ventas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=MYSQL5035.site4now.net;database=db_a78b82_lpb;port=3306;user id=a78b82_lpb;password=LPB2021.;SslMode=none", new MySqlServerVersion(new Version(8, 0, 21)));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("categorias");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(150)
                    .HasColumnName("nombre");

                entity.Property(e => e.RutaImagen)
                    .HasMaxLength(150)
                    .HasColumnName("rutaImagen");
            });

            modelBuilder.Entity<Complemento>(entity =>
            {
                entity.ToTable("complementos");

                entity.HasIndex(e => e.IdTipoComplemento, "FK_TipoComplemento_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.IdTipoComplemento)
                    .HasColumnType("int(11)")
                    .HasColumnName("idTipoComplemento");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(10,2)")
                    .HasColumnName("precio");

                entity.Property(e => e.RutaImagen)
                    .HasMaxLength(100)
                    .HasColumnName("rutaImagen");

                entity.HasOne(d => d.IdTipoComplementoNavigation)
                    .WithMany(p => p.Complementos)
                    .HasForeignKey(d => d.IdTipoComplemento)
                    .HasConstraintName("FK_TipoComplemento");
            });

            modelBuilder.Entity<Configuracionsistema>(entity =>
            {
                entity.ToTable("configuracionsistema");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Llave)
                    .HasMaxLength(100)
                    .HasColumnName("llave");

                entity.Property(e => e.Valor)
                    .HasMaxLength(100)
                    .HasColumnName("valor");
            });

            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.ToTable("cuentas");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CantidadPersonas)
                    .HasColumnType("int(11)")
                    .HasColumnName("cantidadPersonas");

                entity.Property(e => e.CuentaActiva)
                    .IsRequired()
                    .HasColumnName("cuentaActiva")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaApertura).HasColumnName("fechaApertura");

                entity.Property(e => e.FechaCierre).HasColumnName("fechaCierre");

                entity.Property(e => e.IdEmpleado)
                    .HasColumnType("int(11)")
                    .HasColumnName("idEmpleado");

                entity.Property(e => e.IdMesa)
                    .HasColumnType("int(11)")
                    .HasColumnName("idMesa");

                entity.Property(e => e.Propina)
                    .HasColumnType("decimal(10,2)")
                    .HasColumnName("propina");
            });

            modelBuilder.Entity<Mesa>(entity =>
            {
                entity.ToTable("mesas");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(350)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Fechareservada).HasColumnName("fechareservada");

                entity.Property(e => e.Ocupada)
                    .HasColumnName("ocupada")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Reservada)
                    .HasColumnName("reservada")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RutaImagen)
                    .HasMaxLength(100)
                    .HasColumnName("rutaImagen");

                entity.Property(e => e.TipoLugar)
                    .HasMaxLength(45)
                    .HasColumnName("tipoLugar");
            });

            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.ToTable("permisos");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(350)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Permiso1)
                    .HasMaxLength(50)
                    .HasColumnName("permiso");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("productos");

                entity.HasIndex(e => e.IdCategoria, "FK_Productos_Categorias1");

                entity.HasIndex(e => e.IdProveedor, "FK_Productos_Proveedores1");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Cantidad)
                    .HasColumnType("int(11)")
                    .HasColumnName("cantidad");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Complementos)
                    .HasMaxLength(2000)
                    .HasColumnName("complementos");

                entity.Property(e => e.ComplementosSelect)
                    .HasMaxLength(2000)
                    .HasColumnName("complementosSelect");

                entity.Property(e => e.Descuento)
                    .HasColumnType("decimal(18,2)")
                    .HasColumnName("descuento");

                entity.Property(e => e.IdCategoria)
                    .HasColumnType("int(11)")
                    .HasColumnName("idCategoria");

                entity.Property(e => e.IdProveedor)
                    .HasColumnType("int(11)")
                    .HasColumnName("idProveedor");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(150)
                    .HasColumnName("nombre");

                entity.Property(e => e.PrecioCosto)
                    .HasColumnType("decimal(18,2)")
                    .HasColumnName("precioCosto");

                entity.Property(e => e.PrecioVenta)
                    .HasColumnType("decimal(18,2)")
                    .HasColumnName("precioVenta");

                entity.Property(e => e.RutaImagen)
                    .HasMaxLength(150)
                    .HasColumnName("rutaImagen");

                entity.Property(e => e.Stock)
                    .HasColumnType("int(11)")
                    .HasColumnName("stock");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<Proveedore>(entity =>
            {
                entity.ToTable("proveedores");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Compania)
                    .HasMaxLength(150)
                    .HasColumnName("compania");

                entity.Property(e => e.Correo)
                    .HasMaxLength(150)
                    .HasColumnName("correo");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(250)
                    .HasColumnName("direccion");

                entity.Property(e => e.NombreContacto)
                    .HasMaxLength(150)
                    .HasColumnName("nombreContacto");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(150)
                    .HasColumnName("telefono");
            });

            modelBuilder.Entity<RelCuentaProducto>(entity =>
            {
                entity.ToTable("rel_cuenta_productos");

                entity.HasIndex(e => e.IdCuenta, "FK_Cuenta_idx");

                entity.HasIndex(e => e.IdProducto, "FK_Producto_idx");

                entity.HasIndex(e => e.IdCuenta, "FK_Rel_Cuenta_idx");

                entity.HasIndex(e => e.IdProducto, "FK_Rel_Producto_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Cantidad)
                    .HasMaxLength(45)
                    .HasColumnName("cantidad");

                entity.Property(e => e.Descuento)
                    .HasMaxLength(45)
                    .HasColumnName("descuento");

                entity.Property(e => e.IdCuenta)
                    .HasColumnType("int(11)")
                    .HasColumnName("idCuenta");

                entity.Property(e => e.IdProducto)
                    .HasColumnType("int(11)")
                    .HasColumnName("idProducto");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(45)
                    .HasColumnName("nombre");

                entity.Property(e => e.Precio)
                    .HasMaxLength(45)
                    .HasColumnName("precio");

                entity.HasOne(d => d.IdCuentaNavigation)
                    .WithMany(p => p.RelCuentaProductos)
                    .HasForeignKey(d => d.IdCuenta)
                    .HasConstraintName("FK_Rel_Cuenta");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.RelCuentaProductos)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK_Rel_Producto");
            });

            modelBuilder.Entity<RelProductoComplemento>(entity =>
            {
                entity.ToTable("rel_producto_complemento");

                entity.HasIndex(e => e.IdComplemento, "FK_Complemento_idx");

                entity.HasIndex(e => e.IdProducto, "FK_Producto_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.IdComplemento)
                    .HasColumnType("int(11)")
                    .HasColumnName("idComplemento");

                entity.Property(e => e.IdProducto)
                    .HasColumnType("int(11)")
                    .HasColumnName("idProducto");

                entity.HasOne(d => d.IdComplementoNavigation)
                    .WithMany(p => p.RelProductoComplementos)
                    .HasForeignKey(d => d.IdComplemento)
                    .HasConstraintName("FK_Complemento");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.RelProductoComplementos)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK_Producto");
            });

            modelBuilder.Entity<RelRolesPermiso>(entity =>
            {
                entity.HasKey(e => new { e.IdRol, e.IdPermiso })
                    .HasName("PRIMARY");

                entity.ToTable("rel_roles_permisos");

                entity.Property(e => e.IdRol)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idRol");

                entity.Property(e => e.IdPermiso)
                    .HasColumnType("int(11)")
                    .HasColumnName("idPermiso");
            });

            modelBuilder.Entity<Reportemovimiento>(entity =>
            {
                entity.ToTable("reportemovimientos");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Fecha).HasColumnName("fecha");

                entity.Property(e => e.IdUsuario)
                    .HasColumnType("int(11)")
                    .HasColumnName("idUsuario");

                entity.Property(e => e.Modulo)
                    .HasMaxLength(50)
                    .HasColumnName("modulo");

                entity.Property(e => e.RegistroAfectado)
                    .HasMaxLength(150)
                    .HasColumnName("registroAfectado");

                entity.Property(e => e.TipoMovimiento)
                    .HasMaxLength(50)
                    .HasColumnName("tipoMovimiento");

                entity.Property(e => e.ValorActual)
                    .HasMaxLength(0)
                    .HasColumnName("valorActual");

                entity.Property(e => e.ValorAnterior)
                    .HasMaxLength(0)
                    .HasColumnName("valorAnterior");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(350)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estatus).HasColumnName("estatus");

                entity.Property(e => e.Rol)
                    .HasMaxLength(50)
                    .HasColumnName("rol");
            });

            modelBuilder.Entity<Tipocomplemento>(entity =>
            {
                entity.ToTable("tipocomplemento");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(75)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");

                entity.HasIndex(e => e.IdRol, "FK_Usuarios_Roles");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(150)
                    .HasColumnName("apellido");

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(150)
                    .HasColumnName("contrasena");

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(100)
                    .HasColumnName("correoElectronico");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(450)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(250)
                    .HasColumnName("direccion");

                entity.Property(e => e.Estatus).HasColumnName("estatus");
                entity.Property(e => e.Sexo).HasColumnName("sexo");

                entity.Property(e => e.IdRol)
                    .HasColumnType("int(11)")
                    .HasColumnName("idRol");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(150)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(150)
                    .HasColumnName("nombreUsuario");

                entity.Property(e => e.RutaFoto)
                    .HasMaxLength(650)
                    .HasColumnName("rutaFoto");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(10)
                    .HasColumnName("telefono")
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK_Usuarios_Roles");
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.ToTable("ventas");

                entity.HasIndex(e => e.IdCuenta, "FK_Venta_Cuenta_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Descuento)
                    .HasColumnType("decimal(10,2)")
                    .HasColumnName("descuento");

                entity.Property(e => e.Fecha).HasColumnName("fecha");

                entity.Property(e => e.IdCuenta)
                    .HasColumnType("int(11)")
                    .HasColumnName("idCuenta");

                entity.Property(e => e.Metodopago)
                    .HasMaxLength(80)
                    .HasColumnName("metodopago");

                entity.Property(e => e.Propina)
                    .HasColumnType("decimal(10,2)")
                    .HasColumnName("propina");

                entity.Property(e => e.Subtotal)
                    .HasColumnType("decimal(10,2)")
                    .HasColumnName("subtotal");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(10,2)")
                    .HasColumnName("total");

                entity.HasOne(d => d.IdCuentaNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdCuenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Venta_Cuenta");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
