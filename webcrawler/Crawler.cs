using System;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Xml;
using HtmlAgilityPack;

namespace webcrawler
{
    public class Crawler
    {
        public void StartThreading(string seed)
        {
            // Init Uri Url seed string:
            if (Uri.IsWellFormedUriString(seed, UriKind.Absolute))
            {
                MainClass.Url = new Uri(seed);
            }
            // Init Link Queue:
            MainClass.LinkQueue = new Queue<string>();
            // Enqueue seed Url:
            MainClass.LinkQueue.Enqueue(seed);
            // Init Visited List:
            MainClass.VisitedList = new List<string>();
            // Init System Thread:
            MainClass.t = new Thread(start);
            // Start Thread:
            MainClass.t.Start();
            // Read Out:
            Console.Out.Write("Threading Seed URL: " + seed + "\n");
        }

        // Begin Thread:
        public void start()
        {
            while (MainClass.LinkQueue.Count > 0)
            {
                // Parse HTML for each link in Queue:
                ParseHTML(MainClass.LinkQueue.Dequeue().ToString());
                // Load last visited pages to print details:
                string visitedPage = MainClass.VisitedList[MainClass.VisitedList.Count - 1].ToString();
                // Call helper to print to console links:
                PrintQueue(MainClass.LinkQueue, visitedPage);
            }
        }

        // HTML Parser Helper:
        public void ParseHTML(string url)
        {
            // Init isEmpty boolean:
            Boolean isEmpty;

            // Try Parse HTML for url:
            try
            {
                // Set isEmpty:
                isEmpty = false;
                // Check if given url is not null:
                if (url == null || isEmpty)
                {
                    Console.WriteLine("Sorry" + url + "is not found! \n");
                }
                else
                {
                    // Create instance of HtmlWeb:
                    HtmlAgilityPack.HtmlWeb htmlWeb = new HtmlWeb();
                    // Parse Html for url:
                    HtmlAgilityPack.HtmlDocument doc = htmlWeb.Load(url);
                    // Add crawled urls to visited list:
                    MainClass.VisitedList.Add(url);
                    // Fetch all hrefs in parsed HTML:
                    if (doc != null)
                    {
                        FetchHref(doc);
                    }
                    else
                    {
                        Console.WriteLine("Found invalid Html! \n");
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                // If url NOT found:
                isEmpty = true;
                throw ex;
            }
        }

        // Helper function to print queued found links:
        public void PrintQueue(Queue<string> QueueList, string visitedPage)
        {
            // Print how many urls scanned, visited, and the last url scanned for hrefs:
            Console.Out.WriteLine("================================================================ \n");
            Console.Out.WriteLine(" Scanned: " + MainClass.LinkQueue.Count.ToString()
                                  + "\t Visited: " + MainClass.VisitedList.Count.ToString()
                                  + "\t" + "Now Parsing: " + visitedPage + "\n");
            Console.Out.WriteLine("================================================================ \n");
            foreach (string link in QueueList)
            {
                Console.WriteLine("Found url => " + link + "\n");
            }
        }

        public void FetchHref(HtmlDocument doc)
        {
            // Init urls list & do NOT include externals or related.
            // Depending on the level of DEPTH you want to search,
            // you can remove the filters below:
            var urls = doc.DocumentNode.SelectNodes("//a[@href]")
                          .Select(a => a.Attributes["href"].Value)
                          .Where(href =>
                                 !href.StartsWith("mailto:") // Skip Emails
                                 && !href.Contains("imgix.net") // Skip imgix.net tags
                                 && !href.Contains("#") // Skip # tags 
                                 && !href.Contains("blog") // Skip blog (offsite)
                                 && !href.Contains("Search?SearchTerm") // Search terms
                                 && !href.Contains("Venue-Finder") // Venue Finder
                                 && !href.Contains("Spaces") // Venue Finder
                                 && !href.Contains("Top") // Top
        )
        .ToList();

            // Make sure list is not empty:
            if (urls == null ? true : (!urls.Any()))
            {
                Console.Write("Invalid url found! \n");
            }
            else
            {
                // For each HTML <a> tag found in the document:
                foreach (string link in urls)
                {
                    // Extract the href value from the <a> tag:
                    Uri l = new Uri(MainClass.Url, link.ToString());

                    // Check if href does not exist in Visited List OR in Link Queue:
                    if (!MainClass.LinkQueue.Contains(l.ToString()) && !MainClass.VisitedList.Contains(l.ToString()))
                    {
                        // Make sure urls are NOT external:
                        if (l.Host.ToString() == MainClass.Url.Host.ToString())
                        {
                            // Add the href to Link Queue to get scanned:
                            MainClass.LinkQueue.Enqueue(l.ToString());
                        }

                    }
                }
            }

        }
    }
}





