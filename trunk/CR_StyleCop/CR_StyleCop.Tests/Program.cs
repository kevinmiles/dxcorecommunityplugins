using System;
using System.Collections.Generic;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Core;
using CR_StyleCop.Tests.Helpers;

namespace CR_StyleCop.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            string SolutionPath;
            if (args != null && args.Length > 0)
                SolutionPath = args[0];
            else
                SolutionPath = @"c:\Sources\dxcorecommunity\CR_StyleCop\CR_StyleCop.TestCode.sln";

            CR_StyleCop.CR_StyleCopPlugIn plugin = null;
            try
            {
                ParserHelper.RegisterParserServices();

                Console.Write("Parsing solution... ");

                SolutionParser solutionParser = new SolutionParser(SolutionPath);
                SolutionElement solution = solutionParser.GetParsedSolution();
                if (solution == null)
                    return;

                Console.WriteLine("Done.");

                plugin = new CR_StyleCop.CR_StyleCopPlugIn();

                foreach (ProjectElement project in solution.AllProjects)
                {
                    foreach (SourceFile file in project.AllFiles)
                    {
                        Console.WriteLine("Checking code issues for " + file.Name);
                        var codeIssues = plugin.GetCodeIssuesFor(file);
                        foreach (var codeIssue in codeIssues)
                        {
                            Console.WriteLine("Ln " + codeIssue.Range.Start.Line + " Col " + codeIssue.Range.Start.Offset + " Msg: " + codeIssue.Message);
                        }
                    }
                }

                Console.WriteLine("Done");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                ParserHelper.UnRegisterParserServices();
                if (plugin != null)
                    plugin.FinalizePlugIn();
            }

            Console.ReadLine();
        }
    }
}
