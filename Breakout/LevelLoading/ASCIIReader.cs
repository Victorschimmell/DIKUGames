using System.Collections.Generic;
using System.IO;
using System;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Blocks;

namespace Breakout.LevelLoading {
    public class ASCIIReader {
        private string[] fileLines;
        private readonly Dictionary<string, string> legend;
        private readonly List<string> map;
        private readonly List<string> meta;
        public ASCIIReader(string fileName) {
            if (File.Exists(fileName)) {
                // Loading all lines from the file
                fileLines = File.ReadAllLines(fileName);
                
                // Finding the upper- and lowerbound of the map in the file
                int currentIndex = 0;
                int indexMinLoad = 0;
                int indexMaxLoad = 0;
                foreach (string line in fileLines) {
                    if (line  == "Map:") {
                        indexMinLoad = currentIndex;
                    } else if (line == "Map/") {
                        indexMaxLoad = currentIndex;
                        break;
                    } 
                    currentIndex++;
                }

                map = new List<string>();
                if (indexMaxLoad > indexMinLoad) {
                    for (int i = indexMinLoad + 1; i < indexMaxLoad; i++) {
                        map.Add(fileLines[i]);
                    }
                }


                // META
                currentIndex = 0;
                indexMinLoad = 0;
                indexMaxLoad = 0;
                foreach (string line in fileLines) {
                    if (line  == "Meta:") {
                        indexMinLoad = currentIndex;
                    } else if (line == "Meta/") {
                        indexMaxLoad = currentIndex;
                    } 
                    currentIndex++;
                }

                meta = new List<string>();
                if (indexMaxLoad > indexMinLoad) {
                    for (int i = indexMinLoad + 1; i < indexMaxLoad; i++) {
                        meta.Add(fileLines[i]);
                    }
                }

                // Legend
                currentIndex = 0;
                indexMinLoad = 0;
                indexMaxLoad = 0;
                foreach (string line in fileLines) {
                    if (line  == "Legend:") {
                        indexMinLoad = currentIndex;
                    } else if (line == "Legend/") {
                        indexMaxLoad = currentIndex;
                    } 
                    currentIndex++;
                }

                legend = new Dictionary<string, string>();
                if (indexMaxLoad > indexMinLoad) {
                    for (int i = indexMinLoad + 1; i < indexMaxLoad; i++) {
                        string[] legendLines = fileLines[i].Split(") ");
                        if (legendLines.Length == 2){
                            legend.Add(legendLines[0], legendLines[1]);
                        }
                    }
                }
            }
        }

        public List<string> GetMeta() {
            return meta;
        }

        public List<string> GetMap() {
            return map;
        }

        public Dictionary<string, string> GetLegend() {
            return legend;
        }
    }
}