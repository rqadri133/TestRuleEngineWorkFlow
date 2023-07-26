// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using System.Threading.Tasks;
using RulesEngine;
using RulesEngine.Actions;
using RulesEngine.Models;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using TestRuleEngineWorkFlow.Models;


Console.WriteLine("Verifying Rules");

Console.WriteLine("Salary should not be 0/NULL/Empty");

// could use Settings or Environment Variable 
const string filePath = @"/Users/syedqadri//Documents/Development/TESTRULEENGINEWORKFLOW/Employer.json";
string jsonStr = "[{ \"FirstName\":\"Jhon\", \"LastName\":\"Peters\" , \"Salary\":\"100000\", \"Gender\":\"Male\" ,\"City\":\"New York\" }, { \"FirstName\":\"John\", \"LastName\":\"Peters\" , \"Salary\":\"800\", \"Gender\":\"Male\" ,\"City\":\"New York\" }]";

//	2. Salary should not be String
//	3. Gender should Male, Female, M, L
//	4. First Name should not be Empty
//	5. Last Name should not be Empty
//	6. First Name should not be Numbergit

var workflow = new WorkflowRules{
				WorkflowName = "CheckSalaryRules",
				Rules= new []{
					new Rule{
						RuleName = "Check Salary Must be a Number",
                        SuccessEvent =  "20",
                        ErrorMessage =  "Salary is  a number",
                        Expression = "Regex.IsMatch(string(input1.Salary),\"^(0|[1-9][0-9]*)$\")"
					},
					new Rule{
						RuleName = "Check Salary Not Equal 0",
                        SuccessEvent =  "10",
                        ErrorMessage =  "Salary is 0",
						Expression = "input1.Salary != \"0\""
					} ,
                    
                    new Rule{
						RuleName = "Gender Male or Female",
                        SuccessEvent =  "30",
                        ErrorMessage =  "Gender is Not Male or Female",
						Expression = "input1.Gender == \"Male\" OR input1.Gender == \"Female\" OR input1.Gender == \"M\" OR input1.Gender == \"F\" "
					},
                       new Rule{
						RuleName = "First Name and Last Name must not be empty",
                        SuccessEvent =  "10",
                        ErrorMessage =  "First Name Should not be Empty",
						Expression = "input1.FirstName != \"\"  AND  input1.LastName  != \"\" "   
					},
                    
                    new Rule{
						RuleName = "check First Name Should Not be a Number",
                        SuccessEvent =  "40",
                        ErrorMessage =  "First Name is a Number ",
						Expression = "Regex.IsMatch(string(input1.FirstName),\"[^0-9]\")"   
					}




				}
					

			};



         var resettings = new ReSettings{
				  CustomTypes = new Type[]{typeof(Regex)}
			};

         
        // string currentDirectory =  Environment.CurrentDirectory;
        // string fullPath = Path.Combine(currentDirectory,  "Employer.json");

			var re = new RulesEngine.RulesEngine(new []{workflow},resettings);

            using StreamReader reader = new(filePath);
            var json = reader.ReadToEnd();
            EmployerRoot root =  JsonConvert.DeserializeObject<EmployerRoot>(json);
			List<Employer> employers = root.Employers;

// Employer[] input1 =  JsonConvert.DeserializeObject<Employer[]>(filePath);

			foreach (Employer emp in employers)
			{

				var result = re.ExecuteAllRulesAsync("CheckSalaryRules", emp).Result;

				foreach (var res in result)
				{
					var output = res.ActionResult.Output;
					Console.WriteLine($"{res.Rule.RuleName} :\nIsSuccess- {res.IsSuccess}\n \n {res.ExceptionMessage}\n");
				}



}