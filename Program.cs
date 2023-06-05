using Newtonsoft.Json;
using RESTfulAPIConsume.Constants;
using RESTfulAPIConsume.RequestHandlers;
using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Text;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml;
using System.Data;
using Aspose.Cells;
using Aspose.Cells.Utility;
using ChoETL;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;


namespace RESTfulAPIConsume
{
    class Program
    {
        static void Main(string[] args)
        {

            // show date in English
            CultureInfo en = new CultureInfo("en-US");
            // create current month and year variables
            DateTime now = DateTime.Now;
            string current_month_id = now.ToString("yyyyMM");
            //string month_id = "202201";
            string month_id = current_month_id;
            string APIUrl_json_month = RequestConstants.APIUrl_json + month_id;
            Console.WriteLine("month_id " + month_id);
            //Console.WriteLine("APIUrl_json_month " + APIUrl_json_month);

            IRequestHandler httpWebRequestHandler = new HttpWebRequestHandler();
            
            // get request:
            // define the download URL and download the JSON file to local storage
            WebRequest req = WebRequest.Create(APIUrl_json_month);
            req.Method = "GET";
            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            // display response on console
            //documentation: https://docs.microsoft.com/en-us/dotnet/api/system.net.webrequest?view=net-6.0
            // Get the stream containing content returned by the server.
            Stream dataStream = resp.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            //Console.WriteLine(responseFromServer);
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            resp.Close();
            
            // write JSON reponse to local file
            File.WriteAllText(RequestConstants.path_json, responseFromServer);
            
            


            // select summary object from JSON response
            string json_response_string = responseFromServer;
            //Console.WriteLine(json_response_string.GetType());
            var response = json_response_string;
            dynamic data = JObject.Parse(response);
            Regex regEx = new Regex("$.summaryLicenses", RegexOptions.Multiline);
            string regEx_string = regEx.ToString();
            JObject json_summaryLicenses = data.SelectToken(regEx_string);
            //JProperty last = json_summaryLicenses.Properties;
            //Console.WriteLine("json_summaryLicenses is" + last);
            //Console.WriteLine(json_summaryLicenses);
            //Console.WriteLine(json_summaryLicenses.GetType());
            
            // edit countries for object Continum in JSON file  
            // https://stackoverflow.com/questions/33045235/replace-part-of-a-json-with-other-using-a-string-token
            //var countries_path = "summaryLicenses.c-15323ms_splaeur67202206.countries";
            var countries_path = "summaryLicenses.c-15323ms_splaeur67" + month_id + ".countries";
            var json_summaryLicenses_NL_countries = data;
            json_summaryLicenses_NL_countries.SelectToken(countries_path).Replace("NL");
            json_summaryLicenses_NL_countries.SelectToken(regEx_string);
            //Console.WriteLine(o1);

            string string_summaryLicenses_nl = json_summaryLicenses_NL_countries.ToString();
            //Console.WriteLine(string_summaryLicenses_nl);
            //string path_json1 = @"D:\SSIS Projects\RESTfulAPIConsume\octopus_api\downloads\report1.json";
            File.WriteAllText(RequestConstants.path_json_nl, string_summaryLicenses_nl);
            
            
            //JObject test = o1.SelectToken(regEx_string);

            // delete countries from all objects in JSON file           
            //string output = Regex.Replace( input, @"\sstyle=""(.*?)""", @" style=&$1&" );
            //JObject json_summaryLicenses_no_countries= JObject.Parse(json_response_string);
            //Console.WriteLine(json_summaryLicenses_no_countries);
            //var objs = JsonConvert.DeserializeObject(json_response_string);
            //JArray json_summaryLicenses_no_countries_array= (JArray)objs;
            
            
            /*
            // this returns the replaced object
            Regex countries_regex = new Regex("$.summaryLicenses.c-15323ms_splaeur67202206", RegexOptions.Multiline);
            string countries_regex_string = countries_regex.ToString();
            //JObject json_summaryLicenses_no_countries = json_summaryLicenses;
            JObject json_summaryLicenses_no_countries = data.SelectToken(countries_regex_string);
            JArray json_summaryLicenses_no_countries_array = new JArray();
            json_summaryLicenses_no_countries_array.Add(json_summaryLicenses_no_countries);
            foreach(var e in json_summaryLicenses_no_countries_array)
            {
                if(e["countries"] != null)
                    e["countries"]="nl";
            }
            
            Console.WriteLine(json_summaryLicenses_no_countries_array);
            */

            /*
            //dynamic data1 = JObject.Parse(response);
            //regex countries_regex = new Regex(@"\/s*\"!?countries.*\" *: *(\"(.*?)\"|(,|s|))/", RegexOptions.Multiline);
            Regex countries_regex = new Regex("$.summaryLicenses.c-11069msb_qmtheurOFFICE-365-PROPLUS202206", RegexOptions.Multiline);
            string countries_regex_string = countries_regex.ToString();
            //dynamic data_no_countries = JObject.Parse(countries_regex_string);
            JObject json_summaryLicenses_no_countries = data.SelectToken(countries_regex_string);
            Console.WriteLine(json_summaryLicenses_no_countries);
            */


            //json_summaryLicenses[0]["id"] = "KARAM";
            //Console.WriteLine(json_summaryLicenses);

            // convert summary to string
            string string_summaryLicenses = json_summaryLicenses.ToString();
            //Console.WriteLine(string_summaryLicenses);
            //File.WriteAllText(RequestConstants.path_csv, string_summaryLicenses);
            //File.WriteAllText(RequestConstants.path_json, string_summaryLicenses);

            
            

            /* json response is not standard, we need to delete all objects names and keep the json objects
            The following expression can do this based on the patern in all objects names (!?c-.*ms.) 
            regex expression = '/\s*\"!?c-.*ms.*\" *: *(\"(.*?)\"|(,|\s|))/gm';
            
            */
            //Regex regEx1 = new Regex(@"\/s*\"!?c-.*ms.*\" *: *(\"(.*?)\"|(,|s|))/", RegexOptions.Multiline);
            //Regex regEx1 = new Regex("$..c-11069msb_qmtheurOFFICE-365-PROPLUS202203[?(@.id =~ /15697900*/)]", RegexOptions.IgnoreCase);
            //Regex regEx1 = new Regex("$..c-11069msb_qmtheurOFFICE-365-PROPLUS20220*", RegexOptions.IgnoreCase);
            //Regex regEx1 = new Regex("$..c.*", RegexOptions.Multiline);
            //Regex regEx1 = new Regex("$..[?(@.id =~ 15697900)].ws_id*", RegexOptions.IgnoreCase);

            //Regex regEx = new Regex("$.reports[?(@.completionTime =~ /" + month + " 01 */)]", RegexOptions.IgnoreCase);
            //string regEx_string1 = regEx1.ToString();
            //dynamic data1 = JObject.Parse(string_summaryLicenses);
            //JObject json_summaryLicenses1 = data1.SelectToken(regEx_string1);
            //JObject json_summaryLicenses1 = data.GetNestedMember(regEx_string);
            //JObject json_summaryLicenses1 = data.GetNestedPropertyValue(regEx_string);
            //Console.WriteLine(json_summaryLicenses1);
            //string string_summaryLicenses1 = json_summaryLicenses1.ToString();
            

            /* write to CSV file
            // https://www.codeproject.com/Articles/5268371/Cinchoo-ETL-JSON-Reader
            using (var r = new ChoJSONReader(json_summaryLicenses))
            {
                using (var w = new ChoCSVWriter(RequestConstants.path_csv).WithFirstLineHeader())
                {
                    w.Write(r);
                    Console.WriteLine(w);
                }
            }
            */
            
            jsonStringToCSV(string_summaryLicenses);
            //jsonStringToCSV(string_summaryLicenses_nl);
            




            
            
            /*
            // https://blog.aspose.com/2020/04/03/import-data-from-json-to-excel-in-csharp-asp.net/
            // Create a Workbook object
            Workbook workbook = new Workbook();
            Worksheet worksheet = workbook.Worksheets[0];
            // Read JSON File
            string jsonInput = File.ReadAllText(RequestConstants.path_json);   
            // Set JsonLayoutOptions
            JsonLayoutOptions options = new JsonLayoutOptions(); 
            options.ArrayAsTable = true;
            // Import JSON Data
            JsonUtility.ImportData(jsonInput, worksheet.Cells, 0, 0, options);
            // Save Excel file
            workbook.Save(RequestConstants.path_xlsx);
            */

            //Console.WriteLine(resp);
            Console.ReadLine();
            Console.WriteLine("done");
        }
        /*
        public static string GetId(IRequestHandler requestHandler)
        {
            return requestHandler.GetId(RequestConstants.APIUrl);
        }
        */
        


        // https://www.codeproject.com/Questions/1128427/JSON-to-CSV-converter
        public static void jsonStringToCSV(string jsonContent)
       {
           //used NewtonSoft json nuget package
           XmlNode xml = JsonConvert.DeserializeXmlNode("{records:" +jsonContent + "}");
            //Console.WriteLine("xml: "+xml);
           XmlDocument xmldoc = new XmlDocument();
           xmldoc.LoadXml(xml.InnerXml);
            //Console.WriteLine("xml.InnerXml: "+xml.InnerXml);
           XmlReader xmlReader = new XmlNodeReader(xml);
           DataSet dataSet = new DataSet();
            //Console.WriteLine("xmlReader: "+xmlReader);
            //Console.WriteLine("dataSet: "+ dataSet);
           dataSet.ReadXml(xmlReader);
            int dataset_count = dataSet.Tables.Count;
                Console.WriteLine("count: "+dataSet.Tables.Count);
            var lines = new List<string>();
            for(int i=0 ; i<dataset_count; i++)
            {

                //if (dataSet.Tables[i] == 0) { break;}
                //Console.WriteLine("dataSet.Tables[85]: " + dataSet.Tables[84]);
                var dataTable = dataSet.Tables[i];
            
           //var dataTable = dataSet.Tables[1];
           //Console.WriteLine("dataTable: "+dataTable);
                
           //Datatable to CSV
           
          
           string[] columnNames = dataTable.Columns.Cast<DataColumn>().
                                             Select(column => column.ColumnName).
                                             ToArray();
            //Console.WriteLine("columnNames: "+columnNames);

           var header = string.Join(",", columnNames);
            //Console.WriteLine("header: "+header);
           if(i==0){
                    lines.Add(header);
                    }
           
           var valueLines = dataTable.AsEnumerable()
                              .Select(row => string.Join(",", row.ItemArray));
            //Console.WriteLine("valueLines: "+valueLines);
           lines.AddRange(valueLines);

                
                }
            //Console.WriteLine("lines: "+lines);
           File.WriteAllLines(RequestConstants.path_csv, lines);
                
            
            Console.WriteLine("write to csv done");
       }
        
    }
}