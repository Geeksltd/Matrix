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
        Method method;
        MethodInfo methodInformation;
        Type classType;
        object classInstance;

        public MethodPresenter() => Init(Current.Symbol);
        public MethodPresenter(ISymbol symbol) => Init(symbol);

        public Method PresentMethod() => method;

        void Init(ISymbol symbol)
        {
            var asemblyName = symbol.ContainingAssembly.Name;
            if (asemblyName == "MyAshes")
                return;

            var appDTE = (DTE2)Package.GetGlobalService(typeof(SDTE));
            var curProj = appDTE.ActiveDocument.ProjectItem.ContainingProject;
            var vsCurProj = (VSProject)curProj.Object;
            var asmFilePath = vsCurProj.References.OfType<Reference>().FirstOrDefault(r => r.Name == asemblyName).Path;

            var className = symbol.ContainingType.ToDisplayString(Microsoft.CodeAnalysis.SymbolDisplayFormat.FullyQualifiedFormat).Replace("global::", "");
            var functionName = symbol.Name;

            var paramTypes = new List<Type>();
            foreach (var parm in ((IMethodSymbol)symbol).Parameters)
                paramTypes.Add(parm.Type.Name.ToString().ToType());

            var objAssembly = Assembly.LoadFrom(asmFilePath);
            classType = objAssembly.GetType(className);
            classInstance = SampleGenerator.GenerateSample(classType);

            if (!IsValid(functionName, paramTypes))
                return;

            MakeMethod(symbol);
        }
        void MakeMethod(ISymbol symbol)
        {
            var parInfos = methodInformation.GetParameters();
            var parameters = new List<Parameter>();
            var cnt = 1;
            foreach (var pInfo in parInfos)
            {
                parameters.Add(new Parameter() { Name = pInfo.Name, Type = pInfo.ParameterType });
                cnt++;
            }

            method = new Method()
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
        bool IsValid(string functionName, List<Type> paramTypes)
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