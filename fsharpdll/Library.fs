namespace Fsharpdll

module math =
    let hello name =
        printfn "Hello %s" name

    let add a b =
        a + b

///
    /// Simple discriminated union to test in
    /// C#
module Status =

    type Status = UnApproved | Completed

///
    /// Simple module with method that returns a Result object
    /// C#
module Plan = 

    type Plan = { Name : string; Status : Status.Status }

    let planSinAprobar = { Name = "Pulmón"; Status = Status.UnApproved}

    let ValidatePlan plan =

        match plan.Status with 
        | Status.UnApproved -> Error "Unapproved"
        | Status.Completed ->  Ok "Completed"


