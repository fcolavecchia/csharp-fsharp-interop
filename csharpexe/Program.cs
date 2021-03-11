using System;

/// import F# namespace
using Fsharpdll;


namespace csharpexe
{
    class Program
    {
        static void Main(string[] args)
        {

            // One can define a variable from the DU in F#
            Visa.Status studentVisa = Visa.Status.Student;

            // New instances of Person type from F# module
            Person.Person tourist = new Person.Person("John C. Doe", Visa.Status.Tourist);             
            Person.Person student = new Person.Person("Mary C. Eod", studentVisa);

            // One can also instantiate a Person directly from a record in the F# module
            Person.Person defaultStudent = Person.defaultStudent;

            var resultOk = Person.ValidateStudent(defaultStudent);
            Console.WriteLine ($"result (sin aprobar) = {resultOk.ErrorValue} IsOk: {resultOk.IsOk}  IsError: {resultOk.IsError}");

            var resultError = Person.ValidateStudent(tourist);
            Console.WriteLine ($"result (aprobado   ) = {resultError.ResultValue} IsOk: {resultError.IsOk}  IsError: {resultError.IsError}");

            
        }
    }
}
