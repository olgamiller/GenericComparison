using System;
using System.Diagnostics;
using System.Linq.Expressions;
using ComparisonGenerator;

namespace GenericComparison
{
	public class RuntimeTest
	{
		private  Stopwatch _sw;
		private Builder _builder;

		public RuntimeTest(Builder builder)
		{
			_sw = new Stopwatch();
			_builder = builder;
		}

		public void CompareRuntime(Object1[] l, Object1[] r)
		{
			EqualsMethodGenerator cg = new EqualsMethodGenerator();
			var expression = cg.GenerateEqualsMethodExpressionRecurse<Object1>();

			Func<Object1, Object1, bool> EqualsMethod = expression.Compile();
			Test(l, r, EqualsMethod, "Compile():         ");

			EqualsMethod = _builder.GetCompiledMethod<Func<Object1, Object1, bool>>(expression);
			Test(l, r, EqualsMethod, "CompileToMethod(): ");
		}

		public static void CreateEqualTestObjects(out Object1[] l, out Object1[] r, int len)
		{
			l = new Object1[len];
			r = new Object1[len];

			for (int i = 0; i < len; i++)
			{
				int valueL = i;
				int valueR = i;

				string stringL = "test" + valueL;
				string stringR = "test" + valueR;

				l[i] = new Object1(valueL, stringL);
				r[i] = new Object1(valueR, stringR);
			}
		}

		public static void CreateUnequalTestObjects(out Object1[] l, out Object1[] r, int len)
		{
			l = new Object1[len];
			r = new Object1[len];

			for (int i = 0; i < len; i++)
			{
				int valueL = (i & (i+1)) == 0 ? 42 : i;
				int valueR = i;

				string stringL = "test" + valueL;
				string stringR = "test" + valueR;

				l[i] = new Object1(valueL, stringL);
				r[i] = new Object1(valueR, stringR);
			}
		}

		private void Test(Object1[] l, Object1[] r, Func<Object1, Object1, bool> EqualsMethod, string sUsedCompileMethodName)
		{
			int numEqual = 0;
			_sw.Restart();

			for (int i = 0; i < l.Length; i++)
			{
				numEqual += EqualsMethod(l[i], r[i]) ? 1 : 0;
			}

			_sw.Stop();

			Console.WriteLine(string.Format("\n\r{0} {1}", sUsedCompileMethodName, _sw.ElapsedMilliseconds));

			if (numEqual == l.Length)
				Console.WriteLine(string.Format("\tAll {0} objects have equal property values.", l.Length));
			else
				Console.WriteLine(string.Format("\t{0} of {1} objects have unequal property values.", l.Length - numEqual, l.Length));
		}
	}
}

