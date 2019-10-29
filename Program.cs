using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.App;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");
            //Printer.Beep(10000, cantidad: 10);

            //int dummy = 0;
            // var listaObjetos = engine.GetObjetosEscuela(out int conteoEvaluaciones, out int conteoCursos, 
            // out int conteoAsignaturas, out int conteoAlumnos, true, false, false ,false);

            // var listaLugar = from obj in listaObjetos where obj is ILugar select (ILugar) obj;
            // //engine.Escuela.LimpiarLugar();
            // var dictmp = engine.GetDiccionarioObjetos();

            // engine.ImprimirDiccionario(dictmp, true);

            var reporteador = new Reporteador(engine.GetDiccionarioObjetos());
            var evalList = reporteador.GetListaEvaluaciones();
            var evalAsignaturas = reporteador.GetListaAsignaturas();
            var listaEvalXAsig = reporteador.GetDicEvalXAsig();
            var listaPromXAsig = reporteador.GetPromeAlumnoPorAsignatura();
        }

        private static void ImpimirCursosEscuela(Escuela escuela)
        {

            Printer.WriteTitle("Cursos de la Escuela");

            if (escuela?.Cursos != null)
            {
                foreach (var curso in escuela.Cursos)
                {
                    WriteLine($"Nombre {curso.Nombre  }, Id  {curso.UniqueId}");
                }
            }
        }
    }
}
