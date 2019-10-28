# Curso-C-.NetCore
Desarrollo de conocimiento de C# .NetCore 


- Herencia
Se utilizó herencia para simplificar las clases con algunos atributos en común como alumnos, asignaturas, cursos, creando una clase abstracta (no permite instanciarse) que las englobe y estas hereden de dicha clase.

- Polimorfismo
Cuando un objeto (hijo) hereda de otro objeto(padre), el objeto hijo puede tratarse como un objeto padre, pero si el objeto hijo es convertido en objeto padre, ya no puede acceder a los atributos como objeto hijo.
El polimorfismo puede crear problemas que no son tan visibles al usar casting.

- Lista de objetos polimórfica
Se creó una lista de objetos del tipo de objeto padre, es decir, todos los objetos que se enlistaron, heredan de este objeto padre.
Pueden realizarse validaciones utilizando "is" y as". "is" verifica si el objeto señalado es del tipo que se pregunta mientras que "as" trata al objeto como se especifica.
obj is Alumno = ¿Es obj del tipo Alumno?
obj as Alumno = Considera a obj como tipo Alumno.

- Uso e implementación de interfaces
En C# no existe el concepto de herencia múltiple, para esto, se utilizan interfaces que sirven para definir la estructura de un objeto.

- Creación de regiones para comprimir código
Se crearon regiones dentro del código para comprimirlo, es de utilidad para crear conjuntos de código que trabajan conjuntamente.

- Sobrecarga de métodos
La sobrecarga de métodos (override) para crear métodos con más flexibilidad donde el mismo métodos puede tener una cantidad o distintos tipos de parámetros, en este caso se utilizó para omitir parámetros de salida.

- Listas de solo lectura
Las listas de solo lectura ayudan a que una lista no pueda ser modificada al ser utilizada.
Si se desea utilizar una lista que pueda ser utilizada de forma pública, es mejor usar un tipo de lista genérica como IEnumerable.

