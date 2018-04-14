using Matrix.Models;
using Matrix.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                var parameterValues = GetParameterValues(parInfos).ToList();
                var invokeResult = method.MethodInformation.Invoke(method.ClassInstance, parameterValues.ToArray());
                yield return new TestResult()
                {
                    Object = (method.ClassInstance == null ? "" : method.ClassInstance),
                    Result = invokeResult,
                    ParameterValue = method.ToInformation(parameterValues)
                };
            }
        }
        private IEnumerable<object> GetParameterValues(ParameterInfo[] parInfos)
        {
            foreach (var pInfo in parInfos)
            {
                object param = ValueSampler.SampleValue(pInfo.ParameterType);
                if (param == null)
                {
                    throw new NullReferenceException("No matrix params");
                }
                else
                {
                    yield return param;
                }

            }
        }
    }
}
