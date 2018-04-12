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
using Microsoft.CodeAnalysis;
using Matrix.Models;
using Matrix.Infrustructure;

namespace Matrix.Logic
{
    class MethodPresenter
    {
        Method model;
        MethodInfo mI;
        Type classType;
        object classInstance;
        bool flagOk = true;
        public bool FlagOk => flagOk;
        public MethodPresenter() => Init(Current.Symbol);
        public MethodPresenter(ISymbol symbol) => Init(symbol);
        void Init(ISymbol symbol)
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
            classType = objAssembly.GetType(className);
            classInstance = ValueSampler.SampleValue(classType);
            if (classType == null)
            {
                flagOk = false;
                return;
            }

            if (!(classType.GetConstructor(Type.EmptyTypes) == null && classType.IsAbstract && classType.IsSealed))
            {
                if (classInstance == null)
                {
                    if (classType.GetConstructor(Type.EmptyTypes) != null)
                    {
                        classInstance = Activator.CreateInstance(classType);
                    }
                    else
                    {
                        flagOk = false;
                        return;
                    }
                }
            }

            mI = classType.GetMethod(functionName, paramTypes.ToArray());
            if (mI == null)
            {
                flagOk = false;
                return;
            }

            var parInfos = mI.GetParameters();
            var parameters = new List<Parameter>();
            var cnt = 1;
            foreach (var pInfo in parInfos)
            {
                parameters.Add(new Parameter() { Name = pInfo.Name, Type = pInfo.ParameterType.Name });
                cnt++;
            }

            //if (parInfos.Count() == 0) GenerateSamples(1);
            //else GenerateSamples(3);

            model = new Method()
            {
                Namespace = classType.Namespace,
                DeclaringType = mI.DeclaringType.Name,
                XMLDescription = symbol.GetDocumentationCommentXml(),
                MethodName = mI.Name,
                Parameters = parameters,
                ReturnType = mI.ReturnType.Name

            };
        }

        public Method PresentMethod() => model;


    }
}
