using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net;

using HappyStation.Web.Settings;
using HappyStation.Web.ViewModels;

using Newtonsoft.Json;

namespace HappyStation.Web.ControllerServices
{
    public class InstagramService
    {
        public InstagramService(ApplicationSettings settings)
        {
            Contract.Requires(settings != null);

            this.settings = settings;
        }

        public IEnumerable<InstagramFeedViewModel> GetFeed(int count)
        {
            var request =
                (HttpWebRequest)
                    WebRequest.Create("https://api.instagram.com/v1/users/" +
                                      settings.InstagramUserId + "/media/recent?access_token=" +
                                      settings.UserInstagramAccessToken);
            request.Method = "GET";
            var json = String.Empty;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var dataStream = response.GetResponseStream();
                if (dataStream == null)
                {
                    return Parse(json);
                }

                var reader = new StreamReader(dataStream);
                json = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
            }

            return Parse(json).Take(count);
        }

        private IEnumerable<InstagramFeedViewModel> Parse(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return new List<InstagramFeedViewModel>();
            }

            var result = new List<InstagramFeedViewModel>();

            dynamic dyn = JsonConvert.DeserializeObject(json);
            foreach (var data in dyn.data)
            {
                var reg = new InstagramFeedViewModel
                {
                    FullImage = data.images.standard_resolution.url,
                    Link = data.link,
                    Preview = data.images.thumbnail.url
                };

                if (data.caption != null)
                {
                    reg.Title = data.caption.text;
                }

                result.Add(reg);
            }

            return result;
        }

        private readonly ApplicationSettings settings;
    }
}