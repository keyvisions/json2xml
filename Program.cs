using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Security;

namespace json2xml
{
    class Program
    {
        private static string _json = "";
        private static readonly Regex regex_name = new Regex(@"^\s*\{\s*""(?<name>[a-z0-9_]+?)""\s*:\s*(?<rest>[\s\S]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex regex_rest = new Regex(@"^\s*\[\s*(?<rest>[\s\S]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex regex_name_rest = new Regex(@"^""(?<name>[a-z0-9_]+?)""\s*:\s*(?<rest>[\s\S]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex regex_value = new Regex(@"^(?<value>true|false|null|-?(?:0|[1-9])[0-9]*(?:\.[0-9]+)?(?:e[\-+]?[0-9]+)?|""(?:\\""|.)*?"")\s*,?\s*(?<rest>[\s\S]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex regex_final = new Regex(@"^(?:[\]}]\s*,?\s*)(?<rest>[\s\S]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static string json2xml(string json, string name = "root")
        {
            if (json == "")
                return "";

            Match match = regex_name.Match(json);
            if (match.Success)
            {
                if (name != "")
                    return String.Format("<{0}>{1}</{0}>", name, json2xml(match.Groups["rest"].Value, match.Groups["name"].Value)) + json2xml(_json, "");
                return json2xml(match.Groups["rest"].Value, match.Groups["name"].Value) + json2xml(_json, "");
            }

            match = regex_rest.Match(json);
            if (match.Success)
            {
                if (name != "")
                    return String.Format("<{0}>{1}</{0}>", name, json2xml(match.Groups["rest"].Value, match.Groups["name"].Value)) + json2xml(_json, "");
                return json2xml(match.Groups["rest"].Value, match.Groups["name"].Value) + json2xml(_json, "");
            }

            match = regex_name_rest.Match(json);
            if (match.Success)
                return json2xml(match.Groups["rest"].Value, match.Groups["name"].Value);

            match = regex_value.Match(json);
            if (match.Success)
            {
                if (name == "" || name == "root")
                    name = "value";
                string value = match.Groups["value"].Value.Trim('\"');
                if (value == "null")
                    value = "";
                return String.Format("<{0}>{1}</{0}>", name, SecurityElement.Escape(value)) + json2xml(match.Groups["rest"].Value);
            }

            match = regex_final.Match(json);
            if (match.Success)
            {
                _json = match.Groups["rest"].Value;
                return "";
            }

            throw new System.Data.SyntaxErrorException("Invalid JSON syntax");
        }

        static void Main(string[] args)
        {
#if DEBUG
            var watch = System.Diagnostics.Stopwatch.StartNew();
#endif
            string json = "";

            StreamReader sr = new StreamReader("data.json");
            json = sr.ReadToEnd();
            sr.Dispose();

            string xml = json2xml(json);

            using (StreamWriter outputFile = new StreamWriter("data.xml", false))
            {
                outputFile.WriteLine(xml);
            }
#if DEBUG
            watch.Stop();
            Console.WriteLine($"Time: {watch.ElapsedMilliseconds} ms");
#endif
        }
    }
}
