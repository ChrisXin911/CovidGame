using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CovidGame
{
    public class Covid //: DrawableGameComponent
    {
        public bool IsAlive { get; set; } = true;    
        public int Row { get; set; }
        public int Col { get; set; }
        
        public Covid() { }
        public Covid(int x, int y)
        {
            Row = x;
            Col = y;
        }
 
        //the covid the position
           public Rectangle Rectangle { get; set; }
       
    }
}
