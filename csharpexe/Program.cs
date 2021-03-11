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
            // Person.Person student = new Person.Person("Mary C. Eod", studentVisa);

            // One can also instantiate a Person directly from a record in the F# module
            Person.Person defaultStudent = Person.defaultStudent;

            // Call to ValidateStudent function in F#
            var resultOk = Person.ValidateStudent(defaultStudent);

            // C# sets the member .IsOk to True and .IsError to False
            //    when ValidateStudent (returns Ok Person.Person)  
            //    person can be accessed through .ResultValue, 
            //    while .ErrorValue is null

            if(resultOk.IsOk){
                Console.WriteLine ($"{resultOk.ResultValue.Name} is a student");
                Console.WriteLine ($"    resultOk.IsOk   : {resultOk.IsOk}");
                Console.WriteLine ($"    resultOk.IsError: {resultOk.IsError}");
            }

            if(resultOk.ErrorValue is null)
                Console.WriteLine($"    resultOk.ErrorValue is null");

            // On the other hand,
            // C# sets the member .IsOk to False and .IsError to True
            //    when ValidateStudent (returns Error string)  
            //    string can be accessed through .ErrorValue, 
            //    while .ResultValue is null

            var resultError = Person.ValidateStudent(tourist);
            if(resultError.IsError){
                Console.WriteLine ($"{tourist.Name} is a {resultError.ErrorValue}");
                Console.WriteLine ($"    resultOk.IsOk   : {resultOk.IsOk}");
                Console.WriteLine ($"    resultOk.IsError: {resultOk.IsError}");            }

            if(resultError.ResultValue is null)
                Console.WriteLine($"    resultError.ResultValue is null");
            
        }
    }
}
