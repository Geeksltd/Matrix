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
        MethodInfo methodInformation;
        Type classType;
        object classInstance;


        public MethodPresenter() => Init(Current.Symbol);
        public MethodPresenter(ISymbol symbol) => Init(symbol);


        void Init(ISymbol symbol)
        {
            var asmblyName = symbol.ContainingAssembly.Name;
            if (asmblyName == "MyAshes")
                return;

            var appDTE = (DTE2)Package.GetGlobalService(typeof(SDTE));
            var curProj = appDTE.ActiveDocument.ProjectItem.ContainingProject;
            var vsCurProj = (VSProject)curProj.Object;
            var asmFilePath = vsCurProj.References.OfType<Reference>().FirstOrDefault(r => r.Name == asmblyName).Path;

            var paramTypes = new List<Type>();
            foreach (var parm in ((IMethodSymbol)symbol).Parameters)
                paramTypes.Add(parm.Type.Name.ToString().ToType());

            var className = symbol.ContainingType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Replace("global::", "");
            var functionName = symbol.Name;
            if (!IsValid(functionName, paramTypes))
                return;

            var objAssembly = Assembly.LoadFrom(asmFilePath);
            classType = objAssembly.GetType(className);
            classInstance = ValueSampler.SampleValue(classType);
        }

        public Method PresentMethod(ISymbol symbol)
        {
            var parInfos = methodInformation.GetParameters();
            var parameters = new List<Parameter>();
            var cnt = 1;
            foreach (var pInfo in parInfos)
            {
                parameters.Add(new Parameter() { Name = pInfo.Name, Type = pInfo.ParameterType });
                cnt++;
            }

            return new Method()
            {
                Namespace = classType.Namespace,
                DeclaringType = methodInformation.DeclaringType.Name,
                XMLDescription = symbol.GetDocumentationCommentXml(),
                MethodName = methodInformation.Name,
                Parameters = parameters,
                ReturnType = methodInformation.ReturnType.Name,
                MethodInformation = methodInformation,
                ClassInstance = classInstance

            };
        }

        private bool IsValid(string functionName, List<Type> paramTypes)
        {
            if (classType == null)
                return false;

            if (!(classType.GetConstructor(Type.EmptyTypes) == null && classType.IsAbstract && classType.IsSealed))
                if (classInstance == null)
                    if (classType.GetConstructor(Type.EmptyTypes) != null)
                        classInstance = Activator.CreateInstance(classType);
                    else
                        return false;

            methodInformation = classType.GetMethod(functionName, paramTypes.ToArray());
            if (methodInformation == null)
                return false;
            return true;
        }

    }
}
