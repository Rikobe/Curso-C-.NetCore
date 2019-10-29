using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using CoreEscuela.Util;

namespace CoreEscuela
{
    public sealed class EscuelaEngine
    {
        public Escuela Escuela { get; set; }

        public EscuelaEngine()
        {

        }

        public void Inicializar()
        {
            Escuela = new Escuela("Platzi Academya", 2019, TiposEscuela.Primaria,
            ciudad: "Los Mochis", pais: "Sinaloa"
            );

            CargarCursos();
            CargarAsignaturas();
            CargarEvaluaciones();  
        }

        public void ImprimirDiccionario(Dictionary<LlaveDiccionario,IEnumerable<ObjetoEscuelaBase>> dic, bool imprEval)
        {
            foreach (var objdic in dic)
            {
                Printer.WriteTitle(objdic.Key.ToString());

                foreach (var val in objdic.Value)
                {
                    switch (objdic.Key)
                    {
                        case LlaveDiccionario.Evaluacion: 
                            if (imprEval)
                            {
                                Console.WriteLine(val);
                            }
                        break;

                        case LlaveDiccionario.Escuela:
                            Console.WriteLine("Escuela" + val);
                        break;

                        case LlaveDiccionario.Alumno:
                            Console.WriteLine("Alumno: " + val.Nombre);
                        break;

                        case LlaveDiccionario.Curso:
                            var cursotmp = val as Curso;
                            if (cursotmp != null)
                            {
                                int count = cursotmp.Alumnos.Count;
                                Console.WriteLine("Curso: " + val.Nombre + " Cantidad Alumnos: " + count);
                            }
                            
                        break;

                        default: 
                            Console.WriteLine(val);
                        break;
                    }
                }
            }
        }

        public Dictionary<LlaveDiccionario,IEnumerable<ObjetoEscuelaBase>> GetDiccionarioObjetos()
        {
            var diccionario = new Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>>();

            diccionario.Add(LlaveDiccionario.Escuela, new[] {Escuela});
            diccionario.Add(LlaveDiccionario.Curso, Escuela.Cursos.Cast<ObjetoEscuelaBase>());
            var listatmpev = new List<Evaluación>();
            var listatmpas = new List<Asignatura>();
            var listatmpal = new List<Alumno>();
            foreach (var curso in Escuela.Cursos)
            {
                listatmpas.AddRange(curso.Asignaturas);
                listatmpal.AddRange(curso.Alumnos);
        
               foreach (var alumno in curso.Alumnos)
               {
                   listatmpev.AddRange(alumno.Evaluaciones);
               }
               
            }
            diccionario[LlaveDiccionario.Asignatura] = listatmpas.Cast<ObjetoEscuelaBase>();
            diccionario[LlaveDiccionario.Alumno] = listatmpal.Cast<ObjetoEscuelaBase>();
            diccionario[LlaveDiccionario.Evaluacion] = listatmpev.Cast<ObjetoEscuelaBase>();
            
            return diccionario;
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela
        (   out int conteoEvaluaciones, out int conteoCursos, out int conteoAsignaturas, out int conteoAlumnos, 
            bool traeEvaluaciones = true, bool traeAlumnos = true, bool traeAsignaturas = true, bool traeCursos = true)
        {
            conteoAlumnos = conteoAsignaturas = conteoEvaluaciones = 0;

            var listaObj = new List<ObjetoEscuelaBase>();
            listaObj.Add(Escuela);
            
            if (traeCursos)
                listaObj.AddRange(Escuela.Cursos);
            
            conteoCursos = Escuela.Cursos.Count;

            foreach (var curso in Escuela.Cursos)
            {
                conteoAsignaturas += curso.Asignaturas.Count;
                conteoAlumnos += curso.Alumnos.Count;

                if (traeAsignaturas)
                    listaObj.AddRange(curso.Asignaturas);
                
                if (traeAlumnos)
                    listaObj.AddRange(curso.Alumnos);
                
                
                if (traeEvaluaciones)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        listaObj.AddRange(alumno.Evaluaciones);
                        conteoEvaluaciones += alumno.Evaluaciones.Count;
                    }
                }
                
            }

            return listaObj.AsReadOnly();
        }

        #region Métodos de carga
        private void CargarEvaluaciones()
        {
            var rnd = new Random();

            foreach (var curso in Escuela.Cursos)
            {
                foreach (var asignatura in curso.Asignaturas)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        
                        for (int i = 0; i < 5; i++)
                        {
                            var ev = new Evaluación
                            {
                                Asignatura = asignatura,
                                Nombre = $"{asignatura.Nombre} Ev#{i + 1}",
                                Nota = MathF.Round(5 * (float)rnd.NextDouble(), 2),
                                Alumno = alumno
                            };
                            alumno.Evaluaciones.Add(ev);
                        }
                    }
                }
            }

        }

        private void CargarAsignaturas()
        {
            foreach (var curso in Escuela.Cursos)
            {
                var listaAsignaturas = new List<Asignatura>(){
                            new Asignatura{Nombre="Matemáticas"} ,
                            new Asignatura{Nombre="Educación Física"},
                            new Asignatura{Nombre="Castellano"},
                            new Asignatura{Nombre="Ciencias Naturales"}
                };
                curso.Asignaturas = listaAsignaturas;
            }
        }

        private List<Alumno> GenerarAlumnosAlAzar(int cantidad)
        {
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno { Nombre = $"{n1} {n2} {a1}" };

            return listaAlumnos.OrderBy((al) => al.UniqueId).Take(cantidad).ToList();
        }

        private void CargarCursos()
        {
            Escuela.Cursos = new List<Curso>(){
                        new Curso(){ Nombre = "101", Jornada = TiposJornada.Mañana },
                        new Curso() {Nombre = "201", Jornada = TiposJornada.Mañana},
                        new Curso{Nombre = "301", Jornada = TiposJornada.Mañana},
                        new Curso(){ Nombre = "401", Jornada = TiposJornada.Tarde },
                        new Curso() {Nombre = "501", Jornada = TiposJornada.Tarde},
            };

            Random rnd = new Random();
            foreach (var c in Escuela.Cursos)
            {
                int cantRandom = rnd.Next(5, 20);
                c.Alumnos = GenerarAlumnosAlAzar(cantRandom);
            }
        }
        #endregion
    }
}