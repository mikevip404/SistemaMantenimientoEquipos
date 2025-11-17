# 🛠️ Sistema de Mantenimiento de Equipos

Proyecto académico desarrollado con **ASP.NET Core MVC**, **Entity Framework Core**, **Identity**, **Bootstrap 5** y **SQL Server en SmarterASP.NET**.

Este sistema permite gestionar equipos, reportes de mantenimiento y control de usuarios mediante roles (Administrador, Coordinador y Usuario).  
Incluye un dashboard visual con estadísticas dinámicas y diseño moderno tipo panel administrativo.

---

## 🚀 Tecnologías utilizadas

- **ASP.NET Core 8 MVC**
- **Entity Framework Core**
- **SQL Server (SmarterASP.NET)**
- **Identity Framework (Autenticación y Roles)**
- **Bootstrap 5 & Bootstrap Icons**
- **C#**
- **HTML, CSS, Razor**

---

## 🎯 Objetivo del sistema

Crear una plataforma para la gestión de mantenimiento de equipos, permitiendo:

- Registrar, editar y eliminar equipos.
- Registrar y visualizar reportes técnicos.
- Control de accesos mediante roles.
- Estadísticas de mantenimiento.
- Diseño moderno y responsive.

---

# 👤 Roles del sistema

## **1. Administrador (rol más alto)**  
- Puede hacer **todo en el sistema**  
- CRUD de Equipos  
- CRUD de Reportes  
- Acceso completo al Dashboard  
- Gestionar Usuarios  
- **Asignar roles a otros usuarios (único rol que puede hacerlo)**  
- Crear o ascender Coordinadores  
- Crear o ascender Usuarios  

👉 Es único y solo debe existir uno por razones de seguridad.

---

## **2. Coordinador**
- Puede realizar **todas las funciones técnicas y administrativas**, excepto una:  
❌ **No puede asignar roles a los usuarios**  
✔ CRUD Equipos  
✔ CRUD Reportes  
✔ Ver Dashboard  

---

## **3. Usuario (rol por defecto)**
Cuando alguien se registra, automáticamente queda como:
Permisos:
✔ Crear reportes  
✔ Ver sus propios reportes  
✔ Ver listado de equipos  
❌ No puede editar equipos  
❌ No puede asignar roles  

---

## **🎯 Ejemplo Práctico**
1. **Juan registra primero** → Obtiene rol `Admin`
2. **María se registra** → Obtiene rol `Usuario` automáticamente
3. **Pedro se registra** → Obtiene rol `Usuario` automáticamente
4. **Juan (Admin) puede cambiar** a María o Pedro al rol `Coordinador`
