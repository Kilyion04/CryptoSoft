using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace ConsoleApplication1
{
    class Program
    {



        public static string EncryptDecrypt(string textToEncrypt, int encryptionKey)
        {
            StringBuilder inSb = new StringBuilder(textToEncrypt);
            StringBuilder outSb = new StringBuilder(textToEncrypt.Length);
            char c;
            for (int i = 0; i < textToEncrypt.Length; i++)
            {
                c = inSb[i];
                c = (char)(c ^ encryptionKey);
                outSb.Append(c);
            }
            return outSb.ToString();
        }



        static void Main(string[] args)
        {
            ArrayList openArray = new ArrayList();
            ArrayList closeArray = new ArrayList();

            string chemin = @"" + args[0];
            
            int enKey = Convert.ToInt32(1234);

            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(chemin);

            //detect whether its a directory or file
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                foreach ( var file in Directory.GetFiles(chemin))
                {
                    using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            while (!sr.EndOfStream)
                            {
                                openArray.Add(sr.ReadLine());
                            }
                            sr.Close();
                        }
                        fs.Close();

                    }

                    foreach (string line2 in openArray)
                    {
                        closeArray.Add(EncryptDecrypt(line2, enKey));
                    }

                    TextWriter tw = new StreamWriter(file);

                    foreach (string encoded in closeArray)
                    {
                        tw.WriteLine(encoded);
                    }

                    tw.Close();
                }
            }
            else
            {
                using (FileStream fs = new FileStream(chemin, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        while (!sr.EndOfStream)
                        {
                            openArray.Add(sr.ReadLine());
                        }
                        sr.Close();
                    }
                    fs.Close();

                }

                foreach (string line2 in openArray)
                {
                    closeArray.Add(EncryptDecrypt(line2, enKey));
                }

                TextWriter tw = new StreamWriter(chemin);

                foreach (string encoded in closeArray)
                {
                    tw.WriteLine(encoded);
                }

                tw.Close();

            }
        }
    }
}

