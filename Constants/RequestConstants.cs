namespace RESTfulAPIConsume.Constants
{
    
    public static class RequestConstants {


        public const string APIUrl = "https://splareporter2.octopus.cloud/api/";
        public const string workspace = "workspace/{{workspace_id}}/report/licensing/spla-usage-customer";
        public const string format_json = "format=json";
        public const string format_xml = "format=xml";
        public const string token = "full_token";
        public const string language = "language=en";
        public const string payload_structure = "payload[flat_structure]=1";
        public const string payload_month = "payload[month_id]=";
        //public const string month_id = "202204";
        public const string APIUrl_json = APIUrl + workspace + "?_" + format_json + "&_token=" + token + "&_" + language + "&" + payload_structure + "&" + payload_month ;
        public const string APIUrl_xml = APIUrl + workspace + "?_" + format_xml + "&_token=" + token + "&_" + language + "&" + payload_structure + "&" + payload_month ;

        //public const string APIUrl_json = "https://splareporter2.octopus.cloud/api/workspace/{{workspace_id}}/report/licensing/spla-usage-customer?_format=json&_token=full_token&_tenant=tenent_id&_language=en&payload[flat_structure]=1&payload[month_id]=202204";
        //public const string APIUrl_xml = "https://splareporter2.octopus.cloud/api/workspace/{{workspace_id}}/report/licensing/spla-usage-customer?_format=xml&_token=full_token&_tenant=tenant_id&_language=en&payload[flat_structure]=1&payload[month_id]=202203";
        public const string path_json = @"D:\folder\downloads\report.json";
        public const string path_json_nl = @"D:\folder\downloads\report-nl.json";
        public const string path_csv = @"D:\folder\downloads\report.csv";
        public const string path_xlsx = @"D:\folder\downloads\report.xlsx";
    }
}

