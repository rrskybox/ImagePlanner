//Module for installing the observing list database search file (embedded in the app as ImagePlanner.dbq) also installed as same name.

using System;
using System.IO;
using System.Reflection;

namespace ImagePlanner
{
    public partial class DBQFileManagement
    {

        public enum SearchType
        {
            Galaxy,
            Cluster,
            Nebula,
            ConfirmedExoPlanet,
            CandidateExoPlanet
        }

        private static string ImagePlannerGalaxyDestinationSubPath = "Software Bisque\\TheSky Professional Edition 64\\Database Queries\\ImagePlannerGalaxy.dbq";
        private static string ImagePlannerClusterDestinationSubPath = "Software Bisque\\TheSky Professional Edition 64\\Database Queries\\ImagePlannerCluster.dbq";
        private static string ImagePlannerNebulaDestinationSubPath = "Software Bisque\\TheSky Professional Edition 64\\Database Queries\\ImagePlannerNebula.dbq";
        private static string ImagePlannerConfirmedExoPlanetDestinationSubPath = "Software Bisque\\TheSky Professional Edition 64\\Database Queries\\ImagePlannerConfirmedExoPlanet.dbq";
        private static string ImagePlannerCandidateExoPlanetDestinationSubPath = "Software Bisque\\TheSky Professional Edition 64\\Database Queries\\ImagePlannerCandidateExoPlanet.dbq";

        public static bool DBQsInstalled()
        {
            //Checks to see if search database file is already installed or not
            string userDocumentsDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string ImagePlannerGalaxyDestinationPath = userDocumentsDirectory + "\\" + ImagePlannerGalaxyDestinationSubPath;
            string ImagePlannerClusterDestinationPath = userDocumentsDirectory + "\\" + ImagePlannerClusterDestinationSubPath;
            string ImagePlannerNebulaDestinationPath = userDocumentsDirectory + "\\" + ImagePlannerNebulaDestinationSubPath;
            string ImagePlannerConfirmedExoPlanetDestinationPath = userDocumentsDirectory + "\\" + ImagePlannerConfirmedExoPlanetDestinationSubPath;
            string ImagePlannerCandidateExoPlanetDestinationPath = userDocumentsDirectory + "\\" + ImagePlannerCandidateExoPlanetDestinationSubPath;
            return (File.Exists(ImagePlannerGalaxyDestinationPath) &&
                File.Exists(ImagePlannerClusterDestinationPath) &&
                File.Exists(ImagePlannerNebulaDestinationPath) &&
                File.Exists(ImagePlannerConfirmedExoPlanetDestinationPath) &&
                File.Exists(ImagePlannerCandidateExoPlanetDestinationPath));
        }

        public static void InstallDBQs()
        {
            //Make sure the dbq file paths are set up correctly
            string userDocumentsDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string ImagePlannerGalaxyDestinationPath = userDocumentsDirectory + "\\" + ImagePlannerGalaxyDestinationSubPath;
            string ImagePlannerClusterDestinationPath = userDocumentsDirectory + "\\" + ImagePlannerClusterDestinationSubPath;
            string ImagePlannerNebulaDestinationPath = userDocumentsDirectory + "\\" + ImagePlannerNebulaDestinationSubPath;
            string ImagePlannerConfirmedExoPlanetDestinationPath = userDocumentsDirectory + "\\" + ImagePlannerConfirmedExoPlanetDestinationSubPath;
            string ImagePlannerCandidateExoPlanetDestinationPath = userDocumentsDirectory + "\\" + ImagePlannerCandidateExoPlanetDestinationSubPath;

            //Install the dbq file
            ////Collect the file contents to be written
            DBQReadIn(ImagePlannerGalaxyDestinationPath, "ImagePlanner.ImagePlannerGalaxy.dbq");
            DBQReadIn(ImagePlannerClusterDestinationPath, "ImagePlanner.ImagePlannerCluster.dbq");
            DBQReadIn(ImagePlannerNebulaDestinationPath, "ImagePlanner.ImagePlannerNebula.dbq");
            DBQReadIn(ImagePlannerConfirmedExoPlanetDestinationPath, "ImagePlanner.ImagePlannerConfirmedExoPlanet.dbq");
            DBQReadIn(ImagePlannerCandidateExoPlanetDestinationPath, "ImagePlanner.ImagePlannerCandidateExoPlanet.dbq");
            return;
        }

        public static void DBQReadIn(string fpath, string fname)
        {
            ////Collect the file contents to be written
            Assembly dgassembly = Assembly.GetExecutingAssembly();
            Stream dgstream = dgassembly.GetManifestResourceStream(fname);
            Byte[] dgbytes = new Byte[dgstream.Length];
            FileStream dbqgfile = File.Create(fpath);
            int dgreadout = dgstream.Read(dgbytes, 0, (int)dgstream.Length);
            dbqgfile.Close();
            //write to destination file
            File.WriteAllBytes(fpath, dgbytes);
            dgstream.Close();
            return;
        }

        public static string GetDBQPath(SearchType dbqType)
        {
            //Returns a path to the observing list query for the dbqType

            string userDocumentsDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string ImagePlannerGalaxyDestinationPath = userDocumentsDirectory + "\\" + ImagePlannerGalaxyDestinationSubPath;
            string ImagePlannerClusterDestinationPath = userDocumentsDirectory + "\\" + ImagePlannerClusterDestinationSubPath;
            string ImagePlannerNebulaDestinationPath = userDocumentsDirectory + "\\" + ImagePlannerNebulaDestinationSubPath;
            string ImagePlannerConfirmedExoPlanetDestinationPath = userDocumentsDirectory + "\\" + ImagePlannerConfirmedExoPlanetDestinationSubPath;
            string ImagePlannerCandidateExoPlanetDestinationPath = userDocumentsDirectory + "\\" + ImagePlannerCandidateExoPlanetDestinationSubPath;
            switch (dbqType)
            {
                case DBQFileManagement.SearchType.Galaxy:
                    return (ImagePlannerGalaxyDestinationPath);
                case DBQFileManagement.SearchType.Cluster:
                    return (ImagePlannerClusterDestinationPath);
                case DBQFileManagement.SearchType.Nebula:
                    return (ImagePlannerNebulaDestinationPath);
                case DBQFileManagement.SearchType.ConfirmedExoPlanet:
                    return (ImagePlannerConfirmedExoPlanetDestinationPath);
                case DBQFileManagement.SearchType.CandidateExoPlanet:
                    return (ImagePlannerCandidateExoPlanetDestinationPath);
                default:
                    return (ImagePlannerNebulaDestinationPath);
            }
        }

    }
}
