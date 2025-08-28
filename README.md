# 📡 Backend API – Proyecto Basket

Está desarrollado en **.NET 8 (C#)** y corre dentro de un contenedor Docker.

---

## 🚀 Cómo levantar el API

### 1. Con Docker (recomendado)
Asegúrate de tener Docker instalado y corre:

```bash
# Construir la imagen
docker build -t backend-api .

# Levantar el contenedor en el puerto 8080
docker run -d --name basketapi -p 8080:8080 backend-api

📦 Dependencias y contenedores relacionados

SQL Server 2022
Imagen: mcr.microsoft.com/mssql/server:2022-latest
Puerto: 1433
Usado como base de datos principal del proyecto.



🔗 Endpoints principales

GET /api/matches → Lista partidos.

POST /api/matches → Registra partido nuevo.
