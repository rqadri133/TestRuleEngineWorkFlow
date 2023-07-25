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
const string filePath = @"/Users/syedqadri/Documents/Development/TestRuleEngineWorkFlow/Employer.json";
string jsonStr = "[{ \"FirstName\":\"23234\", \"LastName\":\"Peters\" , \"Salary\":\"100000\", \"Gender\":\"Male\" ,\"City\":\"New York\" }, { \"FirstName\":\"John\", \"LastName\":\"Peters\" , \"Salary\":\"tom\", \"Gender\":\"Male\" ,\"City\":\"New York\" }]";

//	2. Salary should not be String
//	3. Gender should Male, Female, M, L
//	4. First Name should not be Empty
//	5. Last Name should not be Empty
//	6. First Name should not be Number

var workflow = new WorkflowRules{
				WorkflowName = "CheckSalaryRules",
				Rules= new []{
					new Rule{
						RuleName = "CheckSalaryIsNotANumber",
                        SuccessEvent =  "20",
                        ErrorMessage =  "Salary is not a number",
                        Expression = "Regex.IsMatch(string(input1.salary),\"[^0-9]\")"
					},
					new Rule{
						RuleName = "Check Salary Equal 0",
                        SuccessEvent =  "10",
                        ErrorMessage =  "Salary is 0",
						Expression = "input1.salary == \"0\""
					} ,
                    
                    new Rule{
						RuleName = "Gender Male or Female",
                        SuccessEvent =  "30",
                        ErrorMessage =  "Gender is MAale or Female",
						Expression = "input1.gender == \"male\" OR input1.gender == \"female\" " 
					},
                       new Rule{
						RuleName = "First Name and Last Name must not be empty",
                        SuccessEvent =  "10",
                        ErrorMessage =  "First Name Should not be Empty",
						Expression = "input1.firstname != \"\"  AND  input1.lastname  != \"\" "   
					},
                    
                    new Rule{
						RuleName = "check First Name Should Not be a Number",
                        SuccessEvent =  "40",
                        ErrorMessage =  "First Name is a Number ",
						Expression = "Regex.IsMatch(string(input1.firstname),\"^[0-9]*$\")"   
					}




				}
					

			};



         var resettings = new ReSettings{
				  CustomTypes = new Type[]{typeof(Regex)}
			};

         
        // string currentDirectory =  Environment.CurrentDirectory;
        // string fullPath = Path.Combine(currentDirectory,  "Employer.json");

			var re = new RulesEngine.RulesEngine(new []{workflow},resettings);
//JavaScriptSerializer js = new JavaScriptSerializer();
//Employer [] employers =  js.Deserialize<Employer[]>(filePath);

			Employer[] input1 =  JsonConvert.DeserializeObject<Employer[]>(jsonStr);

			

			var result = re.ExecuteAllRulesAsync("CheckSalaryRules", input1).Result;

			foreach(var res in result){
				var output = res.ActionResult.Output;
				Console.WriteLine($"{res.Rule.RuleName} :\nIsSuccess- {res.IsSuccess}\n \n {res.ExceptionMessage}\n");
			}



