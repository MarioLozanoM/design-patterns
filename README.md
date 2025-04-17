# PATRONES DE DISEÑO UTILIZADOS

## Patrones Creacionales

### Singleton

Utilizado para tener sólo una instancia de la conexión a la base de datos, se puede encontrar en el archivo `Data/DatabaseConnection.cs`.

### Builder

Utilizado para construir por etapas la query SQL, se puede encontrar en el archivo `Products/QueryBuilder.cs`.

### Prototype

Utilizado para tener una plantilla de filtro base para los productos, se puede encontrar en el archivo `Products/Filter.cs`.

### Factory Method

Utilizado para construir las diferentes implementaciones del payment gateway según el método de pago, se puede encontrar en el archivo `Payments/Gateway.cs`.

## Patrones Estructurales

### Inyector de Dependencias

Utilizado para inyectar las dependencias de los servicios y repositorios, se puede encontrar en el archivo `Program.cs`.

### Repositorio

Utilizado para separar la lógica de acceso a datos de la lógica de negocio, se puede encontrar en los archivo `Products/Repository.cs` y `Payments/Repository.cs`.

### Adaptador

Utilizado para adaptar la interfaz de los servicios de pago a la interfaz que espera la aplicación, se puede encontrar en el archivo `Payments/Adapter.cs`.

### Flyweight

Utilizado para guardar en cache los datos de los productos, se puede encontrar en el archivo `Products/ProductCache.cs`.

## Patrones de Comportamiento

### Cadena de Responsabilidad

Utilizado para manejar las diferentes etapas de la compra, se puede encontrar en el archivo `Payments/StatesHandler.cs`.

### Estrategia

Utilizado para manejar los diferentes métodos de pago, se puede encontrar en el archivo `Payments/Strategy.cs`.

### Comando

Utilizado para agregar/eliminar productos del carrito, se puede encontrar en el archivo `Cart/Commands.cs`.
