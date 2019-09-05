using System;

namespace SMRAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            //Use a hardcoded key. Use this method only for yourself
            //API.ApiKey = Guid.Parse("YOUR-API-KEY-GOES-HERE");

            //Ask the user for the key
            GetApiKey();

            //Some test and informative API calls
            //var Test = API.Test();
            //var Info = API.Info();
            //Console.WriteLine(Info.data.user);

            //Edit the name of a map
            //var EditResult = API.EditMap(Guid.Parse("YOUR-MAP-ID-GOES-HERE"),"ExPeRiMeNtAl.sAv");
            //Console.WriteLine("Result: {0}", EditResult.success ? EditResult.data.name : EditResult.msg);

            //Change the hidden Id of a map
            //var IdResult = API.NewId(Guid.Parse("YOUR-MAP-ID-GOES-HERE"));
            //Console.WriteLine("Result: {0}", IdResult.success ? IdResult.data.ToString() : IdResult.msg);

            /* List the first page of maps from all categories
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
                            Cat.id,
                            Page,
                            ListResult.success ? ListResult.data.maps.Length.ToString() : ListResult.msg);
                    } while (ListResult.data.more);
                }
            }
            //*/

            //Upload a map
            //var UploadResult = API.AddMap(@"C:\Users\AyrA\AppData\Local\FactoryGame\YAY.sav");
            //Console.WriteLine("Result: {0}", UploadResult.success ? UploadResult.data.name : UploadResult.msg);

#if DEBUG
            //Wait for a key in debug mode
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
