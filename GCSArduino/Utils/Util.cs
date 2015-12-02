using System;
using System.Linq;

namespace GCSArduino.Utils
{
    public static class Utils
    {
        public static double PI_D = 3.141592654; // as defined in RoboticsInfra/AlgoUtils/include/appConst.h
        public static double NM_TO_FEET = 6076.1155;
        public static double HOURS_TO_SEC = 3600.0;
        public static double RAD_TO_DEG = (180.0 / PI_D); //(180.0/Math.PI);
        public static double DEG_TO_RAD = (PI_D / 180.0); //(Math.PI/180.0);
        public static double FEET_TO_METER = 0.3048;
        public static double KNOTS_TO_FPS = (NM_TO_FEET / HOURS_TO_SEC);
        public static double NM_TO_METER = (NM_TO_FEET * FEET_TO_METER);
        public static double LIT_WEIGHT = 0.79; // Kg
        public static double LBS_TO_LIT = (0.4536 / LIT_WEIGHT);
        public static double LIT_TO_LBS = (1.0f / LBS_TO_LIT);
        public static double FEET_TO_KM = (0.3048 / 1000.0);
        public static double NM_TO_KM = (NM_TO_METER / 1000.0);
        public static double KM_TO_FEET = (1000.0 / 0.3048);
        public static double m2ft = 3.2808; // meters to feet 
        public static double Km2H_TO_M2Sec = 1 / 3.6;
        public static double M2Sec_TO_Km2H = 3.6;
        public static double RAYTER = 6366707.0;
        public static double EPSILON = 0.0001;
        public static double K0 = 0.9996;

        // *** Earth defines ***
        public static double WGS84_MAJOR = 6378137.0; /* meters */
        public static double WGS84_MINOR = 6356752.3142; /* meters */

        public static double WGS84_ECC1 = 0.081819190842622; /* first eccentricity */
        public static double WGS84_ECC1_SQR = 0.006694379990141399742371834884; /* first eccentricity squared */
        public static double WGS84_ECC2 = 0.082094437949696195116048215728705; /* second eccentricity */
        public static double WGS84_ECC2_SQR = 0.0067394967422765188201111188881042; /* second eccentricity squared */
        public static double EF = 0.0016792203863837255210200656218767;

        public static double A = WGS84_MAJOR;

        public static double R_EQ_D = (2.0925646E+07 / m2ft); // EARTH equatorial radius [m]
        public static double E_D = 0.00335288559; // Earth eccentricity

        public static void ToLatLon(double utmX, double utmY, string utmZone, out double latitude, out double longitude)
        {
            bool isNorthHemisphere = utmZone.Last() >= 'N';

            var diflat = -0.00066286966871111111111111111111111111;
            var diflon = -0.0003868060578;

            var zone = int.Parse(utmZone.Remove(utmZone.Length - 1));
            var c_sa = 6378137.000000;
            var c_sb = 6356752.314245;
            var e2 = Math.Pow((Math.Pow(c_sa, 2) - Math.Pow(c_sb, 2)), 0.5)/c_sb;
            var e2cuadrada = Math.Pow(e2, 2);
            var c = Math.Pow(c_sa, 2)/c_sb;
            var x = utmX - 500000;
            var y = isNorthHemisphere ? utmY : utmY - 10000000;

            var s = ((zone*6.0) - 183.0);
            var lat = y/(c_sa*0.9996);
            var v = (c/Math.Pow(1 + (e2cuadrada*Math.Pow(Math.Cos(lat), 2)), 0.5))*0.9996;
            var a = x/v;
            var a1 = Math.Sin(2*lat);
            var a2 = a1*Math.Pow((Math.Cos(lat)), 2);
            var j2 = lat + (a1/2.0);
            var j4 = ((3*j2) + a2)/4.0;
            var j6 = ((5*j4) + Math.Pow(a2*(Math.Cos(lat)), 2))/3.0;
            var alfa = (3.0/4.0)*e2cuadrada;
            var beta = (5.0/3.0)*Math.Pow(alfa, 2);
            var gama = (35.0/27.0)*Math.Pow(alfa, 3);
            var bm = 0.9996*c*(lat - alfa*j2 + beta*j4 - gama*j6);
            var b = (y - bm)/v;
            var epsi = ((e2cuadrada*Math.Pow(a, 2))/2.0)*Math.Pow((Math.Cos(lat)), 2);
            var eps = a*(1 - (epsi/3.0));
            var nab = (b*(1 - epsi)) + lat;
            var senoheps = (Math.Exp(eps) - Math.Exp(-eps))/2.0;
            var delt = Math.Atan(senoheps/(Math.Cos(nab)));
            var tao = Math.Atan(Math.Cos(delt)*Math.Tan(nab));

            longitude = ((delt*(180.0/Math.PI)) + s) + diflon;
            latitude = ((lat +
                         (1 + e2cuadrada*Math.Pow(Math.Cos(lat), 2) -
                          (3.0/2.0)*e2cuadrada*Math.Sin(lat)*Math.Cos(lat)*(tao - lat))*(tao - lat))*(180.0/Math.PI)) +
                       diflat;
        }

        public static double DegreeBearing(double lat1, double lon1,double lat2, double lon2)
        {
            const double R = 6371; //earth’s radius (mean radius = 6,371km)
            var dLon = ToRad(lon2 - lon1);
            var dPhi = Math.Log(
                Math.Tan(ToRad(lat2) / 2 + Math.PI / 4) / Math.Tan(ToRad(lat1) / 2 + Math.PI / 4));
            if (Math.Abs(dLon) > Math.PI)
                dLon = dLon > 0 ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);
            return ToBearing(Math.Atan2(dLon, dPhi));
        }

        public static void PointRadialDistance(ref double lat1,ref double lon1,
               double bearing, double range)
        {
            const double EarthRadius = 6378137.0;

            double latA = ToRad(lat1);
            double lonA = ToRad(lon1);
            double angularDistance = range / EarthRadius;
            double trueCourse = ToRad(bearing);

            double lat = Math.Asin(Math.Sin(latA) * Math.Cos(angularDistance) + Math.Cos(latA) * Math.Sin(angularDistance) * Math.Cos(trueCourse));

            double dlon = Math.Atan2(Math.Sin(trueCourse) * Math.Sin(angularDistance) * Math.Cos(latA), Math.Cos(angularDistance) - Math.Sin(latA) * Math.Sin(lat));
            double lon = ((lonA + dlon + Math.PI) % (Math.PI * 2)) - Math.PI;

           


            lat1 = ToDegrees(lat);
            lon1 = ToDegrees(lon);

        }
        public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2, String sr)
        {

              double theta = lon1 - lon2;
              double dist = Math.Sin(ToRad(lat1)) * Math.Sin(ToRad(lat2)) + Math.Cos(ToRad(lat1)) * Math.Cos(ToRad(lat2)) * Math.Cos(ToRad(theta));
              dist = Math.Acos(dist);
              dist = ToDegrees(dist);
              dist = dist * 60 * 1.1515;
              if (sr.Equals("K")) {
                dist = dist * 1.609344;
              }
              else if (sr.Equals("N"))
              {
                dist = dist * 0.8684;
                }
              return (dist);
            }
        
        public static double ToRad(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        public static double ToDegrees(double radians)
        {
            return radians * 180 / Math.PI;
        }

        public static double ToBearing(double radians)
        {
            // convert radians to degrees (as bearing: 0...360)
            return (ToDegrees(radians) + 360) % 360;
        }
         public static void OffsetsLatLon(ref double lat,ref  double lon ,int offset)
         {
             //Earth’s radius, sphere
             const double R = 6378137;

            //offsets in meters
             int dn = offset;
             int de = offset;

            //Coordinate offsets in radians
             double dLat = dn/R;
             double dLon = de/(R*Math.Cos(Math.PI*lat/180));

            //OffsetPosition, decimal degrees
           
             lat = lat + dLat*180/Math.PI;
             lon = lon + dLon*180/Math.PI;

        }
        //public static void ConvertMercatorToGeo(ref double mercatorX_lon, ref double mercatorY_lat)
        //{
        //    if ((Math.Abs(mercatorX_lon) > 180 || Math.Abs(mercatorY_lat) > 90))
        //        return;

        //    double num = mercatorX_lon * 0.017453292519943295;
        //    double x = 6378137.0 * num;
        //    double a = mercatorY_lat * 0.017453292519943295;

        //    mercatorX_lon = x;
        //    mercatorY_lat = 3189068.5 * Math.Log((1.0 + Math.Sin(a)) / (1.0 - Math.Sin(a)));
        //}
        //public static void ConvertGeoToMercator(ref double mercatorX_lon, ref double mercatorY_lat)
        //{

        //    if (Math.Abs(mercatorX_lon) < 180 && Math.Abs(mercatorY_lat) < 90)
        //        return;

        //    if ((Math.Abs(mercatorX_lon) > 20037508.3427892) || (Math.Abs(mercatorY_lat) > 20037508.3427892))
        //        return;

        //    double x = mercatorX_lon;
        //    double y = mercatorY_lat;
        //    double num3 = x / 6378137.0;
        //    double num4 = num3 * 57.295779513082323;
        //    double num5 = Math.Floor((double)((num4 + 180.0) / 360.0));
        //    double num6 = num4 - (num5 * 360.0);
        //    double num7 = 1.5707963267948966 - (2.0 * Math.Atan(Math.Exp((-1.0 * y) / 6378137.0)));
        //    mercatorX_lon = num6;
        //    mercatorY_lat = num7 * 57.295779513082323;

        //}
    }
   
}
