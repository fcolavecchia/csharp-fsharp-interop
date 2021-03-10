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

    public class PlanC
    {
        public string Name;
        public Status.Status StatusC;
        public PlanC(string name, Status.Status status){
            Name = name;
            StatusC = status;        
        }
        public PlanC(Plan.Plan plan){
            Name = plan.Name;
            StatusC = plan.Status;        
        }


        // public Status.Status status2 = Status.Status.UnApproved;
        public static FSharpResult<string, string> ValidatePlanC( PlanC plan ){
            if(plan.StatusC == Status.Status.Completed)
            // var resultOk = Status.Equals(plan.StatusC,Status.IsCompleted);
                return FSharpResult<string, string>.NewOk("Completed");
            else    
                return FSharpResult<string, string>.NewError("Unapproved");;
        }   

    }

}
