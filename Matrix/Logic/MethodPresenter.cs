using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EnvDTE80;
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
        Method _method;
        MethodInfo _methodInformation;
        Type _classType;
        object _classInstance;

        public MethodPresenter() => Init(Current.Symbol);
        public MethodPresenter(ISymbol symbol) => Init(symbol);

        public Method PresentMethod() => _method;

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
                paramTypes.Add(parm.Type.Name.ToType());

            var objAssembly = Assembly.LoadFrom(asmFilePath);
            _classType = objAssembly.GetType(className);
            _classInstance = SampleGenerator.GenerateSample(_classType);

            if (!IsValid(functionName, paramTypes))
                return;

            MakeMethod(symbol);
        }
        void MakeMethod(ISymbol symbol)
        {
            var parInfos = _methodInformation.GetParameters();
            var parameters = new List<Parameter>();
            var cnt = 1;
            foreach (var pInfo in parInfos)
            {
                parameters.Add(new Parameter { Name = pInfo.Name, Type = pInfo.ParameterType });
                cnt++;
            }

            _method = new Method
            {
                Namespace = _classType.Namespace,
                DeclaringType = _methodInformation.DeclaringType.Name,
                XMLDescription = symbol.GetDocumentationCommentXml(),
                MethodName = _methodInformation.Name,
                Parameters = parameters,
                ReturnType = _methodInformation.ReturnType.Name,
                MethodInformation = _methodInformation,
                ClassInstance = _classInstance

            };
        }
        bool IsValid(string functionName, List<Type> paramTypes)
        {
            if (_classType == null)
                return false;

            if (!(_classType.GetConstructor(Type.EmptyTypes) == null && _classType.IsAbstract && _classType.IsSealed))
                if (_classInstance == null)
                    if (_classType.GetConstructor(Type.EmptyTypes) != null)
                        _classInstance = Activator.CreateInstance(_classType);
                    else
                        return false;

            _methodInformation = _classType.GetMethod(functionName, paramTypes.ToArray());
            if (_methodInformation == null)
                return false;

            return true;
        }
    }
}