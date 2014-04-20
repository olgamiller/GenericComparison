/*
TestObjects - Objects for ComparisonGenerator testing

Written in 2014 by <Olga Miller> <olga.rgb@googlemail.com>
To the extent possible under law, the author(s) have dedicated all copyright and related and neighboring rights to this software to the public domain worldwide.
This software is distributed without any warranty.
You should have received a copy of the CC0 Public Domain Dedication along with this software. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.
*/

using System;
using System.Diagnostics;
using ComparisonGenerator;

namespace GenericComparison
{
	public class Object3
	{
	}

	public class Object2
	{
		private int PropIntPrivate { get; set; }
		//public int PropInt { private get; set; }
		public string PropString { get; set; }
		public string PropGetString { get { return PropString; } }
		public Object3 PropObject3 { get; set; }

		public Object2(string s)
		{
			PropIntPrivate = 1;
			//PropInt = 1;
			PropString = s;
			PropObject3 = new Object3();
		}
	}

	public class Object1
	{
		public int PropInt { get; set; }
		public Object2 PropObject2 { get; set; }

		public Object1(int i, string s)
		{
			PropInt = i;
			PropObject2 = new Object2(s);
		}
	}
}