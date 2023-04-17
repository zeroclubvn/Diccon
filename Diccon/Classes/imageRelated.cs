﻿using System;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Diccon
{
    internal class ImageRelated
    {
        Random random = new Random();
        int imageIndex = 0;
        public async Task<string> GetImageUrl(string word)
        {
            imageIndex = random.Next(0, 3);
            string rawContent;
            string imageUrl = "none";
            // https://github.com/ZeroClubOfficial/English-Through-Pictures/raw/main/A/alarm.jpg
            string imageTestLink = "https://github.com/ZeroClubOfficial/English-Through-Pictures/raw/main/" + word.Substring(0, 1).ToUpper() + "/" + word + ".jpg";
            Connectivity connect = new Connectivity();
            var alive = await connect.IsWebsiteAlive(new Uri(imageTestLink));
            if (alive == true)
            {
                DicconProp.IsFromPixabay = false;
                return imageTestLink;
            }
            else
                try
                {
                    HttpClient client = new HttpClient();
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, @"https://pixabay.com/api/?key=25829393-af32bf17ec8386b5941fb5f8f&q=" + word + @"&image_type=photo");
                    using (var responseMessage = await client.SendAsync(request))
                    {
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            rawContent = await responseMessage.Content.ReadAsStringAsync();
                            JsonNode jsonContent = JsonNode.Parse(rawContent);
                            imageUrl = jsonContent["hits"][imageIndex++]["webformatURL"].GetValue<string>();

                        }

                    }
                    DicconProp.IsFromPixabay = true;
                    return imageUrl;
                }
                catch (Exception)
                {

                    return "none";
                }

        }
    }
}
