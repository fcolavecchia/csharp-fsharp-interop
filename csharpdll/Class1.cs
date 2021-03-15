using System;
using Microsoft.FSharp.Core;
using Fsharpdll;

namespace Csharpdll
{

    public class PersonC
    {
        public string Name;
        public Visa.Status Status;
        public PersonC(string name, Visa.Status status){
            Name = name;
            Status = status;        
        }
        public PersonC(Person.Person person){
            Name = person.Name;
            Status = person.Status;        
        }

        public  FSharpResult<PersonC, string> ValidateStudentC( PersonC person ){
            if(person.Status == Visa.Status.Student)
                return FSharpResult<PersonC, string>.NewOk(person);
            else    
                return FSharpResult<PersonC, string>.NewError("Tourist");
        }   

    }

    public class PersonC2 
    {
        // public string Name;
        // public Visa.Status StatusC;

        // public Person.Person Person;

        // public PersonC2(Person.Person person){
        //     Name = person.Name;
        //     StatusC = person.Status;        
        // }

        // public Visa.Status status2 = Visa.Status.Student;
        public static FSharpResult<Person.Person, string> ValidateStudentC( Person.Person person ){
            if(person.Status == Visa.Status.Student)
                return FSharpResult<Person.Person, string>.NewOk(person);
            else    
                return FSharpResult<Person.Person, string>.NewError("Tourist");
        }   

    }


}
