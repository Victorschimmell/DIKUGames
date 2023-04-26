using System.Collections.Generic;
using System.IO;
using System;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Blocks;

namespace Breakout.LevelLoading {
    public class ASCIIReader {
        private string currentFile;
        private string[] fileLines;
        private Dictionary<string, string> legend;
        private List<string> map;
        private Dictionary<string, string> meta;
        public ASCIIReader(string fileName) {
            if (File.Exists(fileName)) {
                fileLines = File.ReadAllLines(fileName);
                currentFile = fileName;
            }
        }

        private List<string> readMap() {
            int currentIndex = 0;
            int indexMinRead = 0;
            int indexMaxRead = 0;
            foreach (string line in fileLines) {
                if (line  == "Map:") {
                    indexMinRead = currentIndex;
                } else if (line == "Map/") {
                    indexMaxRead = currentIndex;
                    break;
                } 
                currentIndex++;
            }

            map = new List<string>();
            if (indexMaxRead > indexMinRead) {
                for (int i = indexMinRead + 1; i < indexMaxRead; i++) {
                    map.Add(fileLines[i]);
                }
            }

            return map;
        }

        private Dictionary<string, string> readMeta() {
            int currentIndex = 0;
            int indexMinRead = 0;
            int indexMaxRead = 0;
            foreach (string line in fileLines) {
                if (line  == "Meta:") {
                    indexMinRead = currentIndex;
                } else if (line == "Meta/") {
                    indexMaxRead = currentIndex;
                } 
                currentIndex++;
            }

            meta = new Dictionary<string, string>();
            if (indexMaxRead > indexMinRead) {
                for (int i = indexMinRead + 1; i < indexMaxRead; i++) {
                    string[] metaLines = fileLines[i].Split(": ");
                    if (metaLines.Length == 2){
                        meta.Add(metaLines[0], metaLines[1]);
                    }
                }
            }

            return meta;
        }

        private Dictionary<string, string> readLegend() {
            int currentIndex = 0;
            int indexMinRead = 0;
            int indexMaxRead = 0;
            foreach (string line in fileLines) {
                if (line  == "Legend:") {
                    indexMinRead = currentIndex;
                } else if (line == "Legend/") {
                    indexMaxRead = currentIndex;
                } 
                currentIndex++;
            }

            legend = new Dictionary<string, string>();
            if (indexMaxRead > indexMinRead) {
                for (int i = indexMinRead + 1; i < indexMaxRead; i++) {
                    string[] legendLines = fileLines[i].Split(") ");
                    if (legendLines.Length == 2){
                        legend.Add(legendLines[0], legendLines[1]);
                    }
                }
            }

            return legend;
        }

        public List<string> GetMap() {
            return readMap();
        }

        public Dictionary<string, string> GetMeta() {
            return readMeta();
        }

        public Dictionary<string, string> GetLegend() {
            return readLegend();
        }
    }
}