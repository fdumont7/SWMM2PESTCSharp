﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMM2PEST
{
    class ReadInputFile
    {
        string fileLocation;
        List<Subcatchments> subcatchments;
        ArrayList LIDs;
        public ReadInputFile(string aFileLocation)
        {
            fileLocation = aFileLocation;
            subcatchments = new List<Subcatchments>();
            LIDs = new ArrayList();
        }

        private void readFile()
        {

            StreamReader sr = new StreamReader(fileLocation);


            string line = sr.ReadLine();
            string[] splitLine;
            int num = 0;
            while ((line = sr.ReadLine()) != null)
            {
                if (line == "[SUBCATCHMENTS]")
                {
                    line = sr.ReadLine();
                    line = sr.ReadLine();
                    line = sr.ReadLine(); //skip down two lines
                    
                    while (line != " " && line != "")
                    {
                        splitLine = splitString(line, ' ');
                        subcatchments.Add(new Subcatchments());
                        subcatchments[num].setName(splitLine[0]);
                        subcatchments[num].setArea(Convert.ToDouble(splitLine[3]));
                        subcatchments[num].setPercentImperv(Convert.ToDouble(splitLine[4]));
                        subcatchments[num].setWidth(Convert.ToDouble(splitLine[5]));
                        subcatchments[num].setPercentSlope(Convert.ToDouble(splitLine[6]));
                        line = sr.ReadLine();
                        num++;
                    }
                }
                if (line == "[SUBAREAS]")
                {
                    line = sr.ReadLine();
                    line = sr.ReadLine();
                    line = sr.ReadLine(); //skip down two lines
                    num = 0;  

                    while (line != " " && line != "")
                    {
                        splitLine = splitString(line, ' ');
                        
                        subcatchments[num].setName(splitLine[0]);
                        subcatchments[num].setArea(Convert.ToDouble(splitLine[3]));
                        subcatchments[num].setPercentImperv(Convert.ToDouble(splitLine[4]));
                        subcatchments[num].setWidth(Convert.ToDouble(splitLine[5]));
                        subcatchments[num].setPercentSlope(Convert.ToDouble(splitLine[6]));
                        line = sr.ReadLine();
                        num++;
                    }
                }
            }

        }

        public string[] splitString(string s, char delim)
        {
            char currentChar = s[0];

            int num = 0;
            while (num < s.Length)
            {
                while (num + 1 < s.Length && s[num] == delim && s[num + 1] == delim)
                {
                    s = s.Remove(num, 1);
                }
                num++;
            }
            return s.Split(delim);
        }


    }
}
