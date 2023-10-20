using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Security; 

namespace json2xml
{
    class Program
    {
        // https://json.org/json-it.html
        private static string _json = "";
        private static string json2xml(string json, string name = "root")
        {
            Regex regex = new Regex(@"^\s*\{\s*""(?<name>[a-z0-9_]+?)""\s*:\s*(?<rest>[\s\S]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Match match = regex.Match(json);
            if (match.Success)
                return String.Format("<{0}>{1}</{0}>", name == "" ? "element" : name, json2xml(match.Groups["rest"].Value, match.Groups["name"].Value)) + json2xml(_json, "element");

            regex = new Regex(@"^\s*\[\s*(?<rest>[\s\S]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            match = regex.Match(json);
            if (match.Success)
                return String.Format("<{0}>{1}</{0}>", name, json2xml(match.Groups["rest"].Value, match.Groups["name"].Value)) + json2xml(_json, "element");

            regex = new Regex(@"^""(?<name>[a-z0-9_]+?)""\s*:\s*(?<rest>[\s\S]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            match = regex.Match(json);
            if (match.Success)
                return json2xml(match.Groups["rest"].Value, match.Groups["name"].Value);

            regex = new Regex(@"^(?<value>true|false|null|-?(?:0|[1-9])[0-9]*(?:\.[0-9]+)?(?:e[\-+]?[0-9]+)?|""(?:\\""|.)*?"")\s*,?\s*(?<rest>[\s\S]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            match = regex.Match(json);
            if (match.Success) {
                if (name == "" || name == "root")
                    name = "value";
                string value = match.Groups["value"].Value.Trim('\"');
                if (value == "null")
                    value = "";
                return String.Format("<{0}>{1}</{0}>", name, SecurityElement.Escape(value)) + json2xml(match.Groups["rest"].Value);
            }

            regex = new Regex(@"^(?:[\]}]\s*,?\s*)(?<rest>[\s\S]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            match = regex.Match(json);
            if (match.Success)
                _json = match.Groups["rest"].Value;

            return "";
        }
        static void Main(string[] args)
        {
            string json = "";
            
            StreamReader sr = new StreamReader("data.json");
            json = sr.ReadToEnd();
            sr.Dispose();
            
            string xml = json2xml(json);
          
            Console.Write(xml);
        }
    }
}
