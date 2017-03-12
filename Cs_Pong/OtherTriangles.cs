using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cs_Pong
{
    class Triangle_Size_Bonus : Triangle
    {
        public Triangle_Size_Bonus() : base() { }
        public Triangle_Size_Bonus(float posX, float posY, float __radius,float celX, float celY, float _rotZ, bool _isFixed = false) : base(posX, posY, __radius, celX, celY, _rotZ, _isFixed)
        {
            color = Options.TRIG_SIZE_BON_COLOR;
        }

        public override void effects(Circle circ)
        {
            float newSize = circ.radius * Options.TRIG_SIZE_BON;
            if (newSize < 200 && newSize > 5) circ.radius = newSize;
            isOff = true;
        }
    }
    class Triangle_Size_Malus : Triangle
    {
        public Triangle_Size_Malus() : base() { }
        public Triangle_Size_Malus(float posX, float posY, float __radius, float celX, float celY, float _rotZ, bool _isFixed = false) : base(posX, posY, __radius, celX, celY, _rotZ, _isFixed)
        {
            color = Options.TRIG_SIZE_MAL_COLOR;
        }

        public override void effects(Circle circ)
        {
            float newSize = circ.radius * Options.TRIG_SIZE_MAL;
            if (newSize < 200 && newSize > 5) circ.radius = newSize;
            isOff = true;
        }
    }
    class Triangle_Points_Bonus : Triangle
    {
        public Triangle_Points_Bonus() : base() { }
        public Triangle_Points_Bonus(float posX, float posY, float __radius, float celX, float celY, float _rotZ, bool _isFixed = false) : base(posX, posY, __radius, celX, celY, _rotZ, _isFixed)
        {
            color = Options.TRIG_POINTS_BON_COLOR;
        }

        public override void effects(Circle circ)
        {
            circ.points += Options.TRIG_POINTS_BON;
            isOff = true;
        }
    }
    class Triangle_Points_Malus : Triangle
    {
        public Triangle_Points_Malus() : base() { }
        public Triangle_Points_Malus(float posX, float posY, float __radius, float celX, float celY, float _rotZ, bool _isFixed = false) : base(posX, posY, __radius, celX, celY, _rotZ, _isFixed)
        {
            color = Options.TRIG_POINTS_MAL_COLOR;
        }

        public override void effects(Circle circ)
        {
            circ.points += Options.TRIG_POINTS_MAL;
            isOff = true;
        }
    }
    class Triangle_Cel_Bonus : Triangle
    {
        public Triangle_Cel_Bonus() : base() { }
        public Triangle_Cel_Bonus(float posX, float posY, float __radius, float celX, float celY, float _rotZ, bool _isFixed = false) : base(posX, posY, __radius, celX, celY, _rotZ, _isFixed)
        {
            color = Options.TRIG_CEL_BON_COLOR.Clone();
        }

        public override void effects(Circle circ)
        {
            circ.celX *= Options.TRIG_CEL_BON;
            circ.celY *= Options.TRIG_CEL_BON;
            isOff = true;
        }
    }
    class Triangle_Cel_Malus : Triangle
    {
        public Triangle_Cel_Malus() : base() { }
        public Triangle_Cel_Malus(float posX, float posY, float __radius, float celX, float celY, float _rotZ, bool _isFixed = false) : base(posX, posY, __radius, celX, celY, _rotZ, _isFixed)
        {
            color = Options.TRIG_CEL_MAL_COLOR.Clone();
        }

        public override void effects(Circle circ)
        {
            circ.celX *= Options.TRIG_CEL_MAL;
            circ.celY *= Options.TRIG_CEL_MAL;
            isOff = true;
        }
    }
}
