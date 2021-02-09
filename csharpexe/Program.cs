using System;

/// import F# namespace
using Fsharpdll;


namespace csharpexe
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 1;
            int b = 2;
            int c = math.add(a, b);
            Console.WriteLine($"Add {a:N} + {b:N} = {c:N}");

            // Ejemplo de nueva instancia de clase asociada a un record de F#
            Plan.Plan planCompleto = new Plan.Plan("Prostata",Status.Status.Completed);             
            Plan.Plan planSinAprobar = new Plan.Plan("Mama",Status.Status.Completed);

            // También se puede instanciar un objeto de C# a partir de un record de F#
            Plan.Plan planSinAprobarFSharp = Plan.planSinAprobar;

            var resultOk = Plan.ValidatePlan(planSinAprobarFSharp);
            Console.WriteLine ($"resultado (sin aprobar) = {resultOk.ErrorValue} IsOk: {resultOk.IsOk}  IsError: {resultOk.IsError}");

            var resultError = Plan.ValidatePlan(planCompleto);
            Console.WriteLine ($"resultado (aprobado   ) = {resultError.ResultValue} IsOk: {resultError.IsOk}  IsError: {resultError.IsError}");


            // var texto = result.ResultValue switch {
            //      "Completed" => "Completado",
            //      "Unapproved" => "Sin Aprobar",
            //      _ => "Null"
            // };
            // Console.WriteLine ($"resultado = {texto}");

            // Definición de una variable status a partir de la DU de FSharp
            Status.Status status = Status.Status.UnApproved;

        }
    }
}
