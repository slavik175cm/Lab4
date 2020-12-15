using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace ServiceLayer
{
    public class ServiceLayer
    {
        public List<Models.FullPersonInfo> GetManyPersonFullInfos(string connectionString, int page, int count)
        {
            DataAccess dataAccess = new DataAccess();
            var passwords = dataAccess.GetModels<Models.Password>(connectionString, "[dbo].[ReadPassword]", page, count);
            var emails = dataAccess.GetModels<Models.Email>(connectionString, "[dbo].[ReadEmail]", page, count);
            var phones = dataAccess.GetModels<Models.Phone>(connectionString, "[dbo].[ReadPhone]", page, count);
            var persons = dataAccess.GetModels<Models.Person>(connectionString, "[dbo].[ReadPerson]", page, count);
            var addresses = dataAccess.GetModels<Models.Address>(connectionString, "[dbo].[ReadAddress]", page, count);

            int total = passwords.Count;
            List<Models.FullPersonInfo> businessInfoList = new List<Models.FullPersonInfo>();
            for (int i = 0; i < total; i++)
            {
                businessInfoList.Add(new Models.FullPersonInfo());
            }

            AddSubClasses<Models.FullPersonInfo, Models.Password>(businessInfoList, passwords);
            AddSubClasses<Models.FullPersonInfo, Models.Email>(businessInfoList, emails);
            AddSubClasses<Models.FullPersonInfo, Models.Phone>(businessInfoList, phones);
            AddSubClasses<Models.FullPersonInfo, Models.Person>(businessInfoList, persons);
            AddSubClasses<Models.FullPersonInfo, Models.Address>(businessInfoList, addresses);

            return businessInfoList;
        }

        private void AddSubClasses<TClass, TSubClass>(List<TClass> classes, List<TSubClass> subclasses)
        {
            var subClassName = typeof(TSubClass).Name;
            FieldInfo classField = typeof(TClass).GetField(subClassName);
            int i = 0;
            foreach(var singleClass in classes) 
            {
                if (subclasses.Count <= i) break;
                classField.SetValue(singleClass, subclasses[i++]);
            }
        }
    }
}