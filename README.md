# GenericSharedLibrary

_Biblioteca compartida para eCommerce basado en microservicios desarrollada en .NET 8.0_

![.NET 8.0](https://img.shields.io/badge/.NET-8.0-purple?logo=dotnet)
![Language](https://img.shields.io/badge/language-C%23-239120?logo=c-sharp)

## ✨ Descripción

**GenericSharedLibrary** es una biblioteca compartida diseñada para proyectos de eCommerce basados en microservicios. Su principal objetivo es estandarizar preocupaciones transversales como:

- 🔒 Seguridad  
- 📋 Logging  
- 💾 Acceso a datos  

Estas funcionalidades ayudan a mantener la **consistencia** y **calidad** en todos los microservicios de la arquitectura, facilitando el desarrollo y el mantenimiento a largo plazo.

---

## 🚀 Características principales

- **Autenticación y Autorización** centralizadas
- **Logging** estructurado y configurable
- **Manejo de excepciones** uniforme
- **Acceso a datos** usando patrones modernos de .NET 8.0
- **Integración sencilla** con microservicios existentes
- **Configuración flexible** mediante archivos y variables de entorno

---

## 🏗️ Instalación

1. Asegúrate de tener [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) instalado.
2. Agrega la referencia del proyecto a tu solución:

```bash
dotnet add reference ../GenericSharedLibrary/GenericSharedLibrary.csproj
```
O instala el paquete desde tu repositorio NuGet privado si aplica.

3. Configura las dependencias requeridas en tu microservicio.

---

## ⚡ Uso básico

```csharp
using GenericSharedLibrary.Logging;
using GenericSharedLibrary.Security;
using GenericSharedLibrary.Data;

// Ejemplo de inicialización y uso:
var logger = LoggerFactory.CreateLogger("MyService");
logger.LogInfo("Servicio iniciado correctamente.");

var user = AuthService.Authenticate(token);
if (user.IsAuthorized("Admin"))
{
    // Lógica protegida
}

var dbContext = DataContextFactory.Create();
var items = dbContext.GetItems();
```

---

## 📦 Estructura del proyecto

```
GenericSharedLibrary/
├── Logging/
├── Security/
├── Data/
├── Extensions/
└── README.md
```

---

## 📝 Contribuciones

¡Las contribuciones son bienvenidas! Por favor, abre un [issue](https://github.com/BitterSweetBoy/GenericSharedLibrary/issues) o envía un pull request para sugerencias, mejoras o reporte de bugs.

---

## 🛡️ Licencia

Este proyecto está bajo la licencia MIT. Consulta el archivo [LICENSE](LICENSE) para más información.

---

## 🌐 Autor

**BitterSweetBoy**  
[GitHub](https://github.com/BitterSweetBoy)

---

Si necesitas ayuda o tienes preguntas, no dudes en abrir un issue. ¡Gracias por usar **GenericSharedLibrary** y contribuir a la estandarización de microservicios en .NET!
