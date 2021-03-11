// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open Csharpdll
open Fsharpdll.Visa
open Fsharpdll.Person


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
    let touristC = PersonC("John C. Doe", Status.Tourist)
    let studentC = PersonC("Mary C. Eod", Status.Student)

    let anotherTouristC = PersonC tourist
    let anotherStudentC = PersonC student

    printfn "result = %A" (PersonC.ValidateStudentC touristC)
    printfn "result = %A" (PersonC.ValidateStudentC studentC)

    printfn "Consuming C#, casting de F#"      
    let anotherTouristC = PersonC tourist
    let anotherStudentC = PersonC student

    printfn "result = %A" (PersonC.ValidateStudentC anotherTouristC)
    printfn "result = %A" (PersonC.ValidateStudentC anotherStudentC)



    0 // return an integer exit code