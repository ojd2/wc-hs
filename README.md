# Web Cralwer in .NET
The following program is a simple web crawler that leverages the [Task Parallel Library (TPL)](https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/task-based-asynchronous-programming#tasks-threads-and-culture). By using the Task() operation, the program executes asynchronously, fetching all of the hrefs of a given HTML page in batches, moving onto the next associated seed url concurrently. Reasons for using the TPL framework are in a bid to sustain thread processes for long periods of time, for any website may have thousands of given href tags. Therefore, when traversing through hundreds of href tags, the Task() operation provides a convenient way to run any number of arbitrary statements throughout a given iteration.

## Depth Level of Search
Depending on the depth of search required, the code associated on line 110:

    var urls = doc.DocumentNode.SelectNodes("//a\[@href\]")  
    .Select(a => a.Attributes\["href"\].Value)  
    .Where(href =>  
    !href.StartsWith("mailto:") // Skip Emails  
    && !href.Contains("imgix.net") // Skip imgix.net tags  
    && !href.Contains("#") // Skip # tags  
    && !href.Contains("blog") // Skip blog (offsite)  
    && !href.Contains("Search?SearchTerm") // Search terms  
    && !href.Contains("Venue-Finder") // Venue Finder  
    && !href.Contains("Spaces") // Venue Finder  
    && !href.Contains("Top") // Top 
is used as a gatekeeper, skipping particular references found under the domain: `https://hirespace.com`. For example, if you want to include the term `Venue-Finder`then you can remove the term from the where clause. Subsequently, this will increase the time of execution as it increases and accumulates the number of seed urls to crawl.

