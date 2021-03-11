# Sharing the `Result` type between F\# and C\#


One of the most excruciating aspects of software is error management. Fortunately, more and more modern languages  developed different ways to help the coder to solve this trouble.  One of these techniques is the Result type, present in different languages, such as [OCaml](https://ocaml.org/learn/tutorials/error_handling.html#Result-type), [Rust](https://doc.rust-lang.org/std/result/) and [others](https://en.wikipedia.org/wiki/Result_type). 
Here, I will deal with the [`Result` type in F#](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/results), and its interoperability with the more ubiquitous .NET language sibling, C#. 

Our team is starting a new project based on a .NET client-server architecture. We are relatively new in this universe, being full plain not-even-object-oriented C programmers for a long time. However, recently I have had some experience with functional programming (F#), while others received training in C#. Since .NET provides excellent interoperability features among its languages, we are giving both languages a try.

At this point, it was clear that I we will be sharing data back and forth between different pieces of code written in F# and C#, therefore we need to carefully look at the distinctive features of  interoperability. Luckily (or, truly speaking, thanks to the hard work of many colleagues in the community!), there are excellent sources of documentation for F#. The most useful docs I found that covered the topic in question here are in [Isaac Abraham's](https://twitter.com/isaac_abraham)  book [Get Programming with F#](https://www.manning.com/books/get-programming-with-f-sharp), lessons 25 and 27; and in the [F# for fun and profit](https://fsharpforfunandprofit.com/posts/completeness-seamless-dotnet-interop/) site of [Scott Wlaschin](https://twitter.com/ScottWlaschin). If you write F# code, probably you know this guys very well, but if not, I encourage to read them, both the book and the page are a delight for learning F#.

Anyway, the map between the F# and C# pieces was quite simple, mostly because you can easily map namespaces to namespaces, records to immutable clases, functions to static methods and modules to static classes respectively.
But, a big but, I wanted to manage errors in my F# code with the Result type, and since some of it will make use of the C# classes, I needed a way for methods in those classes to return with this type.  Although Google provided me some answers, they seemed pretty outdated and quite complicated to me. Would it be possible? Let's find out.

## The Result type and the railway way of life

```fsharp
type Result<'T,'TError> =
    | Ok of ResultValue:'T
    | Error of ErrorValue:'TError
```


## The Result type in F#

I will define two simple modules for our example. The first one defines  a _discriminated union_ (DU) that represents the type of a visa one can have to visit a country. The type `Status` can only have one of those two values, `Student` or `Tourist`

```fsharp
module Visa =
    type Status = Student | Tourist
```


The second module defines a `Person` type record, with a name `string` and a visa status of type `Visa.Status`:

```fsharp
module Person = 

    type Person = { Name : string; Status : Visa.Status }

    let defaultStudent = { Name = "Jimmy Default"; Status = Visa.Student}

    // Person -> Result<Person,string>
    let ValidateStudent person =

        match person.Status with 
        | Visa.Student -> Ok person
        | Visa.Tourist -> Error "Tourist"
```


I also defined a static record defaultStudent, and a function ValidateStudent that receives a Person record, inspects its status element and returns a Result<Person,string> type. If the person is a has a student visa, it returns a Person record wrapped in the Ok expression. If it is a tourist, it returns a string wrapped with the Error result.








  








 