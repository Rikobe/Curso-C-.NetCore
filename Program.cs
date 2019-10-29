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
            var listasPromTop = reporteador.GetTopPromePorAsignaruta();

            Printer.WriteTitle("Captura de una evaluación por Consola");
            var newEval = new Evaluación();
            string nombre, notastring;

            Console.WriteLine("Ingrese el nombre de la evaluación");
            Printer.PresioneENTER();
            nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Printer.WriteTitle("El valor del nombre no puede ser vacío");
                WriteLine("Saliendo del programa");
            }
            else
            {
                newEval.Nombre = nombre.ToLower();
                WriteLine("El nombre de la evaluación ha sido ingresado correctamente");
            }

            Console.WriteLine("Ingrese la nota de la evaluación");
            Printer.PresioneENTER();
            notastring = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(notastring))
            {
                Printer.WriteTitle("El valor de la nota no puede ser vacío");
                WriteLine("Saliendo del programa");
            }
            else
            {
                try
                {
                    newEval.Nota = float.Parse(notastring);
                    if (newEval.Nota < 0 || newEval.Nota > 5)
                    {
                        throw new ArgumentOutOfRangeException("La nota debe estar entre 0 y 5");
                    }
                    WriteLine("La nota de la evaluación ha sido ingresada correctamente.");
                }
                catch (ArgumentOutOfRangeException arge)
                {
                    Printer.WriteTitle(arge.Message);
                    WriteLine("Saliendo del programa");
                    
                }
                catch (Exception)
                {
                    Printer.WriteTitle("El valor de la nota no es un número válido");
                    WriteLine("Saliendo del programa");
                }
                
            }
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
