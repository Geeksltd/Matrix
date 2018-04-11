using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using EnvDTE80;
using Matrix.Logic;
using Matrix.Utils;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using VSLangProj;

namespace Matrix.Logic
{
    class ValuePresenter
    {
        MethodInfo MI;
        Type ClassType;
        object classInstance;
        bool flagOk = true;
        public bool FlagOk => flagOk;
        public ValuePresenter(Microsoft.CodeAnalysis.ISymbol symbol)
        {
            var asmName = symbol.ContainingAssembly.Name;
            if (asmName == "MyAshes")
            {
                flagOk = false;
                return;
            }

            var appDTE = (DTE2)Package.GetGlobalService(typeof(SDTE));
            var curProj = appDTE.ActiveDocument.ProjectItem.ContainingProject;
            var vsCurProj = (VSProject)curProj.Object;
            var asmFilePath = vsCurProj.References.OfType<Reference>().FirstOrDefault(r => r.Name == asmName).Path;

            var className = symbol.ContainingType.ToDisplayString(Microsoft.CodeAnalysis.SymbolDisplayFormat.FullyQualifiedFormat).Replace("global::", "");
            var functionName = symbol.Name;

            var paramTypes = new List<Type>();
            foreach (var parm in ((Microsoft.CodeAnalysis.IMethodSymbol)symbol).Parameters)
                paramTypes.Add(parm.Type.Name.ToString().ToType());


            var objAssembly = Assembly.LoadFrom(asmFilePath);
            ClassType = objAssembly.GetType(className);
            classInstance = ValueSampler.SampleValue(ClassType);
            if (ClassType == null)
            {
                flagOk = false;
                return;
            }

            if (!(ClassType.GetConstructor(Type.EmptyTypes) == null && ClassType.IsAbstract && ClassType.IsSealed))
            {
                if (classInstance == null)
                {
                    if (ClassType.GetConstructor(Type.EmptyTypes) != null)
                    {
                        classInstance = Activator.CreateInstance(ClassType);
                    }
                    else
                    {
                        flagOk = false;
                        return;
                    }
                }
            }

            MI = ClassType.GetMethod(functionName, paramTypes.ToArray());
            if (MI == null)
            {
                flagOk = false;
                return;
            }

            
        }
    }
}
