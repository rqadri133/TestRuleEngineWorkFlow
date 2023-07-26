using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using TestRuleEngineWorkFlow.Models;

[JsonObject]
public class EmployerRoot 
{
   public List<Employer>? Employers { get; set; }


}