# Sharing the `Result` type between F\# and C\#


One of the most excruciating aspects of software is error management. Fortunately, more and more modern languages  developed different ways to help the coder to solve this trouble.  One of these techniques is the Result type, present in different languages, such as [OCaml](https://ocaml.org/learn/tutorials/error_handling.html#Result-type), [Rust](https://doc.rust-lang.org/std/result/) and [others](https://en.wikipedia.org/wiki/Result_type). 
Here, I will deal with the [`Result` type in F#](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/results), and its interoperability with the more ubiquitous .NET language sibling, C#. 

Our team is starting a new project based on a .NET client-server architecture. We are relatively new in this universe, being full plain not-even-object-oriented C programmers for a long time. However, recently I have had some experience with functional programming (F#), while others received training in C#. Since .NET provides excellent interoperability features among its languages, we are giving both languages a try.

At this point, it was clear that I we will be sharing data back and forth between different pieces of code written in F# and C#, therefore we need to carefully look at the distinctive features of  interoperability. Luckily (or, truly speaking, thanks to the hard work of many colleagues in the community!), there are excellent sources of documentation for F#. The most useful docs I found that covered the topic in question here are in [Isaac Abraham's](https://twitter.com/isaac_abraham)  book [Get Programming with F#](https://www.manning.com/books/get-programming-with-f-sharp), lessons 25 and 27; and in the [F# for fun and profit](https://fsharpforfunandprofit.com/posts/completeness-seamless-dotnet-interop/) site of [Scott Wlaschin](https://twitter.com/ScottWlaschin). If you write F# code, probably you know this guys very well, but if not, I encourage to read them, both the book and the page are a delight for learning F#.

Anyway, the map between the F# and C# pieces was quite simple, mostly because you can easily map namespaces to namespaces, records to immutable clases, functions to static methods and modules to static classes respectively.
But, I wanted to manage errors in my F# code with the Result type, and since some of it will make use of the C# classes, I needed a way for methods in those classes to return this type.  Although Google provided me some answers, they seemed pretty outdated and quite complicated to me. Would it be possible? Let's find out. 


## The `Result` type and the railway view of life

Let me summarize some of the key aspects of the `Result` type in F#.
The `Result` type is defined in F# as a [struct discriminated union](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/discriminated-unions#struct-discriminated-unions):

```fsharp
[<Struct>]
type Result<'T,'TError> =
    | Ok of ResultValue:'T
    | Error of ErrorValue:'TError
```
A _discriminated union_ is like an `union` type, that is, a value that can hold one of the many different types of data that were declared. Let us imagine that we have a function that validates some input, and returns a `Result` type. This function can return a success value of type `'T` _xor_ an error value of type `'TError`. The calling functions is then responsible on what to do with the error, if it exists.

One of the main advantages of the `Result` type appears when one needs to chain different validations. In that case, with the help of the `Result.bind` method, one can pass the input value to all of these validation functions one after another:

```fsharp
    // output type is Result<T,TError>
    let output = 
        input
        |> Result.bind Validate1 
        |> Result.bind Validate2
        ....
```
if all the `Validate1`, `Validate2` and so on return the same `Result` type. The `Result.bind` function applies `Validate1` to the input. If the validation is successful (that's it, if it returns `Ok T`), then passes the output to the next step, `Validate2`. However, in case of an error (`Error TError`), it exits, preventing the next validations to be performed. Therefore, `output` value will be `Ok T` if _all_ validations were successful.
This is called [_Railway Oriented Programming_](https://fsharpforfunandprofit.com/posts/recipe-part2/), a powerful way to deal with types in the functional programming world. For those readers that would like to dive deep in this topic, I suggest to [take a look](https://www.compositional-it.com/news-blog/validation-with-f-5-and-fstoolkit/) at the [FsToolkit.ErrorHandling](https://demystifyfp.gitbook.io/fstoolkit-errorhandling/) library.


## Using the `Result` type in F#

I will define two simple modules for our example (**github repo here**). The first one defines  a _discriminated union_ (DU) that represents the type of a visa one can have to visit a country. The type `Status` can only have one of those two values, `Student` or `Tourist`

```fsharp
namespace Fsharpdll

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

I also defined a static record `defaultStudent`, and a function `ValidateStudent` that receives a `Person` record, inspects its status element and returns a `Result<Person,string>` type. If the person has a student visa, it returns a `Person` record wrapped in the `Ok` expression. If it is a tourist, it returns a `string` wrapped with the `Error` result. Let us see it in action:

```fsharp
    let tourist = { Name = "John Doe"; Status = Status.Tourist}
    let student = { Name = "Mary Eod"; Status = Status.Student}

    let resultOk = ValidateStudent student 
    
    match resultOk with 
    | Ok s -> printfn $"{s.Name} is a student" 
    | Error e -> printfn $"{student.Name} is a {e}" 

    let resultError = ValidateStudent tourist 
    match resultError with 
    | Ok s -> printfn $"{s.Name} is a tourist" 
    | Error e -> printfn $"{tourist.Name} is a {e}" 
```

which prints:

```bash
Mary Eod is a student
John Doe is a Tourist
```

## Using the `Result` type in C#

Let us assume now that the `Person` type is not defined in F#, but in some C# code. For clarity, I will name this type as `PersonC`:

```csharp
// Import Visa.Status
using Fsharpdll;
...

public class PersonC
    {
        public string Name;
        public Visa.Status StatusC;
        public PersonC(string name, Visa.Status status){
            Name = name;
            StatusC = status;        
        }
    ...
```
(This looks quite straightforward, except may be for the use of the `Visa.Status` type
that is borrowed from the F# code with `using Fsharpdll`. Although I included it as an example of the use of a DU from F# in C#, it is unsubstantial for the discussion below.)

Now, I need to reproduce the `ValidatePerson` function in C#, and return the `Result` type.
I found that one can include the `FSharp.Core` library as a package reference in the C# project, an import it as:

```csharp
using Microsoft.FSharp.Core;
```

which exposes the type `FSharpResult<T,Terror>` to the code! Then,

```csharp
    public  FSharpResult<PersonC, string> ValidateStudentC( PersonC person ){
        if(person.Status == Visa.Status.Student)
            return FSharpResult<PersonC, string>.NewOk(person);
        else    
            return FSharpResult<PersonC, string>.NewError("Tourist");
    }   
```





## Futher reading

- [Decompiling F# into C#](https://fsharpforfunandprofit.com/posts/fsharp-decompiled/), by Scott Wlaschin.

- [Five Things You Should Know About .NET 5](https://auth0.com/blog/dotnet-5-whats-new/) by Andrea Chiarelli.

The latest version of the .Net framework ([.Net 5.0](https://www.youtube.com/watch?v=WmOCtlvNaTQ)) comes with 










  








 