using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Peggle
{
    //I've chosen not to use the default ICloneable interface as it's purpose if ambiguous.
    interface IShallowClone<T>
    {
        T clone();
    }
}
