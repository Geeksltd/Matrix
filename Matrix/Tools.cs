using System;
using System.Collections.Generic;
using System.Linq;
using EnvDTE80;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text;
using VSLangProj;

namespace Matrix
{
    static class Tools
    {
        public static ISymbol CurrentSymbol(SnapshotSpan span)
        {
            var dte = (DTE2)Package.GetGlobalService(typeof(SDTE));
            var doc = (EnvDTE.TextDocument)dte.ActiveDocument.Object("TextDocument");
            var curProj = dte.ActiveDocument.ProjectItem.ContainingProject;
            var vsCurProj = (VSProject)curProj.Object;
            var references = new List<MetadataReference>();
            foreach (VSLangProj.Reference refrence in vsCurProj.References)
                references.Add(MetadataReference.CreateFromFile(refrence.Path));


            var strCode = doc.StartPoint.CreateEditPoint().GetText(doc.EndPoint);
            var syntaxTree = CSharpSyntaxTree.ParseText(strCode);
            var compilation = CSharpCompilation.Create("zarif").AddReferences(references).AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location)).AddSyntaxTrees(syntaxTree);

            var semanticModel = compilation.GetSemanticModel(syntaxTree);
            var synTree = syntaxTree.GetRoot();
            var synNod = synTree.DescendantNodes().Where(x => x.Span == new TextSpan(span.Start, span.Length)).FirstOrDefault();
            // ------------------------------
            if (synNod != null)
            {
                var symbol = semanticModel.GetSymbolInfo(synNod);
                if (symbol.Symbol != null)
                {
                    if (symbol.Symbol.Kind == SymbolKind.Method)
                    {
                        return symbol.Symbol;
                    }
                }
            }

            return null;
        }

        public static Type ToType(this string TypeName)
        {
            var typeName = TypeName.ToUpper();

            if (TypeCode.Boolean.ToString().ToUpper() == typeName)
            {
                return typeof(bool);
            }
            else if (TypeCode.Byte.ToString().ToUpper() == typeName)
            {
                return typeof(byte);
            }
            else if (TypeCode.UInt32.ToString().ToUpper() == typeName)
            {
                return typeof(uint);
            }
            else if (TypeCode.UInt64.ToString().ToUpper() == typeName)
            {
                return typeof(ulong);
            }
            else if (TypeCode.UInt16.ToString().ToUpper() == typeName)
            {
                return typeof(ushort);
            }
            else if (TypeCode.String.ToString().ToUpper() == typeName)
            {
                return typeof(string);
            }
            else if (TypeCode.Single.ToString().ToUpper() == typeName)
            {
                return typeof(float);
            }
            else if (TypeCode.SByte.ToString().ToUpper() == typeName)
            {
                return typeof(sbyte);
            }
            else if (TypeCode.Object.ToString().ToUpper() == typeName)
            {
                return typeof(object);
            }
            else if (TypeCode.Int32.ToString().ToUpper() == typeName)
            {
                return typeof(int);
            }
            else if (TypeCode.Int64.ToString().ToUpper() == typeName)
            {
                return typeof(long);
            }
            else if (TypeCode.Int16.ToString().ToUpper() == typeName)
            {
                return typeof(short);
            }
            else if (TypeCode.Double.ToString().ToUpper() == typeName)
            {
                return typeof(double);
            }
            else if (TypeCode.Decimal.ToString().ToUpper() == typeName)
            {
                return typeof(decimal);
            }
            else if (TypeCode.DBNull.ToString().ToUpper() == typeName)
            {
                return typeof(DBNull);
            }
            else if (TypeCode.DateTime.ToString().ToUpper() == typeName)
            {
                return typeof(DateTime);
            }
            else if (TypeCode.Char.ToString().ToUpper() == typeName)
            {
                return typeof(char);
            }
            else if (TypeCode.Empty.ToString().ToUpper() == typeName)
            {
                return null;
            }

            return null;
        }
    }
}