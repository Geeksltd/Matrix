using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Matrix.Utils
{
    public static class Extensions
    {
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

        public static SyntaxTree GetSyntaxTree(this string strCode) => CSharpSyntaxTree.ParseText(strCode);
        public static CSharpCompilation CreateCompilation(this IEnumerable<SyntaxTree> syntaxTrees) => CSharpCompilation.Create("MyAshes", syntaxTrees: syntaxTrees, references: new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });
        public static CSharpCompilation CreateCompilation(this SyntaxTree syntax) => CreateCompilation(new[] { syntax });

        public static ObservableCollection<T> ConvertReplace<T>(this ObservableCollection<T> original, IEnumerable<T> newItems)
        {
            original.Clear();
            foreach (var item in newItems)
            {
                original.Add(item);
            }
            return original;
        }
        public static bool ChangeAndNotify<T>(this PropertyChangedEventHandler handler,
            ref T field, T value, Expression<Func<T>> memberExpression)
        {
            if (memberExpression == null)
            {
                throw new ArgumentNullException("memberExpression");
            }
            var body = memberExpression.Body as MemberExpression;
            if (body == null)
            {
                throw new ArgumentException("Lambda must return a property.");
            }
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            var vmExpression = body.Expression as ConstantExpression;
            if (vmExpression != null)
            {
                LambdaExpression lambda = Expression.Lambda(vmExpression);
                Delegate vmFunc = lambda.Compile();
                object sender = vmFunc.DynamicInvoke();

                if (handler != null)
                {
                    handler(sender, new PropertyChangedEventArgs(body.Member.Name));
                }
            }

            field = value;
            return true;
        }
    }
}
