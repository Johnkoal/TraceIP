# TraceIP

Prueba Técnica Mercado Libre

Creado en .Net 8.0 - Microsoft Visual Studio Community 2022 (64-bit) (Version 17.11.5)

# **Pasos para ejecutar el proyecto**

1. Clonar el repositorio en un directorio local
2. Comando para clonar el repositorio `git clone https://github.com/Johnkoal/TraceIP.git`

   Debería descargar el siguiente contenido   
   ![image](https://github.com/user-attachments/assets/8c17c838-8b91-4fb1-ad96-52d8aece663e)

4. Tener Docker activo en la máquina de prueba

5. Abrir un terminal e ir a la carpeta de descarga

   ![image](https://github.com/user-attachments/assets/6b087fc2-0ee7-4703-ac3c-ef6bb181d5ff)

6. Ejecutar el archivo  `.\deploy.cmd`  para crear las imágenes de Docker (si está en windows, sino abrir el archivo y ejecutar cada línea manualmente)

   ![image](https://github.com/user-attachments/assets/9400df09-2a8b-4ea5-95f0-54965d0e9798)

7. ejecutar el comando:  `docker-compose up -d`  para crear los contenedores, ya tiene la configuración necesaria y debe aparecer algo así

   ![image](https://github.com/user-attachments/assets/349eb97c-d7a6-4c68-a979-4a285c44b14e)

8. Los dos contenedores están expuestos al host por los puertos 8080 y 8081, las rutas quedarían así:

   Api: http://localhost:8080/swagger/index.html  (se deja swagger habilitado para fines de pruebas)

   Website: http://localhost:8081/    



# **Uso de la plataforma**

1. Abrir el [Sitio web](http://localhost:8081/), ingresar una IP válida y dar en `Consultar`

   ![image](https://github.com/user-attachments/assets/d1f84331-e80b-45cb-9e50-0b7f301f9125)

   ![image](https://github.com/user-attachments/assets/02f61732-ba7c-4848-abef-88fe19f018a7)
 
2. Ir al módulo de Resumen, se debería cargar el resumen de consultas de IP's realizadas, agrupadas por País (Ya que pueden existir muchas consultas realizadas por un País, la `Distancia promedio` que se muestra es el promedio de estas consultas por País.

   ![image](https://github.com/user-attachments/assets/cca91607-f216-4562-8fba-375ab1b45547)




# **Información técnica**

1. Para el desarrollo de la solución, se utilizó la arquitectura `Domain-Driven Design (DDD) N-Capas` ya que era la más acorde debido a la naturaleza de la solución.

   ![image](https://github.com/user-attachments/assets/3b311d8b-c463-4c9f-957f-8d58ca1c1882)

2. El proyecto quedó así

   ![image](https://github.com/user-attachments/assets/d93283ed-38f2-4284-90b0-0ae79149cb22)

3. Explicación de la capa y su contenido

   **Capa de Infraestructura:** Capa transversal que será vista por todas las capas superiores

   ![image](https://github.com/user-attachments/assets/b1c98da4-d2c7-45e0-8259-9e49f3621e78)

   `AppSections` Clases personalizadas que acceden a las secciones del appsetings.json y permiten ser inyectadas en las clases donde serán usadas

   `Exceptions` Sistema personalizado de excepciones que permite capturar y propagar a las capas superiores en donde hubo algún problema.

   `Logger` Capa personalizada para el uso de logs con la libraría Nlog

   **Capa de Dominio** Capa que define los elementos principales que pertenecen a la lógica del negocio

   ![image](https://github.com/user-attachments/assets/dc9b8be1-32f3-4f46-8d8c-d13061b23951)

   `Entities` Entidades del domino, son las clases que serán utilizadas en la capa de negocio

   `Interfaces` Interfaces o contratos, que serán utilizados en toda la capa de negocio

   **Capa de Acceso a datos** Capa que permite el acceso a los datos (base de datos, servicios externos, etc)

   ![image](https://github.com/user-attachments/assets/244b9775-e4eb-4f81-8f93-be1313d7bbc7)

   `Context` Contexto requerido para el acceso a la base de datos SQLite, la cual se usa para la persistencia de todas las consultas de IP's

   `Entities` Modelos de los orígenes de datos, como, modelos de las tablas para las bases y modelos de los contratos de los servicios externos

   `Repositories` Implementaciones de los contratos indicados por el Dominio para el acceso a los datos, ya sea base de datos o servicios (contiene el código de las consultas)

   **Capa de Aplicación** Contiene la lógica del negocio

   ![image](https://github.com/user-attachments/assets/aacc3994-4e65-46f9-9c59-225c24da65ec)

   `Interfaces` Contratos para funcionalidad que solo será usada dentro de la capa de aplicación y no será compartida con otras capas

   `Services` Son los encargados de contener todo el código de negocio, quien consulta los repositorios, organiza la información y la entrega lista a la capa superior (Api o UI)

   **Capa de Servicios** Capa que valida los datos que llegan y expone la información suministrada por la capa de negocio 

   ![image](https://github.com/user-attachments/assets/1b898a97-d7ff-4287-baf0-fb1944c6328f)

   `Controllers` Contiene los Endpoint expuestos por el Api

   `Entities` Modelos de los contratos de Request y Response de cada uno de los Entpoint

   `Database.db` El sistema usa SQLite, por lo que la base de datos se almacena en un solo archivo y se guarda en la carpeta raíz del Api

   `Dockerfile` Archivo que permite la creación de la imagen Docker del Api

   **Endpoints**

   El Api contiene 3 Endpoints, se detallan a continuación

   ![image](https://github.com/user-attachments/assets/35d1a33a-5759-4792-b07e-ccefd4e71745)

   `(Get) /api/traceip/{ip}` Es el servicio principal que recibe una IP y realiza la respectiva consulta en los servicios de los proveedores, también almacena el registro procesado

   ![image](https://github.com/user-attachments/assets/44d9ca49-2897-4007-b22b-edd8d778371f)

   `(Get) /api/traceip/all` Retorna una lista de todas las consultas realizadas

   ![image](https://github.com/user-attachments/assets/e08b2683-7496-4b79-a553-5205621ee3e2)

   `(Delete) /api/traceip` Si por motivos de pruebas es necesario hacer un Test en blanco, se puede ejecutar este servicio el cual eliminará todos los registros almacenados

   ![image](https://github.com/user-attachments/assets/26dd9659-b312-4c27-aae1-61a1bbe0c911)


   **Capa de Presentación (UI)** Capa que accede al Api, no contiene lógica de negocio fuerte, pricipalmente permite la manipulación de los datos ingresados por el usuario y procesados por Api.

   ![image](https://github.com/user-attachments/assets/7659ede2-895a-4e41-bdf8-a6410b6c7521)

   `Models` Modelos dedatos usados por las vistas. 

   `Pages` Vistas html

   `Dockerfile` Archivo que permite la creación de la imagen Docker del Website

   **Deploy** Archivos para generación de imágenes Docker y creación de los contenedores

   `deploy.cmd`  Archivo que contiene la información para la creación de las imágenes Docker y que permite ser ejecutado por una terminal directamente. Si se está en un sistema operativo Windows, de lo contrario debe ser compiada cada línea y ejecutada directmente un la terminal 

   ![image](https://github.com/user-attachments/assets/51ec6792-1d92-49ea-a8c7-7972c249d794)

   `docker-compose.yml` Permite la creación de los contenedores

   ![image](https://github.com/user-attachments/assets/e6a2d921-b023-48d2-8930-437196506638)
   

   **Explicación de variables**

   Los dos contenedores estarán expuestos al host, esto con el fin de que se puedean hacer pruebas individules tanto al Api como al Website

   Variables de entorno del Api

  - `ASPNETCORE_ENVIRONMENT=Production`
  - `AppSettings__Latitude_Local=-34.6037` Latitud simulada de la ubicación del servidor, Buenos Aires como ejemplo
  - `AppSettings__Longitude_Local=-58.3816`* Longitud simulada de la ubicación del servidor, Buenos Aires como ejemplo
  - `ExternalServices__IpApi_Url=https://api.ipapi.com/api/` Url del Api del proveedor https://ipapi.com/
  - `ExternalServices__IpApi_Key=1f429dda9ca25c3424d0b4e6414b3b93` Clave personal del proveedor https://ipapi.com/
  - `ExternalServices__GeoPlugin_Url=http://www.geoplugin.net/json.gp` Url del Api del proveedor http://www.geoplugin.net
  - `ExternalServices__Fixer_Url=http://data.fixer.io/api/latest` Url del Api del proveedor https://fixer.io/
  - `ExternalServices__Fixer_Key=cbae3a7866686301668b09420a725ae9` Clave personal del proveedor https://fixer.io/
  - `ExternalServices__Timezonedb_Url=https://api.timezonedb.com/v2.1/list-time-zone` Url del Api del proveedor https://timezonedb.com/
  - `ExternalServices__Timezonedb_Key=P3B760D0U8LW` Clave personal del proveedor https://timezonedb.com/

   Variables de entorno del Website

  - `ASPNETCORE_ENVIRONMENT=Production`
  - `AppSettings__UrlApi=http://traceip.api:8080/api/` Url del contenedor del Api, la comunicación se hace interna en la subred creada por Docker para el *docker-compose*
   
