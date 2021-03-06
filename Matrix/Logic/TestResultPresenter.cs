﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Matrix.Models;
using Matrix.Utils;

namespace Matrix.Logic
{
    static class TestResultPresenter
    {
        public static IEnumerable<TestResult> GenerateSamples(int num, Method method)
        {
            var parInfos = method.MethodInformation.GetParameters();
            for (var i = 0; i < num; i++)
            {
                var parameterValues = GetParameterValues(parInfos).ToList();
                var invokeResult = method.MethodInformation.Invoke(method.ClassInstance, parameterValues.ToArray());
                yield return new TestResult
                {
                    Object = method.ClassInstance ?? "",
                    Result = invokeResult,
                    ParameterValue = method.ToInformation(parameterValues)
                };
            }
        }

        public static IEnumerable<TestResult> GenerateSamples(Method method, IEnumerable<Example> examples)
        {
            foreach (var example in examples)
            {
                foreach (var item in example.SampleObjects)
                {
                    var thisMethod = example.Type.GetMethod(example.MethodName);
                    var invokeResult = thisMethod.Invoke(item.Instance, item.Parameters.Select(x => x.Value).ToArray());
                    yield return new TestResult
                    {
                        Object = item.Instance,
                        Result = invokeResult,
                        ParameterValue = method.ToInformation(item.Parameters.Select(x => x.Value))
                    };
                }
            }
        }

        public static TestResult GenerateSample(Method method, IEnumerable<Parameter> parameters, Constructor ctor = null, IEnumerable<Parameter> selectedCtorParameters = null)
        {
            var parInfos = method.MethodInformation.GetParameters();
            object instance = null;
            if (ctor == null)
                instance = Activator.CreateInstance(method.ClassInstance.GetType());
            else
                instance = Activator.CreateInstance(method.ClassInstance.GetType(), selectedCtorParameters.Select(x => Convert.ChangeType(x.Value, x.Type)).ToArray());

            var invokeResult = method.MethodInformation.Invoke(instance, parameters.Select(x => Convert.ChangeType(x.Value, x.Type)).ToArray());
            return new TestResult
            {
                Object = instance,
                Result = invokeResult,
                ParameterValue = method.ToInformation(parameters)
            };
        }

        public static TestResult GenerateSample(Method method, IEnumerable<Parameter> parameters)
        {
            var parInfos = method.MethodInformation.GetParameters();
            var invokeResult = method.MethodInformation.Invoke(method.ClassInstance, parameters.Select(x => Convert.ChangeType(x.Value, x.Type)).ToArray());
            return new TestResult
            {
                Object = method.ClassInstance ?? "",
                Result = invokeResult,
                ParameterValue = method.ToInformation(parameters)
            };
        }

        static IEnumerable<object> GetParameterValues(ParameterInfo[] parInfos)
        {
            foreach (var pInfo in parInfos)
            {
                var param = SampleGenerator.GenerateSample(pInfo.ParameterType);
                if (param == null)
                    throw new NullReferenceException("No matrix params");
                else
                    yield return param;
            }
        }
    }
}