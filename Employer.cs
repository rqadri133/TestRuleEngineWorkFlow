using System;
using System.Text;
using Newtonsoft.Json; 
 

 namespace TestRuleEngineWorkFlow.Models 
 {
    
    public class Employer
    {
        [JsonProperty("firstname")]
        public string? Firstname { get; set; }

        [JsonProperty("lastname")]
        public string? Lastname { get; set; }

        [JsonProperty("salary")]
        public string? Salary { get; set; }





        [JsonProperty("gender")]
        public string? Gender { get; set; }

        [JsonProperty("city")]
        public string? City { get; set; }
        
    }
        
 }
        

        // FirstName":"John", "LastName":"Peters" , "Salary":"100000", "Gender":"Male" ,"City":"New York" }, { "FirstName":"John", "LastName":"Peters" , "Salary":"100000", "Gender":"Male" ,"City":"New York" }]


    