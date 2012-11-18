using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Peggle
{
    class QuadCollection
    {
        List<Quad> quads = new List<Quad>();

        public void addQuad(Quad newQuad)
        {
            quads.Add(newQuad);
        }
        
    }
}
