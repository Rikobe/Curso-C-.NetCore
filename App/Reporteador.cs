using System;
using System.Linq;
using System.Collections.Generic;
using CoreEscuela.Entidades;

namespace CoreEscuela.App
{
    public class Reporteador
    {
        Dictionary<LlaveDiccionario,IEnumerable<ObjetoEscuelaBase>> _diccionario;
        public Reporteador(Dictionary<LlaveDiccionario,IEnumerable<ObjetoEscuelaBase>> dicObsEsc)
        {
            if (dicObsEsc == null){
                throw new ArgumentNullException(nameof(dicObsEsc));
            }
            else 
            {
                _diccionario = dicObsEsc;   
            }
        }

        public IEnumerable<Evaluación> GetListaEvaluaciones()
        {
            if (_diccionario.TryGetValue(LlaveDiccionario.Evaluacion, out IEnumerable<ObjetoEscuelaBase> lista))
            {
                return lista.Cast<Evaluación>();
            }
            else
            {
                return new List<Evaluación>();
            }
        }

        public IEnumerable<string> GetListaAsignaturas()
        {
            return GetListaAsignaturas(out var dummy);
        }

        public IEnumerable<string> GetListaAsignaturas(out IEnumerable<Evaluación> listaEvaluaciones)
        {
            listaEvaluaciones = GetListaEvaluaciones();

            return (from Evaluación ev in listaEvaluaciones
                    //where ev.Nota >= 3.0f
                    select ev.Asignatura.Nombre).Distinct();
        }

        public Dictionary<string, IEnumerable <Evaluación>> GetDicEvalXAsig() {
            
            Dictionary<string, IEnumerable <Evaluación>> dicRta = new Dictionary<string, IEnumerable <Evaluación>>();

            var listaAsig = GetListaAsignaturas(out var listaEval);

            foreach (var asig in listaAsig)
            {
                var evalsAsig = from eval in listaEval
                                    where eval.Asignatura.Nombre == asig
                                    select eval;
                dicRta.Add(asig, evalsAsig);
            }

            return dicRta;
        }

        public Dictionary<string, IEnumerable<AlumnoPromedio>> GetPromeAlumnoPorAsignatura()
        {
            var rta = new Dictionary<string, IEnumerable<AlumnoPromedio>>();

            var dicEvalXAsig = GetDicEvalXAsig();

            foreach (var asignConEval in dicEvalXAsig)
            {
                var promediosAlumnos = from eval in asignConEval.Value
                            group eval by new { eval.Alumno.UniqueId, eval.Alumno.Nombre }
                            into grupoEvalsAlumno
                            select new AlumnoPromedio
                            {
                                alumnoId = grupoEvalsAlumno.Key.UniqueId,
                                alumnoNombre = grupoEvalsAlumno.Key.Nombre,
                                promedio = grupoEvalsAlumno.Average(evaluacion => evaluacion.Nota)

                            };
                
                rta.Add(asignConEval.Key,promediosAlumnos);
            }

            return rta;
        }

        public Dictionary<string, IEnumerable<AlumnoPromedio>> GetTopPromePorAsignaruta(int cantTop = 3)
        {
            var rta = new Dictionary<string, IEnumerable<AlumnoPromedio>>();

            var dicPromedioXAsignatura = GetPromeAlumnoPorAsignatura();

            foreach (var asigConProm in dicPromedioXAsignatura)
            {
                var topPromedios = (from prom in asigConProm.Value
                                    orderby prom.promedio descending
                                   select prom).Take(cantTop);
                
                rta.Add(asigConProm.Key, topPromedios);
            }

            return rta;
        }
    }
}