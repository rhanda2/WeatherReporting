using System;
using System.Net;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Xml;
using webApp.Models;

namespace webApp.Controllers
{
    public class HomeController : Controller
    {
            private readonly Dictionary<string, Weather> stations = new Dictionary<string, Weather>()
            {
                { "Ainsworth Regional Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KANW") },
                { "Albion Municipal Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KBVN") },
                { "Alliance, Alliance Municipal Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KAIA") },
                { "Aurora, Aurora Municipal Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KAUH") },
                { "Beatrice Municipal", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KBIE") },
                { "Blair Municipal Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KBTA") },
                { "Bravo (Pine Bluffs)", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=K1BW") },
                { "Brewster Field Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KHDE") },
                { "Broken Bow, Broken Bow Municipal Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KBBW") },
                { "Chadron, Chadron Municipal Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KCDR") },
                { "Columbus, Columbus Municipal Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KOLU") },
                { "Delta - Kimball", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=K1DW") },
                { "Falls City, Brenner Field Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KFNB") },
                { "Foxtrot - Dix", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=K1FW") },
                { "Fremont Municipal Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KFET") },
                { "Gordon Municipal", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KGRN") },
                { "Grand Island, Central Nebraska Regional Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KGRI") },
                { "Hastings, Hastings Municipal Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KHSI") },
                { "Hebron, Hebron Municipal Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KHJH") },
                { "Hotel (Gurley)", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=K1HW") },
                { "Imperial, Imperial Municipal Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KIML") },
                { "Kearney, Kearney Municipal Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KEAR") },
                { "Kimball, Kimball Municipal Airport/Robert E Arraj Field", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KIBM") },
                { "Lexington, Jim Kelly Field Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KLXN") },
                { "Lincoln, Lincoln Municipal Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KLNK") },
                { "McCook, McCook Municipal Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KMCK") },
                { "Nebraska City, Nebraska City Municipal Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KAFK") },
                { "Norfolk, Stefan Memorial Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KOFK") },
                { "North Platte, North Platte Regional Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KLBF") },
                { "O'Neill / Baker Field", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KONL") },
                { "Ogallala, Searle Field Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KOGA") },
                { "Omaha - Eppley Airfield", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KOMA") },
                { "Omaha / Offutt Air Force Base", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KOFF") },
                { "Omaha, Millard Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KMLE") },
                { "Ord, Evelyn Sharp Field Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KODX") },
                { "Plattsmouth, Plattsmouth Municipal Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KPMV") },
                { "Scottsbluff, Heilig Field", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KBFF") },
                { "Sidney, Sidney Municipal Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KSNY") },
                { "Tekamah, Tekamah Municipal Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KTQE") },
                { "Thedford, Thomas County Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KTIF") },
                { "Valentine, Miller Field", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KVTN") },
                { "Wahoo", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KAHQ") },
                { "Wayne, Wayne Municipal Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KLCG") },
                { "York, York Municipal Airport", GetWeather("https://w1.weather.gov/xml/current_obs/display.php?stid=KJYR") }
            };
            


            public IActionResult Index()
            {
            
                return View(stations);
            }


            public IActionResult Privacy()
            {
                return View();
            }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            public static Weather GetWeather(string url)
            {

                //string url = "http://w1.weather.gov/xml/current_obs/KLNK.xml";
                XmlDocument doc = new XmlDocument();
                WebClient webClient = new WebClient();
                webClient.Headers.Add("User-Agent", "Weather App, missaleka@yahoo.com");
                string xml = webClient.DownloadString(url);

                doc.LoadXml(xml);




                doc.Save(Console.Out);
            Console.Write(xml);
                XmlNode tempNode = doc.DocumentElement.SelectSingleNode("temp_f");
                XmlNode windMphNode = doc.DocumentElement.SelectSingleNode("wind_mph");
                XmlNode windDirectionNode = doc.DocumentElement.SelectSingleNode("wind_dir");
                
                XmlNode humidNode = doc.DocumentElement.SelectSingleNode("relative_humidity");
                XmlNode lastUpdated = doc.DocumentElement.SelectSingleNode("observation_time");
                XmlNode windDegrees = doc.DocumentElement.SelectSingleNode("wind_degrees");
                XmlNode dewNode = doc.DocumentElement.SelectSingleNode("dewpoint_f");
                XmlNode pressureNode = doc.DocumentElement.SelectSingleNode("pressure_in");
                string tempF = tempNode.InnerText;
                string windMph = windMphNode.InnerText;
                string windDirection = windDirectionNode.InnerText;
                string lastUpdate = lastUpdated.InnerText;
                string windDeg = windDegrees.InnerText;
                string relativeHumid = humidNode.InnerText;
                string dewPointF = dewNode.InnerText;
                string pressureMb;
                if (pressureNode != null)
                {
                    string pressure = pressureNode.InnerText;
                    Double pressureM = Math.Round(Convert.ToDouble(pressure) * 33.863886667, 2); //convert air pressure from in to mb
                    pressureMb = Convert.ToString(pressureM);
                } else
                {
                    pressureMb = "Not available.";
                }
                
                
              
            Weather w = new Weather()
            {
                Temp = tempF,
                WindMph = windMph,
                WindDir = windDirection,
                TimeLastUpdated = lastUpdate,
                WindDegrees = windDeg,
                RelativeHumidity = relativeHumid,
                Dewpoint = dewPointF,
                PressureMb = pressureMb
            };

                return w;

            }
        }
}
