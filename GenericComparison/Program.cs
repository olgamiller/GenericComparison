/*
GenericComparison - Applying of ComparisonGenerator

Written in 2014 by <Olga Miller> <olga.rgb@googlemail.com>
To the extent possible under law, the author(s) have dedicated all copyright and related and neighboring rights to this software to the public domain worldwide.
This software is distributed without any warranty.
You should have received a copy of the CC0 Public Domain Dedication along with this software. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.
*/

using System;
using System.Linq.Expressions;
using ComparisonGenerator;

namespace GenericComparison
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			TestEqualsMethodGenerator();
			CompareRuntime();
		}

		private static void TestEqualsMethodGenerator()
		{
			EqualsMethodGenerator cg = new EqualsMethodGenerator();
			Expression<Func<Object1, Object1, bool>> expression = cg.GenerateEqualsMethodExpressionRecurse<Object1>();

			Func<Object1, Object1, bool> EqualsMethod = expression.Compile();

			Object1 oL = new Object1(3, "string value");
			Object1 oR = new Object1(3, "string value");

			Console.WriteLine("Should be true:\n\r");
			Console.WriteLine("oL.Equals(oR):        " + oL.Equals(oR));
			Console.WriteLine("oL == oR:             " + (oL == oR));
			Console.WriteLine("equalMethod(oL, oR):  " + EqualsMethod(oL, oR));

			oL = new Object1(3, "string value L");
			oR = new Object1(3, "string value R");

			Console.WriteLine("\n\rShould be false:\n\r");
			Console.WriteLine("oL.Equals(oR):        " + oL.Equals(oR));
			Console.WriteLine("oL == oR:             " + (oL == oR));
			Console.WriteLine("equalMethod(oL, oR):  " + EqualsMethod(oL, oR));
		}

		private static void CompareRuntime()
		{
			Object1[] l;
			Object1[] r;

			RuntimeTest test = new RuntimeTest(new Builder());

			RuntimeTest.CreateEqualTestObjects(out l, out r, 1000000);
			test.CompareRuntime(l, r);

			RuntimeTest.CreateUnequalTestObjects(out l, out r, 1000000);
			test.CompareRuntime(l, r);
		}
	}
}