using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.Data.Json;
using Windows.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using Windows.System;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CheapSharkApp
{
    /// <summary>
    /// MainPage C# code.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<GameObject> SteamGamesList { get; set; }
        List<GameObject> GamersGateGamesList { get; set; }
        List<GameObject> GMGGamesList { get; set; }
        List<GameObject> AmazonGamesList { get; set; }
        List<GameObject> GameStopGamesList { get; set; }
        List<GameObject> GameFlyGamesList { get; set; }
        List<GameObject> GOGGamesList { get; set; }
        List<GameObject> OriginGamesList { get; set; }
        List<GameObject> GetGamesGamesList { get; set; }

        public class GlobalVariables
        {
            public static Uri target = null;
        } 

        // ----------------------------------------------------------------------------------------------------------------------- //
        // Main method.
        // ----------------------------------------------------------------------------------------------------------------------- //
        public MainPage()
        {

            // Initialize page:
            this.InitializeComponent();

            // Grab JSON data and fill page:
            ReadDataFromWeb();

            // Update layout:
            this.UpdateLayout();

        }

        // ----------------------------------------------------------------------------------------------------------------------- //
        // Async method to grab JSON strings from CheapShark.com, and call the Populate methods.
        // ----------------------------------------------------------------------------------------------------------------------- //

        async public void ReadDataFromWeb()
        {
            // Check if there is an internet connection:
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {

                // Create HttpClient:
                var client = new Windows.Web.Http.HttpClient();

                // Grab Steam JSON string:
                var responseSteam = await client.GetAsync(new Uri("http://www.cheapshark.com/pager.php?storeID=0&sortBy=Deal%20Rating&desc=0&title=&AAA=0&steamworks=0&onSale=0&lowerPrice=0&upperPrice=50&pageSize=10&page=0"));
                string resultSteam = await responseSteam.Content.ReadAsStringAsync();

                // Grab GamersGate JSON string:
                var responseGamersGate = await client.GetAsync(new Uri("http://www.cheapshark.com/pager.php?storeID=1&sortBy=Deal%20Rating&desc=0&title=&AAA=0&steamworks=0&onSale=0&lowerPrice=0&upperPrice=50&pageSize=10&page=0"));
                string resultGamersGate = await responseGamersGate.Content.ReadAsStringAsync();

                // Grab GreenManGaming JSON string:
                var responseGMG = await client.GetAsync(new Uri("http://www.cheapshark.com/pager.php?storeID=2&sortBy=Deal%20Rating&desc=0&title=&AAA=0&steamworks=0&onSale=0&lowerPrice=0&upperPrice=50&pageSize=10&page=0"));
                string resultGMG = await responseGMG.Content.ReadAsStringAsync();

                // Grab Amazon JSON string:
                var responseAmazon = await client.GetAsync(new Uri("http://www.cheapshark.com/pager.php?storeID=3&sortBy=Deal%20Rating&desc=0&title=&AAA=0&steamworks=0&onSale=0&lowerPrice=0&upperPrice=50&pageSize=10&page=0"));
                string resultAmazon = await responseAmazon.Content.ReadAsStringAsync();

                // Grab GameStop JSON string:
                var responseGameStop = await client.GetAsync(new Uri("http://www.cheapshark.com/pager.php?storeID=4&sortBy=Deal%20Rating&desc=0&title=&AAA=0&steamworks=0&onSale=0&lowerPrice=0&upperPrice=50&pageSize=10&page=0"));
                string resultGameStop = await responseGameStop.Content.ReadAsStringAsync();

                // Grab GameFly JSON string:
                var responseGameFly = await client.GetAsync(new Uri("http://www.cheapshark.com/pager.php?storeID=5&sortBy=Deal%20Rating&desc=0&title=&AAA=0&steamworks=0&onSale=0&lowerPrice=0&upperPrice=50&pageSize=10&page=0"));
                string resultGameFly = await responseGameFly.Content.ReadAsStringAsync();

                // Grab GOG JSON string:
                var responseGOG = await client.GetAsync(new Uri("http://www.cheapshark.com/pager.php?storeID=6&sortBy=Deal%20Rating&desc=0&title=&AAA=0&steamworks=0&onSale=0&lowerPrice=0&upperPrice=50&pageSize=10&page=0"));
                string resultGOG = await responseGOG.Content.ReadAsStringAsync();

                // Grab Origin JSON string:
                var responseOrigin = await client.GetAsync(new Uri("http://www.cheapshark.com/pager.php?storeID=7&sortBy=Deal%20Rating&desc=0&title=&AAA=0&steamworks=0&onSale=0&lowerPrice=0&upperPrice=50&pageSize=10&page=0"));
                string resultOrigin = await responseOrigin.Content.ReadAsStringAsync();

                // Grab GetGames JSON string:
                var responseGetGames = await client.GetAsync(new Uri("http://www.cheapshark.com/pager.php?storeID=8&sortBy=Deal%20Rating&desc=0&title=&AAA=0&steamworks=0&onSale=0&lowerPrice=0&upperPrice=50&pageSize=10&page=0"));
                string resultGetGames = await responseGetGames.Content.ReadAsStringAsync();

                // Call Populate methods:
                PopulateSteam(resultSteam);
                PopulateGamersGate(resultGamersGate);
                PopulateGMG(resultGMG);
                PopulateAmazon(resultAmazon);
                PopulateGameStop(resultGameStop);
                PopulateGameFly(resultGameFly);
                PopulateGOG(resultGOG);
                PopulateOrigin(resultOrigin);
                PopulateGetGames(resultGetGames);

            }
            else
            {

                // If no internet connection, output errors:

                for (int i = 1; i < 11; i++)
                {
                    Button buttonSteamItem = this.FindName("buttonSteamItem" + (i).ToString()) as Button;
                    buttonSteamItem.Content = "Network Error";
                }

                for (int i = 1; i < 11; i++)
                {
                    Button buttonGamersGateItem = this.FindName("buttonGamersGateItem" + (i).ToString()) as Button;
                    buttonGamersGateItem.Content = "Network Error";
                }

                for (int i = 1; i < 11; i++)
                {
                    Button buttonGMGItem = this.FindName("buttonGMGItem" + (i).ToString()) as Button;
                    buttonGMGItem.Content = "Network Error";
                }

                for (int i = 1; i < 11; i++)
                {
                    Button buttonAmazonItem = this.FindName("buttonAmazonItem" + (i).ToString()) as Button;
                    buttonAmazonItem.Content = "Network Error";
                }

                for (int i = 1; i < 11; i++)
                {
                    Button buttonGameStopItem = this.FindName("buttonGameStopItem" + (i).ToString()) as Button;
                    buttonGameStopItem.Content = "Network Error";
                }

                for (int i = 1; i < 11; i++)
                {
                    Button buttonGameFlyItem = this.FindName("buttonGameFlyItem" + (i).ToString()) as Button;
                    buttonGameFlyItem.Content = "Network Error";
                }

                for (int i = 1; i < 11; i++)
                {
                    Button buttonGOGItem = this.FindName("buttonGOGItem" + (i).ToString()) as Button;
                    buttonGOGItem.Content = "Network Error";
                }

                for (int i = 1; i < 11; i++)
                {
                    Button buttonOriginItem = this.FindName("buttonOriginItem" + (i).ToString()) as Button;
                    buttonOriginItem.Content = "Network Error";
                }

                for (int i = 1; i < 11; i++)
                {
                    Button buttonGetGamesItem = this.FindName("buttonGetGamesItem" + (i).ToString()) as Button;
                    buttonGetGamesItem.Content = "Network Error";
                } 
            }
        }

        // ----------------------------------------------------------------------------------------------------------------------- //
        // Object to hold JSON objects. Each GameObject corresponds to one game listing.
        // ----------------------------------------------------------------------------------------------------------------------- //

        public class GameObject
        {

            [DataMember]
            public string InternalName { get; set; }

            [DataMember]
            public string Title { get; set; }

            [DataMember]
            public string MetacriticLink { get; set; }

            [DataMember]
            public string DealID { get; set; }

            [DataMember]
            public string StoreID { get; set; }

            [DataMember]
            public string GameID { get; set; }

            [DataMember]
            public string SalePrice { get; set; }

            [DataMember]
            public string NormalPrice { get; set; }

            [DataMember]
            public string Savings { get; set; }

            [DataMember]
            public string Score { get; set; }

            [DataMember]
            public string ReleaseDate { get; set; }

            [DataMember]
            public string LastChange { get; set; }

            [DataMember]
            public string DealRating { get; set; }

            [DataMember]
            public string Thumb { get; set; }
        }

        // ----------------------------------------------------------------------------------------------------------------------- //
        // Methods to create JSON objects and populate MainPage.
        // ----------------------------------------------------------------------------------------------------------------------- //

        public void PopulateSteam(string jsonResult)
        {
            // Create List of 10 Objects from Steam: 
            SteamGamesList = JsonConvert.DeserializeObject<List<GameObject>>(jsonResult);
            GameObject steamGame1 = SteamGamesList[0];
            GameObject steamGame2 = SteamGamesList[1];
            GameObject steamGame3 = SteamGamesList[2];
            GameObject steamGame4 = SteamGamesList[3];
            GameObject steamGame5 = SteamGamesList[4];
            GameObject steamGame6 = SteamGamesList[5];
            GameObject steamGame7 = SteamGamesList[6];
            GameObject steamGame8 = SteamGamesList[7];
            GameObject steamGame9 = SteamGamesList[8];
            GameObject steamGame10 = SteamGamesList[9];

            // Output data:
            buttonSteamItem1.Content = steamGame1.Title + "\n" + "  $" + steamGame1.NormalPrice + "  \x2192  $" + steamGame1.SalePrice;
            buttonSteamItem1.Click += buttonSteamItem1_Click;

            buttonSteamItem2.Content = steamGame2.Title + "\n" + "  $" + steamGame2.NormalPrice + "  \x2192  $" + steamGame2.SalePrice;
            buttonSteamItem3.Content = steamGame3.Title + "\n" + "  $" + steamGame3.NormalPrice + "  \x2192  $" + steamGame3.SalePrice;
            buttonSteamItem4.Content = steamGame4.Title + "\n" + "  $" + steamGame4.NormalPrice + "  \x2192  $" + steamGame4.SalePrice;
            buttonSteamItem5.Content = steamGame5.Title + "\n" + "  $" + steamGame5.NormalPrice + "  \x2192  $" + steamGame5.SalePrice;
            buttonSteamItem6.Content = steamGame6.Title + "\n" + "  $" + steamGame6.NormalPrice + "  \x2192  $" + steamGame6.SalePrice;
            buttonSteamItem7.Content = steamGame7.Title + "\n" + "  $" + steamGame7.NormalPrice + "  \x2192  $" + steamGame7.SalePrice;
            buttonSteamItem8.Content = steamGame8.Title + "\n" + "  $" + steamGame8.NormalPrice + "  \x2192  $" + steamGame8.SalePrice;
            buttonSteamItem9.Content = steamGame9.Title + "\n" + "  $" + steamGame9.NormalPrice + "  \x2192  $" + steamGame9.SalePrice;
            buttonSteamItem10.Content = steamGame10.Title + "\n" + "  $" + steamGame10.NormalPrice + "  \x2192  $" + steamGame10.SalePrice;
        }

        public void PopulateGamersGate(string jsonResult)
        {
            // Create List of 10 Objects from GetGames:
            GamersGateGamesList = JsonConvert.DeserializeObject<List<GameObject>>(jsonResult);
            GameObject gamersGateGame1 = GamersGateGamesList[0];
            GameObject gamersGateGame2 = GamersGateGamesList[1];
            GameObject gamersGateGame3 = GamersGateGamesList[2];
            GameObject gamersGateGame4 = GamersGateGamesList[3];
            GameObject gamersGateGame5 = GamersGateGamesList[4];
            GameObject gamersGateGame6 = GamersGateGamesList[5];
            GameObject gamersGateGame7 = GamersGateGamesList[6];
            GameObject gamersGateGame8 = GamersGateGamesList[7];
            GameObject gamersGateGame9 = GamersGateGamesList[8];
            GameObject gamersGateGame10 = GamersGateGamesList[9];

            //Output data:
            buttonGamersGateItem1.Content = gamersGateGame1.Title + "\n" + "  $" + gamersGateGame1.NormalPrice + "  \x2192  $" + gamersGateGame1.SalePrice;
            buttonGamersGateItem2.Content = gamersGateGame2.Title + "\n" + "  $" + gamersGateGame2.NormalPrice + "  \x2192  $" + gamersGateGame2.SalePrice;
            buttonGamersGateItem3.Content = gamersGateGame3.Title + "\n" + "  $" + gamersGateGame3.NormalPrice + "  \x2192  $" + gamersGateGame3.SalePrice;
            buttonGamersGateItem4.Content = gamersGateGame4.Title + "\n" + "  $" + gamersGateGame4.NormalPrice + "  \x2192  $" + gamersGateGame4.SalePrice;
            buttonGamersGateItem5.Content = gamersGateGame5.Title + "\n" + "  $" + gamersGateGame5.NormalPrice + "  \x2192  $" + gamersGateGame5.SalePrice;
            buttonGamersGateItem6.Content = gamersGateGame6.Title + "\n" + "  $" + gamersGateGame6.NormalPrice + "  \x2192  $" + gamersGateGame6.SalePrice;
            buttonGamersGateItem7.Content = gamersGateGame7.Title + "\n" + "  $" + gamersGateGame7.NormalPrice + "  \x2192  $" + gamersGateGame7.SalePrice;
            buttonGamersGateItem8.Content = gamersGateGame8.Title + "\n" + "  $" + gamersGateGame8.NormalPrice + "  \x2192  $" + gamersGateGame8.SalePrice;
            buttonGamersGateItem9.Content = gamersGateGame9.Title + "\n" + "  $" + gamersGateGame9.NormalPrice + "  \x2192  $" + gamersGateGame9.SalePrice;
            buttonGamersGateItem10.Content = gamersGateGame10.Title + "\n" + "  $" + gamersGateGame10.NormalPrice + "  \x2192  $" + gamersGateGame10.SalePrice;
        }

        public void PopulateGMG(string jsonResult)
        {
            // Create List of 10 Objects from GreenManGaming:
            GMGGamesList = JsonConvert.DeserializeObject<List<GameObject>>(jsonResult);
            GameObject GMGGame1 = GMGGamesList[0];
            GameObject GMGGame2 = GMGGamesList[1];
            GameObject GMGGame3 = GMGGamesList[2];
            GameObject GMGGame4 = GMGGamesList[3];
            GameObject GMGGame5 = GMGGamesList[4];
            GameObject GMGGame6 = GMGGamesList[5];
            GameObject GMGGame7 = GMGGamesList[6];
            GameObject GMGGame8 = GMGGamesList[7];
            GameObject GMGGame9 = GMGGamesList[8];
            GameObject GMGGame10 = GMGGamesList[9];

            // Output data:
            buttonGMGItem1.Content = GMGGame1.Title + "\n" + "  $" + GMGGame1.NormalPrice + "  \x2192  $" + GMGGame1.SalePrice;
            buttonGMGItem2.Content = GMGGame2.Title + "\n" + "  $" + GMGGame2.NormalPrice + "  \x2192  $" + GMGGame2.SalePrice;
            buttonGMGItem3.Content = GMGGame3.Title + "\n" + "  $" + GMGGame3.NormalPrice + "  \x2192  $" + GMGGame3.SalePrice;
            buttonGMGItem4.Content = GMGGame4.Title + "\n" + "  $" + GMGGame4.NormalPrice + "  \x2192  $" + GMGGame4.SalePrice;
            buttonGMGItem5.Content = GMGGame5.Title + "\n" + "  $" + GMGGame5.NormalPrice + "  \x2192  $" + GMGGame5.SalePrice;
            buttonGMGItem6.Content = GMGGame6.Title + "\n" + "  $" + GMGGame6.NormalPrice + "  \x2192  $" + GMGGame6.SalePrice;
            buttonGMGItem7.Content = GMGGame7.Title + "\n" + "  $" + GMGGame7.NormalPrice + "  \x2192  $" + GMGGame7.SalePrice;
            buttonGMGItem8.Content = GMGGame8.Title + "\n" + "  $" + GMGGame8.NormalPrice + "  \x2192  $" + GMGGame8.SalePrice;
            buttonGMGItem9.Content = GMGGame9.Title + "\n" + "  $" + GMGGame9.NormalPrice + "  \x2192  $" + GMGGame9.SalePrice;
            buttonGMGItem10.Content = GMGGame10.Title + "\n" + "  $" + GMGGame10.NormalPrice + "  \x2192  $" + GMGGame10.SalePrice;
        }

        public void PopulateAmazon(string jsonResult)
        {
            // Create List of 10 Objects from Amazon:
            AmazonGamesList = JsonConvert.DeserializeObject<List<GameObject>>(jsonResult);
            GameObject AmazonGame1 = AmazonGamesList[0];
            GameObject AmazonGame2 = AmazonGamesList[1];
            GameObject AmazonGame3 = AmazonGamesList[2];
            GameObject AmazonGame4 = AmazonGamesList[3];
            GameObject AmazonGame5 = AmazonGamesList[4];
            GameObject AmazonGame6 = AmazonGamesList[5];
            GameObject AmazonGame7 = AmazonGamesList[6];
            GameObject AmazonGame8 = AmazonGamesList[7];
            GameObject AmazonGame9 = AmazonGamesList[8];
            GameObject AmazonGame10 = AmazonGamesList[9];

            // Output data:
            buttonAmazonItem1.Content = AmazonGame1.Title + "\n" + "  $" + AmazonGame1.NormalPrice + "  \x2192  $" + AmazonGame1.SalePrice;
            buttonAmazonItem2.Content = AmazonGame2.Title + "\n" + "  $" + AmazonGame2.NormalPrice + "  \x2192  $" + AmazonGame2.SalePrice;
            buttonAmazonItem3.Content = AmazonGame3.Title + "\n" + "  $" + AmazonGame3.NormalPrice + "  \x2192  $" + AmazonGame3.SalePrice;
            buttonAmazonItem4.Content = AmazonGame4.Title + "\n" + "  $" + AmazonGame4.NormalPrice + "  \x2192  $" + AmazonGame4.SalePrice;
            buttonAmazonItem5.Content = AmazonGame5.Title + "\n" + "  $" + AmazonGame5.NormalPrice + "  \x2192  $" + AmazonGame5.SalePrice;
            buttonAmazonItem6.Content = AmazonGame6.Title + "\n" + "  $" + AmazonGame6.NormalPrice + "  \x2192  $" + AmazonGame6.SalePrice;
            buttonAmazonItem7.Content = AmazonGame7.Title + "\n" + "  $" + AmazonGame7.NormalPrice + "  \x2192  $" + AmazonGame7.SalePrice;
            buttonAmazonItem8.Content = AmazonGame8.Title + "\n" + "  $" + AmazonGame8.NormalPrice + "  \x2192  $" + AmazonGame8.SalePrice;
            buttonAmazonItem9.Content = AmazonGame9.Title + "\n" + "  $" + AmazonGame9.NormalPrice + "  \x2192  $" + AmazonGame9.SalePrice;
            buttonAmazonItem10.Content = AmazonGame10.Title + "\n" + "  $" + AmazonGame10.NormalPrice + "  \x2192  $" + AmazonGame10.SalePrice;
        }

        public void PopulateGameStop(string jsonResult)
        {
            // Create List of 10 Objects from GameStop:
            GameStopGamesList = JsonConvert.DeserializeObject<List<GameObject>>(jsonResult);
            GameObject GameStopGame1 = GameStopGamesList[0];
            GameObject GameStopGame2 = GameStopGamesList[1];
            GameObject GameStopGame3 = GameStopGamesList[2];
            GameObject GameStopGame4 = GameStopGamesList[3];
            GameObject GameStopGame5 = GameStopGamesList[4];
            GameObject GameStopGame6 = GameStopGamesList[5];
            GameObject GameStopGame7 = GameStopGamesList[6];
            GameObject GameStopGame8 = GameStopGamesList[7];
            GameObject GameStopGame9 = GameStopGamesList[8];
            GameObject GameStopGame10 = GameStopGamesList[9];

            // Output data:
            buttonGameStopItem1.Content = GameStopGame1.Title + "\n" + "  $" + GameStopGame1.NormalPrice + "  \x2192  $" + GameStopGame1.SalePrice;
            buttonGameStopItem2.Content = GameStopGame2.Title + "\n" + "  $" + GameStopGame2.NormalPrice + "  \x2192  $" + GameStopGame2.SalePrice;
            buttonGameStopItem3.Content = GameStopGame3.Title + "\n" + "  $" + GameStopGame3.NormalPrice + "  \x2192  $" + GameStopGame3.SalePrice;
            buttonGameStopItem4.Content = GameStopGame4.Title + "\n" + "  $" + GameStopGame4.NormalPrice + "  \x2192  $" + GameStopGame4.SalePrice;
            buttonGameStopItem5.Content = GameStopGame5.Title + "\n" + "  $" + GameStopGame5.NormalPrice + "  \x2192  $" + GameStopGame5.SalePrice;
            buttonGameStopItem6.Content = GameStopGame6.Title + "\n" + "  $" + GameStopGame6.NormalPrice + "  \x2192  $" + GameStopGame6.SalePrice;
            buttonGameStopItem7.Content = GameStopGame7.Title + "\n" + "  $" + GameStopGame7.NormalPrice + "  \x2192  $" + GameStopGame7.SalePrice;
            buttonGameStopItem8.Content = GameStopGame8.Title + "\n" + "  $" + GameStopGame8.NormalPrice + "  \x2192  $" + GameStopGame8.SalePrice;
            buttonGameStopItem9.Content = GameStopGame9.Title + "\n" + "  $" + GameStopGame9.NormalPrice + "  \x2192  $" + GameStopGame9.SalePrice;
            buttonGameStopItem10.Content = GameStopGame10.Title + "\n" + "  $" + GameStopGame10.NormalPrice + "  \x2192  $" + GameStopGame10.SalePrice;
        }

        public void PopulateGameFly(string jsonResult)
        {
            // Create List of 10 Objects from GameFly:
            GameFlyGamesList = JsonConvert.DeserializeObject<List<GameObject>>(jsonResult);
            GameObject GameFlyGame1 = GameFlyGamesList[0];
            GameObject GameFlyGame2 = GameFlyGamesList[1];
            GameObject GameFlyGame3 = GameFlyGamesList[2];
            GameObject GameFlyGame4 = GameFlyGamesList[3];
            GameObject GameFlyGame5 = GameFlyGamesList[4];
            GameObject GameFlyGame6 = GameFlyGamesList[5];
            GameObject GameFlyGame7 = GameFlyGamesList[6];
            GameObject GameFlyGame8 = GameFlyGamesList[7];
            GameObject GameFlyGame9 = GameFlyGamesList[8];
            GameObject GameFlyGame10 = GameFlyGamesList[9];

            // Output data:
            buttonGameFlyItem1.Content = GameFlyGame1.Title + "\n" + "  $" + GameFlyGame1.NormalPrice + "  \x2192  $" + GameFlyGame1.SalePrice;
            buttonGameFlyItem2.Content = GameFlyGame2.Title + "\n" + "  $" + GameFlyGame2.NormalPrice + "  \x2192  $" + GameFlyGame2.SalePrice;
            buttonGameFlyItem3.Content = GameFlyGame3.Title + "\n" + "  $" + GameFlyGame3.NormalPrice + "  \x2192  $" + GameFlyGame3.SalePrice;
            buttonGameFlyItem4.Content = GameFlyGame4.Title + "\n" + "  $" + GameFlyGame4.NormalPrice + "  \x2192  $" + GameFlyGame4.SalePrice;
            buttonGameFlyItem5.Content = GameFlyGame5.Title + "\n" + "  $" + GameFlyGame5.NormalPrice + "  \x2192  $" + GameFlyGame5.SalePrice;
            buttonGameFlyItem6.Content = GameFlyGame6.Title + "\n" + "  $" + GameFlyGame6.NormalPrice + "  \x2192  $" + GameFlyGame6.SalePrice;
            buttonGameFlyItem7.Content = GameFlyGame7.Title + "\n" + "  $" + GameFlyGame7.NormalPrice + "  \x2192  $" + GameFlyGame7.SalePrice;
            buttonGameFlyItem8.Content = GameFlyGame8.Title + "\n" + "  $" + GameFlyGame8.NormalPrice + "  \x2192  $" + GameFlyGame8.SalePrice;
            buttonGameFlyItem9.Content = GameFlyGame9.Title + "\n" + "  $" + GameFlyGame9.NormalPrice + "  \x2192  $" + GameFlyGame9.SalePrice;
            buttonGameFlyItem10.Content = GameFlyGame10.Title + "\n" + "  $" + GameFlyGame10.NormalPrice + "  \x2192  $" + GameFlyGame10.SalePrice;
        }

        public void PopulateGOG(string jsonResult)
        {
            // Create List of 10 Objects from GOG: 
            GOGGamesList = JsonConvert.DeserializeObject<List<GameObject>>(jsonResult);
            GameObject GOGGame1 = GOGGamesList[0];
            GameObject GOGGame2 = GOGGamesList[1];
            GameObject GOGGame3 = GOGGamesList[2];
            GameObject GOGGame4 = GOGGamesList[3];
            GameObject GOGGame5 = GOGGamesList[4];
            GameObject GOGGame6 = GOGGamesList[5];
            GameObject GOGGame7 = GOGGamesList[6];
            GameObject GOGGame8 = GOGGamesList[7];
            GameObject GOGGame9 = GOGGamesList[8];
            GameObject GOGGame10 = GOGGamesList[9];

            // Output data:
            buttonGOGItem1.Content = GOGGame1.Title + "\n" + "  $" + GOGGame1.NormalPrice + "  \x2192  $" + GOGGame1.SalePrice;
            buttonGOGItem2.Content = GOGGame2.Title + "\n" + "  $" + GOGGame2.NormalPrice + "  \x2192  $" + GOGGame2.SalePrice;
            buttonGOGItem3.Content = GOGGame3.Title + "\n" + "  $" + GOGGame3.NormalPrice + "  \x2192  $" + GOGGame3.SalePrice;
            buttonGOGItem4.Content = GOGGame4.Title + "\n" + "  $" + GOGGame4.NormalPrice + "  \x2192  $" + GOGGame4.SalePrice;
            buttonGOGItem5.Content = GOGGame5.Title + "\n" + "  $" + GOGGame5.NormalPrice + "  \x2192  $" + GOGGame5.SalePrice;
            buttonGOGItem6.Content = GOGGame6.Title + "\n" + "  $" + GOGGame6.NormalPrice + "  \x2192  $" + GOGGame6.SalePrice;
            buttonGOGItem7.Content = GOGGame7.Title + "\n" + "  $" + GOGGame7.NormalPrice + "  \x2192  $" + GOGGame7.SalePrice;
            buttonGOGItem8.Content = GOGGame8.Title + "\n" + "  $" + GOGGame8.NormalPrice + "  \x2192  $" + GOGGame8.SalePrice;
            buttonGOGItem9.Content = GOGGame9.Title + "\n" + "  $" + GOGGame9.NormalPrice + "  \x2192  $" + GOGGame9.SalePrice;
            buttonGOGItem10.Content = GOGGame10.Title + "\n" + "  $" + GOGGame10.NormalPrice + "  \x2192  $" + GOGGame10.SalePrice;
        }

        public void PopulateOrigin(string jsonResult)
        {
            // Create List of 10 Objects from Origin:
            OriginGamesList = JsonConvert.DeserializeObject<List<GameObject>>(jsonResult);
            GameObject OriginGame1 = OriginGamesList[0];
            GameObject OriginGame2 = OriginGamesList[1];
            GameObject OriginGame3 = OriginGamesList[2];
            GameObject OriginGame4 = OriginGamesList[3];
            GameObject OriginGame5 = OriginGamesList[4];
            GameObject OriginGame6 = OriginGamesList[5];
            GameObject OriginGame7 = OriginGamesList[6];
            GameObject OriginGame8 = OriginGamesList[7];
            GameObject OriginGame9 = OriginGamesList[8];
            GameObject OriginGame10 = OriginGamesList[9];

            // Output data:

            buttonOriginItem1.Content = OriginGame1.Title + "\n" + "  $" + OriginGame1.NormalPrice + "  \x2192  $" + OriginGame1.SalePrice;
            buttonOriginItem2.Content = OriginGame2.Title + "\n" + "  $" + OriginGame2.NormalPrice + "  \x2192  $" + OriginGame2.SalePrice;
            buttonOriginItem3.Content = OriginGame3.Title + "\n" + "  $" + OriginGame3.NormalPrice + "  \x2192  $" + OriginGame3.SalePrice;
            buttonOriginItem4.Content = OriginGame4.Title + "\n" + "  $" + OriginGame4.NormalPrice + "  \x2192  $" + OriginGame4.SalePrice;
            buttonOriginItem5.Content = OriginGame5.Title + "\n" + "  $" + OriginGame5.NormalPrice + "  \x2192  $" + OriginGame5.SalePrice;
            buttonOriginItem6.Content = OriginGame6.Title + "\n" + "  $" + OriginGame6.NormalPrice + "  \x2192  $" + OriginGame6.SalePrice;
            buttonOriginItem7.Content = OriginGame7.Title + "\n" + "  $" + OriginGame7.NormalPrice + "  \x2192  $" + OriginGame7.SalePrice;
            buttonOriginItem8.Content = OriginGame8.Title + "\n" + "  $" + OriginGame8.NormalPrice + "  \x2192  $" + OriginGame8.SalePrice;
            buttonOriginItem9.Content = OriginGame9.Title + "\n" + "  $" + OriginGame9.NormalPrice + "  \x2192  $" + OriginGame9.SalePrice;
            buttonOriginItem10.Content = OriginGame10.Title + "\n" + "  $" + OriginGame10.NormalPrice + "  \x2192  $" + OriginGame10.SalePrice;
        }

        public void PopulateGetGames(string jsonResult)
        {
            // Create List of 10 Objects from GetGames:
            GetGamesGamesList = JsonConvert.DeserializeObject<List<GameObject>>(jsonResult);
            GameObject GetGamesGame1 = GetGamesGamesList[0];
            GameObject GetGamesGame2 = GetGamesGamesList[1];
            GameObject GetGamesGame3 = GetGamesGamesList[2];
            GameObject GetGamesGame4 = GetGamesGamesList[3];
            GameObject GetGamesGame5 = GetGamesGamesList[4];
            GameObject GetGamesGame6 = GetGamesGamesList[5];
            GameObject GetGamesGame7 = GetGamesGamesList[6];
            GameObject GetGamesGame8 = GetGamesGamesList[7];
            GameObject GetGamesGame9 = GetGamesGamesList[8];
            GameObject GetGamesGame10 = GetGamesGamesList[9];

            // Output data:
            buttonGetGamesItem1.Content = GetGamesGame1.Title + "\n" + "  $" + GetGamesGame1.NormalPrice + "  \x2192  $" + GetGamesGame1.SalePrice;
            buttonGetGamesItem2.Content = GetGamesGame2.Title + "\n" + "  $" + GetGamesGame2.NormalPrice + "  \x2192  $" + GetGamesGame2.SalePrice;
            buttonGetGamesItem3.Content = GetGamesGame3.Title + "\n" + "  $" + GetGamesGame3.NormalPrice + "  \x2192  $" + GetGamesGame3.SalePrice;
            buttonGetGamesItem4.Content = GetGamesGame4.Title + "\n" + "  $" + GetGamesGame4.NormalPrice + "  \x2192  $" + GetGamesGame4.SalePrice;
            buttonGetGamesItem5.Content = GetGamesGame5.Title + "\n" + "  $" + GetGamesGame5.NormalPrice + "  \x2192  $" + GetGamesGame5.SalePrice;
            buttonGetGamesItem6.Content = GetGamesGame6.Title + "\n" + "  $" + GetGamesGame6.NormalPrice + "  \x2192  $" + GetGamesGame6.SalePrice;
            buttonGetGamesItem7.Content = GetGamesGame7.Title + "\n" + "  $" + GetGamesGame7.NormalPrice + "  \x2192  $" + GetGamesGame7.SalePrice;
            buttonGetGamesItem8.Content = GetGamesGame8.Title + "\n" + "  $" + GetGamesGame8.NormalPrice + "  \x2192  $" + GetGamesGame8.SalePrice;
            buttonGetGamesItem9.Content = GetGamesGame9.Title + "\n" + "  $" + GetGamesGame9.NormalPrice + "  \x2192  $" + GetGamesGame9.SalePrice;
            buttonGetGamesItem10.Content = GetGamesGame10.Title + "\n" + "  $" + GetGamesGame10.NormalPrice + "  \x2192  $" + GetGamesGame10.SalePrice;
        }

        private void clickAction(object sender, RoutedEventArgs e, List<GameObject> gameList, int pos)
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                // Grab URL of game:
                string url = gameList[pos].DealID;

                // Set URI:
                GlobalVariables.target = new Uri("http://www.cheapshark.com/redirect.php?dealID=" + url);

                // Navigate WebView to URI:
                WebViewPage.GlobalVariables.target = GlobalVariables.target;
                this.Frame.Navigate(typeof(WebViewPage));
            }
        }

        private void runButtonSteam(Button button, int index, object sender, RoutedEventArgs e){
            if (button.Content.Equals("Loading..."))
            {

            }
            else
            {
                clickAction(sender, e, SteamGamesList, index);
            }
        }

        private void runButtonGamersGate(Button button, int index, object sender, RoutedEventArgs e)
        {
            if (button.Content.Equals("Loading..."))
            {

            }
            else
            {
                clickAction(sender, e, GamersGateGamesList, index);
            }
        }

        private void runButtonGMG(Button button, int index, object sender, RoutedEventArgs e)
        {
            if (button.Content.Equals("Loading..."))
            {

            }
            else
            {
                clickAction(sender, e, GMGGamesList, index);
            }
        }

        private void runButtonAmazon(Button button, int index, object sender, RoutedEventArgs e)
        {
            if (button.Content.Equals("Loading..."))
            {

            }
            else
            {
                clickAction(sender, e, AmazonGamesList, index);
            }
        }

        private void runButtonGameStop(Button button, int index, object sender, RoutedEventArgs e)
        {
            if (button.Content.Equals("Loading..."))
            {

            }
            else
            {
                clickAction(sender, e, GameStopGamesList, index);
            }
        }

        private void runButtonGameFly(Button button, int index, object sender, RoutedEventArgs e)
        {
            if (button.Content.Equals("Loading..."))
            {

            }
            else
            {
                clickAction(sender, e, GameFlyGamesList, index);
            }
        }


        private void runButtonGOG(Button button, int index, object sender, RoutedEventArgs e)
        {
            if (button.Content.Equals("Loading..."))
            {

            }
            else
            {
                clickAction(sender, e, GOGGamesList, index);
            }
        }

        private void runButtonOrigin(Button button, int index, object sender, RoutedEventArgs e)
        {
            if (button.Content.Equals("Loading..."))
            {

            }
            else
            {
                clickAction(sender, e, OriginGamesList, index);
            }
        }


        private void runButtonGetGames(Button button, int index, object sender, RoutedEventArgs e)
        {
            if (button.Content.Equals("Loading..."))
            {

            }
            else
            {
                clickAction(sender, e, GetGamesGamesList, index);
            }
        }


        private void buttonSteamItem1_Click(object sender, RoutedEventArgs e)
        {
            runButtonSteam(buttonSteamItem1, 0, sender, e);
        }

        private void buttonSteamItem2_Click(object sender, RoutedEventArgs e)
        {
            runButtonSteam(buttonSteamItem2, 1, sender, e);
        }

        private void buttonSteamItem3_Click(object sender, RoutedEventArgs e)
        {
            runButtonSteam(buttonSteamItem3, 2, sender, e);
        }

        private void buttonSteamItem4_Click(object sender, RoutedEventArgs e)
        {
            runButtonSteam(buttonSteamItem4, 3, sender, e);
        }

        private void buttonSteamItem5_Click(object sender, RoutedEventArgs e)
        {
            runButtonSteam(buttonSteamItem5, 4, sender, e);
        }

        private void buttonSteamItem6_Click(object sender, RoutedEventArgs e)
        {
            runButtonSteam(buttonSteamItem6, 5, sender, e);
        }

        private void buttonSteamItem7_Click(object sender, RoutedEventArgs e)
        {
            runButtonSteam(buttonSteamItem7, 6, sender, e);
        }

        private void buttonSteamItem8_Click(object sender, RoutedEventArgs e)
        {
            runButtonSteam(buttonSteamItem8, 7, sender, e);
        }

        private void buttonSteamItem9_Click(object sender, RoutedEventArgs e)
        {
            runButtonSteam(buttonSteamItem9, 8, sender, e);
        }

        private void buttonSteamItem10_Click(object sender, RoutedEventArgs e)
        {
            runButtonSteam(buttonSteamItem10, 9, sender, e);
        }

        private void buttonGamersGateItem1_Click(object sender, RoutedEventArgs e)
        {
            runButtonGamersGate(buttonGamersGateItem1, 0, sender, e);
        }

        private void buttonGamersGateItem2_Click(object sender, RoutedEventArgs e)
        {
            runButtonGamersGate(buttonGamersGateItem2, 1, sender, e);
        }

        private void buttonGamersGateItem3_Click(object sender, RoutedEventArgs e)
        {
            runButtonGamersGate(buttonGamersGateItem3, 2, sender, e);
        }

        private void buttonGamersGateItem4_Click(object sender, RoutedEventArgs e)
        {
            runButtonGamersGate(buttonGamersGateItem4, 3, sender, e);
        }

        private void buttonGamersGateItem5_Click(object sender, RoutedEventArgs e)
        {
            runButtonGamersGate(buttonGamersGateItem5, 4, sender, e);
        }

        private void buttonGamersGateItem6_Click(object sender, RoutedEventArgs e)
        {
            runButtonGamersGate(buttonGamersGateItem6, 5, sender, e);
        }

        private void buttonGamersGateItem7_Click(object sender, RoutedEventArgs e)
        {
            runButtonGamersGate(buttonGamersGateItem7, 6, sender, e);
        }

        private void buttonGamersGateItem8_Click(object sender, RoutedEventArgs e)
        {
            runButtonGamersGate(buttonGamersGateItem8, 7, sender, e);
        }

        private void buttonGamersGateItem9_Click(object sender, RoutedEventArgs e)
        {
            runButtonGamersGate(buttonGamersGateItem9, 8, sender, e);
        }

        private void buttonGamersGateItem10_Click(object sender, RoutedEventArgs e)
        {
            runButtonGamersGate(buttonGamersGateItem10, 9, sender, e);
        }

        private void buttonGMGItem1_Click(object sender, RoutedEventArgs e)
        {
            runButtonGMG(buttonGMGItem1, 0, sender, e);
        }

        private void buttonGMGItem2_Click(object sender, RoutedEventArgs e)
        {
            runButtonGMG(buttonGMGItem2, 1, sender, e);
        }

        private void buttonGMGItem3_Click(object sender, RoutedEventArgs e)
        {
            runButtonGMG(buttonGMGItem3, 2, sender, e);
        }

        private void buttonGMGItem4_Click(object sender, RoutedEventArgs e)
        {
            runButtonGMG(buttonGMGItem4, 3, sender, e);
        }

        private void buttonGMGItem5_Click(object sender, RoutedEventArgs e)
        {
            runButtonGMG(buttonGMGItem5, 4, sender, e);
        }

        private void buttonGMGItem6_Click(object sender, RoutedEventArgs e)
        {
            runButtonGMG(buttonGMGItem6, 5, sender, e);
        }

        private void buttonGMGItem7_Click(object sender, RoutedEventArgs e)
        {
            runButtonGMG(buttonGMGItem7, 6, sender, e);
        }

        private void buttonGMGItem8_Click(object sender, RoutedEventArgs e)
        {
            runButtonGMG(buttonGMGItem8, 7, sender, e);
        }

        private void buttonGMGItem9_Click(object sender, RoutedEventArgs e)
        {
            runButtonGMG(buttonGMGItem9, 8, sender, e);
        }

        private void buttonGMGItem10_Click(object sender, RoutedEventArgs e)
        {
            runButtonGMG(buttonGMGItem10, 9, sender, e);
        }

        private void buttonAmazonItem1_Click(object sender, RoutedEventArgs e)
        {
            runButtonAmazon(buttonAmazonItem1, 0, sender, e);
        }

        private void buttonAmazonItem2_Click(object sender, RoutedEventArgs e)
        {
            runButtonAmazon(buttonAmazonItem2, 1, sender, e);
        }

        private void buttonAmazonItem3_Click(object sender, RoutedEventArgs e)
        {
            runButtonAmazon(buttonAmazonItem3, 2, sender, e);
        }

        private void buttonAmazonItem4_Click(object sender, RoutedEventArgs e)
        {
            runButtonAmazon(buttonAmazonItem4, 3, sender, e);
        }

        private void buttonAmazonItem5_Click(object sender, RoutedEventArgs e)
        {
            runButtonAmazon(buttonAmazonItem5, 4, sender, e);
        }

        private void buttonAmazonItem6_Click(object sender, RoutedEventArgs e)
        {
            runButtonAmazon(buttonAmazonItem6, 5, sender, e);
        }

        private void buttonAmazonItem7_Click(object sender, RoutedEventArgs e)
        {
            runButtonAmazon(buttonAmazonItem7, 6, sender, e);
        }

        private void buttonAmazonItem8_Click(object sender, RoutedEventArgs e)
        {
            runButtonAmazon(buttonAmazonItem8, 7, sender, e);
        }

        private void buttonAmazonItem9_Click(object sender, RoutedEventArgs e)
        {
            runButtonAmazon(buttonAmazonItem9, 8, sender, e);
        }

        private void buttonAmazonItem10_Click(object sender, RoutedEventArgs e)
        {
            runButtonAmazon(buttonAmazonItem10, 9, sender, e);
        }

        private void buttonGameStopItem1_Click(object sender, RoutedEventArgs e)
        {
            runButtonGameStop(buttonGameStopItem1, 0, sender, e);
        }

        private void buttonGameStopItem2_Click(object sender, RoutedEventArgs e)
        {
            runButtonGameStop(buttonGameStopItem2, 1, sender, e);
        }

        private void buttonGameStopItem3_Click(object sender, RoutedEventArgs e)
        {
            runButtonGameStop(buttonGameStopItem3, 2, sender, e);
        }

        private void buttonGameStopItem4_Click(object sender, RoutedEventArgs e)
        {
            runButtonGameStop(buttonGameStopItem4, 3, sender, e);
        }

        private void buttonGameStopItem5_Click(object sender, RoutedEventArgs e)
        {
            runButtonGameStop(buttonGameStopItem5, 4, sender, e);
        }

        private void buttonGameStopItem6_Click(object sender, RoutedEventArgs e)
        {
            runButtonGameStop(buttonGameStopItem6, 5, sender, e);
        }

        private void buttonGameStopItem7_Click(object sender, RoutedEventArgs e)
        {
            runButtonGameStop(buttonGameStopItem7, 6, sender, e);
        }

        private void buttonGameStopItem8_Click(object sender, RoutedEventArgs e)
        {
            runButtonGameStop(buttonGameStopItem8, 7, sender, e);
        }

        private void buttonGameStopItem9_Click(object sender, RoutedEventArgs e)
        {
            runButtonGameStop(buttonGameStopItem9, 8, sender, e);
        }

        private void buttonGameStopItem10_Click(object sender, RoutedEventArgs e)
        {
            runButtonGameStop(buttonGameStopItem10, 9, sender, e);
        }

        private void buttonGameFlyItem1_Click(object sender, RoutedEventArgs e)
        {
            runButtonGameFly(buttonGameFlyItem1, 0, sender, e);
        }

        private void buttonGameFlyItem2_Click(object sender, RoutedEventArgs e)
        {
            runButtonGameFly(buttonGameFlyItem2, 1, sender, e);
        }

        private void buttonGameFlyItem3_Click(object sender, RoutedEventArgs e)
        {
            runButtonGameFly(buttonGameFlyItem3, 2, sender, e);
        }

        private void buttonGameFlyItem4_Click(object sender, RoutedEventArgs e)
        {
            runButtonGameFly(buttonGameFlyItem4, 3, sender, e);
        }

        private void buttonGameFlyItem5_Click(object sender, RoutedEventArgs e)
        {
            runButtonGameFly(buttonGameFlyItem5, 4, sender, e);
        }

        private void buttonGameFlyItem6_Click(object sender, RoutedEventArgs e)
        {
            runButtonGameFly(buttonGameFlyItem6, 5, sender, e);
        }

        private void buttonGameFlyItem7_Click(object sender, RoutedEventArgs e)
        {
            runButtonGameFly(buttonGameFlyItem7, 6, sender, e);
        }

        private void buttonGameFlyItem8_Click(object sender, RoutedEventArgs e)
        {
            runButtonGameFly(buttonGameFlyItem8, 7, sender, e);
        }

        private void buttonGameFlyItem9_Click(object sender, RoutedEventArgs e)
        {
            runButtonGameFly(buttonGameFlyItem9, 8, sender, e);
        }

        private void buttonGameFlyItem10_Click(object sender, RoutedEventArgs e)
        {
            runButtonGameFly(buttonGameFlyItem10, 9, sender, e);
        }

        private void buttonGOGItem1_Click(object sender, RoutedEventArgs e)
        {
            runButtonGOG(buttonGOGItem1, 0, sender, e);
        }

        private void buttonGOGItem2_Click(object sender, RoutedEventArgs e)
        {
            runButtonGOG(buttonGOGItem2, 1, sender, e);
        }

        private void buttonGOGItem3_Click(object sender, RoutedEventArgs e)
        {
            runButtonGOG(buttonGOGItem3, 2, sender, e);
        }

        private void buttonGOGItem4_Click(object sender, RoutedEventArgs e)
        {
            runButtonGOG(buttonGOGItem4, 3, sender, e);
        }

        private void buttonGOGItem5_Click(object sender, RoutedEventArgs e)
        {
            runButtonGOG(buttonGOGItem5, 4, sender, e);
        }

        private void buttonGOGItem6_Click(object sender, RoutedEventArgs e)
        {
            runButtonGOG(buttonGOGItem6, 5, sender, e);
        }

        private void buttonGOGItem7_Click(object sender, RoutedEventArgs e)
        {
            runButtonGOG(buttonGOGItem7, 6, sender, e);
        }

        private void buttonGOGItem8_Click(object sender, RoutedEventArgs e)
        {
            runButtonGOG(buttonGOGItem8, 7, sender, e);
        }

        private void buttonGOGItem9_Click(object sender, RoutedEventArgs e)
        {
            runButtonGOG(buttonGOGItem9, 8, sender, e);
        }

        private void buttonGOGItem10_Click(object sender, RoutedEventArgs e)
        {
            runButtonGOG(buttonGOGItem10, 9, sender, e);
        }

        private void buttonOriginItem1_Click(object sender, RoutedEventArgs e)
        {
            runButtonOrigin(buttonOriginItem1, 0, sender, e);
        }

        private void buttonOriginItem2_Click(object sender, RoutedEventArgs e)
        {
            runButtonOrigin(buttonOriginItem2, 1, sender, e);
        }

        private void buttonOriginItem3_Click(object sender, RoutedEventArgs e)
        {
            runButtonOrigin(buttonOriginItem3, 2, sender, e);
        }

        private void buttonOriginItem4_Click(object sender, RoutedEventArgs e)
        {
            runButtonOrigin(buttonOriginItem4, 3, sender, e);
        }

        private void buttonOriginItem5_Click(object sender, RoutedEventArgs e)
        {
            runButtonOrigin(buttonOriginItem5, 4, sender, e);
        }

        private void buttonOriginItem6_Click(object sender, RoutedEventArgs e)
        {
            runButtonOrigin(buttonOriginItem6, 5, sender, e);
        }

        private void buttonOriginItem7_Click(object sender, RoutedEventArgs e)
        {
            runButtonOrigin(buttonOriginItem7, 6, sender, e);
        }

        private void buttonOriginItem8_Click(object sender, RoutedEventArgs e)
        {
            runButtonOrigin(buttonOriginItem8, 7, sender, e);
        }

        private void buttonOriginItem9_Click(object sender, RoutedEventArgs e)
        {
            runButtonOrigin(buttonOriginItem9, 8, sender, e);
        }

        private void buttonOriginItem10_Click(object sender, RoutedEventArgs e)
        {
             runButtonOrigin(buttonOriginItem10, 9, sender, e);
        }

        private void buttonGetGamesItem1_Click(object sender, RoutedEventArgs e)
        {
            runButtonGetGames(buttonGetGamesItem1, 0, sender, e);
        }

        private void buttonGetGamesItem2_Click(object sender, RoutedEventArgs e)
        {
            runButtonGetGames(buttonGetGamesItem2, 1, sender, e);
        }

        private void buttonGetGamesItem3_Click(object sender, RoutedEventArgs e)
        {
            runButtonGetGames(buttonGetGamesItem3, 2, sender, e);
        }

        private void buttonGetGamesItem4_Click(object sender, RoutedEventArgs e)
        {
            runButtonGetGames(buttonGetGamesItem4, 3, sender, e);
        }

        private void buttonGetGamesItem5_Click(object sender, RoutedEventArgs e)
        {
            runButtonGetGames(buttonGetGamesItem5, 4, sender, e);
        }

        private void buttonGetGamesItem6_Click(object sender, RoutedEventArgs e)
        {
            runButtonGetGames(buttonGetGamesItem6, 5, sender, e);
        }

        private void buttonGetGamesItem7_Click(object sender, RoutedEventArgs e)
        {
            runButtonGetGames(buttonGetGamesItem7, 6, sender, e);
        }

        private void buttonGetGamesItem8_Click(object sender, RoutedEventArgs e)
        {
            runButtonGetGames(buttonGetGamesItem8, 7, sender, e);
        }

        private void buttonGetGamesItem9_Click(object sender, RoutedEventArgs e)
        {
            runButtonGetGames(buttonGetGamesItem9, 8, sender, e);
        }

        private void buttonGetGamesItem10_Click(object sender, RoutedEventArgs e)
        {
            runButtonGetGames(buttonGetGamesItem10, 9, sender, e);
        }

        private void buttonBrowse_Click(object sender, RoutedEventArgs e)
        {
            BrowsePage.GlobalVariables.storeOnLoad = -1;
            this.Frame.Navigate(typeof(BrowsePage));
        }

        private void buttonBrowseSteam_Click(object sender, RoutedEventArgs e)
        {
            BrowsePage.GlobalVariables.storeOnLoad = 0;
            this.Frame.Navigate(typeof(BrowsePage));
        }

        private void buttonBrowseGamersGate_Click(object sender, RoutedEventArgs e)
        {
            BrowsePage.GlobalVariables.storeOnLoad = 1;
            this.Frame.Navigate(typeof(BrowsePage));
        }

        private void buttonBrowseGMG_Click(object sender, RoutedEventArgs e)
        {
            BrowsePage.GlobalVariables.storeOnLoad = 2;
            this.Frame.Navigate(typeof(BrowsePage));
        }

        private void buttonBrowseAmazon_Click(object sender, RoutedEventArgs e)
        {
            BrowsePage.GlobalVariables.storeOnLoad = 3;
            this.Frame.Navigate(typeof(BrowsePage));
        }

        private void buttonBrowseGameStop_Click(object sender, RoutedEventArgs e)
        {
            BrowsePage.GlobalVariables.storeOnLoad = 4;
            this.Frame.Navigate(typeof(BrowsePage));
        }

        private void buttonBrowseGameFly_Click(object sender, RoutedEventArgs e)
        {
            BrowsePage.GlobalVariables.storeOnLoad = 5;
            this.Frame.Navigate(typeof(BrowsePage));
        }

        private void buttonBrowseGOG_Click(object sender, RoutedEventArgs e)
        {
            BrowsePage.GlobalVariables.storeOnLoad = 6;
            this.Frame.Navigate(typeof(BrowsePage));
        }

        private void buttonBrowseOrigin_Click(object sender, RoutedEventArgs e)
        {
            BrowsePage.GlobalVariables.storeOnLoad = 7;
            this.Frame.Navigate(typeof(BrowsePage));
        }

        private void buttonBrowseGetGames_Click(object sender, RoutedEventArgs e)
        {
            BrowsePage.GlobalVariables.storeOnLoad = 8;
            this.Frame.Navigate(typeof(BrowsePage));
        }

        private void buttonBrowseShinyLoot_Click(object sender, RoutedEventArgs e)
        {
            BrowsePage.GlobalVariables.storeOnLoad = 9;
            this.Frame.Navigate(typeof(BrowsePage));
        }

        private void buttonBrowseHumble_Click(object sender, RoutedEventArgs e)
        {
            BrowsePage.GlobalVariables.storeOnLoad = 10;
            this.Frame.Navigate(typeof(BrowsePage));
        }

        private void buttonBrowseDesura_Click(object sender, RoutedEventArgs e)
        {
            BrowsePage.GlobalVariables.storeOnLoad = 11;
            this.Frame.Navigate(typeof(BrowsePage));
        }
    }
}
