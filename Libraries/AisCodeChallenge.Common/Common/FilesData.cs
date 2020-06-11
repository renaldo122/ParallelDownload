using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web;

namespace AisCodeChallenge.Common
{
    public static class FilesData
    {
        public static string DownloadFiles = ConfigurationManager.AppSettings["DownloadFiles"];
        public static string LastFiles = ConfigurationManager.AppSettings["LastFiles"];
        public static string LastFilesUrls = ConfigurationManager.AppSettings["LastFilesUrls"];
        public static string TextFile = ConfigurationManager.AppSettings["TextFile"];
        

        public static string GetFileName(string path){
            return Path.GetFileName(path);
        }


        public static string GetFileExtension(string path)  {
            return Path.GetExtension(path);
        }

        public static string GetTextFromUrl(string url)
        {
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(url);
                StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
            catch(Exception ex){
                //Log Exception
                return "";
            }
        }

        public static string MapPathFolder(string Virtualpath)
        {
            return HttpContext.Current.Server.MapPath(Virtualpath);
        }

        public static string GetCombinePath(string path, string fileName)
        {
            return Path.Combine(path, fileName);
        }
        public static void CreateDirectoryIfNotExists(string Virtualpath)
        {
            try
            {
                if (!Directory.Exists(MapPathFolder(Virtualpath)))
                {
                    Directory.CreateDirectory(MapPathFolder(Virtualpath));
                }
            }
            catch (Exception ex)
            {
                //Log Exception
            }
        }

        public static string GetDirectoryPath(string Virutalpath, string path)
        {
            try
            {
                var fullpath = Virutalpath;
                fullpath = Virutalpath + "\\" + path;
                var directory = HttpContext.Current.Server.MapPath(fullpath);
                return directory.Replace("\\", "\\");
            }
            catch (Exception ex)
            { 
                //Log Exception
                return "";
            }
        }

        public static bool SetFileTextWithData(string fileName, List<Uri> linesUrl)
        {
            FileInfo fi = new FileInfo(fileName);
            try
            {
                if (fi.Exists)  {
                    fi.Delete();
                }
                using (StreamWriter sw = fi.CreateText())
                {
                    foreach(var line in linesUrl) {
                        sw.WriteLine(line.AbsoluteUri);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                //Log Exception
                return false;
            }
        }

        public static List<string> GetFileTextWithData(string fileName)
        {
            FileInfo fi = new FileInfo(fileName);
            List<string> linesUrl = new List<string>();
            try
            {
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null) {
                        linesUrl.Add(s);
                    }
                }
                return linesUrl;
            }
            catch (Exception Ex)  {
                //Log Exception
                return linesUrl;
            }
        }

        public static bool DeleteDirectoryOrContent(string directoryName, bool deleteDirectory = false)
        {
            try
            {
                if (!Directory.Exists(directoryName))
                    return false;
                if (deleteDirectory)  {
                    Directory.Delete(directoryName, true);
                }
                else
                {
                    DirectoryInfo directory = new DirectoryInfo(directoryName);
                    foreach (FileInfo file in directory.GetFiles())
                    {
                        file.Delete();
                    }
                }
                return true;
            }
            catch (Exception ex)  {
                //Log Exception
                return false;
            }
        }
    }
}
