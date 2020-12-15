using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace SupportServices
{
    public class XMLGeneratorService : IXMLGenerator
    {
        private const int tabLength = 4;

        public string Generate<T>(List<T> models, int startId)
        {
            string response = "<?xml version = \"1.0\" encoding='\"UTF-8\"?>\n";
            int currentId = startId;
            foreach(var model in models)
            {
                response += GetTag("item" + currentId.ToString(), 0, true);
                response += GenerateSingle(model, 1);
                response += GetTag("item" + currentId.ToString(), 0, false);
                currentId++;
            }
            return response;
        }
        
        private string GenerateSingle(object model, int deep)
        {
            var modelFields = model.GetType().GetFields();
            var response = "";
            foreach(var field in modelFields)
            {
                response += GetTag(field.Name, deep, true);
                if (!field.GetValue(model).GetType().IsNested)
                {
                    var modelSubFields = field.GetValue(model).GetType().GetFields();
                    foreach (var subField in modelSubFields)
                    {
                        response += GetTag(subField.Name, deep + 1, true);
                        response += GetText(Convert.ToString(subField.GetValue(field.GetValue(model))), deep + 2);
                        response += GetTag(subField.Name, deep + 1, false);
                    }
                } else
                {
                    response += GenerateSingle(field.GetValue(model), deep + 1);
                }

                response += GetTag(field.Name, deep, false);
            }

            return response;
        }

        private string GetTag(string tagName, int deep, bool isOpenningTag)
        {
            string response = "";
            for (int i = 0; i < deep * tabLength; i++)
                response += " ";
            response += "<";
            if (!isOpenningTag) response += "/";
            response += tagName;
            response += ">\n";
            return response;
        }
        
        private string GetText(string text, int deep)
        {
            string response = "";
            for (int i = 0; i < deep * tabLength; i++)
                response += " ";
            response += text + '\n';
            return response;
        }

    }
}
