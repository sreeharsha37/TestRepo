using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APIScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>()
            {
                "Stash-SamFM-2.7",
                "Stash-SamFM-2.5",
                "Stash-SamFM",
                "Stash-SamFMMovilitasService-2.7",
                "Stash-SamFMMovilitasService-2.5",
                "Stash-SamFMMovilitasService",
                "Stash-OrchestrationAPI-2.5",
                "Stash-OrchestrationAPI-2.7",
                "Stash-OrchestrationAPI-master",
                "Stash-SamFM-ReleaseQA",
                "Stash-SamFM-ReleaseQA-2.5",
                "Stash-SamFM-ReleaseQA-2.7",
            };

            foreach (var item in list)
            {
                if(!GetJob(item))
                {
                    Console.WriteLine("Failed  " + item);
                    break;
                }
            }

            //if(args!= null && args.Length > 0 && !string.IsNullOrEmpty(args[0]))
            //{
            //    Console.WriteLine(GetJob(args[0]));
            //}
            Console.WriteLine("Rest All good");
            Console.Read();
        }

        public static bool GetJob(string job = "Stash-SamFM-2.7")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://jenkins.samfm.net/job/" + job + "/lastBuild/api/json");
            request.Method = "GET";
            try
            {
                WebResponse webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                var response = responseReader.ReadToEnd();
                dynamic res = JsonConvert.DeserializeObject(response);
                string ress = res.result.Value;
                if (ress == "SUCCESS")
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(e.Message);
            }

            return false;
        }
    }
}
