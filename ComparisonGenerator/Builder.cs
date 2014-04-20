using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace ComparisonGenerator
{
	public class Builder
	{
		private int _count = 0;
		private readonly string _assemblyName = "MyDynamicAssembly";
		private readonly string _moduleName = "MyDynamicModule";
		private readonly string _typeName = "MyType";
		private readonly string _methodName = "MyMethod";
		private ModuleBuilder _moduleBuilder;

		public Builder()
		{
			AssemblyName assemblyName = new AssemblyName(_assemblyName);
			AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
			_moduleBuilder = assemblyBuilder.DefineDynamicModule(_moduleName);
		}

		public T GetCompiledMethod<T>(Expression<T> expression)
		{
			TypeBuilder typeBuilder = _moduleBuilder.DefineType(_typeName + _count++);
			MethodBuilder methodBuilder = typeBuilder.DefineMethod(_methodName, MethodAttributes.Public | MethodAttributes.Static);
			expression.CompileToMethod(methodBuilder);
			MethodInfo info = typeBuilder.CreateType().GetMethod(_methodName);

			return (T)Convert.ChangeType(Delegate.CreateDelegate(typeof(T), info), typeof(T));
		}
	}
}

