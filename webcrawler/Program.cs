﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace webcrawler
{
    class MainClass
    {
        // Queue to store the links that will get scanned
        public static Queue<string> LinkQueue;
        // List to store crawled links
        public static List<string> VisitedList;
        // Url entered by the user
        public static Uri Url;
        // Init Thread:
        public static Thread t;

        public static void Main(string[] args)
        {
            string seed = "https://hirespace.com/";
            Crawler c = new Crawler();
            c.StartThreading(seed);
        }
    }
}
