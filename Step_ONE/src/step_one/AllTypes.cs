using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

namespace Step_ONE.src.step_one
{
    class AllTypes
    {
        public static void allTypes()
        {
            foreach (var r in Assembly.GetEntryAssembly().GetReferencedAssemblies())
            {
                var a = Assembly.Load(new AssemblyName(r.FullName));
                int methodCount = 0;
                foreach (var t in a.DefinedTypes)
                {
                    methodCount += t.GetMethods().Count();
                }

                Console.WriteLine(
                    "{0:N0} types with {1:N0}methods in {2} assembly.",
                    arg0:a.DefinedTypes.Count(),
                    arg1:methodCount,
                    arg2:r.Name);
            }
        }
    }
}
