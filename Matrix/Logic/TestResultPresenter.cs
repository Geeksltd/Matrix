using Matrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix.Logic
{
    class TestResultPresenter
    {
        public IEnumerable<TestResult> GenerateSamples(int num, Method method)
        {
            var parInfos = method.MethodInformation.GetParameters();
            for (var i = 0; i < num; i++)
            {
                var parameterValues = new List<object>();
                var cnt = 1;
                var strInvokedFunc = method.MethodInformation.Name + "(";
                foreach (var pInfo in parInfos)
                {
                    object param = null;
                    param = ValueSampler.SampleValue(pInfo.ParameterType);
                    if (param == null)
                    {
                        throw new NullReferenceException();
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
                var invokeResult = method.MethodInformation.Invoke(method.ClassInstance, parameterValues.ToArray());
                yield return new TestResult()
                {
                    Object = (method.ClassInstance == null ? "" : method.ClassInstance.ToString()),
                    Result = invokeResult.ToString(),
                    Value = strInvokedFunc
                };
            }
        }
    }
}
