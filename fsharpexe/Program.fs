// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open Csharpdll
open Fsharpdll.Visa
open Fsharpdll.Person

// A 'type cast', or simply, a name change
// from the C# class to the F# record
type PersonF = PersonC

[<EntryPoint>]
let main argv =

    let tourist = { Name = "John Doe"; Status = Status.Tourist}
    let student = { Name = "Mary Eod"; Status = Status.Student}

    printfn "Consuming F#"
    let resultOk = ValidateStudent student 
    match resultOk with 
    | Ok s -> printfn $"{s.Name} is a student" 
    | Error e -> printfn $"{student.Name} is a {e}" 

    let resultError = ValidateStudent tourist 
    match resultError with 
    | Ok s -> printfn $"{s.Name} is a tourist" 
    | Error e -> printfn $"{tourist.Name} is a {e}" 

    
    printfn "\nConsuming C#"      
    // Create a record type from a C# Class constructor
    let touristC = PersonC("John C. Doe", Status.Tourist)
    let studentC = PersonC("Mary C. Eod", Status.Student)

    // Use the method from a C# Class that returns a Result type
    let resultOkC = PersonC.ValidateStudentC studentC

    // The method returns Ok PersonC 
    match resultOkC with 
    | Ok s -> printfn $"{s.Name} is a student" 
    | Error e -> printfn $"{studentC.Name} is a {e}" 


    let resultErrorC = PersonC.ValidateStudentC touristC
    
    // or the method returns Error string
    match resultErrorC with 
    | Ok s -> printfn $"{s.Name} is a student" 
    | Error e -> printfn $"{touristC.Name} is a {e}"


    printfn "\nConsuming C#, overloaded constructor from F# type"
    // Here we use the overloaded constructor in the C# Class
    // that receives a Person.Person F# record to build an
    // equivalent object and comes back as a PersonC record
    // 
    let anotherTouristC = PersonC tourist
    let anotherStudentC = PersonC student

    let resultOkF = PersonF.ValidateStudentC anotherStudentC
    match resultOkF with 
    | Ok s -> printfn $"{s.Name} is a student" 
    | Error e -> printfn $"{anotherStudentC.Name} is a {e}" 

    let resultErrorF = PersonF.ValidateStudentC anotherTouristC
    match resultErrorF with 
    | Ok s -> printfn $"{s.Name} is a student" 
    | Error e -> printfn $"{anotherTouristC.Name} is a {e}"

    printfn "\nConsuming C#, casting  F#"
    // This is maximal obfuscation!
    // Here we use the overloaded constructor in the C# Class
    // that receives a Person.Person F# record to build an
    // equivalent object and comes back as a PersonC,
    // but we use the type 'cast' in line 8...
    // That cast is simply a name change, just in case
    // one needs it.
    let yetAnotherTouristF = PersonF tourist
    let yetAnotherStudentF = PersonF student

    let resultOkF = PersonF.ValidateStudentC yetAnotherStudentF
    match resultOkF with 
    | Ok s -> printfn $"{s.Name} is a student" 
    | Error e -> printfn $"{yetAnotherStudentF.Name} is a {e}" 

    let resultErrorF = PersonF.ValidateStudentC yetAnotherTouristF
    match resultErrorF with 
    | Ok s -> printfn $"{s.Name} is a student" 
    | Error e -> printfn $"{yetAnotherTouristF.Name} is a {e}"

    0 // return an integer exit code