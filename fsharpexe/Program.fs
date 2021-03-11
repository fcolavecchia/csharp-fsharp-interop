// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open Csharpdll
open Fsharpdll.Visa
open Fsharpdll.Person


[<EntryPoint>]
let main argv =

    let tourist = { Name = "John Doe"; Status = Status.Tourist}
    let student = { Name = "Mary Eod"; Status = Status.Student}

    // let validaPlan = ValidateStudent tourist
    printfn "Consuming F#"
    printfn "result = %A" (ValidateStudent tourist)
    printfn "result = %A" (ValidateStudent student)

    printfn "Consuming C#"      
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