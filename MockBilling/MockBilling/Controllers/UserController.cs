using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MockBilling.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return Ok("OK");
        }

        [HttpGet("by_login")]
        public ActionResult GetPlaylistByLogin(string login, string password)
        {
            List<UserLogin> items;
            int playlist = -1;

            // Read json file
            using (StreamReader r = new StreamReader("Users/json.json"))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<UserLogin>>(json);
            }

            items.ForEach(k =>
            {
                if(k.login == login && k.password == password)
                {
                    playlist = k.playlistId;
                }
            });

            if(playlist != -1)
            {
                return Ok(playlist);
            }

            return Ok("null");
        }

        [HttpGet("by_key")]
        public ActionResult GetPlayliswtByKey(string key)
        {
            List<PlaylistKey> items;
            int playlist = -1;

            // Read json file
            using (StreamReader r = new StreamReader("Users/keys.json"))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<PlaylistKey>>(json);
            }

            try
            {
                playlist = items.Where(t => t.Key == key).ToList()[0].PlaylistId;
            }
            catch (ArgumentOutOfRangeException)
            {
                return Ok("null");
            }


            return Ok(playlist);
        }

        [HttpGet("by_ip")]
        public ActionResult GetPlaylistByIp(string ip)
        {
            List<PlaylistIp> items;
            int playlist = -1;

            // Read json file
            using (StreamReader r = new StreamReader("Users/PlaylistIP.json"))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<PlaylistIp>>(json);
            }

            try
            {
                playlist = items.Where(t => t.Ip == ip).ToList()[0].Playlist;
            }
            catch (ArgumentOutOfRangeException)
            {
                return Ok("null");
            }

            return Ok(playlist);
        }

        private class UserLogin
        {
            public string login { get; set; }
            public string password { get; set; }
            public int playlistId { get; set; }
        }

        private class PlaylistKey
        {
            public int PlaylistId { get; set; }
            public string Key { get; set; }

        }

        private class PlaylistIp
        {
            public string Ip { get; set; }
            public int Playlist { get; set; }
        }
    }
    //"login": "petya",
    //"password": "123",
    //"playlistId": 3

}


