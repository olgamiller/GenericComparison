
Generic Comparison - Create generic comparison method via reflection

Written in 2014 by <Olga Miller> <olga.rgb@googlemail.com>
To the extent possible under law, the author(s) have dedicated all copyright and related and neighboring rights to this software to the public domain worldwide.
This software is distributed without any warranty.
You should have received a copy of the CC0 Public Domain Dedication along with this software. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.

Reflection techniques are used to create expressions while traversing object properties.
These expressions can be compiled to native methods using Compile() or CompileToMethod(MethodBuilder).
This can be useful e.g. for Unit-Tests or to override Equals(object) method of objects with many properties.