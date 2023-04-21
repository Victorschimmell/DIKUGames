using System.Collections.Generic;
using System.IO;
using System;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Blocks;

namespace Breakout.LevelLoading {
    public class ASCIIReader {
        private readonly Dictionary<string, string> legend;
        private readonly List<string> map;
        private readonly List<string> meta;
        public ASCIIReader(string fileName) {
            string[] lines = File.ReadAllLines(fileName);
            
            // MAP
            // Finding the upper- and lowerbound of the map in the file
            int currentIndex = 0;
            int indexMinLoad = 0;
            int indexMaxLoad = 0;
            foreach (string line in lines) {
                if (line  == "Map:") {
                    indexMinLoad = currentIndex;
                } else if (line.Contains("Map")) {
                    indexMaxLoad = currentIndex;
                } 
                currentIndex++;
            }

            if (indexMaxLoad < indexMinLoad) {
                throw new Exception("Invalid ASCII file, Map does not work");
            }

            // Save the map in the List 'map'
            map = new List<string>();
            for (int i = indexMinLoad + 1; i < indexMaxLoad; i++) {
                map.Add(lines[i]);
            }


            // META
            currentIndex = 0;
            indexMinLoad = 0;
            indexMaxLoad = 0;
            foreach (string line in lines) {
                if (line  == "Meta:") {
                    indexMinLoad = currentIndex;
                } else if (line.Contains("Meta")) {
                    indexMaxLoad = currentIndex;
                } 
                currentIndex++;
            }

            if (indexMaxLoad < indexMinLoad) {
                throw new Exception("Invalid ASCII file, Meta does not work");
            }

            meta = new List<string>();
            for (int i = indexMinLoad + 1; i < indexMaxLoad; i++) {
                meta.Add(lines[i]);
            }

            // Legend
            currentIndex = 0;
            indexMinLoad = 0;
            indexMaxLoad = 0;
            foreach (string line in lines) {
                if (line  == "Legend:") {
                    indexMinLoad = currentIndex;
                } else if (line.Contains("Legend")) {
                    indexMaxLoad = currentIndex;
                } 
                currentIndex++;
            }

            if (indexMaxLoad < indexMinLoad) {
                throw new Exception("Invalid ASCII file, Legend does not work");
            }

            legend = new Dictionary<string, string>();
            for (int i = indexMinLoad + 1; i < indexMaxLoad; i++) {
                string[] legendLines = lines[i].Split(") ");
                if (legendLines.Length == 2){
                    legend.Add(legendLines[0], legendLines[1]);
                } else {
                    throw new Exception("Invalid ASCII file, Legend does not work");
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