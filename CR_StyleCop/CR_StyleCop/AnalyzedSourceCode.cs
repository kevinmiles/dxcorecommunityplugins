namespace CR_StyleCop
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using StyleCop;

    internal class AnalyzedSourceCode : SourceCode
    {
        private readonly string codeToAnalyze;
        private readonly string path;

        public AnalyzedSourceCode(CodeProject project, SourceParser parser, string path, string codeToAnalyze)
            : base(project, parser)
        {
            this.path = path;
            this.codeToAnalyze = codeToAnalyze;
        }

        public AnalyzedSourceCode(CodeProject project, SourceParser parser, IEnumerable<Configuration> configurations, string path, string codeToAnalyze)
            : base(project, parser, configurations)
        {
            this.path = path;
            this.codeToAnalyze = codeToAnalyze;
        }

        public override bool Exists
        {
            get { return true; }
        }

        public override string Name
        {
            get { return this.path; }
        }

        public override string Path
        {
            get { return this.path; }
        }

        public override TextReader Read()
        {
            return new StringReader(this.codeToAnalyze);
        }

        public override DateTime TimeStamp
        {
            get { return DateTime.Now; }
        }

        public override string Type
        {
            get { return "cs"; }
        }
    }
}
