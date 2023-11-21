# Sitio Web de Restaurante con Razor y .NET Core 6

## Descripción

Este proyecto es un sitio web de restaurante construido con Razor Pages y .NET Core 6, utilizando MongoDB como sistema de base de datos. Es una aplicación monolítica que integra la interfaz de usuario, la lógica de negocio y el acceso a datos, optimizada para escenarios que requieren una arquitectura simplificada y una fácil implementación.

## Características

- **Gestión de Menú:** Permite a los usuarios ver y explorar los platos disponibles.
- **Reservas Online:** Los clientes pueden hacer reservas online.
- **Gestión de Pedidos:** Los empleados pueden procesar pedidos y realizar seguimientos.
- **Administración de Usuarios:** Incluye funcionalidades para la administración de usuarios y roles.
- **Reportes:** Generación de reportes de ventas y rendimiento del restaurante.

## Requisitos Previos

Para ejecutar este proyecto, necesitarás:

- [.NET Core 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- Un IDE compatible, como [Visual Studio 2019](https://visualstudio.microsoft.com/vs/) o [Visual Studio Code](https://code.visualstudio.com/)
- [MongoDB](https://www.mongodb.com/try/download/community) instalado y en ejecución en tu máquina local o acceso a un cluster de MongoDB.

## Configuración y Ejecución

1. **Clonar el Repositorio:**
2. git clone [URL-del-repositorio]
cd [Nombre-del-Directorio]


La aplicación estará disponible en `http://localhost:5000` por defecto.

## Estructura del Proyecto

- **Pages:** Contiene las Razor Pages (páginas .cshtml y sus modelos de página correspondientes).
- **Models:** Clases de modelo que representan las entidades de negocio y se mapean a documentos en MongoDB.
- **Data:** Incluye clases para el acceso a datos y manejo de la conexión a MongoDB.
- **Services:** Contiene la lógica de negocio y servicios.
- **wwwroot:** Archivos estáticos como CSS, JavaScript e imágenes.

## Contribuciones

Las contribuciones son bienvenidas. Por favor, lee las [directrices de contribución](CONTRIBUTING.md) para más información.

## Licencia

Este proyecto está bajo [MIT License](LICENSE).
