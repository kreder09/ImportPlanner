# Import Planner

Aplicación WinForms desarrollada en C# para asistir en la planificación de compras e importaciones mediante el análisis de consumo histórico, stock disponible e importaciones en tránsito.

## Descripción

Import Planner es una herramienta de soporte a decisiones diseñada para centralizar información proveniente de múltiples sistemas y generar recomendaciones de compra basadas en datos históricos y operativos.

La aplicación integra información desde SQL Server y Oracle para calcular cobertura de stock, analizar tendencias de consumo y visualizar inventario en tránsito, permitiendo planificar compras de forma más eficiente.

---

## Problema

En muchas organizaciones, la planificación de compras requiere consultar múltiples fuentes de información:

- Consumo histórico de artículos.
- Stock disponible.
- Importaciones en tránsito.
- Tiempos de reposición (Lead Time).
- Frecuencia de abastecimiento.

Realizar este análisis manualmente implica invertir tiempo, aumenta el riesgo de errores y dificulta la toma de decisiones.

---

## Solución

Import Planner consolida toda esta información en una única interfaz y genera sugerencias de compra basadas en:

- Consumo histórico.
- Cobertura actual de stock.
- Crecimiento proyectado.
- Lead Time.
- Frecuencia de compra.
- Mercadería actualmente en tránsito.

---

## Funcionalidades

### Gestión de parámetros

- Selección de proveedor.
- Filtrado por familia de artículos.
- Configuración de Lead Time.
- Configuración de frecuencia de abastecimiento.
- Aplicación de porcentaje de crecimiento proyectado.
- Selección de base histórica de cálculo.

### Análisis de consumo

- Comparación de consumo histórico entre años.
- Promedios automáticos.
- Cálculo de tendencias.

### Gestión de inventario

- Consulta de stock disponible.
- Visualización de cobertura en meses.
- Detección de faltantes.

### Importaciones en tránsito

- Consulta automática de órdenes en proceso.
- Visualización de cantidades pendientes de recepción.
- Integración con el cálculo de cobertura.

### Sugerencia de compra

- Cálculo automático de cantidad sugerida.
- Consideración de stock actual e importaciones pendientes.

---

## Tecnologías utilizadas

- C#
- .NET Framework
- WinForms
- SQL Server
- Oracle Database
- ADO.NET
- Repository Pattern

---

## Arquitectura

El proyecto se encuentra organizado en capas para separar responsabilidades:


ImportPlanner
│
├── Formularios
│   └── frmPlanificador.cs
│
└── ImportPlanner.Datos
    │
    ├── Conexion
    │   ├── ConexionSqlServer.cs
    │   └── ConexionOracle.cs
    │
    ├── Modelos
    │   ├── ConsumoVentana.cs
    │   └── ParametrosPlanificacion.cs
    │
    └── Repositorios
        ├── ConsumoRepository.cs
        ├── ImportacionesRepository.cs
        ├── ProveedorRepository.cs
        └── StockRepository.cs


### Componentes principales

- ConsumoRepository
- StockRepository
- ImportacionesRepository
- ProveedorRepository
- ConexionSqlServer
- ConexionOracle

---

## Optimizaciones implementadas

### Carga masiva de stock

Originalmente la aplicación realizaba una consulta Oracle por cada artículo procesado.

Se refactorizó el acceso a datos para obtener todos los stocks mediante una única consulta agregada.

**Resultado:**

- Reducción significativa de consultas a base de datos.
- Menor tiempo de ejecución.
- Menor carga sobre Oracle.

---

## Capturas

### Pantalla principal

_Agregar captura de configuración de parámetros._

### Resultados de planificación

_Agregar captura del análisis generado._

### Cobertura de stock

_Agregar captura destacando el sistema de colores._

---

## Configuración

Crear las cadenas de conexión en `App.config`.

Ejemplo:

```xml
<connectionStrings>

  <add
    name="ERP"
    connectionString="Server=SERVIDOR;Database=ERP;Integrated Security=True;" />

  <add
    name="WMS"
    connectionString="Data Source=SERVIDOR;User Id=usuario;Password=password;" />

</connectionStrings>
```

---

## Aprendizajes obtenidos

Durante el desarrollo de este proyecto se trabajó en:

- Integración entre SQL Server y Oracle.
- Optimización de consultas.
- Diseño de herramientas de soporte a decisiones.
- Refactorización y mantenimiento de aplicaciones WinForms.
- Separación de responsabilidades mediante repositorios.
- Análisis de requerimientos de negocio.

---

## Autor

**Federico Frustacci**

Analista de Sistemas en formación, enfocado en desarrollo de software, bases de datos y soluciones empresariales.
