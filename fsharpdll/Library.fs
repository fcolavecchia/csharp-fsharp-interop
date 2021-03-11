namespace Fsharpdll


///
    /// Simple discriminated union to test in
    /// C#
module Visa =

    type Status = Student | Tourist

///
    /// Simple module with method that returns a Result object
    /// C#
module Person = 

    type Person = { Name : string; Status : Visa.Status }

    let defaultStudent = { Name = "Jimmy Default"; Status = Visa.Student}

    let ValidateStudent person =

        match person.Status with 
        | Visa.Student -> Ok person
        | Visa.Tourist -> Error "Tourist"


