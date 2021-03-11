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
        public PersonC(Person.Person plan){
            Name = plan.Name;
            StatusC = plan.Status;        
        }


        // public Visa.Status status2 = Visa.Status.Student;
        public static FSharpResult<string, string> ValidateStudentC( PersonC plan ){
            if(plan.StatusC == Visa.Status.Tourist)
            // var resultOk = Status.Equals(plan.StatusC,Status.IsCompleted);
                return FSharpResult<string, string>.NewOk("Tourist");
            else    
                return FSharpResult<string, string>.NewError("Unapproved");;
        }   

    }

}
