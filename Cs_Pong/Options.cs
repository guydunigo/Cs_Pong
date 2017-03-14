namespace Cs_Pong
{
    static class Options
    {
        // System data :
        public const int DELAY_TIME = 10;
        public const int NB_BALLS = 1;
        public const double NB_STARS_PER_AREA = .0005;
        // Mass per volume :
        public const float MASS_PER_VOLUME_CIRC = 1;
        public const float MASS_PER_VOLUME_RECT = 1;
        public const float MASS_PER_VOLUME_TRI = 1;
        // Physics coefs :
        public const float G_COEF = 50;
        public const float ACCg = 9.81f * G_COEF;

        public const float FRICT_COEF = 1;
        public const float BOUNCE_COEF = 1;

        public const float CELERITY_LIM = 5;

        // Points coefs :
        public const int BAR_BONUS = 10;
        public const int WALLS_MALUS = -10;
        public const int TRIG_BONUS = 10;

        // Triangle powerups and traps :
        public const int TRIG_RADIUS = 20;
        public const int PROBA_BONUS_MALUS = 1000;

        public static Color TRIG_CEL_BON_COLOR = new Color(200, 200, 255); // Grey/White
        public static Color TRIG_CEL_MAL_COLOR = new Color(210, 100, 0); // Orange
        public static Color TRIG_POINTS_BON_COLOR = new Color(0,255,0); // Green
        public static Color TRIG_POINTS_MAL_COLOR = new Color(255, 0, 0);// Red
        public static Color TRIG_SIZE_BON_COLOR = new Color(0, 200, 200); // Cyan
        public static Color TRIG_SIZE_MAL_COLOR = new Color(200, 200, 0); // Yellow

        public const float TRIG_CEL_BON = .5f;
        public const int TRIG_CEL_MAL = 2;

        public const int TRIG_POINTS_BON = 50;
        public const int TRIG_POINTS_MAL = -50;

        public const float TRIG_SIZE_BON = .5f;
        public const int TRIG_SIZE_MAL = 2;
    }

}
