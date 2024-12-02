using System;
namespace RecibosNominaCGA.Helpers
{
	public class Settings
	{
        //0=default,1=light,2=Dark
        const int theme = 0;
        const string version_actual = "";
        const string aplicacion = "AppReciboElectronico";
        public static int Theme
        {
            get => Preferences.Get(nameof(Theme), theme);
            set => Preferences.Set(nameof(Theme), value);
        }

        private static string VERSION_ACTUAL
        {
            get => Preferences.Get(nameof(VERSION_ACTUAL), version_actual);
            set => Preferences.Set(nameof(VERSION_ACTUAL), value);
        }
        private static string APLICACION
        {
            get => aplicacion;
        }
        public static string ObtenerAplicacion()
        {
            return APLICACION;
        }
        public static string Obtenerversion()
        {
            if (VERSION_ACTUAL == null)
            {
                VERSION_ACTUAL = VersionTracking.Default.CurrentVersion;
            }
            return VERSION_ACTUAL;
        }
    }
}

