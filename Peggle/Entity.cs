using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Peggle
{
    public interface Entity
    {
        Location location { get; set; }
    }
}
