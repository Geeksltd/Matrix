using System.Collections.Generic;
using System.Linq;
using EnvDTE80;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text;


namespace Matrix.Logic
{
    static class CodeAnalysis
    {
        public static ISymbol GetCurrentSymbol(SnapshotSpan span)
        {
            var syntax = GetCurrentDocument().GetSyntaxTree();
            var semanticModel = syntax.CreateCompilation().GetSemanticModel(syntax);
            var root = syntax.GetRoot();
            var synNod = root.DescendantNodes().Where(x => x.FullSpan == new TextSpan(span.Start, span.Length)).FirstOrDefault();
            if (synNod != null)
            {
                var symbol = semanticModel.GetSymbolInfo(synNod);
                if (symbol.Symbol != null)
                    if (symbol.Symbol.Kind == SymbolKind.Method)
                        return symbol.Symbol;
            }
            return null;
        }

        public static string GetCurrentDocument()
        {
            var dte = (DTE2)Package.GetGlobalService(typeof(SDTE));
            var doc = (EnvDTE.TextDocument)dte.ActiveDocument.Object("TextDocument");
            return doc.StartPoint.CreateEditPoint().GetText(doc.EndPoint);
        }


        public static SyntaxTree GetSyntaxTree(this string strCode) => CSharpSyntaxTree.ParseText(strCode);
        public static CSharpCompilation CreateCompilation(this IEnumerable<SyntaxTree> syntaxTrees) => CSharpCompilation.Create("MyAshes", syntaxTrees: syntaxTrees, references: new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });
        public static CSharpCompilation CreateCompilation(this SyntaxTree syntax) => CreateCompilation(new[] { syntax });
    }
}
