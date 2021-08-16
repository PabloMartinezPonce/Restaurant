using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public virtual DbSet<Configuracionsistema> Configuracionsistemas { get; set; }
        public virtual DbSet<Cuenta> Cuentas { get; set; }
        public virtual DbSet<Mesa> Mesas { get; set; }
        public virtual DbSet<Permiso> Permisos { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Proveedore> Proveedores { get; set; }
        public virtual DbSet<RelRolesPermiso> RelRolesPermisos { get; set; }
        public virtual DbSet<Reportemovimiento> Reportemovimientos { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Venta> Ventas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;database=restaurante;port=3306;user id=root;password=localhost123", new MySqlServerVersion(new Version(8, 0, 21)));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("categorias");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(150)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Configuracionsistema>(entity =>
            {
                entity.ToTable("configuracionsistema");

                entity.Property(e => e.Id).HasColumnName("id");

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

                entity.HasIndex(e => e.IdEmpleado, "FK_Cuentas_Empleado");

                entity.HasIndex(e => e.IdMesa, "FK_Cuentas_Mesas");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CuentaActiva)
                    .IsRequired()
                    .HasColumnName("cuentaActiva")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaApertura)
                    .HasColumnType("datetime(6)")
                    .HasColumnName("fechaApertura");

                entity.Property(e => e.FechaCierre)
                    .HasColumnType("datetime(6)")
                    .HasColumnName("fechaCierre");

                entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");

                entity.Property(e => e.IdMesa).HasColumnName("idMesa");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cuentas_Empleado");

                entity.HasOne(d => d.IdMesaNavigation)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.IdMesa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cuentas_Mesas");
            });

            modelBuilder.Entity<Mesa>(entity =>
            {
                entity.ToTable("mesas");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(350)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Mesa1)
                    .HasMaxLength(50)
                    .HasColumnName("mesa");
            });

            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.ToTable("permisos");

                entity.Property(e => e.Id).HasColumnName("id");

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

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .HasColumnName("codigo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Descuento)
                    .HasColumnType("decimal(18,2)")
                    .HasColumnName("descuento");

                entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");

                entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");

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

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK_Productos_Categorias1");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdProveedor)
                    .HasConstraintName("FK_Productos_Proveedores1");
            });

            modelBuilder.Entity<Proveedore>(entity =>
            {
                entity.ToTable("proveedores");

                entity.Property(e => e.Id).HasColumnName("id");

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

            modelBuilder.Entity<RelRolesPermiso>(entity =>
            {
                entity.HasKey(e => new { e.IdRol, e.IdPermiso })
                    .HasName("PRIMARY");

                entity.ToTable("rel_roles_permisos");

                entity.HasIndex(e => e.IdPermiso, "FK_Rel_Roles_Permisos_Permisos");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.Property(e => e.IdPermiso).HasColumnName("idPermiso");

                entity.HasOne(d => d.IdPermisoNavigation)
                    .WithMany(p => p.RelRolesPermisos)
                    .HasForeignKey(d => d.IdPermiso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rel_Roles_Permisos_Permisos");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.RelRolesPermisos)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rel_Roles_Permisos_Roles");
            });

            modelBuilder.Entity<Reportemovimiento>(entity =>
            {
                entity.ToTable("reportemovimientos");

                entity.HasIndex(e => e.IdUsuario, "FK_ReporteMovimientos_Usuarios");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime(6)")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

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

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Reportemovimientos)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReporteMovimientos_Usuarios");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(350)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estatus).HasColumnName("estatus");

                entity.Property(e => e.Rol)
                    .HasMaxLength(50)
                    .HasColumnName("rol");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");

                entity.HasIndex(e => e.IdRol, "FK_Usuarios_Roles");

                entity.Property(e => e.Id).HasColumnName("id");

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

                entity.Property(e => e.IdRol).HasColumnName("idRol");

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

                entity.HasIndex(e => e.IdCuenta, "FK_Ventas_Cuentas");

                entity.HasIndex(e => e.IdProducto, "FK_Ventas_Productos");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descuento)
                    .HasColumnType("decimal(18,2)")
                    .HasColumnName("descuento");

                entity.Property(e => e.EstaPagado).HasColumnName("estaPagado");

                entity.Property(e => e.IdCuenta).HasColumnName("idCuenta");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.PrecioVenta)
                    .HasColumnType("decimal(18,2)")
                    .HasColumnName("precioVenta");

                entity.Property(e => e.Unidades).HasColumnName("unidades");

                entity.HasOne(d => d.IdCuentaNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdCuenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ventas_Cuentas");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ventas_Productos");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
