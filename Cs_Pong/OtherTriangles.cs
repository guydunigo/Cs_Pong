namespace Cs_Pong
{
    class Triangle_Size_Bonus : Triangle
    {
        public Triangle_Size_Bonus() : this(0,0,0,0,0,0,false) { }
        public Triangle_Size_Bonus(float posX, float posY, float __radius,float celX, float celY, float _rotZ, bool _isFixed = false) : base(posX, posY, __radius, celX, celY, _rotZ, _isFixed)
        {
            Color = Options.TRIG_SIZE_BON_COLOR;
        }

        public override void Effects(Circle circ)
        {
            float newSize = circ.Radius * Options.TRIG_SIZE_BON;
            if (newSize < 150 && newSize > 5) circ.Radius = newSize;
            IsOff = true;
        }
    }
    class Triangle_Size_Malus : Triangle
    {
        public Triangle_Size_Malus() : this(0, 0, 0, 0, 0, 0, false) { }
        public Triangle_Size_Malus(float posX, float posY, float __radius, float celX, float celY, float _rotZ, bool _isFixed = false) : base(posX, posY, __radius, celX, celY, _rotZ, _isFixed)
        {
            Color = Options.TRIG_SIZE_MAL_COLOR.Clone();
        }

        public override void Effects(Circle circ)
        {
            float newSize = circ.Radius * Options.TRIG_SIZE_MAL;
            if (newSize < 150 && newSize > 5) circ.Radius = newSize;
            IsOff = true;
        }
    }
    class Triangle_Points_Bonus : Triangle
    {
        public Triangle_Points_Bonus() : this(0, 0, 0, 0, 0, 0, false) { }
        public Triangle_Points_Bonus(float posX, float posY, float __radius, float celX, float celY, float _rotZ, bool _isFixed = false) : base(posX, posY, __radius, celX, celY, _rotZ, _isFixed)
        {
            Color = Options.TRIG_POINTS_BON_COLOR.Clone();
        }

        public override void Effects(Circle circ)
        {
            circ.points += Options.TRIG_POINTS_BON;
            IsOff = true;
        }
    }
    class Triangle_Points_Malus : Triangle
    {
        public Triangle_Points_Malus() : this(0, 0, 0, 0, 0, 0, false) { }
        public Triangle_Points_Malus(float posX, float posY, float __radius, float celX, float celY, float _rotZ, bool _isFixed = false) : base(posX, posY, __radius, celX, celY, _rotZ, _isFixed)
        {
            Color = Options.TRIG_POINTS_MAL_COLOR.Clone();
        }

        public override void Effects(Circle circ)
        {
            circ.points += Options.TRIG_POINTS_MAL;
            IsOff = true;
        }
    }
    class Triangle_Cel_Bonus : Triangle
    {
        public Triangle_Cel_Bonus() : this(0, 0, 0, 0, 0, 0, false) { }
        public Triangle_Cel_Bonus(float posX, float posY, float __radius, float celX, float celY, float _rotZ, bool _isFixed = false) : base(posX, posY, __radius, celX, celY, _rotZ, _isFixed)
        {
            Color = Options.TRIG_CEL_BON_COLOR.Clone();
        }

        public override void Effects(Circle circ)
        {
            circ.CelX *= Options.TRIG_CEL_BON;
            circ.CelY *= Options.TRIG_CEL_BON;
            IsOff = true;
        }
    }
    class Triangle_Cel_Malus : Triangle
    {
        public Triangle_Cel_Malus() : this(0, 0, 0, 0, 0, 0, false) { }
        public Triangle_Cel_Malus(float posX, float posY, float __radius, float celX, float celY, float _rotZ, bool _isFixed = false) : base(posX, posY, __radius, celX, celY, _rotZ, _isFixed)
        {
            Color = Options.TRIG_CEL_MAL_COLOR.Clone();
        }

        public override void Effects(Circle circ)
        {
            circ.CelX *= Options.TRIG_CEL_MAL;
            circ.CelY *= Options.TRIG_CEL_MAL;
            IsOff = true;
        }
    }
}
