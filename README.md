# TraceIP

Prueba Técnica Mercado Libre

Creado en .Net 8.0 - Microsoft Visual Studio Community 2022 (64-bit) (Version 17.11.5)

Pasos para ejecutar el proyecto

1. Clonar el repoitorio en un directorio local
2. Url del repositorio https://github.com/Johnkoal/TraceIP.git
   Debería descargar el siguiente contenido
   
   ![image](https://github.com/user-attachments/assets/8c17c838-8b91-4fb1-ad96-52d8aece663e)

4. Tener Docker activo en la máquina de prueba

5. Abrir un terminal e ir a la carpeta de descarga

   ![image](https://github.com/user-attachments/assets/6b087fc2-0ee7-4703-ac3c-ef6bb181d5ff)

6. Ejecutar el archivo  *.\deploy.cmd*  para crear las imágenes de Docker (si está en windows, sino abrir el archivo y ejecutar cada línea manualmente)

   ![image](https://github.com/user-attachments/assets/9400df09-2a8b-4ea5-95f0-54965d0e9798)

7. ejecutar el comando:  *docker-compose up -d*  para crear los contenedores, ya tiene la configuración necesaria y debe aparecer algo así

   ![image](https://github.com/user-attachments/assets/349eb97c-d7a6-4c68-a979-4a285c44b14e)

8. Los dos contenedores están expuestos al host por los puertos 8080 y 8081, las rutasquedarían así:

   Api: http://localhost:8080/swagger/index.html  (se deja swagger habilitado para fines de pruebas)

   Web: http://localhost:8081/    



Uso de la plataforma

1. Abrir el sitio web, ingresar una IP válida y dar en consultar

   ![image](https://github.com/user-attachments/assets/d1f84331-e80b-45cb-9e50-0b7f301f9125)

   ![image](https://github.com/user-attachments/assets/02f61732-ba7c-4848-abef-88fe19f018a7)
 
2. Ir al módulo de Resumen, se debería cargar el resumen de consultas de IP's realizadas, agrupadas por País (Ya que pueden existir muchas consultas realizadas por un País, la *Distancia promedio* que se muestra en un promedio de estas consultas del País.

   ![image](https://github.com/user-attachments/assets/6b80834c-cc01-4211-80cf-d1c0778fcba9)



Información técnica

1. Para el desarrollo de la solución, se utilizó la arquitectura *Domain-Driven Design (DDD) N-Capas* ya que era la más acorde debido a la naturaleza de la solución.

   ![image](https://github.com/user-attachments/assets/3b311d8b-c463-4c9f-957f-8d58ca1c1882)

2. El proyecto queda así

   ![image](https://github.com/user-attachments/assets/d93283ed-38f2-4284-90b0-0ae79149cb22)

3. Explicación de cada capa

   Infraestructura: Capa transversal que será vista por todas las capas superiores

   ![image](https://github.com/user-attachments/assets/b1c98da4-d2c7-45e0-8259-9e49f3621e78)

   **AppSections:** Clases personalizadas que acceden a las secciones del appsetings.json, y permiten ser inyectadas en las clases donde serán usadas

   **Exceptions:** Sistema personalizado de excepciones que permite capturar y propagar a las capas superiores en donde hubo algún problema.

   **Logger:** Capa personalizada para el uso de logs con la libraría Nlog

   
   
5. sdf
6. sdf
7. sdf
8. 


   


