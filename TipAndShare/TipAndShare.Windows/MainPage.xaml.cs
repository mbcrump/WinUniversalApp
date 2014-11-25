using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TipAndShare
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            SendLiveTileUpdate();
        }

        private void SendLiveTileUpdate()
        {
            // Create a live update for a square tile
            XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquarePeekImageAndText02);

            XmlNodeList tileTextAttributes = tileXml.GetElementsByTagName("text");
            tileTextAttributes[0].InnerText = "Tip Calc";
            tileTextAttributes[1].InnerText = "A Good Tip is 20%";   // this will grab the latest review text

            //XmlNodeList tileImageAttributes = tileXml.GetElementsByTagName("image");
            //((XmlElement)tileImageAttributes[0]).SetAttribute("src", "ms-appx:///Assets/tile-150.png");
            //((XmlElement)tileImageAttributes[0]).SetAttribute("alt", "Contoso Food & Dining logo");

            var sqTileBinding = (XmlElement)tileXml.GetElementsByTagName("binding").Item(0);
            sqTileBinding.SetAttribute("branding", "none"); // removes logo from lower left-hand corner of tile

            // Create a live update for a wide tile
            XmlDocument wideTileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWideSmallImageAndText04);

            XmlNodeList wideTileTextAttributes = wideTileXml.GetElementsByTagName("text");
            wideTileTextAttributes[0].AppendChild(wideTileXml.CreateTextNode("Tip Calculator"));
            wideTileTextAttributes[1].AppendChild(wideTileXml.CreateTextNode("A Good Tip is 20%"));

            XmlNodeList wideTileImageAttributes = wideTileXml.GetElementsByTagName("image");
            ((XmlElement)wideTileImageAttributes[0]).SetAttribute("src", "ms-appx:///Assets/SplashScreen.scale-wide-100.scale-100.png");
            ((XmlElement)wideTileImageAttributes[0]).SetAttribute("alt", "Tip Calculator");

            var wideTileBinding = (XmlElement)wideTileXml.GetElementsByTagName("binding").Item(0);
            wideTileBinding.SetAttribute("branding", "none"); // removes logo from lower left-hand corner of tile

            // Add the wide tile to the square tile's payload, so they are sibling elements under visual 
            IXmlNode node = tileXml.ImportNode(wideTileXml.GetElementsByTagName("binding").Item(0), true);
            tileXml.GetElementsByTagName("visual").Item(0).AppendChild(node);

            // Create a tile notification that will expire in 1 day and send the live tile update.  
            TileNotification tileNotification = new TileNotification(tileXml);
            tileNotification.ExpirationTime = DateTimeOffset.UtcNow.AddDays(1);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }

       
    }
}
