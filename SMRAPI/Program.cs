using System;
using System.IO;

namespace SMRAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            //Below are examples of API calls. Uncomment those you want to test





            //Desktop folder is used to download the demo map
            var Desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            //Use a hardcoded key. Use this method only for yourself or testing purposes
            //API.ApiKey = Guid.Parse("YOUR-API-KEY-GOES-HERE");

            //Ask the user for the key instead of hardcoding it
            GetApiKey();

            //Test and informative API calls
            //var Test = API.Test();
            var Info = API.Info();

            //Download preview and file of first map (if available)
            //This example always uses the hidden_id so we don't have to check for public==1 first.
            /*
            if (Info.data.maps != null && Info.data.maps.Length > 0)
            {
                var Map = Info.data.maps[0];
                Console.WriteLine("Saving preview of {0}...", Map.name);
                File.WriteAllBytes(Path.Combine(Desktop, $"map_{Map.hidden_id}.png"), API.Preview(Map.hidden_id));
                Console.WriteLine("Saving map...");
                using (var FS = File.Create(Path.Combine(Desktop, $"map_{Map.hidden_id}.sav")))
                {
                    API.Download(Map.hidden_id, FS);
                }
            }
            //*/

            //Console.WriteLine(Info.data.user);

            //Edit the name of a map
            //var EditResult = API.EditMap(Guid.Parse("YOUR-MAP-ID-GOES-HERE"),"ExPeRiMeNtAl.sAv");
            //Show new name. Some special characters will be replaced and ".sav" will be lowercase.
            //Console.WriteLine("Result: {0}", EditResult.success ? EditResult.data.name : EditResult.msg);

            //Change the hidden Id of a map
            //var IdResult = API.NewId(Guid.Parse("YOUR-MAP-ID-GOES-HERE"));
            //Console.WriteLine("Result: {0}", IdResult.success ? IdResult.data.ToString() : IdResult.msg);

            //List all pages of maps from all categories
            //Don't do this in a productive environment as this will get slower as more maps are added.
            //This code here merely serves as a demonstration of the paging mechanism
            //We might also add a limitation on this if we see a big impact on the server.
            /*
            foreach (var Cat in Info.data.categories.list)
            {
                if (Cat.id > 0)
                {
                    Responses.ListResponse ListResult;
                    int Page = 0;
                    do
                    {
                        Page++;
                        ListResult = API.List(Category: Cat.id, Page: Page);
                        Console.WriteLine("Category={0} Page={1}; Count={2}",
                            Cat.id, Page,
                            ListResult.success ? ListResult.data.maps.Length.ToString() : ListResult.msg);
                    } while (ListResult.data.more);
                }
            }
            //*/

            //Upload a map
            //var UploadResult = API.AddMap(@"C:\Users\AyrA\AppData\Local\FactoryGame\YAY.sav");
            //Console.WriteLine("Result: {0}", UploadResult.success ? UploadResult.data.name : UploadResult.msg);

#if DEBUG
            //Wait for a key in debug mode to allow us to read the console
            Console.Error.WriteLine("#END");
            Console.ReadKey(true);
#endif
        }

        /// <summary>
        /// Dynamically gets the API key from a user
        /// </summary>
        private static void GetApiKey()
        {
            //Select a random port
            var R = new Random();
            var Port = R.Next(3000, 50000);

            using (var L = new HTTP(Port))
            {
                L.ApiKeyEvent += delegate (object sender, Guid key)
                {
                    //Set key, print some information and stop the listener
                    API.ApiKey = key;
                    Console.WriteLine("Got an API key: {0}", key);
                    Console.WriteLine("Access: {0}", key == Guid.Empty ? "Deny" : "Grant");
                    Console.WriteLine("API test success: {0}", API.Test().success ? "Yes" : "No");
                    Console.Error.WriteLine("HTTP server stopped. Demo ended. Press any key to exit");
                    L.Stop();
                };
                L.Start();
                L.OpenBrowser();
                Console.Error.WriteLine("Follow the instructions in your browser window.");
                Console.Error.WriteLine("Press [CTRL]+[C] to abort.");
                L.WaitForExit();
            }
        }
    }
}
