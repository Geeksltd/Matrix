using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using EnvDTE;
using VSLangProj;

namespace Matrix
{
    public class SamplePresenter
    {
        private MethodInfo MI;
        private Type ClassType;
        private object classInstance = null;
        private bool flagOk = true;
        public bool FlagOk
        {
            get
            {
                return flagOk;
            }
        }

        string @namespace = "", @declaringType = "", @methodname = "", @paramsType = "", @return = "", @doc = "", @tRows = "", @remarks = "";
        string @rowTemp = "<tr><td> @object </td><td> @call </td><td>➜ @result</td></tr>";
        string @remarkTemp = "<h2>Remarks</h2><ul>@lis</ul>";
        string @liTemp = "<li>@li</li>";

        string strPageContent = Constant.HtmlPage;
        public SamplePresenter()
        {

        }
        public SamplePresenter(Microsoft.CodeAnalysis.ISymbol symbol)
        {   
            string asmName = symbol.ContainingAssembly.Name;
            if (asmName == "zarif")
            {
                flagOk = false;
                return;
            }
            DTE2 appDTE = (DTE2)Package.GetGlobalService(typeof(SDTE));
            Project curProj = appDTE.ActiveDocument.ProjectItem.ContainingProject;
            VSProject vsCurProj = (VSProject)curProj.Object;
            string asmFilePath = vsCurProj.References.OfType<Reference>().FirstOrDefault(r => r.Name == asmName).Path;

            string className = symbol.ContainingType.ToDisplayString(Microsoft.CodeAnalysis.SymbolDisplayFormat.FullyQualifiedFormat).Replace("global::","");
            string functionName = symbol.Name;

            List<Type> paramTypes = new List<Type>();
            foreach (var parm in ((Microsoft.CodeAnalysis.IMethodSymbol)symbol).Parameters)
            {
                paramTypes.Add(parm.Type.Name.ToString().ToType());
            }

            Assembly objAssembly = Assembly.LoadFrom(asmFilePath);
            ClassType = objAssembly.GetType(className);
            //var ClassCons2 = ClassType.GetConstructors();
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
            string StyleFilePath = Path.GetTempPath() + "Style.css";
            string SampleFilePath = Path.GetTempPath() + "present.html";
            if (!File.Exists(StyleFilePath))
            {
                File.WriteAllText(StyleFilePath, Constant.StylePage);
            }
            
            //-----------Assigning Value---------
            @namespace = ClassType.Namespace;
            if (MI.DeclaringType != null)
            {
                declaringType = "<p><em>DeclaringType: <strong>"+ MI.DeclaringType.Name + "</strong></em></p>";
            }
            //remarks = "Remarks of method";
            doc = symbol.GetDocumentationCommentXml();
            string @nameSpaceDot = @namespace + ".";
            @methodname = MI.Name;
            @return = MI.ReturnType.Name;
            ParameterInfo[] parInfos = MI.GetParameters();
            
            var cnt = 1;
            foreach (ParameterInfo pInfo in parInfos)
            {
                @paramsType += pInfo.ParameterType.Name;
                if (parInfos.Count() != cnt)
                {
                    @paramsType += ",";
                }
                cnt += 1;
            }
            if (parInfos.Count() == 0)
            {
                GenerateSamples(1);
            }
            else
            {
                GenerateSamples(3);
            }
            
            //-----------------------------------
            strPageContent = strPageContent.Replace("@namespace", @namespace).Replace("@declaringType",declaringType).Replace("@methodname", @methodname).Replace("@return",@return);
            strPageContent = strPageContent.Replace("@params", paramsType).Replace("@doc",@doc).Replace("@tRows",@tRows).Replace("@remarks",remarks);
            System.IO.File.WriteAllText(SampleFilePath, strPageContent);
        }

        public void GenerateSamples(int num)
        {
            var parInfos = MI.GetParameters();
            for (int i = 0; i < num; i++)
            {
                List<object> parameterValues = new List<object>();
                var cnt = 1;
                var strInvokedFunc = MI.Name + "(";
                foreach (ParameterInfo pInfo in parInfos)
                {
                    object param = null;
                    param = ValueSampler.SampleValue(pInfo.ParameterType);
                    if (param == null)
                    {
                        flagOk = false;
                        return;
                    }
                    else
                    {
                        parameterValues.Add(param);
                        strInvokedFunc += param.ToString();
                        if (parInfos.Count() != cnt)
                        {
                            strInvokedFunc += ",";
                        }
                    }
                    cnt += 1;
                }
                strInvokedFunc += ")";
                var InvokeResult = MI.Invoke(classInstance, parameterValues.ToArray());
                @tRows += rowTemp.Replace("@object",(classInstance == null? "" : classInstance.ToString())).Replace("@call", strInvokedFunc).Replace("@result", InvokeResult.ToString());
            }
        }
    }
}
