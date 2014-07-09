using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using System.Net.Http;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace CheapSharkApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BrowsePage : Page
    {
        Uri targetUri { get; set; }
        List<GameObject> GamesList { get; set; }
        Boolean goToNextPage = true;
        String searchURL = null;
        int counter = 0;
        int pageSize = 60;
        String sortBy = "Deal%20Rating";
        int order = 0;
        String filterTitle = "";
        int position = 0;
        String success = null;

        public class GlobalVariables
        {
            public static int storeOnLoad = -1;
        } 

        public BrowsePage()
        {
            this.InitializeComponent();
            if (GlobalVariables.storeOnLoad == -1) {
                ReadDataFromWeb("http://www.cheapshark.com/pager.php?storeID=0,1,2,3,4,5,6,7,8,9,10,11&sortBy=Deal%20Rating&desc=0&title=&AAA=0&steamworks=0&onSale=0&lowerPrice=0&upperPrice=50&pageSize=" + pageSize + "&page=0");
            }
            else{
                ReadDataFromWeb("http://www.cheapshark.com/pager.php?storeID=" + GlobalVariables.storeOnLoad.ToString() + "&sortBy=Deal%20Rating&desc=0&title=&AAA=0&steamworks=0&onSale=0&lowerPrice=0&upperPrice=50&pageSize=" + pageSize + "&page=0");

                switch (GlobalVariables.storeOnLoad)
                {
                    case 0:
                        manageChecks(true, false, false, false, false, false, false, false, false, false, false, false);
                        break;
                    case 1:
                        manageChecks(false, true, false, false, false, false, false, false, false, false, false, false);
                        break;
                    case 2:
                        manageChecks(false, false, true, false, false, false, false, false, false, false, false, false);
                        break;
                    case 3:
                        manageChecks(false, false, false, true, false, false, false, false, false, false, false, false);
                        break;
                    case 4:
                        manageChecks(false, false, false, false, true, false, false, false, false, false, false, false);
                        break;
                    case 5:
                        manageChecks(false, false, false, false, false, true, false, false, false, false, false, false);
                        break;
                    case 6:
                        manageChecks(false, false, false, false, false, false, true, false, false, false, false, false);
                        break;
                    case 7:
                        manageChecks(false, false, false, false, false, false, false, true, false, false, false, false);
                        break;
                    case 8:
                        manageChecks(false, false, false, false, false, false, false, false, true, false, false, false);
                        break;
                    case 9:
                        manageChecks(false, false, false, false, false, false, false, false, false, true, false, false);
                        break;
                    case 10:
                        manageChecks(false, false, false, false, false, false, false, false, false, false, true, false);
                        break;
                    case 11:
                        manageChecks(false, false, false, false, false, false, false, false, false, false, false, true);
                        break;
                    default:
                        break;
                }
            }
            counter++;
        }

        public void manageChecks(Boolean a, Boolean b, Boolean c, Boolean d, Boolean e, Boolean f, Boolean g, Boolean h, Boolean i, Boolean j, Boolean k, Boolean l)
        {
            if (a)
            {
                checkSteam.IsChecked = true;
            }
            else
            {
                checkSteam.IsChecked = false;
            }

            if (b)
            {
                checkGamersGate.IsChecked = true;
            }
            else
            {
                checkGamersGate.IsChecked = false;
            }


            if (c)
            {
                checkGMG.IsChecked = true;
            }
            else
            {
                checkGMG.IsChecked = false;
            }

            if (d)
            {
                checkAmazon.IsChecked = true;
            }
            else
            {
                checkAmazon.IsChecked = false;
            }

            if (e)
            {
                checkGameStop.IsChecked = true;
            }
            else
            {
                checkGameStop.IsChecked = false;
            }


            if (f)
            {
                checkGameFly.IsChecked = true;
            }
            else
            {
                checkGameFly.IsChecked = false;
            }

            if (g)
            {
                checkGOG.IsChecked = true;
            }
            else
            {
                checkGOG.IsChecked = false;
            }

            if (h)
            {
                checkOrigin.IsChecked = true;
            }
            else
            {
                checkOrigin.IsChecked = false;
            }

            if (i)
            {
                checkGetGames.IsChecked = true;
            }
            else
            {
                checkGetGames.IsChecked = false;
            }

            if (j)
            {
                checkShinyLoot.IsChecked = true;
            }
            else
            {
                checkShinyLoot.IsChecked = false;
            }

            if (k)
            {
                checkHumble.IsChecked = true;
            }
            else
            {
                checkHumble.IsChecked = false;
            }

            if (l)
            {
                checkDesura.IsChecked = true;
            }
            else
            {
                checkDesura.IsChecked = false;
            }
        }

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

        async public void ReadDataFromWeb(string searchURL)
        {
            BrowseStackPanel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            // Check if there is an internet connection:
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                    //Increment page:
                    searchURL += counter.ToString();

                    // Create HttpClient:
                    var client = new Windows.Web.Http.HttpClient();

                    // Grab JSON string:
                    var responseSearch = await client.GetAsync(new Uri(searchURL));
                    string resultSearch = await responseSearch.Content.ReadAsStringAsync();

                    if (resultSearch.Equals("[]"))
                    {
                        buttonNextPage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        goToNextPage = false;
                    }
                    else
                    {
                        buttonNextPage.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        PopulateTable(resultSearch);
                    }

                    if (goToNextPage)
                    {
                        buttonNextPage.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    }

                BrowseStackPanel.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }


        async public void SetThumb(string gameThumb, BitmapImage bitmap)
        {
            // Check if there is an internet connection:
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {

                var httpClient = new HttpClient();
                var contentBytes = await httpClient.GetByteArrayAsync(gameThumb);
                var ims = new InMemoryRandomAccessStream();
                var dataWriter = new DataWriter(ims);
                dataWriter.WriteBytes(contentBytes);
                await dataWriter.StoreAsync();
                ims.Seek(0);

                bitmap.SetSource(ims);

            }
        }

        public void PopulateTable(string jsonResult)
        {
            // Check if there is an internet connection:
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                GamesList = JsonConvert.DeserializeObject<List<GameObject>>(jsonResult);

                if (GamesList.Count < pageSize)
                {
                    buttonNextPage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    goToNextPage = false;

                    List<BitmapImage> listBitmaps = new List<BitmapImage>();
                    PopulatePageSection(listBitmaps);
                }
                else
                {
                    List<BitmapImage> listBitmaps = new List<BitmapImage>();
                    PopulatePageSection(listBitmaps);
                }
            }     
        }


        public void PopulatePageSection(List<BitmapImage> listBitmaps)
        {

            for (int i = 1; i <= pageSize; i++)
            {
                Button tbTitle = this.FindName("row" + (i).ToString() + "Title") as Button;
                Button tbMail = this.FindName("row" + (i).ToString() + "MailButton") as Button;
                TextBlock tbSavings = this.FindName("row" + (i).ToString() + "Savings") as TextBlock;
                TextBlock tbPrice = this.FindName("row" + (i).ToString() + "Price") as TextBlock;
                Image tbImage = this.FindName("row" + (i).ToString() + "Image") as Image;
                Image tbStore = this.FindName("row" + (i).ToString() + "StoreImage") as Image;

                noticeLoading.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                tbTitle.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                tbMail.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                tbSavings.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                tbPrice.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                tbImage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                tbStore.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }

            for (int i = 0; i < GamesList.Count; i++)
            {
                int j = i + 1;

                Button tbTitle = this.FindName("row" + (j).ToString() + "Title") as Button;
                Button tbMail = this.FindName("row" + (j).ToString() + "MailButton") as Button;
                TextBlock tbSavings = this.FindName("row" + (j).ToString() + "Savings") as TextBlock;
                TextBlock tbPrice = this.FindName("row" + (j).ToString() + "Price") as TextBlock;
                Image tbImage = this.FindName("row" + (j).ToString() + "Image") as Image;
                Image tbStore = this.FindName("row" + (j).ToString() + "StoreImage") as Image;

                string gameTitle = GamesList[i].Title;
                int gameSavings = (int)Convert.ToDouble(GamesList[i].Savings);
                string gameStore = GamesList[i].StoreID;
                string gameNormalPrice = GamesList[i].NormalPrice;
                string gameSalesPrice = GamesList[i].SalePrice;
                string gameThumb = GamesList[i].Thumb;

                tbTitle.Content = gameTitle;
                tbSavings.Text = gameSavings.ToString() + "%";
                tbPrice.Text = "$" + gameNormalPrice + "  \x2192  " + "$" + gameSalesPrice;
                BitmapImage bitmap = new BitmapImage();
                SetThumb(gameThumb, bitmap);
                listBitmaps.Add(bitmap);
                tbImage.Source = listBitmaps[i];

                if (gameStore == "1")
                {
                    var imageUri = new Uri("ms-appx:///Assets/0.png");
                    tbStore.Source = new BitmapImage(imageUri);
                }
                else if (gameStore == "2")
                {
                    var imageUri = new Uri("ms-appx:///Assets/1.png");
                    tbStore.Source = new BitmapImage(imageUri);
                }
                else if (gameStore == "3")
                {
                    var imageUri = new Uri("ms-appx:///Assets/2.png");
                    tbStore.Source = new BitmapImage(imageUri); 
                }
                else if (gameStore == "4")
                {
                    var imageUri = new Uri("ms-appx:///Assets/3.png");
                    tbStore.Source = new BitmapImage(imageUri);
                }
                else if (gameStore == "5")
                {
                    var imageUri = new Uri("ms-appx:///Assets/4.png");
                    tbStore.Source = new BitmapImage(imageUri);
                }
                else if (gameStore == "6")
                {
                    var imageUri = new Uri("ms-appx:///Assets/5.png");
                    tbStore.Source = new BitmapImage(imageUri);
                }
                else if (gameStore == "7")
                {
                    var imageUri = new Uri("ms-appx:///Assets/6.png");
                    tbStore.Source = new BitmapImage(imageUri);
                }
                else if (gameStore == "8")
                {
                    var imageUri = new Uri("ms-appx:///Assets/7.png");
                    tbStore.Source = new BitmapImage(imageUri);
                }
                else if (gameStore == "9")
                {
                    var imageUri = new Uri("ms-appx:///Assets/8.png");
                    tbStore.Source = new BitmapImage(imageUri);
                }
                else if (gameStore == "10")
                {
                    var imageUri = new Uri("ms-appx:///Assets/9.png");
                    tbStore.Source = new BitmapImage(imageUri);
                }
                else if (gameStore == "11")
                {
                    var imageUri = new Uri("ms-appx:///Assets/10.png");
                    tbStore.Source = new BitmapImage(imageUri);
                }
                else if (gameStore == "12")
                {
                    var imageUri = new Uri("ms-appx:///Assets/11.png");
                    tbStore.Source = new BitmapImage(imageUri);
                }

                noticeLoading.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                tbTitle.Visibility = Windows.UI.Xaml.Visibility.Visible;
                tbMail.Visibility = Windows.UI.Xaml.Visibility.Visible;
                tbSavings.Visibility = Windows.UI.Xaml.Visibility.Visible;
                tbPrice.Visibility = Windows.UI.Xaml.Visibility.Visible;
                tbImage.Visibility = Windows.UI.Xaml.Visibility.Visible;
                tbStore.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        private void createURL()
        {
            // Check if there is an internet connection:
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                searchURL = "http://www.cheapshark.com/pager.php?storeID=";
                Boolean isFirst = true;

                if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {

                    if (counter == 0)
                    {
                        buttonPreviousPage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    }
                    else
                    {
                        buttonPreviousPage.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    }

                    if (checkSteam.IsChecked == true)
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                            searchURL += "0";
                        }
                        else
                        {
                            searchURL += ",0";
                        }
                    }

                    if (checkGamersGate.IsChecked == true)
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                            searchURL += "1";
                        }
                        else
                        {
                            searchURL += ",1";
                        }
                    }

                    if (checkGMG.IsChecked == true)
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                            searchURL += "2";
                        }
                        else
                        {
                            searchURL += ",2";
                        }
                    }

                    if (checkAmazon.IsChecked == true)
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                            searchURL += "3";
                        }
                        else
                        {
                            searchURL += ",3";
                        }
                    }

                    if (checkGameStop.IsChecked == true)
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                            searchURL += "4";
                        }
                        else
                        {
                            searchURL += ",4";
                        }
                    }

                    if (checkGameFly.IsChecked == true)
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                            searchURL += "5";
                        }
                        else
                        {
                            searchURL += ",5";
                        }
                    }

                    if (checkGOG.IsChecked == true)
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                            searchURL += "6";
                        }
                        else
                        {
                            searchURL += ",6";
                        }
                    }

                    if (checkOrigin.IsChecked == true)
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                            searchURL += "7";
                        }
                        else
                        {
                            searchURL += ",7";
                        }
                    }

                    if (checkGetGames.IsChecked == true)
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                            searchURL += "8";
                        }
                        else
                        {
                            searchURL += ",8";
                        }
                    }

                    if (checkShinyLoot.IsChecked == true)
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                            searchURL += "9";
                        }
                        else
                        {
                            searchURL += ",9";
                        }
                    }

                    if (checkHumble.IsChecked == true)
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                            searchURL += "10";
                        }
                        else
                        {
                            searchURL += ",10";
                        }
                    }

                    if (checkDesura.IsChecked == true)
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                            searchURL += "11";
                        }
                        else
                        {
                            searchURL += ",11";
                        }
                    }

                    int onSale = 0;
                    if (checkOnSale.IsChecked == true)
                    {
                        onSale = 1;
                    }

                    int AAA = 0;
                    if (checkAAA.IsChecked == true)
                    {
                        AAA = 1;
                    }

                    int steamworks = 0;
                    if (checkSteamWorks.IsChecked == true)
                    {
                        steamworks = 1;
                    }

                    if (isFirst)
                    {
                        warningCheckOneBox.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        noticeLoading.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        BrowseStackPanel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    }
                    else
                    {
                        //Set layout visibilities:
                        BrowseStackPanel.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        warningCheckOneBox.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        noticeLoading.Visibility = Windows.UI.Xaml.Visibility.Visible;

                        //Get filter string:
                        filterTitle = filterTextBox.Text.ToString();
                        if(!filterTitle.Equals("Filter...") || !filterTitle.Equals("")){
                            searchURL += "&title=" + filterTitle;
                        }

                        //Finish searchURL:
                        searchURL += "&sortBy=" + sortBy + "&desc=" + order + "&AAA=" + AAA + "&steamworks=" + steamworks + "&onSale=" + onSale + "&lowerPrice=0&upperPrice=50&pageSize=" + pageSize + "&page=";

                        //ReadDataFromWeb() pulls the JSON string into an object
                        //Will also determine page for searchURL:
                        ReadDataFromWeb(searchURL);
                    }
                }
            }
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void buttonCheckAll_Click(object sender, RoutedEventArgs e)
        {
            counter = 0;

            manageChecks(true, true, true, true, true, true, true, true, true, true, true, true);
        }

        private void buttonCheckNone_Click(object sender, RoutedEventArgs e)
        {
            counter = 0;

            manageChecks(false, false, false, false, false, false, false, false, false, false, false, false);
        }

        private void checkActivate_Check(object sender, RoutedEventArgs e)
        {
            counter = 0;
            createURL();
            counter++;
        }

        private void nextPageClick(object sender, RoutedEventArgs e)
        {
            String filterTitleTemp = filterTitle;
            filterTitle = filterTextBox.Text.ToString();
            if (!filterTitleTemp.Equals(filterTitle))
            {
                counter = 0;
            }
            createURL();
            counter++;
        }

        private void previousPageClick(object sender, RoutedEventArgs e)
        {
            String filterTitleTemp = filterTitle;
            filterTitle = filterTextBox.Text.ToString();
            if (!filterTitleTemp.Equals(filterTitle))
            {
                counter = 0;
                createURL();
                counter++;
            }
            else
            {
                counter--;
                counter--;
                createURL();
                counter++;
            }
        }

        private void clickAction(object sender, RoutedEventArgs e, int pos)
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                // Grab URL of game:
                string url = GamesList[pos].DealID;

                // Set URI:
                targetUri = new Uri("http://www.cheapshark.com/redirect.php?dealID=" + url);

                // Navigate WebView to URI:
                WebViewPage.GlobalVariables.target = targetUri;
                this.Frame.Navigate(typeof(WebViewPage));
            }
        }

        private void mailNotificationAction(object sender, RoutedEventArgs e, int pos)
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                pos++;
                TextBlock mailNotificationTextBlock = this.FindName("mailNotificationTextBlock" + pos.ToString()) as TextBlock;
                pos--;
                mailNotificationTextBlock.Visibility = Windows.UI.Xaml.Visibility.Visible;
                mailNotificationTextBlock.Text = "To get price alerts via email for " + GamesList[pos].Title + ", fill out the information below.";
            }
        }

        private void mailNotificationSubmit(object sender, RoutedEventArgs e)
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                // Grab dealID of game:
                string dealID = GamesList[position].GameID;

                TextBox tbEmail = this.FindName("emailTextBox" + (position).ToString()) as TextBox;
                String email = tbEmail.Text.ToString();

                TextBox tbPrice = this.FindName("priceTextBox" + (position).ToString()) as TextBox;
                String price = tbPrice.Text.ToString();

                // Set URI:
                targetUri = new Uri("http://www.cheapshark.com/createPriceAlert.php?email=" + email + "&gameID=" + dealID + "&price=" + price);

                // Grab JSON string:
                submitMailNotification(targetUri, position);
            }
        }

        async public void submitMailNotification(Uri targetUri, int position)
        {
            // Create HttpClient:
            var client = new Windows.Web.Http.HttpClient();

            // Grab string (success/fail):
            var responseSearch = await client.GetAsync(new Uri(targetUri.ToString()));
            success = await responseSearch.Content.ReadAsStringAsync();

            if (success.Equals("true"))
            {
                
            }
            else
            {

            }
        }

        private void linkGame1_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 0);
        }

        private void mailNotificationGame1_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 0);
            position = 0;
        }

        private void linkGame2_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 1);
        }

        private void mailNotificationGame2_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 1);
            position = 1;
        }

        private void linkGame3_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 2);
        }

        private void mailNotificationGame3_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 2);
            position = 2;
        }

        private void linkGame4_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 3);
        }

        private void mailNotificationGame4_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 3);
            position = 3;
        }

        private void linkGame5_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 4);
        }

        private void mailNotificationGame5_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 4);
            position = 4;
        }

        private void linkGame6_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 5);
        }

        private void mailNotificationGame6_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 5);
            position = 5;
        }

        private void linkGame7_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 6);
        }

        private void mailNotificationGame7_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 6);
            position = 6;
        }

        private void linkGame8_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 7);
        }

        private void mailNotificationGame8_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 7);
            position = 7;
        }

        private void linkGame9_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 8);
        }

        private void mailNotificationGame9_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 8);
            position = 8;
        }

        private void linkGame10_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 9);
        }

        private void mailNotificationGame10_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 9);
            position = 9;
        }

        private void linkGame11_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 10);
        }

        private void mailNotificationGame11_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 10);
            position = 10;
        }

        private void linkGame12_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 11);
        }

        private void mailNotificationGame12_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 11);
            position = 11;
        }

        private void linkGame13_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 12);
        }

        private void mailNotificationGame13_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 12);
            position = 12;
        }

        private void linkGame14_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 13);
        }

        private void mailNotificationGame14_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 13);
            position = 13;
        }

        private void linkGame15_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 14);
        }

        private void mailNotificationGame15_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 14);
            position = 14;
        }

        private void linkGame16_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 15);
        }

        private void mailNotificationGame16_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 15);
            position = 15;
        }

        private void linkGame17_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 16);
        }

        private void mailNotificationGame17_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 16);
            position = 16;
        }

        private void linkGame18_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 17);
        }

        private void mailNotificationGame18_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 17);
            position = 17;
        }

        private void linkGame19_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 18);
        }

        private void mailNotificationGame19_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 18);
            position = 18;
        }

        private void linkGame20_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 19);
        }

        private void mailNotificationGame20_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 19);
            position = 19;
        }

        private void linkGame21_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 20);
        }

        private void mailNotificationGame21_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 20);
            position = 20;
        }

        private void linkGame22_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 21);
        }

        private void mailNotificationGame22_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 21);
            position = 21;
        }

        private void linkGame23_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 22);
        }

        private void mailNotificationGame23_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 22);
            position = 22;
        }

        private void linkGame24_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 23);
        }

        private void mailNotificationGame24_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 23);
            position = 23;
        }

        private void linkGame25_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 24);
        }

        private void mailNotificationGame25_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 24);
            position = 24;
        }

        private void linkGame26_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 25);
        }

        private void mailNotificationGame26_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 25);
            position = 25;
        }

        private void linkGame27_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 26);
        }

        private void mailNotificationGame27_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 26);
            position = 26;
        }

        private void linkGame28_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 27);
        }

        private void mailNotificationGame28_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 27);
            position = 27;
        }

        private void linkGame29_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 28);
        }

        private void mailNotificationGame29_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 28);
            position = 28;
        }

        private void linkGame30_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 29);
        }

        private void mailNotificationGame30_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 29);
            position = 29;
        }

        private void linkGame31_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 30);
        }

        private void mailNotificationGame31_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 30);
            position = 30;
        }

        private void linkGame32_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 31);
        }

        private void mailNotificationGame32_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 31);
            position = 31;
        }

        private void linkGame33_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 32);
        }

        private void mailNotificationGame33_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 32);
            position = 32;
        }

        private void linkGame34_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 33);
        }

        private void mailNotificationGame34_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 33);
            position = 33;
        }

        private void linkGame35_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 34);
        }

        private void mailNotificationGame35_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 34);
            position = 34;
        }

        private void linkGame36_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 35);
        }

        private void mailNotificationGame36_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 35);
            position = 35;
        }

        private void linkGame37_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 36);
        }

        private void mailNotificationGame37_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 36);
            position = 36;
        }

        private void linkGame38_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 37);
        }

        private void mailNotificationGame38_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 37);
            position = 37;
        }

        private void linkGame39_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 38);
        }

        private void mailNotificationGame39_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 38);
            position = 38;
        }

        private void linkGame40_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 39);
        }

        private void mailNotificationGame40_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 39);
            position = 39;
        }

        private void linkGame41_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 40);
        }

        private void mailNotificationGame41_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 40);
            position = 40;
        }

        private void linkGame42_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 41);
        }

        private void mailNotificationGame42_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 41);
            position = 41;
        }

        private void linkGame43_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 42);
        }

        private void mailNotificationGame43_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 42);
            position = 42;
        }

        private void linkGame44_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 43);
        }

        private void mailNotificationGame44_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 43);
            position = 43;
        }

        private void linkGame45_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 44);
        }

        private void mailNotificationGame45_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 44);
            position = 44;
        }

        private void linkGame46_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 45);
        }

        private void mailNotificationGame46_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 45);
            position = 45;
        }

        private void linkGame47_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 46);
        }

        private void mailNotificationGame47_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 46);
            position = 46;
        }

        private void linkGame48_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 47);
        }

        private void mailNotificationGame48_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 47);
            position = 47;
        }

        private void linkGame49_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 48);
        }

        private void mailNotificationGame49_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 48);
            position = 48;
        }

        private void linkGame50_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 49);
        }

        private void mailNotificationGame50_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 49);
            position = 49;
        }

        private void linkGame51_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 50);
        }

        private void mailNotificationGame51_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 50);
            position = 50;
        }

        private void linkGame52_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 51);
        }

        private void mailNotificationGame52_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 51);
            position = 51;
        }

        private void linkGame53_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 52);
        }

        private void mailNotificationGame53_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 52);
            position = 52;
        }

        private void linkGame54_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 53);
        }

        private void mailNotificationGame54_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 53);
            position = 53;
        }

        private void linkGame55_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 54);
        }

        private void mailNotificationGame55_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 54);
            position = 54;
        }

        private void linkGame56_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 55);
        }

        private void mailNotificationGame56_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 55);
            position = 55;
        }

        private void linkGame57_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 56);
        }

        private void mailNotificationGame57_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 56);
            position = 56;
        }

        private void linkGame58_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 57);
        }

        private void mailNotificationGame58_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 57);
            position = 57;
        }

        private void linkGame59_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 58);
        }

        private void mailNotificationGame59_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 58);
            position = 58;
        }

        private void linkGame60_Click(object sender, RoutedEventArgs e)
        {
            clickAction(sender, e, 59);
        }

        private void mailNotificationGame60_Click(object sender, RoutedEventArgs e)
        {
            mailNotificationAction(sender, e, 59);
            position = 59;
        }

        private void sortByStore_Click(object sender, RoutedEventArgs e)
        {
            setTitlesDefault();

            if (sortBy.Equals("Store"))
            {

                order = 1;
                buttonStoreSort.Content = "Store ▲";
            }
            else
            {
                sortBy = "Store";
                order = 0;
                buttonStoreSort.Content = "Store ▼";
            }

            createURL();
        }

        private void sortBySavings_Click(object sender, RoutedEventArgs e)
        {
            setTitlesDefault();

            if (sortBy.Equals("Savings"))
            {
                order = 1;
                buttonStoreSort.Content = "Savings ▲";
            }
            else
            {
                sortBy = "Savings";
                order = 0;
                buttonStoreSort.Content = "Savings ▼";
            }

            createURL();
        }

        private void sortByPrice_Click(object sender, RoutedEventArgs e)
        {
            setTitlesDefault();

            if (sortBy.Equals("Price"))
            {
                order = 1;
                buttonPriceSort.Content = "Price ▲";
            }
            else
            {
                sortBy = "Price";
                order = 0;
                buttonPriceSort.Content = "Price ▼";
            }

            createURL();
        }

        private void sortByTitle_Click(object sender, RoutedEventArgs e)
        {
            if (sortBy.Equals("Title"))
            {
                order = 1;
                buttonTitleSort.Content = "Title ▲";
            }
            else
            {
                sortBy = "Title";
                order = 0;
                buttonTitleSort.Content = "Title ▼";
            }

            createURL();
        }

        private void setTitlesDefault()
        {
            buttonStoreSort.Content = "Store";
            buttonSavingsSort.Content = "Savings";
            buttonPriceSort.Content = "Price";
            buttonTitleSort.Content = "Title";
        }

    }
}
