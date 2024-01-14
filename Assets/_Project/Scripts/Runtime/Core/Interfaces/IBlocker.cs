using System.Collections.Generic;
using TNRD;

namespace CanvasDEV.Runtime.Core.Interfaces
{
    public interface IBlocker
    {
        public List<IBlockable> Blockables { get; set; }

        public void Block();
        public void UnBlock();
    }
}