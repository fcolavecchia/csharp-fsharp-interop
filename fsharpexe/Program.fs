// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open Csharpdll
open Fsharpdll.Status
open Fsharpdll.Plan

// Define a function to construct a message to print
let from whom =
    sprintf "from %s" whom

let testMathC a b = 

    let c = MathFunctions.add (a, b)
    c

[<EntryPoint>]
let main argv =
    let message = from "F#" // Call the function
    printfn "Hello world %s" message

    let c = testMathC 3 4
    printfn "c = %d" c

    let planCompleto = { Name = "Prostata"; Status = Status.Completed}
    let planSinAprobar = { Name = "Mama"; Status = Status.UnApproved}

    // let validaPlan = ValidatePlan planCompleto
    printfn "resultado = %A" (ValidatePlan planCompleto)
    printfn "resultado = %A" (ValidatePlan planSinAprobar)
      


    0 // return an integer exit code