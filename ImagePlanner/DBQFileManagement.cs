//Module for installing the observing list database search file (embedded in the app as ImagePlanner.dbq) also installed as same name.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace ImagePlanner
{
    public partial class DBQFileManagement
    {

        public enum SearchType
        {
            Galaxy,
            Cluster,
            Nebula
        }


        //Location to deposit database query: "C:\Users\" + System.Environment.UserName + 
        //   "\Documents\Software Bisque\TheSkyX Professional Edition\Database Queries\ImagePlanner.dbq"

        private static string ImagePlannerGalaxyDestinationSubPath = "\\Documents\\Software Bisque\\TheSkyX Professional Edition\\Database Queries\\ImagePlannerGalaxy.dbq";
        private static string ImagePlannerClusterDestinationSubPath = "\\Documents\\Software Bisque\\TheSkyX Professional Edition\\Database Queries\\ImagePlannerCluster.dbq";
        private static string ImagePlannerNebulaDestinationSubPath = "\\Documents\\Software Bisque\\TheSkyX Professional Edition\\Database Queries\\ImagePlannerNebula.dbq";

        public static bool DBQsInstalled()
        {
            //Checks to see if search database file is already installed or not
            string ImagePlannerGalaxyDestinationPath = "C:\\Users\\" + System.Environment.UserName + ImagePlannerGalaxyDestinationSubPath;
            string ImagePlannerClusterDestinationPath = "C:\\Users\\" + System.Environment.UserName + ImagePlannerClusterDestinationSubPath;
            string ImagePlannerNebulaDestinationPath = "C:\\Users\\" + System.Environment.UserName + ImagePlannerNebulaDestinationSubPath;
            return (File.Exists(ImagePlannerGalaxyDestinationPath) && File.Exists(ImagePlannerClusterDestinationPath) && File.Exists(ImagePlannerNebulaDestinationPath));
        }

        public static void InstallDBQs()
        {
            //Make sure the dbq file paths are set up correctly
            string ImagePlannerGalaxyDestinationPath = "C:\\Users\\" + System.Environment.UserName + ImagePlannerGalaxyDestinationSubPath;
            string ImagePlannerClusterDestinationPath = "C:\\Users\\" + System.Environment.UserName + ImagePlannerClusterDestinationSubPath;
            string ImagePlannerNebulaDestinationPath = "C:\\Users\\" + System.Environment.UserName + ImagePlannerNebulaDestinationSubPath;

            //Install the dbq file
            //ImagePlannerGalaxyDestinationPath = "C:\\Users\\" + System.Environment.UserName +
            //    "\\Documents\\Software Bisque\\TheSkyX Professional Edition\\Database Queries\\ImagePlannerGalaxy.dbq";

            ////Collect the file contents to be written
            Assembly dgassembly = Assembly.GetExecutingAssembly();
            Stream dgstream = dgassembly.GetManifestResourceStream("ImagePlanner.ImagePlannerGalaxy.dbq");
            Byte[] dgbytes = new Byte[dgstream.Length];
            FileStream dbqgfile = File.Create(ImagePlannerGalaxyDestinationPath);
            int dgreadout = dgstream.Read(dgbytes, 0, (int)dgstream.Length);
            dbqgfile.Close();
            //write to destination file
            File.WriteAllBytes(ImagePlannerGalaxyDestinationPath, dgbytes);
            dgstream.Close();

            //ImagePlannerClusterDestinationPath = "C:\\Users\\" + System.Environment.UserName +
            //    "\\Documents\\Software Bisque\\TheSkyX Professional Edition\\Database Queries\\ImagePlannerCluster.dbq";

            //Collect the file contents to be written
            Assembly dcassembly = Assembly.GetExecutingAssembly();
            Stream dcstream = dcassembly.GetManifestResourceStream("ImagePlanner.ImagePlannerCluster.dbq");
            Byte[] dcbytes = new Byte[dcstream.Length];
            FileStream dbqcfile = File.Create(ImagePlannerClusterDestinationPath);
            int dcreadout = dcstream.Read(dcbytes, 0, (int)dcstream.Length);
            dbqcfile.Close();
            //write to destination file
            File.WriteAllBytes(ImagePlannerClusterDestinationPath, dcbytes);
            dcstream.Close();

            //ImagePlannerNebulaDestinationPath = "C:\\Users\\" + System.Environment.UserName +
            //    "\\Documents\\Software Bisque\\TheSkyX Professional Edition\\Database Queries\\ImagePlannerNebula.dbq";

            //Collect the file contents to be written
            Assembly dnassembly = Assembly.GetExecutingAssembly();
            Stream dnstream = dnassembly.GetManifestResourceStream("ImagePlanner.ImagePlannerNebula.dbq");
            Byte[] dnbytes = new Byte[dnstream.Length];
            FileStream dbqnfile = File.Create(ImagePlannerNebulaDestinationPath);
            int dnreadout = dnstream.Read(dnbytes, 0, (int)dnstream.Length);
            dbqnfile.Close();
            //write to destination file
            File.WriteAllBytes(ImagePlannerNebulaDestinationPath, dnbytes);
            dnstream.Close();
            return;
        }

        public static string GetDBQPath(SearchType dbqType)
        {
            //Returns a path to the observing list query for the dbqType

            string ImagePlannerGalaxyDestinationPath = "C:\\Users\\" + System.Environment.UserName + ImagePlannerGalaxyDestinationSubPath;
            string ImagePlannerClusterDestinationPath = "C:\\Users\\" + System.Environment.UserName + ImagePlannerClusterDestinationSubPath;
            string ImagePlannerNebulaDestinationPath = "C:\\Users\\" + System.Environment.UserName + ImagePlannerNebulaDestinationSubPath;
            switch (dbqType)
            {
                case DBQFileManagement.SearchType.Galaxy:
                    return (ImagePlannerGalaxyDestinationPath);
                case DBQFileManagement.SearchType.Cluster:
                    return (ImagePlannerClusterDestinationPath);
                case DBQFileManagement.SearchType.Nebula:
                    return (ImagePlannerNebulaDestinationPath);
                default:
                    return (ImagePlannerNebulaDestinationPath);
            }
        }

    }
}
