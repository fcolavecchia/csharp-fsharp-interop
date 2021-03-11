using System;
using Microsoft.FSharp.Core;
using Fsharpdll;

namespace Csharpdll
{
    public class MathFunctions
    {
        public static int add(int a, int b)
        {
            return a + b;
        }
    }

    public class PersonC
    {
        public string Name;
        public Visa.Status StatusC;
        public PersonC(string name, Visa.Status status){
            Name = name;
            StatusC = status;        
        }
        public PersonC(Person.Person person){
            Name = person.Name;
            StatusC = person.Status;        
        }


        // public Visa.Status status2 = Visa.Status.Student;
        public static FSharpResult<PersonC, string> ValidateStudentC( PersonC person ){
            if(person.StatusC == Visa.Status.Student)
                return FSharpResult<PersonC, string>.NewOk(person);
            else    
                return FSharpResult<PersonC, string>.NewError("Tourist");;
        }   

    }

}
